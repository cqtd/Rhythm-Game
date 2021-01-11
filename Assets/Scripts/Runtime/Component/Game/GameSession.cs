using System;
using System.Collections;
using Rhythm.BMS;
using UnityEngine;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Rhythm
{
	[DefaultExecutionOrder(Constant.Order.GAME_MANAGER)]
	public class GameSession : MonoBehaviour
	{
		[SerializeField] private NoteVisualizer m_drawer = default;
		[SerializeField] private Parser m_parser = default;
		[SerializeField] private Speaker m_sound = default;
		[SerializeField] private Transform m_noteParent = default;
		private double _currentBPM = default;

		private double _currentScrollTime = default;
		private double _currentTime = default;

		private KeyCode[] _keyBindings = default;
		private double _stopTime = default;

		private bool forceCompleteSong = false;

		[NonSerialized] public double scrollValue = default;

		private WaitForSeconds Wait2Sec = default;

		private Pattern Current => m_parser.pattern;
		private event Action<Enum.JudgeType> onHandleNote;

		// Use this for initialization
		private void Start()
		{
			Initialize();
		}

		private void Update()
		{
			QuickExit();

			if (Game.Instance.option.autoPlay)
			{
				return;
			}

			ProcessInputOnUpdate();
		}

		/// <summary>
		///     Call 160 times in a second
		/// </summary>
		private void FixedUpdate()
		{
			if (Game.Instance.isPaused)
			{
				return;
			}

			double prevStop = 0;
			double deltaTime = Time.fixedDeltaTime;

			PlayNotes();

			_currentTime += Time.fixedDeltaTime;
			if (_stopTime > 0.0)
			{
				if (_stopTime >= Time.fixedDeltaTime)
				{
					_stopTime -= Time.fixedDeltaTime;
					return;
				}

				deltaTime -= _stopTime;
				prevStop = _stopTime;
			}

			double average = _currentBPM * deltaTime;

			ObjectBase next = null;
			bool flag = false;

			if (Current.stopObjs.Count > 0)
			{
				next = Current.stopObjs.Peek;
				if (next.timing < _currentScrollTime + deltaTime)
				{
					flag = true;
					average = 0;
				}
			}

			if (Current.bpmObjs.Count > 0)
			{
				BpmObject bpm = Current.bpmObjs.Peek;
				if (next == null)
				{
					next = bpm;
				}
				else if (bpm.beat <= next.beat)
				{
					next = bpm;
				}

				if (next.timing < _currentScrollTime + deltaTime)
				{
					flag = true;
					average = 0;
				}
			}

			double sub = 0;
			double prevTime = _currentScrollTime;
			while (next != null && next.timing + _stopTime < _currentScrollTime + Time.fixedDeltaTime)
			{
				// bpm obj
				if (next is BpmObject b)
				{
					double diff = b.timing - prevTime;
					average += _currentBPM * diff;
					_currentBPM = b.Bpm;

					prevTime = b.timing;
					Current.bpmObjs.RemoveLast();
				}

				// stop obj
				if (next is StopObject stop)
				{
					double diff = stop.timing - prevTime;
					average += _currentBPM * diff;
					prevTime = stop.timing;

					double duration = Current.stopDurations[stop.Key] / _currentBPM * 240;
					_stopTime += duration;
					Current.stopObjs.RemoveLast();

					if (prevTime + _stopTime >= _currentScrollTime + deltaTime)
					{
						double sdiff = _currentScrollTime + deltaTime - prevTime;
						sub += sdiff;
						_stopTime -= sdiff;
						break;
					}
				}

				next = null;

				if (Current.stopObjs.Count > 0)
				{
					next = Current.stopObjs.Peek;
				}

				if (Current.bpmObjs.Count > 0)
				{
					BpmObject bpm = Current.bpmObjs.Peek;
					if (next == null)
					{
						next = bpm;
					}
					else if (bpm.beat <= next.beat)
					{
						next = bpm;
					}
				}
			}

			deltaTime -= sub;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
			if (deltaTime < 0)
			{
				Debug.LogError($"deltaTime이 음수입니다. {deltaTime}");
			}
#endif

			if (flag && prevTime <= _currentScrollTime + deltaTime)
			{
				average += _currentBPM * (_currentScrollTime + deltaTime - prevTime);
			}


			_stopTime -= prevStop;
			average /= 60;
			_currentScrollTime += deltaTime;

			scrollValue += average * Game.Instance.Setting.speed;

			// 스크롤링
			Vector3 scrollPos = m_noteParent.transform.position;
			scrollPos.y = (float) -scrollValue;
			m_noteParent.transform.position = scrollPos;
		}

		private void OnEnable()
		{
			result = new GameResult();

			noteData = new NoteData();
			scoreData = new ScoreData();
			healthData = new HealthData();
			feverData = new FeverData();

			onHandleNote += noteData.OnHandleNote;
			onHandleNote += feverData.OnHandleNote;
			onHandleNote += scoreData.OnHandleNote;
			onHandleNote += healthData.OnHandleNote;

			Message.Register<int>(Event.OnFeverIncrease, OnFeverIncrease);
			Message.Register(Event.OnFeverFinished, OnFeverFinished);
			
			Message.Register(Event.OnSpeedDown, OnSpeedDown);
			Message.Register(Event.OnSpeedUp, OnSpeedUp);
		}

		private void OnDisable()
		{
			onHandleNote -= noteData.OnHandleNote;
			onHandleNote -= scoreData.OnHandleNote;
			onHandleNote -= healthData.OnHandleNote;

			Message.Unregister<int>(Event.OnFeverIncrease, OnFeverIncrease);
			Message.Unregister(Event.OnFeverFinished, OnFeverFinished);
			
			Message.Unregister(Event.OnSpeedDown, OnSpeedDown);
			Message.Unregister(Event.OnSpeedUp, OnSpeedUp);
		}
		
		private void OnSpeedDown()
		{
			// Game.Instance.Setting.speed -= 0.5f;
		}

		private void OnSpeedUp()
		{
			// Game.Instance.Setting.speed += 0.5f;
		}


#if UNITY_EDITOR
		[MenuItem("Debug/Toggle Auto Play")]
		private static void ToggleAutoPlay()
		{
			bool auto = EditorPrefs.GetBool("Rhythm.AutoPlay", false);
			EditorPrefs.SetBool("Rhythm.AutoPlay", !auto);
		}
#endif

		private void Initialize()
		{

#if UNITY_EDITOR
			Game.Instance.option.autoPlay = EditorPrefs.GetBool("Rhythm.AutoPlay");
#else
			Game.Instance.option.autoPlay = false;
#endif
			Game.Instance.isPaused = true;

			KeySettingObject settingObject = KeySettingObject.Load();
			_keyBindings = settingObject.Keys;

			Wait2Sec = new WaitForSeconds(2.0f);

			StartCoroutine(PreLoad());
		}

		private void QuickExit()
		{
#if UNITY_EDITOR || DEVELOPMENT_BUILD

#if ENABLE_INPUT_SYSTEM
			if (Keyboard.current.f10Key.wasPressedThisFrame)
#else
			if (Input.GetKeyDown(KeyCode.F10))
#endif
			{
				forceCompleteSong = true;
				Debug.Log("Force Complete this song.");
			}
#endif
		}

		/// <summary>
		///     @TODO :: New input의 Action 방식으로 변경
		/// </summary>
		private void ProcessInputOnUpdate()
		{
			// for (int i = 0; i < _keyBindings.Length; ++i)
			// {
			// 	int lineIdx = i == 9 ? 5 : i;
			//
			// 	// 키가 눌러졌으면
			// 	if (Input.GetKeyDown(_keyBindings[i]))
			// 	{
			// 		NoteObject n = Current.lanes[lineIdx].noteList.Count > 0
			// 			? Current.lanes[lineIdx].noteList.Peek
			// 			: null;
			// 		
			// 		Message.Execute<NoteKey>(Event.OnKeyDown, (NoteKey) i);
			//
			// 		// 노트가 존재하고 Extra가 1이 아닐 때
			// 		if (n != null && n.Extra != 1)
			// 		{
			// 			// 소리 재생
			// 			m_sound.PlayKeySound(n.KeySound);
			//
			// 			// 판정
			// 			if (Judge.Evaluate(n, _currentTime) != Enum.JudgeType.IGNORE)
			// 			{
			// 				// 노트 프로세스
			// 				HandleNote(Current.lanes[lineIdx], lineIdx);
			// 			}
			// 		}
			// 	}
			//
			// 	// 키를 뗐을 때 (롱노트)
			// 	if (Input.GetKeyUp(_keyBindings[i]))
			// 	{
			// 		// 현재 시간에 해당하는 노트 피킹 
			// 		NoteObject n = Current.lanes[lineIdx].noteList.Count > 0
			// 			? Current.lanes[lineIdx].noteList.Peek
			// 			: null;
			// 		
			// 		if (n != null && n.Extra == 1)
			// 		{
			// 			// 사운드 재생
			// 			m_sound.PlayKeySound(n.KeySound);
			//
			// 			// 노트 프로세스
			// 			HandleNote(Current.lanes[lineIdx], lineIdx);
			// 		}
			//
			// 		// m_keyPressed[lineIdx].DOFade(0f, m_duration * 0.4f);
			//
			// 		Message.Execute<NoteKey>(Event.OnKeyUp, (NoteKey) i);
			// 	}
			// }
		}

		public void OnKeyDown(NoteKey key)
		{
			int lineIdx = (int) key == 9 ? 5 : (int) key;
			NoteObject n = Current.lanes[lineIdx].noteList.Count > 0
				? Current.lanes[lineIdx].noteList.Peek
				: null;
					
			Message.Execute<NoteKey>(Event.OnKeyDown, key);

			// 노트가 존재하고 Extra가 1이 아닐 때
			if (n != null && n.Extra != 1)
			{
				// 소리 재생
				m_sound.PlayKeySound(n.KeySound);

				// 판정
				if (Judge.Evaluate(n, _currentTime) != Enum.JudgeType.IGNORE)
				{
					// 노트 프로세스
					HandleNote(Current.lanes[lineIdx], lineIdx);
				}
			}
		}

		public void OnKeyUp(NoteKey key)
		{
			int lineIdx = (int) key == 9 ? 5 : (int) key;

			// 현재 시간에 해당하는 노트 피킹 
			NoteObject n = Current.lanes[lineIdx].noteList.Count > 0
				? Current.lanes[lineIdx].noteList.Peek
				: null;
					
			if (n != null && n.Extra == 1)
			{
				// 사운드 재생
				m_sound.PlayKeySound(n.KeySound);

				// 노트 프로세스
				HandleNote(Current.lanes[lineIdx], lineIdx);
			}

			// m_keyPressed[lineIdx].DOFade(0f, m_duration * 0.4f);

			Message.Execute<NoteKey>(Event.OnKeyUp, key);
		}


		private IEnumerator PreLoad()
		{
			TimeCheck.Start();
			
			// Pattern 생성
			yield return m_parser.Parse();

			// 오디오 클립 준비
			m_sound.AddAudioClips();

			// 노트 생성
			m_drawer.Generate();

			noteData.totalNote = Current.noteCount;
			_currentBPM = Current.bpmObjs.Peek.Bpm;
			Current.bpmObjs.RemoveLast();

			yield return new WaitUntil(() => m_sound.isPrepared);

			// 준비 끝
			
			Message.Execute(Event.OnFadeIn, 1.0f);

			Debug.Log("2초 후 게임 시작");
			yield return Wait2Sec;
			_currentTime += Game.Instance.Setting.judgemenetSyncOffset / 1000.0f;


			// 게임 시작
			Game.Instance.isPaused = false;
			StartCoroutine(WaitForGameFinishCoroutine());
		}

		private void HandleNote(Lane l, int idx, float volume = 1.0f)
		{
			if (l.noteList.Count <= 0)
			{
				return;
			}

			NoteObject n = l.noteList.Peek;

			// 노트 끄기
			n.Model.SetActive(false);
			l.noteList.RemoveLast();

			// 노트 판정
			Enum.JudgeType judge = Judge.Evaluate(n, _currentTime);

			// POOR 인 경우
			if (l.noteList.Count > 0 && l.noteList.Peek.Extra == 1 && judge == Enum.JudgeType.POOR)
			{
				l.noteList.Peek.Model.SetActive(false);
				l.noteList.RemoveLast();
			}

			// 무시하는 노트면
			if (n.Extra == 1 && judge == Enum.JudgeType.IGNORE)
			{
				// 강제로 POOR 입력
				judge = Enum.JudgeType.POOR;
				// 비주얼 끄기
				n.Model.SetActive(false);
				l.noteList.RemoveLast();
			}

			// BAD 판정 이상인 경우
			if (judge > Enum.JudgeType.BAD)
			{
				NoteKey note = (NoteKey) idx;
				Message.Execute<NoteKey>(Event.OnHandleNote, note);
			}

			onHandleNote?.Invoke(judge);

			int gap = (int) (n.timing - _currentTime) * 1000;
			if (gap > 0)
			{
				scoreData.gapLate += gap;
				scoreData.lateCount += 1;
			}
			else
			{
				scoreData.gapEarly += gap;
				scoreData.earlyCount += 1;
			}

			Message.Execute<long>(Event.OnScoreUpdate, scoreData.score);
			Message.Execute<long>(Event.OnComboUpdate, scoreData.combo);
			Message.Execute<int>(Event.OnExistGap, gap);
			Message.Execute<Enum.JudgeType>(Event.OnJudgeUpdate, judge);

			Message.Execute<float>(Event.OnFeverUpdate, feverData.gauge / 100f);

			LifeCheck();
		}

		private void OnFeverIncrease(int fever)
		{
			scoreData.fever = fever;

			// Debug.Log($"Fever : {fever}");
		}

		private void OnFeverFinished()
		{
			scoreData.fever = 1;
			// Debug.Log("Fever End");
		}

		private void LifeCheck()
		{
			if (healthData.health <= 0f)
			{
				// Failed
			}
		}

		private void PlayNotes()
		{
			while (Current.noteObjs.Count > 0 && Current.noteObjs.Peek.timing <= _currentTime)
			{
				int keySound = Current.noteObjs.Peek.KeySound;
				m_sound.PlayKeySound(keySound);
				Current.noteObjs.RemoveLast();
			}

			if (Game.Instance.option.autoPlay)
			{
				for (int i = 0; i < Current.lanes.Length; ++i)
				{
					Lane l = Current.lanes[i];
					while (l.noteList.Count > 0 && l.noteList.Peek.timing <= _currentTime)
					{
						m_sound.PlayKeySound(l.noteList.Peek.KeySound);
						HandleNote(l, i);

						NoteKey noteKey = (NoteKey) i;
						Message.Execute(Event.OnKeyDownAuto, noteKey);
					}

					while (l.mineList.Count > 0 &&
					       Judge.Evaluate(l.mineList.Peek, _currentTime) == Enum.JudgeType.POOR)
					{
						NoteObject n = l.mineList.Peek;
						n.Model.SetActive(false);
						l.mineList.RemoveLast();
					}
				}
			}
			else
			{
				for (int i = 0; i < Current.lanes.Length; ++i)
				{
					Lane l = Current.lanes[i];
					while (l.noteList.Count > 0 && Judge.Evaluate(l.noteList.Peek, _currentTime) == Enum.JudgeType.POOR)
					{
						NoteObject n = l.noteList.Peek;
						m_sound.PlayKeySound(n.KeySound, 0.3f);
						HandleNote(l, i, 0.3f);
					}

					while (l.mineList.Count > 0 &&
					       Judge.Evaluate(l.mineList.Peek, _currentTime) == Enum.JudgeType.POOR)
					{
						NoteObject n = l.mineList.Peek;
						n.Model.SetActive(false);
						l.mineList.RemoveLast();
					}
				}
			}
		}

		public IEnumerator WaitForGameFinishCoroutine()
		{
			while (true)
			{
				bool allNoteProcessed = noteData.hitCount >= Current.noteCount;
				bool songDone = allNoteProcessed;

				if (songDone || forceCompleteSong)
				{
					// Game Finished

					// calculate
					scoreData.accuracyAverage = scoreData.accuracySum / noteData.hitCount;

					scoreData.gapEarlyAverage = scoreData.gapEarly / (double) scoreData.earlyCount;
					scoreData.gapLateAverage = scoreData.gapLate / (double) scoreData.lateCount;

					// Transfer Data
					result.healthData = healthData;
					result.noteData = noteData;
					result.scoreData = scoreData;
					result.playOption = Game.Instance.option;

					result.songInfo = Game.Instance.header.title;

					Game.Instance.SetResult(result);

					yield return Wait2Sec;
					//@TODO :: 노트가 없더라도 노래가 끝났는지 확인해야 함
					// Fentast

					Message.Execute(Event.OnFadeOut, 1.0f);

					yield return new WaitForSeconds(0.35f);

					SceneLoader.Change(Enum.Travel.Result);

					yield break;
				}

				// every 2 second
				yield return Wait2Sec;
			}
		}

		#region Result Object

		private GameResult result;
		private NoteData noteData;
		private ScoreData scoreData;
		private HealthData healthData;
		private FeverData feverData;

		#endregion
	}
}