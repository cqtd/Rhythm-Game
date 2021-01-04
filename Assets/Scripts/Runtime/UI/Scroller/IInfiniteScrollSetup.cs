using UnityEngine;

namespace Rhythm.UI
{
	public interface IInfiniteScrollSetup
	{
		void OnPostSetupItems();
		void OnUpdateItem(int itemCount, GameObject obj);
	}
}