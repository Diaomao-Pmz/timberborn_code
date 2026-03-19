using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.GoodStackSystem;
using Timberborn.InventorySystem;
using Timberborn.TerrainPhysics;
using UnityEngine;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x0200000B RID: 11
	public class RecoveredGoodStack : BaseComponent, IGoodStackInventory, IInitializableEntity, INonStackPickable
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000249D File Offset: 0x0000069D
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000024A5 File Offset: 0x000006A5
		public Inventory Inventory { get; private set; }

		// Token: 0x06000022 RID: 34 RVA: 0x000024AE File Offset: 0x000006AE
		public RecoveredGoodStack(EntityService entityService)
		{
			this._entityService = entityService;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024BD File Offset: 0x000006BD
		public void InitializeInventory(Inventory inventory)
		{
			Asserts.FieldIsNull<RecoveredGoodStack>(this, this.Inventory, "Inventory");
			this.Inventory = inventory;
			this.Inventory.Enable();
			this.Inventory.InventoryChanged += this.OnInventoryChanged;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024FC File Offset: 0x000006FC
		public void InitializeEntity()
		{
			if (!this._initialGoodAmounts.IsDefault)
			{
				this.GiveGoodAmounts(this._initialGoodAmounts);
			}
			if (this.Inventory.IsEmpty)
			{
				Debug.LogWarning(string.Format("RecoveredGoodStack at {0} ", base.GetComponent<BlockObject>().Coordinates) + "was empty after initialization. Deleting.");
				this.Delete();
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002563 File Offset: 0x00000763
		public void SetInitialGoods(IEnumerable<GoodAmount> initialGoodAmounts)
		{
			this._initialGoodAmounts = initialGoodAmounts.ToImmutableArray<GoodAmount>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002571 File Offset: 0x00000771
		public void MergeInto(RecoveredGoodStack otherGoodStack)
		{
			otherGoodStack.GiveGoodAmounts(this.Inventory.Stock);
			this.Delete();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002590 File Offset: 0x00000790
		public void GiveGoodAmounts(IEnumerable<GoodAmount> goodAmounts)
		{
			foreach (GoodAmount good in goodAmounts)
			{
				this.Inventory.Give(good);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025E0 File Offset: 0x000007E0
		public void Delete()
		{
			this._entityService.Delete(this);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025EE File Offset: 0x000007EE
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			if (this.Inventory.IsEmpty)
			{
				this.Delete();
			}
		}

		// Token: 0x04000018 RID: 24
		public readonly EntityService _entityService;

		// Token: 0x04000019 RID: 25
		public ImmutableArray<GoodAmount> _initialGoodAmounts;
	}
}
