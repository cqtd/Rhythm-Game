using System.Collections.Generic;

namespace Rhythm.BMS
{
	///<help>
	/// http://cosmic.mearie.org/2005/03/bmsguide/
	/// </help>
	public sealed class Pattern
	{
		public int noteCount = 0;
		public int barCount = 0;

		public readonly ExtendedList<ChangeBGObject> changeBgObjs;
		public readonly ExtendedList<NoteObject> noteObjs;
		public readonly ExtendedList<BpmObject> bpmObjs;
		public readonly ExtendedList<StopObject> stopObjs;

		public readonly Dictionary<string, double> stopDurations;
		public readonly Dictionary<int, double> beatCTable;
		public readonly Dictionary<string, string> bgVideoTable;
		public readonly Dictionary<string, string> backgroundImage;

		public readonly Lane[] lanes;

		public Pattern()
		{
			beatCTable = new Dictionary<int, double>();
			stopDurations = new Dictionary<string, double>();
			bgVideoTable = new Dictionary<string, string>();
			backgroundImage = new Dictionary<string, string>();

			stopObjs = new ExtendedList<StopObject>();
			bpmObjs = new ExtendedList<BpmObject>();
			changeBgObjs = new ExtendedList<ChangeBGObject>();
			noteObjs = new ExtendedList<NoteObject>();

			lanes = new Lane[9];

			for (int i = 0; i < 9; ++i)
			{
				lanes[i] = new Lane();
			}
		}

		public void AddBGAChange(int bar, double beat, double beatLength, string key, bool isPic = false)
		{
			changeBgObjs.Add(new ChangeBGObject(bar, key, beat, beatLength, isPic));
		}

		public void AddNote(int line, int bar, double beat, double beatLength, int keySound, int extra)
		{
			if (extra == -1)
			{
				lanes[line].mineList.Add(new NoteObject(bar, keySound, beat, beatLength, extra));
			}
			else
			{
				++noteCount;
				lanes[line].noteList.Add(new NoteObject(bar, keySound, beat, beatLength, extra));
			}
		}

		public void AddBGSound(int bar, double beat, double beatLength, int keySound)
		{
			noteObjs.Add(new NoteObject(bar, keySound, beat, beatLength, 0));
		}

		public void AddNewBeatC(int bar, double beatC)
		{
			beatCTable.Add(bar, beatC);
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
			return beatCTable.ContainsKey(bar) ? beatCTable[bar] : 1.0;
		}

		public void GetBeatsAndTimings(double bpm)
		{
			foreach (BpmObject b in bpmObjs)
			{
				b.CalculateBeat(GetPreviousBarBeatSum(b.bar), GetBeatC(b.bar));
			}

			bpmObjs.Sort();

			if (bpmObjs.Count == 0 || bpmObjs.Count > 0 && bpmObjs[bpmObjs.Count - 1].beat != 0)
			{
				AddBPM(0, 0, 1, bpm);
			}

			bpmObjs[bpmObjs.Count - 1].timing = 0;
			for (int i = bpmObjs.Count - 2; i > -1; --i)
			{
				bpmObjs[i].timing = bpmObjs[i + 1].timing +
				                    (bpmObjs[i].beat - bpmObjs[i + 1].beat) / (bpmObjs[i + 1].Bpm / 60);
			}

			foreach (StopObject s in stopObjs)
			{
				s.CalculateBeat(GetPreviousBarBeatSum(s.bar), GetBeatC(s.bar));
				s.timing = GetTimingInSecond(s);
			}

			stopObjs.Sort();

			foreach (ChangeBGObject c in changeBgObjs)
			{
				c.CalculateBeat(GetPreviousBarBeatSum(c.bar), GetBeatC(c.bar));
				c.timing = GetTimingInSecond(c);
				int idx = stopObjs.Count - 1;
				double sum = 0;
				while (idx > 0 && c.beat > stopObjs[--idx].beat)
				{
					sum += stopDurations[stopObjs[idx].Key] / GetBpm(stopObjs[idx].beat) * 240;
				}

				c.timing += sum;
			}

			changeBgObjs.Sort();

			CalCulateTimingsInListExtension(noteObjs);

			foreach (Lane l in lanes)
			{
				CalCulateTimingsInListExtension(l.noteList);
				CalCulateTimingsInListExtension(l.mineList);
			}
		}

		public void CalCulateTimingsInListExtension(ExtendedList<NoteObject> list)
		{
			foreach (NoteObject n in list)
			{
				n.CalculateBeat(GetPreviousBarBeatSum(n.bar), GetBeatC(n.bar));
				n.timing = GetTimingInSecond(n);
				int idx = stopObjs.Count;
				double sum = 0;
				while (idx > 0 && n.beat > stopObjs[--idx].beat)
				{
					sum += stopDurations[stopObjs[idx].Key] / GetBpm(stopObjs[idx].beat) * 240;
				}

				n.timing += sum;
			}

			list.Sort();
		}

		private double GetBpm(double beat)
		{
			if (bpmObjs.Count == 1)
			{
				return bpmObjs[0].Bpm;
			}

			int idx = bpmObjs.Count - 1;
			while (idx > 0 && beat >= bpmObjs[--idx].beat)
			{
				
			}

			return bpmObjs[idx + 1].Bpm;
		}

		private double GetTimingInSecond(ObjectBase obj)
		{
			double timing = 0;
			int i;
			for (i = bpmObjs.Count - 1; i > 0 && obj.beat > bpmObjs[i - 1].beat; --i)
			{
				timing += (bpmObjs[i - 1].beat - bpmObjs[i].beat) / bpmObjs[i].Bpm * 60;
			}

			timing += (obj.beat - bpmObjs[i].beat) / bpmObjs[i].Bpm * 60;
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