using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.Reproduction;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.ReproductionUI
{
	// Token: 0x02000005 RID: 5
	public class BreedingPodBatchControlRowItemFactory
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000210F File Offset: 0x0000030F
		public BreedingPodBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212C File Offset: 0x0000032C
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			BreedingPod breedingPod = entity.GetComponent<BreedingPod>();
			if (breedingPod)
			{
				string elementName = "Game/BatchControl/BreedingPodBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				this._tooltipRegistrar.Register(visualElement, () => this.GetTooltipText(breedingPod));
				return new BreedingPodBatchControlRowItem(visualElement, breedingPod, UQueryExtensions.Q<Label>(visualElement, "Progress", null));
			}
			return null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A4 File Offset: 0x000003A4
		public string GetTooltipText(BreedingPod breedingPod)
		{
			return this._loc.T(BreedingPodBatchControlRowItemFactory.ProgressLocKey) + ": " + BreedingPodBatchControlRowItemFactory.GetProgress(breedingPod);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C6 File Offset: 0x000003C6
		public static string GetProgress(BreedingPod breedingPod)
		{
			return NumberFormatter.FormatAsPercentFloored((double)breedingPod.CalculateProgress());
		}

		// Token: 0x04000009 RID: 9
		public static readonly string ProgressLocKey = "Breeding.Progress";

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000B RID: 11
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000C RID: 12
		public readonly ILoc _loc;
	}
}
