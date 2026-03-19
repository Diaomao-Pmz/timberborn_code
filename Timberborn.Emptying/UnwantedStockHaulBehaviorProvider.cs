using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Hauling;
using Timberborn.InventorySystem;

namespace Timberborn.Emptying
{
	// Token: 0x02000010 RID: 16
	public class UnwantedStockHaulBehaviorProvider : BaseComponent, IAwakableComponent, IHaulBehaviorProvider
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002C58 File Offset: 0x00000E58
		public void Awake()
		{
			this._inventories = base.GetComponent<Inventories>();
			this._removeUnwantedStockWorkplaceBehavior = base.GetComponent<RemoveUnwantedStockWorkplaceBehavior>();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C74 File Offset: 0x00000E74
		public void GetWeightedBehaviors(IList<WeightedBehavior> weightedBehaviors)
		{
			using (List<Inventory>.Enumerator enumerator = this._inventories.EnabledInventories.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasUnwantedStock)
					{
						weightedBehaviors.Add(new WeightedBehavior(UnwantedStockHaulBehaviorProvider.UnwantedStockWeight, this._removeUnwantedStockWorkplaceBehavior));
					}
				}
			}
		}

		// Token: 0x04000020 RID: 32
		public static readonly float UnwantedStockWeight = 0.5f;

		// Token: 0x04000021 RID: 33
		public Inventories _inventories;

		// Token: 0x04000022 RID: 34
		public RemoveUnwantedStockWorkplaceBehavior _removeUnwantedStockWorkplaceBehavior;
	}
}
