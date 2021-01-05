using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using Random = System.Random;

namespace Rhythm.BMS
{
	///<help>
	/// http://cosmic.mearie.org/2005/03/bmsguide/
	/// </help>
	public class Parser : MonoBehaviour
	{
		[SerializeField] private Speaker m_sound = default;

		private string[] _bmsTexts;
		private Dictionary<string, double> bpms;

		private BeMusicHeader header;
		public Pattern pattern;

		private const int READ_ITERATION = 10;

		private void Awake()
		{
			bpms = new Dictionary<string, double>();
		}

		public IEnumerator Parse()
		{
			pattern = new Pattern();
			GetFileInternal();
			
			TimeCheck.Log("IO");
			
			yield return ParseGameData();
			TimeCheck.Log("게임 데이터 파싱 코루틴");
			
			yield return ParseMainData();
			TimeCheck.Log("메인 데이터 파싱 코루틴");

			pattern.GetBeatsAndTimings(header.bpm);
			TimeCheck.Log("오브젝트");
		}

		private void GetFileInternal()
		{
			header = Game.Instance.header;
			_bmsTexts = File.ReadAllLines(Game.Instance.header.path);
		}

		private IEnumerator ParseGameData()
		{
			int i = 0;
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

				if (i == READ_ITERATION)
				{
					yield return null;
				}

				i++;
			}
		}

		private IEnumerator ParseMainData()
		{
			int iteration = 0;
			
			bool isInIfBrace = false;
			bool isIfVaild = false;
			int random = 1;

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
					random = rand.Next(1, int.Parse(text.Substring(8)) + 1);
					continue;
				}

				if (text.StartsWith("#if", StringComparison.OrdinalIgnoreCase))
				{
					isInIfBrace = true;
					if (text[4] - '0' == random)
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

				if (pattern.barCount < bar)
				{
					pattern.barCount = bar; //나중에 1 더해야함
				}
				
				if (iteration == READ_ITERATION)
				{
					yield return null;
				}

				iteration++;

				switch (text[4])
				{
					case '0':
					{
						int beatLength = (text.Length - 7) / 2;
						switch (text[5])
						{
							case '1':
							{
								for (int i = 7; i < text.Length - 1; i += 2)
								{
									int beat = (i - 7) / 2;
									int keySound = Decoder.Decode36(text.Substring(i, 2));

									if (keySound != 0)
									{
										pattern.AddBGSound(bar, beat, beatLength, keySound);
									}
								}

								break;
							}
							case '2':
							{
								double beatC = double.Parse(text.Substring(7));
								pattern.AddNewBeatC(bar, beatC);
								
								break;
							}
							case '3':
							{
								for (int i = 7; i < text.Length - 1; i += 2)
								{
									int beat = (i - 7) / 2;
									double bpm = int.Parse(text.Substring(i, 2), NumberStyles.HexNumber);

									if (bpm != 0)
									{
										pattern.AddBPM(bar, beat, beatLength, bpm);
									}
								}

								break;
							}
							case '4':
							{
								for (int i = 7; i < text.Length - 1; i += 2)
								{
									int beat = (i - 7) / 2;
									string key = text.Substring(i, 2);

									if (string.CompareOrdinal(key, "00") != 0)
									{
										if (pattern.bgVideoTable.ContainsKey(key))
										{
											pattern.AddBGAChange(bar, beat, beatLength, key);
										}
										else
										{
											pattern.AddBGAChange(bar, beat, beatLength, key, true);
										}
									}
								}

								break;
							}
							case '8':
							{
								for (int i = 7; i < text.Length - 1; i += 2)
								{
									int beat = (i - 7) / 2;
									string key = text.Substring(i, 2);
									if (string.Compare(key, "00", StringComparison.Ordinal) != 0)
									{
										pattern.AddBPM(bar, beat, beatLength, bpms[key]);
									}
								}

								break;
							}
							case '9':
							{
								for (int i = 7; i < text.Length - 1; i += 2)
								{
									int beat = (i - 7) / 2;
									string sub = text.Substring(i, 2);
									if (string.CompareOrdinal(sub, "00") != 0)
									{
										pattern.AddStop(bar, beat, beatLength, sub);
									}
								}

								break;
							}
						}

						break;
					}
					case '1':
					case '5':
					{
						int line, beatLength;
						line = text[5] - '1';
						beatLength = (text.Length - 7) / 2;

						for (int i = 7; i < text.Length - 1; i += 2)
						{
							int beat = (i - 7) / 2;
							
							int keySound = Decoder.Decode36(text.Substring(i, 2));
							if (keySound != 0)
							{
								if (text[4] == '5')
								{
									if ((LNBits & (1 << line)) != 0)
									{
										pattern.AddNote(line, bar, beat, beatLength, -1, 1);
										LNBits &= ~(1 << line); //erase bit
										continue;
									}

									LNBits |= 1 << line; //write bit
								}

								if (header.lnType.HasFlag(Enum.Lntype.LNOBJ) && keySound == header.lnObj)
								{
									pattern.AddNote(line, bar, beat, beatLength, keySound, 1);
								}
								else
								{
									pattern.AddNote(line, bar, beat, beatLength, keySound, 0);
								}
							}
						}

						break;
					}
					case 'D':
					case 'E':
					{
						int line, beatLength;
						line = text[5] - '1';
						beatLength = (text.Length - 7) / 2;

						for (int i = 7; i < text.Length - 1; i += 2)
						{
							int beat = (i - 7) / 2;
							
							int keySound = Decoder.Decode36(text.Substring(i, 2));
							if (keySound != 0)
							{
								pattern.AddNote(line, bar, beat, beatLength, keySound, -1);
							}
						}

						break;
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
			header.lnObj = Decoder.Decode36(s.Substring(7, 2));
			header.lnType |= Enum.Lntype.LNOBJ;
		}

		private void HandleBMP(string s)
		{
			string key = s.Substring(4, 2);
			string extend = s.Substring(s.Length - 3, 3);
			string Path = s.Substring(7, s.Length - 7);

			if (string.Compare(extend, "mpg", StringComparison.OrdinalIgnoreCase) == 0)
			{
				pattern.bgVideoTable.Add(key, Path);
			}
			else if (string.Compare(extend, "bmp", StringComparison.OrdinalIgnoreCase) == 0)
			{
				pattern.backgroundImage.Add(key, Path);
			}
			else if (string.Compare(extend, "png", StringComparison.OrdinalIgnoreCase) == 0)
			{
				pattern.backgroundImage.Add(key, Path);
			}
		}

		private void HandleBPM(string s)
		{
			if (s[4] == ' ')
			{
				header.bpm = double.Parse(s.Substring(5));
			}
			else
			{
				string key = s.Substring(4, 2);
				double bpm = double.Parse(s.Substring(7));

				bpms.Add(key, bpm);
			}
		}

		private void HandleStop(string s)
		{
			if (s[7] == ' ')
			{
				string sub = s.Substring(5, 2);
				double stopDuration = int.Parse(s.Substring(8)) / 192.0;
				if (!pattern.stopDurations.ContainsKey(sub))
				{
					pattern.stopDurations.Add(sub, stopDuration);
				}
			}
		}

		#endregion
		
		
	}

	enum PatternObject
	{
		BGSound = 1,
		NewBeatC = 2,
		BPM = 3,
		BGAChange = 4,
		Stop = 9,
	}
}