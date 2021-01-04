using UnityEngine;

namespace Rhythm
{
	public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		public static T Instance {
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<T>();

					if (_instance == null)
					{
						_instance = new GameObject($"{typeof(T).Name}").AddComponent<T>();
						DontDestroyOnLoad(_instance.gameObject);
					}
				}

				return _instance;
			}
		}

		protected virtual void Awake()
		{
			Initialize();
		}

#if UNITY_EDITOR && UNITY_2019_3_OR_NEWER
		/// <link>
		///     https://docs.unity3d.com/Manual/ScriptingRestrictions.html
		/// </link>
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		private static void ReloadDomain()
		{
			_instance = null;
		}
#endif

		protected abstract void Initialize();
	}
}