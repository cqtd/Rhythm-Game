using System.Collections.Generic;
using UnityEngine;

namespace Rhythm.BMS
{
	public class SongInfo
	{
		public readonly List<BeMusicHeader> Headers = default;

		public string SongName = default;

		public SongInfo()
		{
			Headers = new List<BeMusicHeader> {Capacity = 4};
		}
	}
}