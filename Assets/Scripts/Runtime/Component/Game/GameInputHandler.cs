using System;
using UnityEngine;

namespace Rhythm
{
	public class GameInputHandler : MonoBehaviour
	{
		[SerializeField] private KeyCode m_speedDown = KeyCode.F3;
		[SerializeField] private KeyCode m_speedUp = KeyCode.F4;

		private void Update()
		{
			if (Input.GetKeyDown(m_speedDown))
			{
				Message.Execute(Event.OnSpeedDown);
			}

			if (Input.GetKeyUp(m_speedUp))
			{
				Message.Execute(Event.OnSpeedUp);
			}
		}
	}
}