using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rhythm.UI
{
	public class JudgeSensitivityUI : MonoBehaviour, IPointerClickHandler
	{
		private Text text;

		private void Start()
		{
			text = GetComponentInChildren<Text>();
			UpdateText();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				if (Game.Instance.judgementSensitivity > 29)
				{
					return;
				}

				Game.Instance.judgementSensitivity += 1;
				UpdateText();
			}
			else if (eventData.button == PointerEventData.InputButton.Right)
			{
				if (Game.Instance.judgementSensitivity < -29)
				{
					return;
				}

				Game.Instance.judgementSensitivity -= 1;
				UpdateText();
			}
		}

		private void UpdateText()
		{
			text.text = Game.Instance.judgementSensitivity + "ms";
		}
	}
}