using System;
using Timberborn.Goods;

namespace Timberborn.InventorySystem
{
	// Token: 0x02000008 RID: 8
	public readonly struct GoodReservation
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002731 File Offset: 0x00000931
		public Inventory Inventory { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002739 File Offset: 0x00000939
		public GoodAmount GoodAmount { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002741 File Offset: 0x00000941
		public bool FixedAmount { get; }

		// Token: 0x06000024 RID: 36 RVA: 0x00002749 File Offset: 0x00000949
		public GoodReservation(Inventory inventory, GoodAmount goodAmount, bool fixedAmount)
		{
			this.Inventory = inventory;
			this.GoodAmount = goodAmount;
			this.FixedAmount = fixedAmount;
		}
	}
}
