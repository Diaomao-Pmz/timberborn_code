using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Carrying;
using Timberborn.Common;
using Timberborn.Emptying;
using Timberborn.TimeSystem;
using Timberborn.WorkSystem;
using UnityEngine;

namespace Timberborn.DistributionSystem
{
	// Token: 0x02000011 RID: 17
	public class DistrictCrossingWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003169 File Offset: 0x00001369
		public DistrictCrossingWorkplaceBehavior(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003183 File Offset: 0x00001383
		public void Awake()
		{
			this._districtCrossing = base.GetComponent<DistrictCrossing>();
			this._districtCrossingInventory = base.GetComponent<DistrictCrossingInventory>();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000031A0 File Offset: 0x000013A0
		public override Decision Decide(BehaviorAgent agent)
		{
			if (this.IsCloseToDistrictCrossing(agent))
			{
				if (this.TryEmptying(agent))
				{
					return Decision.ReleaseNextTick();
				}
				if (this.TryExport(agent))
				{
					return Decision.ReleaseNextTick();
				}
			}
			else
			{
				if (this.TryExport(agent))
				{
					return Decision.ReleaseNextTick();
				}
				if (this.TryEmptying(agent))
				{
					return Decision.ReleaseNextTick();
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000031F8 File Offset: 0x000013F8
		public bool IsCloseToDistrictCrossing(BehaviorAgent agent)
		{
			Vector2 vector = agent.Transform.position.XZ();
			Vector2 vector2 = this._districtCrossing.Transform.position.XZ();
			return (vector - vector2).sqrMagnitude < DistrictCrossingWorkplaceBehavior.PrioritizeEmptyingDistanceSquared;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003240 File Offset: 0x00001440
		public bool TryEmptying(BehaviorAgent agent)
		{
			return this._districtCrossingInventory.Inventory.OutputGoods.Count > 0 && agent.GetComponent<EmptyingStarter>().StartEmptying(this._districtCrossingInventory.Inventory);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003280 File Offset: 0x00001480
		public bool TryExport(BehaviorAgent agent)
		{
			if (this._districtCrossing.CanExport)
			{
				this._districtCrossing.GetLinkedDistrictDistributableGoods(this._linkedDistrictDistributableGoods);
				bool result = this.TryExportDistributableGoods(agent);
				this._linkedDistrictDistributableGoods.Clear();
				return result;
			}
			return false;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000032B4 File Offset: 0x000014B4
		public bool TryExportDistributableGoods(BehaviorAgent agent)
		{
			for (int i = 0; i < this._linkedDistrictDistributableGoods.Count; i++)
			{
				if (this.TryExportGood(agent, this._linkedDistrictDistributableGoods[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000032F0 File Offset: 0x000014F0
		public bool TryExportGood(BehaviorAgent agent, DistributableGood linkedDistributableGood)
		{
			string goodId = linkedDistributableGood.GoodId;
			DistributableGood myDistributableGood = this._districtCrossing.GetMyDistributableGood(goodId);
			if (this._districtCrossing.CanExportGood(myDistributableGood, linkedDistributableGood))
			{
				int amountToExport = this._districtCrossing.GetAmountToExport(myDistributableGood, linkedDistributableGood);
				if (this._districtCrossingInventory.Inventory.UnreservedAmountInStock(goodId) > 0)
				{
					this._districtCrossingInventory.TransferStock(goodId, amountToExport);
				}
				else if (this.TryStartCarrying(agent, goodId, amountToExport, linkedDistributableGood))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003364 File Offset: 0x00001564
		public bool TryStartCarrying(BehaviorAgent agent, string goodId, int amountToBring, DistributableGood linkedDistributableGood)
		{
			int val = this._districtCrossingInventory.Inventory.UnreservedCapacity(goodId);
			int num = Math.Min(amountToBring, val);
			if (num > 0 && agent.GetComponent<CarrierInventoryFinder>().TryCarryFromAnyInventoryLimited(goodId, this._districtCrossingInventory.Inventory, num))
			{
				linkedDistributableGood.UpdateLastImportTimestamp(this._dayNightCycle.PartialDayNumber);
				return true;
			}
			return false;
		}

		// Token: 0x04000028 RID: 40
		public static readonly float PrioritizeEmptyingDistanceSquared = 4f;

		// Token: 0x04000029 RID: 41
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400002A RID: 42
		public DistrictCrossing _districtCrossing;

		// Token: 0x0400002B RID: 43
		public DistrictCrossingInventory _districtCrossingInventory;

		// Token: 0x0400002C RID: 44
		public readonly List<DistributableGood> _linkedDistrictDistributableGoods = new List<DistributableGood>();
	}
}
