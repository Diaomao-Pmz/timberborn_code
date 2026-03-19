using System;
using Timberborn.TooltipSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.SliderToggleSystem
{
	// Token: 0x0200000B RID: 11
	public readonly struct SliderToggleItem
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000026CF File Offset: 0x000008CF
		public Func<TooltipContent> TooltipGetter { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000026D7 File Offset: 0x000008D7
		public string IconClass { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000026DF File Offset: 0x000008DF
		public Sprite Icon { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000026E7 File Offset: 0x000008E7
		public string SelectedClass { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000026EF File Offset: 0x000008EF
		public Action ClickAction { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026F7 File Offset: 0x000008F7
		public Func<SliderToggleState> StateGetter { get; }

		// Token: 0x0600002F RID: 47 RVA: 0x000026FF File Offset: 0x000008FF
		public SliderToggleItem(Func<TooltipContent> tooltipGetter, string iconClass, Sprite icon, string selectedClass, Action clickAction, Func<SliderToggleState> stateGetter)
		{
			this.TooltipGetter = tooltipGetter;
			this.IconClass = iconClass;
			this.Icon = icon;
			this.SelectedClass = selectedClass;
			this.ClickAction = clickAction;
			this.StateGetter = stateGetter;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000272E File Offset: 0x0000092E
		public static SliderToggleItem CreateBlockable(Func<TooltipContent> tooltipGetter, string iconClass, Action clickAction, Func<SliderToggleState> stateGetter)
		{
			return new SliderToggleItem(tooltipGetter, iconClass, null, null, clickAction, stateGetter);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000273C File Offset: 0x0000093C
		public static SliderToggleItem Create(Func<string> tooltipGetter, string iconClass, Action clickAction, Func<bool> isActiveGetter)
		{
			return new SliderToggleItem(() => TooltipContent.Create(tooltipGetter), iconClass, null, null, clickAction, () => SliderToggleItem.ConvertActiveState(isActiveGetter()));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002780 File Offset: 0x00000980
		public static SliderToggleItem Create(Func<VisualElement> tooltipGetter, string iconClass, string selectedClass, Action clickAction, Func<bool> isActiveGetter)
		{
			return new SliderToggleItem(() => TooltipContent.Create(tooltipGetter), iconClass, null, selectedClass, clickAction, () => SliderToggleItem.ConvertActiveState(isActiveGetter()));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027C4 File Offset: 0x000009C4
		public static SliderToggleItem Create(Func<string> tooltipGetter, Sprite icon, Action clickAction, Func<bool> isActiveGetter)
		{
			return new SliderToggleItem(() => TooltipContent.Create(tooltipGetter), null, icon, null, clickAction, () => SliderToggleItem.ConvertActiveState(isActiveGetter()));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002806 File Offset: 0x00000A06
		public static SliderToggleState ConvertActiveState(bool isActive)
		{
			if (!isActive)
			{
				return SliderToggleState.None;
			}
			return SliderToggleState.Active;
		}
	}
}
