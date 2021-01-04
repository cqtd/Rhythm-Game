namespace Rhythm.BMS
{
	public sealed class Line
	{
		public ExtendedList<NoteObject> LandMineList;
		public ExtendedList<NoteObject> NoteList;

		public Line()
		{
			NoteList = new ExtendedList<NoteObject>
			{
				Capacity = 225
			};
			LandMineList = new ExtendedList<NoteObject>
			{
				Capacity = 20
			};
		}
	}
}