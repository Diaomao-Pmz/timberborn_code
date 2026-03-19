using System;
using UnityEngine.UIElements;

namespace Timberborn.ModdingUI
{
	// Token: 0x02000005 RID: 5
	public interface IModManagerTooltipRegistrar
	{
		// Token: 0x06000004 RID: 4
		void RegisterModWarning(VisualElement element, ModItem modItem);

		// Token: 0x06000005 RID: 5
		void RegisterModIcon(VisualElement element, ModItem modItem);

		// Token: 0x06000006 RID: 6
		void RegisterIncreaseButton(VisualElement element);

		// Token: 0x06000007 RID: 7
		void RegisterDecreaseButton(VisualElement element);
	}
}
