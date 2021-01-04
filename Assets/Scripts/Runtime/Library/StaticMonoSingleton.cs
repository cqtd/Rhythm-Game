using UnityEngine;
using UnityEngine.Assertions;

namespace Rhythm
{
	public abstract class StaticMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		public static T Instance {
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<T>();

					Assert.IsNotNull(_instance);
				}

				return _instance;
			}
		}


		public static bool HasInstance => _instance != null;

		protected virtual void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

#if UNITY_EDITOR
		/// <link>
		///     https://docs.unity3d.com/Manual/ScriptingRestrictions.html
		/// </link>
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		private static void ReloadDomain()
		{
			_instance = null;
		}
#endif
	}
}