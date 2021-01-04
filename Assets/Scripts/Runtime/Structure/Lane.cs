namespace Rhythm.BMS
{
	public sealed class Lane
	{
		public readonly ExtendedList<NoteObject> mineList;
		public readonly ExtendedList<NoteObject> noteList;

		public Lane()
		{
			mineList = new ExtendedList<NoteObject>
			{
				Capacity = 20
			};
			
			noteList = new ExtendedList<NoteObject>
			{
				Capacity = 225
			};
		}
	}
}