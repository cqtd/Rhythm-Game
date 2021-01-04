﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Rhythm.BMS;
using Rhythm.Firstpass;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rhythm.UI
{
	public class PatternUI : MonoBehaviour
	{
		private const float sizeX = 150;
		private const float space = 10;

		[Header("Components")] [SerializeField]
		private RectTransform m_contentRoot = default;

		[SerializeField] private PatternDifficultyUI m_prototype = default;
		[SerializeField] private RectTransform m_starRoot = default;

		[Header("Song Info")] [SerializeField] private TextMeshProUGUI m_title = default;

		[SerializeField] private TextMeshProUGUI m_composer = default;
		[SerializeField] private TextMeshProUGUI m_bpm = default;

		[Header("Pattern Info")] [SerializeField]
		private TextMeshProUGUI m_score = default;

		[SerializeField] private TextMeshProUGUI m_rate = default;
		[SerializeField] private TextMeshProUGUI m_combo = default;
		[SerializeField] private RawImage m_artwork = default;

		private List<PatternDifficultyUI> _elements;
		private List<BeMusicHeader> _headers;
		private SongInfo _song;

		private Dictionary<string, List<Texture2D>> textureMap;

		private void Awake()
		{
			_elements = new List<PatternDifficultyUI>();
			_headers = new List<BeMusicHeader>();

			textureMap = new Dictionary<string, List<Texture2D>>();

			m_prototype.gameObject.SetActive(false);

			Reference.Use(m_combo);
		}

		private void Update()
		{
			// @TODO :: 코루틴으로 빼기

			int page = (int) (TurnTable.selectedPatternIndex / 4f);
			float xPos = page * -(sizeX * 4 + space * 4);

			m_contentRoot.anchoredPosition = new Vector2(Mathf.Lerp(m_contentRoot.anchoredPosition.x, xPos, 0.1f),
				m_contentRoot.anchoredPosition.y);
		}

		private void OnEnable()
		{
			Message.Register<SongInfo>(Event.OnSongChanged, OnSongSelectionChanged);
			Message.Register<int>(Event.OnPatternChanged, OnPatternSelectionChanged);
		}

		private void OnDisable()
		{
			Message.Unregister<SongInfo>(Event.OnSongChanged, OnSongSelectionChanged);
			Message.Unregister<int>(Event.OnPatternChanged, OnPatternSelectionChanged);

			_elements.Clear();
			_headers.Clear();
		}

		private PatternDifficultyUI Get(int index)
		{
			if (_elements.Count <= index)
			{
				PatternDifficultyUI instance = Instantiate(m_prototype, m_contentRoot);
				_elements.Add(instance);

				return instance;
			}

			return _elements[index];
		}

		private void OnSongSelectionChanged(SongInfo songInfo)
		{
			_song = songInfo;

			SetHeaders(songInfo.Headers);
			RefreshSongInfo();
		}

		private void OnPatternSelectionChanged(int patternIndex)
		{
			for (int i = 0; i < _headers.Count; i++)
			{
				if (patternIndex == i)
				{
					Get(i).Select();
				}
				else
				{
					Get(i).Deselect();
				}
			}

			RefreshStars(patternIndex);
			RefreshScores();

			m_artwork.texture = _song.Headers[patternIndex].stageTexture;

			// Scroll
			// 현재 Update에서 구현스크롤 코루틴으로 바꾸기
		}

		private IEnumerator LoadImage(string path)
		{
			bool complete = false;
			Texture2D tex2D = null;
			TextureDownloader.Instance.GetTexture2D(path, texture =>
			{
				tex2D = texture;
				complete = true;
			});

			yield return new WaitUntil(() => complete);

			m_artwork.texture = tex2D;
		}

		private void SetHeaders(List<BeMusicHeader> headers)
		{
			_headers = headers.ToList();

			RefreshPatternList();
		}

		private void RefreshPatternList()
		{
			if (_headers.Count < _elements.Count)
			{
				for (int i = _headers.Count; i < _elements.Count; i++)
				{
					PatternDifficultyUI instance = Get(i);
					instance.gameObject.SetActive(false);
				}
			}

			for (int i = 0; i < _headers.Count; i++)
			{
				PatternDifficultyUI instance = Get(i);

				instance.Init(_headers[i]);
				instance.gameObject.SetActive(true);
			}

			LayoutRebuilder.MarkLayoutForRebuild(m_contentRoot);
		}

		private void RefreshStars(int patternIndex)
		{
			int childCount = m_starRoot.childCount;
			int level = _headers[patternIndex].playerLevel;

			if (level >= childCount)
			{
				Debug.LogWarning($"별 개수보다 난이도가 높음! {level}");
				level = childCount;
			}

			for (int i = level; i < childCount; i++)
			{
				m_starRoot.GetChild(i).gameObject.SetActive(false);
			}

			for (int i = 0; i < level; i++)
			{
				m_starRoot.GetChild(i).gameObject.SetActive(true);
			}
		}

		private void RefreshSongInfo()
		{
			m_title.SetText(_song.SongName);
			m_composer.SetText(_song.Headers.FirstOrDefault()?.artist);
			m_bpm.SetText(_song.Headers.FirstOrDefault()?.bpm.ToString(CultureInfo.InvariantCulture));
		}

		private void RefreshScores()
		{
			BeMusicHeader header = _headers[TurnTable.selectedPatternIndex];

			string key = $"Rhythm.{_song.SongName}.{header.subtitle}";
			string value = PlayerPrefs.GetString(key, null);

			if (!string.IsNullOrEmpty(value))
			{
				ScoreData obj = JsonUtility.FromJson<ScoreData>(value);

				m_score.SetText(obj.score.ToString());
				m_rate.SetText($"{obj.accuracyAverage:N2}%");
				m_combo.SetText(obj.bestCombo.ToString());
			}
		}
	}
}