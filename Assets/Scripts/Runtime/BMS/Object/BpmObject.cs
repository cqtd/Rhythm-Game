namespace Rhythm.BMS
{
	public class BpmObject : ObjectBase
	{
		public BpmObject(int bar, double bpm, double beat, double beatLength) : base(bar, beat, beatLength)
		{
			Bpm = bpm;
		}

		public BpmObject(BpmObject bpm) : base(bpm.Bar, bpm.Beat)
		{
			Bpm = bpm.Bpm;
		}

		public double Bpm { get; }
	}
}