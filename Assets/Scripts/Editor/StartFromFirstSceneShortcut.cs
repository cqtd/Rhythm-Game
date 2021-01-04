using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Rhythm.Editor
{
	internal class StartFromFirstSceneShortcut
	{
		[InitializeOnLoadMethod]
		public static void Init()
		{
			EditorApplication.playModeStateChanged += LogPlayModeState;
		}

		private static void LogPlayModeState(PlayModeStateChange state)
		{
			if (state == PlayModeStateChange.EnteredEditMode && EditorPrefs.HasKey("LastActiveSceneToolbar"))
			{
				EditorSceneManager.OpenScene(
					SceneUtility.GetScenePathByBuildIndex(EditorPrefs.GetInt("LastActiveSceneToolbar")));
				EditorPrefs.DeleteKey("LastActiveSceneToolbar");
			}
		}

		[MenuItem("Tools/Play At First #&P")]
		private static void PlayAtFirst()
		{
			if (!EditorApplication.isPlaying)
			{
				EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
				EditorPrefs.SetInt("LastActiveSceneToolbar", SceneManager.GetActiveScene().buildIndex);
				EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(0));
			}

			EditorApplication.isPlaying = !EditorApplication.isPlaying;
		}
	}
}