using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.AutomationUI
{
	// Token: 0x02000008 RID: 8
	public class AutomatableBatchControlRowItemFactory
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021AB File Offset: 0x000003AB
		public AutomatableBatchControlRowItemFactory(VisualElementLoader visualElementLoader, AutomationStateIconBuilder automationStateIconBuilder)
		{
			this._visualElementLoader = visualElementLoader;
			this._automationStateIconBuilder = automationStateIconBuilder;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021C4 File Offset: 0x000003C4
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			Automatable automatable = entity.GetComponent<Automatable>();
			if (automatable != null)
			{
				string elementName = "Game/BatchControl/AutomatableBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				AutomationStateIcon automationStateIcon = this._automationStateIconBuilder.Create(UQueryExtensions.Q<Image>(visualElement, "StateIcon", null), () => automatable.Input).SetClickableIcon().Build();
				return AutomatableBatchControlRowItem.Create(visualElement, automatable, automationStateIcon);
			}
			return null;
		}

		// Token: 0x0400000B RID: 11
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000C RID: 12
		public readonly AutomationStateIconBuilder _automationStateIconBuilder;
	}
}
