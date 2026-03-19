using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BatchControl;
using Timberborn.BlockSystem;
using Timberborn.CoreUI;
using Timberborn.InventorySystem;
using UnityEngine.UIElements;

namespace Timberborn.InventorySystemBatchControl
{
	// Token: 0x02000005 RID: 5
	public class InventoryCapacityBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem, IFinishableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000210D File Offset: 0x0000030D
		public VisualElement Root { get; }

		// Token: 0x06000006 RID: 6 RVA: 0x00002115 File Offset: 0x00000315
		public InventoryCapacityBatchControlRowItem(VisualElement root, Inventory inventory, IEnumerable<InventoryCapacityBatchControlGood> goods)
		{
			this.Root = root;
			this._inventory = inventory;
			this._goods = goods.ToImmutableArray<InventoryCapacityBatchControlGood>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000213C File Offset: 0x0000033C
		public void UpdateRowItem()
		{
			if (this._inventory.Enabled && this._inventory.GetComponent<BlockObject>().IsFinished)
			{
				for (int i = 0; i < this._goods.Count; i++)
				{
					this._goods[i].UpdateGoodAmount();
				}
				this.SetFinishedState(true);
				return;
			}
			this.SetFinishedState(false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000219E File Offset: 0x0000039E
		public void SetFinishedState(bool isFinished)
		{
			this.Root.ToggleDisplayStyle(isFinished);
		}

		// Token: 0x0400000A RID: 10
		public readonly IReadOnlyList<InventoryCapacityBatchControlGood> _goods;

		// Token: 0x0400000B RID: 11
		public readonly Inventory _inventory;
	}
}
