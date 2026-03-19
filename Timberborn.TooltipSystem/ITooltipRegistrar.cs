using System;
using UnityEngine.UIElements;

namespace Timberborn.TooltipSystem
{
	// Token: 0x02000004 RID: 4
	public interface ITooltipRegistrar
	{
		// Token: 0x06000003 RID: 3
		void RegisterLocalizable(VisualElement visualElement, string tooltipTextLocKey);

		// Token: 0x06000004 RID: 4
		void RegisterLocalizable(VisualElement visualElement, Func<string> tooltipTextLocKeyGetter);

		// Token: 0x06000005 RID: 5
		void RegisterUpdatable(VisualElement visualElement, Func<string> tooltipTextGetter);

		// Token: 0x06000006 RID: 6
		void Register(VisualElement visualElement, string tooltipText);

		// Token: 0x06000007 RID: 7
		void Register(VisualElement visualElement, Func<string> tooltipTextGetter);

		// Token: 0x06000008 RID: 8
		void Register(VisualElement visualElement, VisualElement tooltipElement);

		// Token: 0x06000009 RID: 9
		void Register(VisualElement visualElement, Func<VisualElement> tooltipElementGetter);

		// Token: 0x0600000A RID: 10
		void Register(VisualElement visualElement, Func<TooltipContent> tooltipContentGetter);

		// Token: 0x0600000B RID: 11
		void RegisterWithKeyBinding(VisualElement visualElement, string keyBinding);

		// Token: 0x0600000C RID: 12
		void RegisterWithKeyBinding(VisualElement visualElement, string tooltipText, string keyBinding);

		// Token: 0x0600000D RID: 13
		void ShowPriority(VisualElement visualElement);

		// Token: 0x0600000E RID: 14
		void HidePriority();
	}
}
