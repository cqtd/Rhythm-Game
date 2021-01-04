using System.Collections.Generic;
using UnityEngine;

namespace Rhythm
{
	public sealed class Resource : MonoSingleton<Resource>
	{
		private Dictionary<string, Object> _cached;

		protected override void Initialize()
		{
			_cached ??= new Dictionary<string, Object>();
		}

		public static T Load<T>(string path) where T : Object
		{
			if (!Instance._cached.TryGetValue(path, out Object obj))
			{
				obj = Resources.Load<T>(path);
				Instance._cached[path] = obj;
			}

			return obj as T;
		}
	}
}