using System;
using UnityEngine;
using UnityEngine.UI;

namespace Cqunity.Rhythmical
{
	public class Note : MonoBehaviour
	{
		[SerializeField] private float m_noteSpeed = 400;
		[SerializeField] private Image m_image = default;

		private void Update()
		{
			transform.localPosition += Vector3.down * m_noteSpeed * Time.deltaTime;
		}

		public void TryExecute()
		{
			m_image.enabled = false;
		}
	}
}