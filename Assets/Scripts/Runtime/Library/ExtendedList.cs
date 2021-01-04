using System.Collections.Generic;

namespace Rhythm
{
	public class ExtendedList<T> : List<T>
	{
		public T Peek => this[Count - 1];

		public void RemoveLast()
		{
			RemoveAt(Count - 1);
		}
	}
}