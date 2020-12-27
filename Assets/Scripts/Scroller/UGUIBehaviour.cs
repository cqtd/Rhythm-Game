using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Scroller
{
	[RequireComponent(typeof(RectTransform))]
	public abstract class UGUIBehaviour : UIBehaviour
	{
		/// <summary>
		/// 미리 캐시된 RectTransform
		/// </summary>
		[HideInInspector] public RectTransform rectTransform = default;

#if UNITY_EDITOR
		protected override void Reset()
		{
			base.Reset();

			rectTransform = GetComponent<RectTransform>();
			Assert.IsNotNull(rectTransform);
		}
#endif
	}
}