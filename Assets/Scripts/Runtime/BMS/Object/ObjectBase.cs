using System;

namespace Rhythm.BMS
{
	public abstract class ObjectBase : IComparable<ObjectBase>
	{
		public ObjectBase(int bar, double beat, double beatLength)
		{
			this.bar = bar;
			this.beat = beat / beatLength * 4.0;
		}

		public ObjectBase(int bar, double beat)
		{
			this.bar = bar;
			this.beat = beat;
		}

		public int bar { get; protected set; }
		public double beat { get; protected set; }
		public double timing { get; set; }

		public int CompareTo(ObjectBase other)
		{
			if (beat < other.beat)
			{
				return 1;
			}

			if (beat == other.beat)
			{
				return 0;
			}

			return -1;
		}

		public void CalculateBeat(double prevBeats, double beatC)
		{
			beat = beat * beatC + prevBeats;
		}
	}
}