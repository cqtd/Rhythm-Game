using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cqunity.Rhythmical
{
	public class Lane : MonoBehaviour
	{
		[SerializeField] private Transform m_beginPos;
		[SerializeField] private KeyCode m_keyBinding;

		private List<Note> m_notes;

		private void Start()
		{
			m_notes = new List<Note>();
		}

		public Vector3 spawnPosition {
			get
			{
				return m_beginPos.position;
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(m_keyBinding))
			{
				
			}
		}

		public void Add(Note note)
		{
			m_notes.Add(note);
		}
	}
}