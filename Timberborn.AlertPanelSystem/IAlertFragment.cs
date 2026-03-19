using System;
using UnityEngine.UIElements;

namespace Timberborn.AlertPanelSystem
{
	// Token: 0x0200000B RID: 11
	public interface IAlertFragment
	{
		// Token: 0x0600001D RID: 29
		void InitializeAlertFragment(VisualElement root);

		// Token: 0x0600001E RID: 30
		void UpdateAlertFragment();
	}
}
