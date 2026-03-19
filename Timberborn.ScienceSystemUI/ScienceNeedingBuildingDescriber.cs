using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.ScienceSystem;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.ScienceSystemUI
{
	// Token: 0x02000008 RID: 8
	public class ScienceNeedingBuildingDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000223A File Offset: 0x0000043A
		public ScienceNeedingBuildingDescriber(ProductionItemFactory productionItemFactory, DescribedAmountFactory describedAmountFactory, ResourceAmountFormatter resourceAmountFormatter, ILoc loc, ITooltipRegistrar tooltipRegistrar)
		{
			this._productionItemFactory = productionItemFactory;
			this._describedAmountFactory = describedAmountFactory;
			this._resourceAmountFormatter = resourceAmountFormatter;
			this._loc = loc;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002267 File Offset: 0x00000467
		public void Awake()
		{
			this._scienceNeedingBuilding = base.GetComponent<ScienceNeedingBuilding>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._time = UnitFormatter.FormatHours(1, this._loc);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002293 File Offset: 0x00000493
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (this._blockObject.IsPreview)
			{
				yield return this.CreateScienceUsageEntityDescription();
			}
			yield break;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022A4 File Offset: 0x000004A4
		public string DescribeScienceUsage()
		{
			int scienceUsedPerHour = this._scienceNeedingBuilding.ScienceUsedPerHour;
			string param = this._resourceAmountFormatter.FormatPerHour(this._loc.T(ScienceNeedingBuildingDescriber.SciencePointsLocKey), (float)scienceUsedPerHour);
			return this._loc.T<string>(ScienceNeedingBuildingDescriber.DescriptionLocKey, param);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022EC File Offset: 0x000004EC
		public EntityDescription CreateScienceUsageEntityDescription()
		{
			int scienceUsedPerHour = this._scienceNeedingBuilding.ScienceUsedPerHour;
			VisualElement visualElement = this._describedAmountFactory.CreatePlain(ScienceNeedingBuildingDescriber.ScienceClass, scienceUsedPerHour.ToString("0.#"));
			this._tooltipRegistrar.Register(visualElement, this.DescribeScienceUsage());
			return EntityDescription.CreateInputSectionWithTime(this._productionItemFactory.CreateInput(visualElement), 2147483646, this._time);
		}

		// Token: 0x0400000F RID: 15
		public static readonly string DescriptionLocKey = "GoodConsuming.SupplyDescription";

		// Token: 0x04000010 RID: 16
		public static readonly string ScienceClass = "described-amount--science";

		// Token: 0x04000011 RID: 17
		public static readonly string SciencePointsLocKey = "Science.SciencePoints";

		// Token: 0x04000012 RID: 18
		public readonly ProductionItemFactory _productionItemFactory;

		// Token: 0x04000013 RID: 19
		public readonly DescribedAmountFactory _describedAmountFactory;

		// Token: 0x04000014 RID: 20
		public readonly ResourceAmountFormatter _resourceAmountFormatter;

		// Token: 0x04000015 RID: 21
		public readonly ILoc _loc;

		// Token: 0x04000016 RID: 22
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000017 RID: 23
		public ScienceNeedingBuilding _scienceNeedingBuilding;

		// Token: 0x04000018 RID: 24
		public BlockObject _blockObject;

		// Token: 0x04000019 RID: 25
		public string _time;
	}
}
