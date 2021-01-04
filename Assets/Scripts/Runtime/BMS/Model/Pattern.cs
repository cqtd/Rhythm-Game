using System.Collections.Generic;

namespace Rhythm.BMS
{
	public sealed class Pattern
	{
		public Pattern()
		{
			BeatCTable = new Dictionary<int, double>();
			stopDurations = new Dictionary<string, double>();
			BGVideoTable = new Dictionary<string, string>();
			
			stopObjs = new ExtendedList<StopObject>
			{
				Capacity = 5
			};
			
			bpmObjs = new ExtendedList<BpmObject>
			{
				Capacity = 5
			};
			
			changeBgObjs = new ExtendedList<ChangeBGObject>
			{
				Capacity = 10
			};
			
			noteObjs = new ExtendedList<NoteObject>();
			
			Lines = new Line[9];
			
			for (int i = 0; i < 9; ++i)
			{
				Lines[i] = new Line();
			}
		}

		public int NoteCount  = 0;
		public int BarCount  = 0;

		public readonly ExtendedList<ChangeBGObject> changeBgObjs;
		public readonly ExtendedList<NoteObject> noteObjs;
		public readonly ExtendedList<BpmObject> bpmObjs;
		public readonly ExtendedList<StopObject> stopObjs;
		
		public readonly Dictionary<string, double> stopDurations;
		public readonly Dictionary<int, double> BeatCTable;
		public readonly Dictionary<string, string> BGVideoTable;
		
		public readonly Line[] Lines;

		public void AddBGAChange(int bar, double beat, double beatLength, string key, bool isPic = false)
		{
			changeBgObjs.Add(new ChangeBGObject(bar, key, beat, beatLength, isPic));
		}

		public void AddNote(int line, int bar, double beat, double beatLength, int keySound, int extra)
		{
			if (extra == -1)
			{
				Lines[line].LandMineList.Add(new NoteObject(bar, keySound, beat, beatLength, extra));
			}
			else
			{
				++NoteCount;
				Lines[line].NoteList.Add(new NoteObject(bar, keySound, beat, beatLength, extra));
			}
		}

		public void AddBGSound(int bar, double beat, double beatLength, int keySound)
		{
			noteObjs.Add(new NoteObject(bar, keySound, beat, beatLength, 0));
		}

		public void AddNewBeatC(int bar, double beatC)
		{
			BeatCTable.Add(bar, beatC);
		}

		public void AddBPM(int bar, double beat, double beatLength, double bpm)
		{
			bpmObjs.Add(new BpmObject(bar, bpm, beat, beatLength));
		}

		public void AddBPM(BpmObject bpm)
		{
			bpmObjs.Add(bpm);
		}

		public void AddStop(int bar, double beat, double beatLength, string key)
		{
			stopObjs.Add(new StopObject(bar, key, beat, beatLength));
		}

		public double GetBeatC(int bar)
		{
			return BeatCTable.ContainsKey(bar) ? BeatCTable[bar] : 1.0;
		}

		public void GetBeatsAndTimings()
		{
			foreach (BpmObject b in bpmObjs)
			{
				b.CalculateBeat(GetPreviousBarBeatSum(b.Bar), GetBeatC(b.Bar));
			}

			bpmObjs.Sort();

			if (bpmObjs.Count == 0 || bpmObjs.Count > 0 && bpmObjs[bpmObjs.Count - 1].Beat != 0)
			{
				// can be problem. static field에서 mono singleton 필드로 변경하였음
				AddBPM(0, 0, 1, Game.Instance.header.bpm);
			}

			bpmObjs[bpmObjs.Count - 1].Timing = 0;
			for (int i = bpmObjs.Count - 2; i > -1; --i)
			{
				bpmObjs[i].Timing = bpmObjs[i + 1].Timing + (bpmObjs[i].Beat - bpmObjs[i + 1].Beat) / (bpmObjs[i + 1].Bpm / 60);
			}

			foreach (StopObject s in stopObjs)
			{
				s.CalculateBeat(GetPreviousBarBeatSum(s.Bar), GetBeatC(s.Bar));
				s.Timing = GetTimingInSecond(s);
			}

			stopObjs.Sort();

			foreach (ChangeBGObject c in changeBgObjs)
			{
				c.CalculateBeat(GetPreviousBarBeatSum(c.Bar), GetBeatC(c.Bar));
				c.Timing = GetTimingInSecond(c);
				int idx = stopObjs.Count - 1;
				double sum = 0;
				while (idx > 0 && c.Beat > stopObjs[--idx].Beat)
				{
					sum += stopDurations[stopObjs[idx].Key] / GetBPM(stopObjs[idx].Beat) * 240;
				}

				c.Timing += sum;
			}

			changeBgObjs.Sort();

			CalCulateTimingsInListExtension(noteObjs);

			foreach (Line l in Lines)
			{
				CalCulateTimingsInListExtension(l.NoteList);
				CalCulateTimingsInListExtension(l.LandMineList);
			}
		}

		public void CalCulateTimingsInListExtension(ExtendedList<NoteObject> list)
		{
			foreach (NoteObject n in list)
			{
				n.CalculateBeat(GetPreviousBarBeatSum(n.Bar), GetBeatC(n.Bar));
				n.Timing = GetTimingInSecond(n);
				int idx = stopObjs.Count;
				double sum = 0;
				while (idx > 0 && n.Beat > stopObjs[--idx].Beat)
				{
					sum += stopDurations[stopObjs[idx].Key] / GetBPM(stopObjs[idx].Beat) * 240;
				}

				n.Timing += sum;
			}

			list.Sort();
		}

		private double GetBPM(double beat)
		{
			if (bpmObjs.Count == 1)
			{
				return bpmObjs[0].Bpm;
			}

			int idx = bpmObjs.Count - 1;
			while (idx > 0 && beat >= bpmObjs[--idx].Beat)
			{
				;
			}

			return bpmObjs[idx + 1].Bpm;
		}

		private double GetTimingInSecond(ObjectBase obj)
		{
			double timing = 0;
			int i;
			for (i = bpmObjs.Count - 1; i > 0 && obj.Beat > bpmObjs[i - 1].Beat; --i)
			{
				timing += (bpmObjs[i - 1].Beat - bpmObjs[i].Beat) / bpmObjs[i].Bpm * 60;
			}

			timing += (obj.Beat - bpmObjs[i].Beat) / bpmObjs[i].Bpm * 60;
			return timing;
		}

		public double GetPreviousBarBeatSum(int bar)
		{
			double sum = 0;
			for (int i = 0; i < bar; ++i)
			{
				sum += 4.0 * GetBeatC(i);
			}

			return sum;
		}
	}
}