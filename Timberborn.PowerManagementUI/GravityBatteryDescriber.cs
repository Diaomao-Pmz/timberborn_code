using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.PowerManagement;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.PowerManagementUI
{
	// Token: 0x02000007 RID: 7
	public class GravityBatteryDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000024DC File Offset: 0x000006DC
		public GravityBatteryDescriber(ILoc loc, DescribedAmountFactory describedAmountFactory, ProductionItemFactory productionItemFactory)
		{
			this._loc = loc;
			this._describedAmountFactory = describedAmountFactory;
			this._productionItemFactory = productionItemFactory;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002515 File Offset: 0x00000715
		public void Awake()
		{
			this._gravityBattery = base.GetComponent<GravityBattery>();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002523 File Offset: 0x00000723
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (this._gravityBattery)
			{
				string tooltipText = this.GetTooltipText();
				string amount = this.FormatCapacity(this._gravityBattery.CapacityPerTile);
				VisualElement output = this._describedAmountFactory.CreatePlain(GravityBatteryDescriber.CapacityClass, amount, tooltipText);
				VisualElement content = this._productionItemFactory.CreateOutput(output);
				yield return EntityDescription.CreateOutputSection(content, 2147483646);
			}
			yield break;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002533 File Offset: 0x00000733
		public string GetTooltipText()
		{
			return this._loc.T<string>(GravityBatteryDescriber.PowerCapacityLocKey, this.FormatCapacity(this._gravityBattery.CapacityPerTile));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002556 File Offset: 0x00000756
		public string FormatCapacity(int capacity)
		{
			return this._loc.T<int>(this._capacityPhrase, capacity);
		}

		// Token: 0x0400001E RID: 30
		public static readonly string CapacityClass = "described-amount--power";

		// Token: 0x0400001F RID: 31
		public static readonly string PowerCapacityLocKey = "Mechanical.PowerCapacity";

		// Token: 0x04000020 RID: 32
		public readonly ILoc _loc;

		// Token: 0x04000021 RID: 33
		public readonly DescribedAmountFactory _describedAmountFactory;

		// Token: 0x04000022 RID: 34
		public readonly ProductionItemFactory _productionItemFactory;

		// Token: 0x04000023 RID: 35
		public GravityBattery _gravityBattery;

		// Token: 0x04000024 RID: 36
		public readonly Phrase _capacityPhrase = Phrase.New().Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatPowerCapacityPerMeter));
	}
}
