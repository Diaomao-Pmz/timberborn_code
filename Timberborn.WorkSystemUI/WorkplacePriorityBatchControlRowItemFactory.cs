using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.PrioritySystem;
using Timberborn.PrioritySystemUI;
using Timberborn.TooltipSystem;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x0200001C RID: 28
	public class WorkplacePriorityBatchControlRowItemFactory
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0000399B File Offset: 0x00001B9B
		public WorkplacePriorityBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, WorkplacePrioritySpriteLoader workplacePrioritySpriteLoader, PriorityColors priorityColors, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._workplacePrioritySpriteLoader = workplacePrioritySpriteLoader;
			this._priorityColors = priorityColors;
			this._loc = loc;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000039C8 File Offset: 0x00001BC8
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			WorkplacePriority workplacePriority = entity.GetComponent<WorkplacePriority>();
			if (workplacePriority != null)
			{
				string elementName = "Game/BatchControl/PrioritizableBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Image image = UQueryExtensions.Q<Image>(visualElement, "Priority", null);
				this._tooltipRegistrar.Register(image, () => this.GetTitle(workplacePriority));
				UQueryExtensions.Q<Button>(visualElement, "Increase", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					WorkplacePriorityBatchControlRowItemFactory.IncreasePriority(workplacePriority);
				}, 0);
				UQueryExtensions.Q<Button>(visualElement, "Decrease", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					WorkplacePriorityBatchControlRowItemFactory.DecreasePriority(workplacePriority);
				}, 0);
				return new WorkplacePriorityBatchControlRowItem(visualElement, workplacePriority, image, this._workplacePrioritySpriteLoader, this._priorityColors);
			}
			return null;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003A86 File Offset: 0x00001C86
		public string GetTitle(WorkplacePriority workplacePriority)
		{
			return this._loc.T(WorkplacePriorityBatchControlRowItemFactory.TitleLocKey) + ": " + this._loc.T(workplacePriority.Priority.GetLocKey());
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public static void IncreasePriority(WorkplacePriority workplacePriority)
		{
			Priority priority = workplacePriority.Priority;
			workplacePriority.SetPriority(priority.Next<Priority>());
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public static void DecreasePriority(WorkplacePriority workplacePriority)
		{
			Priority priority = workplacePriority.Priority;
			workplacePriority.SetPriority(priority.Previous<Priority>());
		}

		// Token: 0x04000086 RID: 134
		public static readonly string TitleLocKey = "Work.PriorityTitle";

		// Token: 0x04000087 RID: 135
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000088 RID: 136
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000089 RID: 137
		public readonly WorkplacePrioritySpriteLoader _workplacePrioritySpriteLoader;

		// Token: 0x0400008A RID: 138
		public readonly PriorityColors _priorityColors;

		// Token: 0x0400008B RID: 139
		public readonly ILoc _loc;
	}
}
