using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cqunity.Rhythmical
{
	public class TimingManager : MonoBehaviour
	{
		public List<GameObject> m_noteList = new List<GameObject>();

		[SerializeField] private Transform m_center = default;
		[SerializeField] private RectTransform[] m_timingRect = default;
		private Vector2[] m_timingBoxes = default;

		private void Start()
		{
			
		}
	}
}