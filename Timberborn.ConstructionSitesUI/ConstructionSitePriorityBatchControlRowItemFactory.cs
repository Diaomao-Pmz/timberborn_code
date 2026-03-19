using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.BuilderPrioritySystem;
using Timberborn.BuilderPrioritySystemUI;
using Timberborn.Common;
using Timberborn.ConstructionSites;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.PrioritySystem;
using Timberborn.PrioritySystemUI;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.ConstructionSitesUI
{
	// Token: 0x0200000A RID: 10
	public class ConstructionSitePriorityBatchControlRowItemFactory
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000026B5 File Offset: 0x000008B5
		public ConstructionSitePriorityBatchControlRowItemFactory(BuilderPrioritySpriteLoader builderPrioritySpriteLoader, ILoc loc, PriorityColors priorityColors, ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._builderPrioritySpriteLoader = builderPrioritySpriteLoader;
			this._loc = loc;
			this._priorityColors = priorityColors;
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026E4 File Offset: 0x000008E4
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			ConstructionSite component = entity.GetComponent<ConstructionSite>();
			if (component != null)
			{
				BuilderPrioritizable builderPrioritizable = component.GetComponent<BuilderPrioritizable>();
				string elementName = "Game/BatchControl/PrioritizableBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Image image = UQueryExtensions.Q<Image>(visualElement, "Priority", null);
				this._tooltipRegistrar.Register(image, () => this.GetTooltipText(builderPrioritizable));
				UQueryExtensions.Q<Button>(visualElement, "Increase", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					ConstructionSitePriorityBatchControlRowItemFactory.IncreasePriority(builderPrioritizable);
				}, 0);
				UQueryExtensions.Q<Button>(visualElement, "Decrease", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					ConstructionSitePriorityBatchControlRowItemFactory.DecreasePriority(builderPrioritizable);
				}, 0);
				return new ConstructionSitePriorityBatchControlRowItem(this._builderPrioritySpriteLoader, visualElement, builderPrioritizable, component, image, this._priorityColors);
			}
			return null;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027AC File Offset: 0x000009AC
		public static void IncreasePriority(BuilderPrioritizable builderPrioritizable)
		{
			Priority priority = builderPrioritizable.Priority;
			builderPrioritizable.SetPriority(priority.Next<Priority>());
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027CC File Offset: 0x000009CC
		public static void DecreasePriority(BuilderPrioritizable builderPrioritizable)
		{
			Priority priority = builderPrioritizable.Priority;
			builderPrioritizable.SetPriority(priority.Previous<Priority>());
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027EC File Offset: 0x000009EC
		public string GetTooltipText(BuilderPrioritizable builderPrioritizable)
		{
			return this._loc.T(ConstructionSitePriorityBatchControlRowItemFactory.TitleLocKey) + " " + this._loc.T(builderPrioritizable.Priority.GetLocKey());
		}

		// Token: 0x04000027 RID: 39
		public static readonly string TitleLocKey = "ConstructionSites.PriorityTitle";

		// Token: 0x04000028 RID: 40
		public readonly BuilderPrioritySpriteLoader _builderPrioritySpriteLoader;

		// Token: 0x04000029 RID: 41
		public readonly ILoc _loc;

		// Token: 0x0400002A RID: 42
		public readonly PriorityColors _priorityColors;

		// Token: 0x0400002B RID: 43
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400002C RID: 44
		public readonly VisualElementLoader _visualElementLoader;
	}
}
