using UnityEngine;
using UnityEngine.InputSystem;

namespace Rhythm
{
	using BMS;
	
	[RequireComponent(typeof(TurnTable))]
	public sealed class SelectorInputHandler : MonoBehaviour, NewInput.ISelectorActions
	{
		[SerializeField] private TurnTable turnTable = default;

		private NewInput input;

#if UNITY_EDITOR
		private void Reset()
		{
			turnTable = GetComponent<TurnTable>();
		}
#endif

		private void OnEnable()
		{
			if (input == null)
			{
				input = new NewInput();
				input.Selector.SetCallbacks(this);
			}
			
			input.Selector.Enable();
		}

		private void OnDisable()
		{
			input.Selector.Disable();
		}

		public void OnUp(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				turnTable.OnPrevSong();
			}
		}

		public void OnDown(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				turnTable.OnNextSong();
			}
		}

		public void OnRight(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				turnTable.OnNextPattern();
			}
		}

		public void OnLeft(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				turnTable.OnPrevPattern();
			}
		}

		public void OnOption(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				turnTable.OnOption();
			}
		}

		public void OnDecide(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				turnTable.OnDecide();
			}
		}
	}
}