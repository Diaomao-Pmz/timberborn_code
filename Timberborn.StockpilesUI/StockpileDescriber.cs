using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.Stockpiles;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000015 RID: 21
	public class StockpileDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003156 File Offset: 0x00001356
		public StockpileDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003165 File Offset: 0x00001365
		public void Awake()
		{
			this._stockpile = base.GetComponent<Stockpile>();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003173 File Offset: 0x00001373
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (!this._stockpile.Enabled)
			{
				string content = string.Format("{0}{1} {2}", SpecialStrings.RowStarter, this._loc.T(StockpileDescriber.CapacityLocKey), this._stockpile.MaxCapacity);
				yield return EntityDescription.CreateTextSection(content, 70);
			}
			yield break;
		}

		// Token: 0x0400004B RID: 75
		public static readonly string CapacityLocKey = "Inventory.Capacity";

		// Token: 0x0400004C RID: 76
		public readonly ILoc _loc;

		// Token: 0x0400004D RID: 77
		public Stockpile _stockpile;
	}
}
