using System;
using UnityEngine;

namespace Rhythm.BMS
{
	public sealed class BeMusicHeader : IComparable<BeMusicHeader>
	{
		public string artist = default;
		public string backBmp = default;
		public string banner = default;
		public string genre = default;
		public int lnObj = default;
		public Enum.Lntype lnType = default;
		public int player = default;

		public int playerLevel = default;
		public string preview = default;
		public int rank = default;
		public string stageFile = default;

		public Texture2D stageTexture = default;
		public string subtitle = default;
		public string title = default;

		public string parentPath = default;
		public string path = default;

		public float total = 400;
		public double bpm = default;


		public int CompareTo(BeMusicHeader h)
		{
			if (playerLevel > h.playerLevel)
			{
				return 1;
			}

			if (playerLevel == h.playerLevel)
			{
				return 0;
			}

			return -1;
		}
	}
}