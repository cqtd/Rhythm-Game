using UnityEngine;

namespace Rhythm.Firstpass
{
	public class EditorOnly : MonoBehaviour
	{
		private void Awake()
		{
			gameObject.SetActive(false);
		}

		private void Reset()
		{
			gameObject.tag = "EditorOnly";
		}

		private void Start()
		{
			gameObject.SetActive(false);
		}
	}
}