using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cqunity.Rhythmical
{
	public class Detector : MonoBehaviour
	{
		private List<Note> m_notes;

		private void Start()
		{
			m_notes = new List<Note>();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Note"))
			{
				AudioManager.instance.Play();
				m_notes.Add(other.GetComponent<Note>());
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			m_notes.Remove(other.GetComponent<Note>());
		}

		private void Update()
		{
			
		}
	}
}