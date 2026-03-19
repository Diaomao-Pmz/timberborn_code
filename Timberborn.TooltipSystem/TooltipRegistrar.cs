using System;
using Timberborn.KeyBindingSystemUI;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.TooltipSystem
{
	// Token: 0x0200000D RID: 13
	public class TooltipRegistrar : ITooltipRegistrar
	{
		// Token: 0x06000048 RID: 72 RVA: 0x000028B9 File Offset: 0x00000AB9
		public TooltipRegistrar(Tooltip tooltip, ILoc loc, InputBindingDescriber inputBindingDescriber, TooltipContainer tooltipContainer, KeyBindingDescriber keyBindingDescriber)
		{
			this._tooltip = tooltip;
			this._loc = loc;
			this._inputBindingDescriber = inputBindingDescriber;
			this._tooltipContainer = tooltipContainer;
			this._keyBindingDescriber = keyBindingDescriber;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028E8 File Offset: 0x00000AE8
		public void RegisterLocalizable(VisualElement visualElement, string tooltipTextLocKey)
		{
			this.Register(visualElement, TooltipContent.Create(() => this._loc.T(tooltipTextLocKey)));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002924 File Offset: 0x00000B24
		public void RegisterLocalizable(VisualElement visualElement, Func<string> tooltipTextLocKeyGetter)
		{
			this.Register(visualElement, TooltipContent.Create(() => this._loc.T(tooltipTextLocKeyGetter())));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000295D File Offset: 0x00000B5D
		public void RegisterUpdatable(VisualElement visualElement, Func<string> tooltipTextGetter)
		{
			this.Register(visualElement, TooltipContent.CreateUpdatable(tooltipTextGetter));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000296C File Offset: 0x00000B6C
		public void Register(VisualElement visualElement, string tooltipText)
		{
			this.Register(visualElement, TooltipContent.Create(() => tooltipText));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000299E File Offset: 0x00000B9E
		public void Register(VisualElement visualElement, Func<string> tooltipTextGetter)
		{
			this.Register(visualElement, TooltipContent.Create(tooltipTextGetter));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029B0 File Offset: 0x00000BB0
		public void Register(VisualElement visualElement, VisualElement tooltipElement)
		{
			this.Register(visualElement, TooltipContent.Create(() => tooltipElement));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029E2 File Offset: 0x00000BE2
		public void Register(VisualElement visualElement, Func<VisualElement> tooltipElementGetter)
		{
			this.Register(visualElement, TooltipContent.Create(tooltipElementGetter));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000029F1 File Offset: 0x00000BF1
		public void Register(VisualElement visualElement, Func<TooltipContent> tooltipContentGetter)
		{
			this._tooltip.RegisterTooltip(visualElement, tooltipContentGetter);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A00 File Offset: 0x00000C00
		public void RegisterWithKeyBinding(VisualElement visualElement, string keyBinding)
		{
			this.Register(visualElement, TooltipContent.CreateWithKeyBinding(this.GetKeyBindingDescription(keyBinding), () => this.GetKeyBindingText(keyBinding)));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A48 File Offset: 0x00000C48
		public void RegisterWithKeyBinding(VisualElement visualElement, string tooltipText, string keyBinding)
		{
			this.Register(visualElement, TooltipContent.CreateWithKeyBinding(tooltipText, () => this.GetKeyBindingText(keyBinding)));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A82 File Offset: 0x00000C82
		public void ShowPriority(VisualElement visualElement)
		{
			this._tooltipContainer.ShowPriority(visualElement);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A90 File Offset: 0x00000C90
		public void HidePriority()
		{
			this._tooltipContainer.HidePriority();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public void Register(VisualElement visualElement, TooltipContent tooltipContent)
		{
			this.Register(visualElement, () => tooltipContent);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002ACD File Offset: 0x00000CCD
		public string GetKeyBindingDescription(string keyBinding)
		{
			return this._inputBindingDescriber.GetKeyBindingDisplayName(keyBinding);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002ADC File Offset: 0x00000CDC
		public string GetKeyBindingText(string keyBinding)
		{
			string result;
			if (!this._keyBindingDescriber.TryGetKeyBindingText(keyBinding, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x04000029 RID: 41
		public readonly Tooltip _tooltip;

		// Token: 0x0400002A RID: 42
		public readonly ILoc _loc;

		// Token: 0x0400002B RID: 43
		public readonly InputBindingDescriber _inputBindingDescriber;

		// Token: 0x0400002C RID: 44
		public readonly TooltipContainer _tooltipContainer;

		// Token: 0x0400002D RID: 45
		public readonly KeyBindingDescriber _keyBindingDescriber;
	}
}
