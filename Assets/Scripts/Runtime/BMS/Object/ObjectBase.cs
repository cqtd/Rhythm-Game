using System;

namespace Rhythm.BMS
{
	public abstract class ObjectBase : IComparable<ObjectBase>
	{
		public ObjectBase(int bar, double beat, double beatLength)
		{
			Bar = bar;
			Beat = beat / beatLength * 4.0;
		}

		public ObjectBase(int bar, double beat)
		{
			Bar = bar;
			Beat = beat;
		}

		public int Bar { get; protected set; }
		public double Beat { get; protected set; }
		public double Timing { get; set; }

		public int CompareTo(ObjectBase other)
		{
			if (Beat < other.Beat)
			{
				return 1;
			}

			if (Beat == other.Beat)
			{
				return 0;
			}

			return -1;
		}

		public void CalculateBeat(double prevBeats, double beatC)
		{
			Beat = Beat * beatC + prevBeats;
		}
	}
}