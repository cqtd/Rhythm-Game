using System;
using Rhythm.BMS;
using UnityEngine;

namespace Rhythm
{
	public sealed class Game : MonoSingleton<Game>
	{
		[Header("Settings")] public int judgementSensitivity = default;

		public PlayOption option = default;
		[NonSerialized] public BeMusicHeader header = default;

		[NonSerialized] public bool isPaused;

		[NonSerialized] private GameResult result = default;

		[NonSerialized] public SongInfo song = default;

		public SettingObject Setting => SettingObject.Get();

		public void SetResult(GameResult gameResult)
		{
			result = gameResult;
			gameResult.Save();
		}

		public GameResult GetLastResult()
		{
			return result;
		}

		protected override void Initialize()
		{
			result = new GameResult();
			option = PlayOption.Load();
		}

		public void GameStart() { }
	}
}