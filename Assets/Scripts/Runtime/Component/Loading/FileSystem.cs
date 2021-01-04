using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Rhythm.BMS
{
	public class FileSystem : StaticMonoSingleton<FileSystem>
	{
		public static bool loaded;

		[SerializeField] private string m_rootPath;

		[SerializeField] private int limitedSongCount = 30;

		private string[] _directories;
		private int _patternCount = 0;

		public Dictionary<SongInfo, AudioClip> PreviewClips;
		public SongInfo[] songInfoArray;

		public Action<string> LogAction { get; set; }
		public Action<float> ProgressAction { get; set; }

#if UNITY_EDITOR
		private bool limitSongCount => EditorPrefs.GetBool("Limit Song Count");
#endif


		protected override void Awake()
		{
			base.Awake();
			PreviewClips = new Dictionary<SongInfo, AudioClip>();

			StartCoroutine(InitializeSongDatabase());
		}

		private IEnumerator InitializeSongDatabase()
		{
			if (string.IsNullOrEmpty(m_rootPath))
			{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
				m_rootPath = @"D:\Resources\DjMax ONLINE BMS";
#else
			m_rootPath = $@"{Directory.GetParent(Application.dataPath)}\BMSFiles";
#endif
				_directories = Directory.GetDirectories(m_rootPath);
				int directoryCount = _directories.Length;
#if UNITY_EDITOR
				if (limitSongCount)
				{
					songInfoArray = new SongInfo[limitedSongCount];
				}
				else
				{
					songInfoArray = new SongInfo[directoryCount];
				}
#else
				songInfoArray = new SongInfo[directoryCount];
#endif
				for (int i = 0; i < directoryCount; ++i)
				{
					songInfoArray[i] = ParseHeader(_directories[i]);

					LogAction?.Invoke(
						$"로딩 중...\n{(float) i / _directories.Length * 100f:N2}% [{i}/{_directories.Length}]");
					ProgressAction?.Invoke((float) i / _directories.Length);
#if UNITY_EDITOR
					if (limitSongCount)
					{
						if (i == limitedSongCount - 1)
						{
							break;
						}
					}
#endif
					yield return null;
				}
			}

			ProgressAction?.Invoke(1.0f);
			LogAction?.Invoke("로딩 완료");

			yield return new WaitForSeconds(1.0f);

			SceneLoader.Change(Enum.Travel.Select);
		}

		private bool PredicateBMS(string str)
		{
			foreach (string extension in Constant.BMS_EXTENSIONS)
			{
				if (str.EndsWith(extension))
				{
					return true;
				}
			}

			return false;
		}

		private SongInfo ParseHeader(string dir)
		{
			SongInfo songinfo = new SongInfo();

			string[] Files = Directory
				.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories)
				.Where(PredicateBMS)
				.ToArray();

			if (Files.Length == 0)
			{
				return songinfo;
			}

			foreach (string path in Files)
			{
				StreamReader reader = new StreamReader(path, Encoding.GetEncoding(932));
				BeMusicHeader currentHeader = new BeMusicHeader();
				string s;
				currentHeader.path = path;
				currentHeader.parentPath = Directory.GetParent(path).ToString();

				bool errorFlag = false;

				while ((s = reader.ReadLine()) != null)
				{
					if (s.Length <= 3)
					{
						continue;
					}

					try
					{
						if (s.Length > 10 && string.Compare(s.Substring(0, 10), "#PLAYLEVEL", true) == 0)
						{
							int lvl = 0;
							int.TryParse(s.Substring(11), out lvl);
							currentHeader.playerLevel = lvl;
						}
						else if (s.Length > 11 && string.Compare(s.Substring(0, 10), "#STAGEFILE", true) == 0)
						{
							currentHeader.stageFile = s.Substring(11);
						}
						else if (s.Length >= 9 && string.Compare(s.Substring(0, 9), "#SUBTITLE", true) == 0)
						{
							currentHeader.subtitle = s.Substring(10).Trim('[', ']');
						}
						else if (s.Length >= 8 && string.Compare(s.Substring(0, 8), "#PREVIEW", true) == 0)
						{
							currentHeader.preview = s.Substring(9, s.Length - 13);
						}
						else if (s.Length >= 8 && string.Compare(s.Substring(0, 8), "#BACKBMP", true) == 0)
						{
							currentHeader.backBmp = s.Substring(9);
						}
						else if (s.Length >= 7 && string.Compare(s.Substring(0, 7), "#PLAYER", true) == 0)
						{
							currentHeader.player = s[8] - '0';
						}
						else if (s.Length >= 7 && string.Compare(s.Substring(0, 7), "#ARTIST", true) == 0)
						{
							currentHeader.artist = s.Substring(8);
						}
						else if (s.Length >= 7 && string.Compare(s.Substring(0, 7), "#BANNER", true) == 0)
						{
							currentHeader.banner = s.Substring(8);
						}
						else if (s.Length >= 7 && string.Compare(s.Substring(0, 7), "#LNTYPE", true) == 0)
						{
							currentHeader.lnType |= (Enum.Lntype) (1 << (s[8] - '0'));
						}
						else if (s.Length >= 6 && string.Compare(s.Substring(0, 6), "#GENRE", true) == 0)
						{
							currentHeader.genre = s.Substring(7);
						}
						else if (s.Length >= 6 && string.Compare(s.Substring(0, 6), "#TITLE", true) == 0)
						{
							currentHeader.title = s.Substring(7);
							if (!string.IsNullOrEmpty(currentHeader.title))
							{
								int idx;
								if ((idx = currentHeader.title.LastIndexOf('[')) >= 0)
								{
									string name = currentHeader.title.Remove(idx);
									if (string.IsNullOrEmpty(songinfo.SongName) ||
									    songinfo.SongName.Length > name.Length)
									{
										songinfo.SongName = name;
									}

									currentHeader.subtitle = currentHeader.title.Substring(idx).Trim('[', ']');
								}
								else
								{
									if (string.IsNullOrEmpty(songinfo.SongName) ||
									    songinfo.SongName.Length > currentHeader.title.Length)
									{
										songinfo.SongName = currentHeader.title;
									}
								}
							}
						}
						else if (s.Length >= 6 && string.Compare(s.Substring(0, 6), "#TOTAL", true) == 0)
						{
							float.TryParse(s.Substring(7), out float total);
							currentHeader.total = total;
						}
						else if (s.Length >= 5 && string.Compare(s.Substring(0, 5), "#RANK", true) == 0)
						{
							currentHeader.rank = int.Parse(s.Substring(6));
						}
						else if (s.Length >= 6 && string.Compare(s.Substring(0, 4), "#BPM", true) == 0 && s[4] == ' ')
						{
							currentHeader.bpm = double.Parse(s.Substring(5));
						}
						else if (s.Length >= 30 && s.CompareTo("*---------------------- MAIN DATA FIELD") == 0)
						{
							break;
						}
					}
					catch (Exception e)
					{
						Debug.LogWarning("error parsing " + s + "\n" + e);
						errorFlag = true;
						break;
					}
				}

				if (!errorFlag)
				{
					++_patternCount;

					if (!string.IsNullOrEmpty(currentHeader.preview) && !PreviewClips.ContainsKey(songinfo))
					{
						StartCoroutine(LoadPreviewCoroutine(songinfo, currentHeader));
					}

					string stagePath = "file://" + currentHeader.parentPath + "/" + currentHeader.stageFile;
					StartCoroutine(LoadImage(stagePath, currentHeader));

					songinfo.Headers.Add(currentHeader);
				}
			}

			songinfo.Headers.Sort();
			return songinfo;
		}

		private IEnumerator LoadImage(string path, BeMusicHeader header)
		{
			bool complete = false;
			Texture2D tex2D = null;
			TextureDownloader.Instance.GetTexture2D(path, texture =>
			{
				tex2D = texture;
				complete = true;
			});

			yield return new WaitUntil(() => complete);

			header.stageTexture = tex2D;
		}

		private IEnumerator LoadPreviewCoroutine(SongInfo info, BeMusicHeader header)
		{
			AudioType type = AudioType.OGGVORBIS;

			string url = $@"{header.parentPath}\{header.preview}";

			int extensionFailCount = 0;
			do
			{
				if (File.Exists(url + Constant.SOUND_EXTENSIONS[extensionFailCount]))
				{
					break;
				}

				url.Replace(Constant.SOUND_EXTENSIONS[extensionFailCount],
					Constant.SOUND_EXTENSIONS[extensionFailCount + 1]);
				++extensionFailCount;
			} while (extensionFailCount < Constant.SOUND_EXTENSIONS.Length - 1);

			if (string.Compare(Constant.SOUND_EXTENSIONS[extensionFailCount], ".wav",
				StringComparison.OrdinalIgnoreCase) == 0)
			{
				type = AudioType.WAV;
			}
			else if (string.Compare(Constant.SOUND_EXTENSIONS[extensionFailCount], ".mp3",
				StringComparison.OrdinalIgnoreCase) == 0)
			{
				type = AudioType.MPEG;
			}

			string path = "file://" + url + Constant.SOUND_EXTENSIONS[extensionFailCount];

			bool complete = false;
			AudioClip audioClip = null;
			AudioDownloader.Instance.GetAudioClip(path, clip =>
				{
					audioClip = clip;
					complete = true;
				}
				, type);

			yield return new WaitUntil(() => complete);
			if (audioClip != null)
			{
				if (!PreviewClips.ContainsKey(info))
				{
					PreviewClips.Add(info, audioClip);
				}
			}
			else
			{
				Debug.LogWarning("Audio download failed.");
			}
		}

#if UNITY_EDITOR
		[MenuItem("Debug/Limit Song Count")]
		private static void LimitSongCount()
		{
			EditorPrefs.SetBool("Limit Song Count", true);
		}

		[MenuItem("Debug/Unlimit Song Count")]
		private static void UnlimitSongCount()
		{
			EditorPrefs.SetBool("Limit Song Count", false);
		}
#endif
	}
}