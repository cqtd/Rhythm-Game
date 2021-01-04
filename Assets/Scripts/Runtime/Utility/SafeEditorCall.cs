using System;
using System.Diagnostics;

namespace Rhythm
{
	public class SafeEditorCall
	{
		[Conditional("UNITY_EDITOR")]
		public static void CallEditorOnly(Action action)
		{
			action.Invoke();
		}
	}
}