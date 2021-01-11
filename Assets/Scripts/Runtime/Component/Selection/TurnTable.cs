using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace Rhythm.BMS
{
	public class TurnTable : MonoBehaviour
	{
		public static int selectedSongIndex = 0;
		public static int selectedPatternIndex = 0;

		public static bool canChangedSong;
		public static bool canChangedPattern;

		[Header("Song")] [SerializeField] private KeyCode m_nextSong = KeyCode.DownArrow;

		[SerializeField] private KeyCode m_prevSong = KeyCode.UpArrow;

		[Header("Pattern")] [SerializeField] private KeyCode m_nextPattern = KeyCode.RightArrow;

		[SerializeField] private KeyCode m_prevPattern = KeyCode.LeftArrow;

		[Header("Key")] [SerializeField] private KeyCode m_option = KeyCode.Tab;

		[SerializeField] private KeyCode m_start = KeyCode.Return;

		private bool isPreparing;

		private List<SongInfo> songList = default;

		private void Start()
		{
			ImportSongs_Internal();

			selectedSongIndex = 0;
			selectedPatternIndex = 0;

			canChangedSong = true;
			canChangedPattern = true;

			// run at next frame
			Timing.CallDelayed(0.01f, () => { ChangeSong(0); });
		}

		public void OnNextSong()
		{
			if (canChangedSong)
			{
				ChangeSong(1);
			}
		}

		public void OnPrevSong()
		{
			if (canChangedSong)
			{
				ChangeSong(-1);
			}
		}

		public void OnNextPattern()
		{
			if (canChangedPattern)
			{
				int headerCount = songList[selectedSongIndex].Headers.Count;

				selectedPatternIndex += 1;
				selectedPatternIndex += headerCount;
				selectedPatternIndex %= headerCount;

				Message.Execute(Event.OnPatternChanged, selectedPatternIndex);
			}
		}

		public void OnPrevPattern()
		{
			if (canChangedPattern)
			{
				int headerCount = songList[selectedSongIndex].Headers.Count;

				selectedPatternIndex -= 1;
				selectedPatternIndex += headerCount;
				selectedPatternIndex %= headerCount;

				Message.Execute(Event.OnPatternChanged, selectedPatternIndex);
			}
		}

		public void OnOption()
		{
			Message.Execute(Event.OnToggleOption);
		}

		public void OnDecide()
		{
			GameStart();
		}

		private void OnEnable()
		{
			Message.Register<SongInfo>(Event.OnSongPointerDown, OnSongPointerDown);
		}

#if UNITY_EDITOR && UNITY_2019_3_OR_NEWER
		/// <link>
		///     https://docs.unity3d.com/Manual/ScriptingRestrictions.html
		/// </link>
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		private static void ReloadDomain()
		{
			selectedSongIndex = 0;
			selectedPatternIndex = 0;
		}
#endif

		private void ImportSongs_Internal()
		{
			songList = new List<SongInfo>();
			foreach (SongInfo songInfo in FileSystem.Instance.songInfoArray)
			{
				songList.Add(songInfo);
			}
		}

		private void ChangeSong(int offset)
		{
			selectedSongIndex += offset;

			selectedSongIndex += songList.Count;
			selectedSongIndex %= songList.Count;
			selectedPatternIndex = 0;

			Message.Execute<int>(Event.OnChangeSong, offset);
			Message.Execute<SongInfo>(Event.OnSongChanged, songList[selectedSongIndex]);
			Message.Execute(Event.OnPatternChanged, selectedPatternIndex);
		}

		private void OnSongPointerDown(SongInfo songInfo)
		{
			int index = songList.IndexOf(songInfo);
			int indexOffset = index - selectedSongIndex;
		}


		private void GameStart()
		{
			if (isPreparing)
			{
				return;
			}

			isPreparing = true;

			StartCoroutine(GameStartCoroutine());
		}

		private IEnumerator GameStartCoroutine()
		{
			Game.Instance.song = songList[selectedSongIndex];
			Game.Instance.header = songList[selectedSongIndex].Headers[selectedPatternIndex];

			string path = $"file://{Game.Instance.header.parentPath}/{Game.Instance.header.stageFile}";

			bool complete = false;
			Texture2D t = null;
			TextureDownloader.Instance.GetTexture2D(path, texture =>
			{
				t = texture;
				complete = true;
			});

			yield return new WaitUntil(() => complete);

			SceneLoader.Change(Enum.Travel.Game);
		}
	}
}