using System;
using UnityEngine.Events;

namespace Rhythm
{
	[Serializable]
	public class UnityJudgeIntEvent : UnityEvent<Enum.JudgeType, int> { }
}