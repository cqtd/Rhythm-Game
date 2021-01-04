using System;
using UnityEngine.Events;

namespace Rhythm
{
	[Serializable]
	public class UnityGameResultFloatDoubleEvent : UnityEvent<GameResult, float, double> { }
}