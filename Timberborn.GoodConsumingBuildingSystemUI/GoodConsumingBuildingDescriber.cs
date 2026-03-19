using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.GoodConsumingBuildingSystem;
using Timberborn.GoodsUI;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.GoodConsumingBuildingSystemUI
{
	// Token: 0x02000004 RID: 4
	public class GoodConsumingBuildingDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public GoodConsumingBuildingDescriber(GoodDescriber goodDescriber, ResourceAmountFormatter resourceAmountFormatter, ILoc loc, DescribedAmountFactory describedAmountFactory, ProductionItemFactory productionItemFactory)
		{
			this._goodDescriber = goodDescriber;
			this._resourceAmountFormatter = resourceAmountFormatter;
			this._loc = loc;
			this._describedAmountFactory = describedAmountFactory;
			this._productionItemFactory = productionItemFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EB File Offset: 0x000002EB
		public void Awake()
		{
			this._goodConsumingBuilding = base.GetComponent<GoodConsumingBuilding>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._workplace = base.GetComponent<Workplace>();
			this._time = UnitFormatter.FormatHours(1, this._loc);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002123 File Offset: 0x00000323
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (this._blockObject.IsPreview || this._blockObject.IsUnfinished)
			{
				yield return this.DescribeSupply();
			}
			if (this._blockObject.IsPreview && !this._workplace)
			{
				string content = SpecialStrings.RowStarter + this._loc.T(GoodConsumingBuildingDescriber.NeedsHaulersLocKey);
				yield return EntityDescription.CreateTextSection(content, 2030);
			}
			yield break;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002133 File Offset: 0x00000333
		public EntityDescription DescribeSupply()
		{
			return EntityDescription.CreateInputSectionWithTime(this._productionItemFactory.CreateInput(this.CreateElements()), int.MaxValue, this._time);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002156 File Offset: 0x00000356
		public IEnumerable<VisualElement> CreateElements()
		{
			foreach (ConsumedGoodSpec consumedGoodSpec in this._goodConsumingBuilding.ConsumedGoods)
			{
				DescribedGood describedGood = this._goodDescriber.GetDescribedGood(consumedGoodSpec.GoodId);
				float goodPerHour = consumedGoodSpec.GoodPerHour;
				string param = this._resourceAmountFormatter.FormatPerHour(describedGood.DisplayName, goodPerHour);
				string tooltip = this._loc.T<string>(GoodConsumingBuildingDescriber.DescriptionLocKey, param);
				yield return this._describedAmountFactory.CreatePlain(string.Empty, goodPerHour.ToString("0.##"), describedGood.Icon, tooltip);
			}
			ImmutableArray<ConsumedGoodSpec>.Enumerator enumerator = default(ImmutableArray<ConsumedGoodSpec>.Enumerator);
			yield break;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string DescriptionLocKey = "GoodConsuming.SupplyDescription";

		// Token: 0x04000007 RID: 7
		public static readonly string NeedsHaulersLocKey = "GoodConsuming.NeedsHaulers";

		// Token: 0x04000008 RID: 8
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000009 RID: 9
		public readonly ResourceAmountFormatter _resourceAmountFormatter;

		// Token: 0x0400000A RID: 10
		public readonly ILoc _loc;

		// Token: 0x0400000B RID: 11
		public readonly DescribedAmountFactory _describedAmountFactory;

		// Token: 0x0400000C RID: 12
		public readonly ProductionItemFactory _productionItemFactory;

		// Token: 0x0400000D RID: 13
		public GoodConsumingBuilding _goodConsumingBuilding;

		// Token: 0x0400000E RID: 14
		public BlockObject _blockObject;

		// Token: 0x0400000F RID: 15
		public Workplace _workplace;

		// Token: 0x04000010 RID: 16
		public string _time;
	}
}
