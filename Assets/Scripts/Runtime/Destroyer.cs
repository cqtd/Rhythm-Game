using System;
using UnityEngine;

namespace Cqunity.Rhythmical
{
	public class Destroyer : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Note"))
			{
				Destroy(other.gameObject);
			}
		}
	}
}