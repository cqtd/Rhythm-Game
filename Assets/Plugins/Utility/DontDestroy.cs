using System;
using UnityEngine;

namespace Rhythm.Firstpass
{
	public class DontDestroy : MonoBehaviour
	{
		private void Start()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}