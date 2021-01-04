using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using Random = System.Random;

namespace Rhythm.BMS
{
	public class Parser : MonoBehaviour
	{
		[SerializeField] private Speaker m_sound = default;

		[HideInInspector] public UnityStrginStringEvent onBackgroundImageParsed = new UnityStrginStringEvent();
		private string[] _bmsTexts;
		private Dictionary<string, double> _bpmContainer;

		private BeMusicHeader _header;

		public Pattern pattern;

		private void Awake()
		{
			_bpmContainer = new Dictionary<string, double>();
		}

		public void Parse()
		{
			pattern = new Pattern();

			GetFileInternal();
			ParseGameData();
			ParseMainData();

			pattern.GetBeatsAndTimings();
		}

		private void GetFileInternal()
		{
			_header = Game.Instance.header;
			_bmsTexts = File.ReadAllLines(Game.Instance.header.path);
		}

		private void ParseGameData()
		{
			foreach (string text in _bmsTexts)
			{
				if (text.Length <= 3)
				{
					continue;
				}

				if (text.StartsWith("#WAV", StringComparison.OrdinalIgnoreCase))
				{
					HandleWav(text);
				}
				else if (text.StartsWith("#LNOBJ", StringComparison.OrdinalIgnoreCase))
				{
					HandleLnObj(text);
				}
				else if (text.StartsWith("#BMP", StringComparison.OrdinalIgnoreCase))
				{
					HandleBMP(text);
				}
				else if (text.StartsWith("#BPM", StringComparison.OrdinalIgnoreCase))
				{
					HandleBPM(text);
				}
				else if (text.StartsWith("#STOP", StringComparison.OrdinalIgnoreCase))
				{
					HandleStop(text);
				}
				else if (text.StartsWith("*---------------------- MAIN DATA FIELD",
					StringComparison.OrdinalIgnoreCase))
				{
					break;
				}
			}
		}

		private void ParseMainData()
		{
			bool isInIfBrace = false;
			bool isIfVaild = false;
			double beatC = 1.0f;
			int RandomValue = 1;

			int LNBits = 0;
			Random rand = new Random();

			foreach (string text in _bmsTexts)
			{
				if (!text.StartsWith("#", StringComparison.OrdinalIgnoreCase))
				{
					continue;
				}

				if (text.StartsWith("#random", StringComparison.OrdinalIgnoreCase))
				{
					RandomValue = rand.Next(1, int.Parse(text.Substring(8)) + 1);
					continue;
				}

				if (text.StartsWith("#if", StringComparison.OrdinalIgnoreCase))
				{
					isInIfBrace = true;
					if (text[4] - '0' == RandomValue)
					{
						isIfVaild = true;
					}

					continue;
				}

				if (text.StartsWith("#endif", StringComparison.OrdinalIgnoreCase))
				{
					isInIfBrace = false;
					isIfVaild = false;
					continue;
				}

				if (isInIfBrace && !isIfVaild)
				{
					continue;
				}

				if (!int.TryParse(text.Substring(1, 3), out int bar))
				{
					continue;
				}

				if (pattern.BarCount < bar)
				{
					pattern.BarCount = bar; //나중에 1 더해야함
				}

				if (text[4] == '1' || text[4] == '5')
				{
					int line, beatLength;
					line = text[5] - '1';
					beatLength = (text.Length - 7) / 2;

					for (int i = 7; i < text.Length - 1; i += 2)
					{
						int keySound = Decoder.Decode36(text.Substring(i, 2));
						if (keySound != 0)
						{
							if (text[4] == '5')
							{
								if ((LNBits & (1 << line)) != 0)
								{
									pattern.AddNote(line, bar, (i - 7) / 2, beatLength, -1, 1);
									LNBits &= ~(1 << line); //erase bit
									continue;
								}

								LNBits |= 1 << line; //write bit
							}

							if (_header.lnType.HasFlag(Enum.Lntype.LNOBJ) && keySound == _header.lnObj)
							{
								pattern.AddNote(line, bar, (i - 7) / 2, beatLength, keySound, 1);
							}
							else
							{
								pattern.AddNote(line, bar, (i - 7) / 2, beatLength, keySound, 0);
							}
						}
					}
				}
				else if (text[4] == '0')
				{
					int beatLength;
					if (text[5] == '1')
					{
						beatLength = (text.Length - 7) / 2;
						//bar = int.Parse(s.Substring(1, 3));
						for (int i = 7; i < text.Length - 1; i += 2)
						{
							int keySound = Decoder.Decode36(text.Substring(i, 2));

							if (keySound != 0)
							{
								pattern.AddBGSound(bar, (i - 7) / 2, beatLength, keySound);
							}
						}
					}
					else if (text[5] == '2')
					{
						beatC = double.Parse(text.Substring(7));
						pattern.AddNewBeatC(bar, beatC);
					}
					else if (text[5] == '3')
					{
						beatLength = (text.Length - 7) / 2;
						for (int i = 7; i < text.Length - 1; i += 2)
						{
							double bpm = int.Parse(text.Substring(i, 2), NumberStyles.HexNumber);

							if (bpm != 0)
							{
								pattern.AddBPM(bar, (i - 7) / 2, beatLength, bpm);
							}
						}
					}
					else if (text[5] == '4')
					{
						beatLength = (text.Length - 7) / 2;
						for (int i = 7; i < text.Length - 1; i += 2)
						{
							string key = text.Substring(i, 2);

							if (string.Compare(key, "00") != 0)
							{
								if (pattern.BGVideoTable.ContainsKey(key))
								{
									pattern.AddBGAChange(bar, (i - 7) / 2, beatLength, key);
								}
								else
								{
									pattern.AddBGAChange(bar, (i - 7) / 2, beatLength, key, true);
								}
							}
						}
					}
					else if (text[5] == '8')
					{
						beatLength = (text.Length - 7) / 2;
						//int idx = Decode36(s.Substring(7, 2)) - 1;
						for (int i = 7; i < text.Length - 1; i += 2)
						{
							string key = text.Substring(i, 2);
							if (key.CompareTo("00") != 0)
							{
								pattern.AddBPM(bar, (i - 7) / 2, beatLength, _bpmContainer[key]);
							}
						}
					}
					else if (text[5] == '9')
					{
						beatLength = (text.Length - 7) / 2;
						for (int i = 7; i < text.Length - 1; i += 2)
						{
							string sub = text.Substring(i, 2);
							if (string.Compare(sub, "00") != 0)
							{
								pattern.AddStop(bar, (i - 7) / 2, beatLength, sub);
							}
						}
					}
				}
				else if (text[4] == 'D' || text[4] == 'E')
				{
					int line, beatLength;
					line = text[5] - '1';
					beatLength = (text.Length - 7) / 2;

					for (int i = 7; i < text.Length - 1; i += 2)
					{
						int keySound = Decoder.Decode36(text.Substring(i, 2));
						if (keySound != 0)
						{
							pattern.AddNote(line, bar, (i - 7) / 2, beatLength, keySound, -1);
						}
					}
				}
			}
		}

		#region HANDLE LINE

		private void HandleWav(string s)
		{
			int key = Decoder.Decode36(s.Substring(4, 2));
			string path = s.Substring(7, s.Length - 11);
			m_sound.pathes.Add(key, path);
		}

		private void HandleLnObj(string s)
		{
			_header.lnObj = Decoder.Decode36(s.Substring(7, 2));
			_header.lnType |= Enum.Lntype.LNOBJ;
		}

		private void HandleBMP(string s)
		{
			string key = s.Substring(4, 2);
			string extend = s.Substring(s.Length - 3, 3);
			string Path = s.Substring(7, s.Length - 7);

			if (string.Compare(extend, "mpg", StringComparison.OrdinalIgnoreCase) == 0)
			{
				pattern.BGVideoTable.Add(key, Path);
			}
			else if (string.Compare(extend, "bmp", StringComparison.OrdinalIgnoreCase) == 0)
			{
				onBackgroundImageParsed?.Invoke(key, Path);
				// GameUI.BGImageTable.Add(key, Path);
			}
			else if (string.Compare(extend, "png", StringComparison.OrdinalIgnoreCase) == 0)
			{
				onBackgroundImageParsed?.Invoke(key, Path);
				// GameUI.BGImageTable.Add(key, Path);
			}
		}

		private void HandleBPM(string s)
		{
			if (s[4] == ' ')
			{
				_header.bpm = double.Parse(s.Substring(5));
			}
			else
			{
				string key = s.Substring(4, 2);
				double bpm = double.Parse(s.Substring(7));

				//Debug.Log(exBpms.Count + "/" + bpm);
				_bpmContainer.Add(key, bpm);
			}
		}

		private void HandleStop(string s)
		{
			if (s[7] == ' ')
			{
				string sub = s.Substring(5, 2);
				double stopDuration = int.Parse(s.Substring(8)) / 192.0;
				//pat.LegacyStopDuratns.Add(stopDuration); // 나누기 192
				if (!pattern.stopDurations.ContainsKey(sub))
				{
					pattern.stopDurations.Add(sub, stopDuration);
				}
			}
		}

		#endregion
	}
}