using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rhythm
{
	public class SceneLoader : MonoBehaviour
	{
		private IEnumerator LoadScene(int idx)
		{
			AsyncOperation op = SceneManager.LoadSceneAsync(idx);

			while (!op.isDone)
			{
				yield return null;
			}
		}

		public void ChangeScene(int travel)
		{
			StartCoroutine(LoadScene(travel));
		}

		public static void Change(Enum.Travel travel)
		{
			Change((int) travel);
		}

		public static void Change(int travel)
		{
			SceneManager.LoadSceneAsync(travel);
		}
	}
}