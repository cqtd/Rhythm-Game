using System;
using UnityEngine;

namespace Scroller
{
    public class ScrollManager : UGUIBehaviour
    {
        [SerializeField] private ScrollEntity m_prefab = default;
        [SerializeField] private float m_padding = default;
        [SerializeField] private VerticalAlignment m_verticalAlignment = default;
        [SerializeField] private HorizontalAlignment m_horizontalAlignment = default;

        protected override void Awake()
        {
            base.Awake();

            Vector2 sizeDelta = m_prefab.rectTransform.sizeDelta;
        }

        private Vector2 GetEstimatedPosition(int index)
        {
            throw new NotImplementedException();
        }

#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();

            m_padding = 10f;
        }
#endif
    }
    
    public enum VerticalAlignment
    {
        TOP,
        MID,
        BOT,
    }

    public enum HorizontalAlignment
    {
        LEFT,
        CENTER,
        RIGHT,
    }
}
