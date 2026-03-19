using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.BatchControl
{
	// Token: 0x02000027 RID: 39
	public class ToggleButtonBatchControlRowItemFactory
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00003E9B File Offset: 0x0000209B
		public ToggleButtonBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003EB8 File Offset: 0x000020B8
		public IBatchControlRowItem Create(string buttonClass, Action valueSetter, Func<bool> valueGetter, string tooltipLocKey)
		{
			string elementName = "Game/BatchControl/ToggleButtonBatchControlRowItem";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			Button button = UQueryExtensions.Q<Button>(visualElement, "ToggleButtonBatchControlRowItem", null);
			button.AddToClassList(buttonClass);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				valueSetter();
			}, 0);
			this._tooltipRegistrar.Register(button, () => this.GetTooltipText(tooltipLocKey, valueGetter));
			return new ToggleButtonBatchControlRowItem(visualElement, button, valueGetter);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003F48 File Offset: 0x00002148
		public string GetTooltipText(string tooltipLocKey, Func<bool> getValue)
		{
			string str = getValue() ? this._loc.T(ToggleButtonBatchControlRowItemFactory.ToggleStateOnLocKey) : this._loc.T(ToggleButtonBatchControlRowItemFactory.ToggleStateOffLocKey);
			return this._loc.T(tooltipLocKey) + ": " + str;
		}

		// Token: 0x04000071 RID: 113
		public static readonly string ToggleStateOnLocKey = "Toggle.State.On";

		// Token: 0x04000072 RID: 114
		public static readonly string ToggleStateOffLocKey = "Toggle.State.Off";

		// Token: 0x04000073 RID: 115
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000074 RID: 116
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000075 RID: 117
		public readonly ILoc _loc;
	}
}
