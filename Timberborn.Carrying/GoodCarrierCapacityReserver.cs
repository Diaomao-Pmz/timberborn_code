using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterNavigation;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using UnityEngine;

namespace Timberborn.Carrying
{
	// Token: 0x02000011 RID: 17
	public class GoodCarrierCapacityReserver : BaseComponent, IAwakableComponent, IPostLoadableEntity
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002FCB File Offset: 0x000011CB
		public void Awake()
		{
			this._goodCarrier = base.GetComponent<GoodCarrier>();
			this._goodReserver = base.GetComponent<GoodReserver>();
			this._navigator = base.GetComponent<Navigator>();
			this._citizen = base.GetComponent<Citizen>();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002FFD File Offset: 0x000011FD
		public void PostLoadEntity()
		{
			if (this._goodCarrier.IsCarrying && !this._goodReserver.HasReservedCapacity && !this.ReserveCapacityForCarrier())
			{
				Debug.Log("Emptying hands due to failed reservation.");
				this._goodCarrier.EmptyHands();
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003038 File Offset: 0x00001238
		public bool ReserveCapacityForCarrier()
		{
			Inventory inventory = this.FindInventoryForCarriedGoods();
			if (inventory)
			{
				this._goodReserver.ReserveCapacity(inventory, this._goodCarrier.CarriedGoods);
				return true;
			}
			return false;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003070 File Offset: 0x00001270
		public Inventory FindInventoryForCarriedGoods()
		{
			Vector3 start = this._navigator.CurrentAccessOrPosition();
			GoodAmount carriedGoods = this._goodCarrier.CarriedGoods;
			if (this._citizen.HasAssignedDistrict)
			{
				float num;
				return this._citizen.AssignedDistrict.GetComponent<DistrictInventoryPicker>().ClosestInventoryWithCapacity(start, carriedGoods, out num);
			}
			return null;
		}

		// Token: 0x04000031 RID: 49
		public GoodCarrier _goodCarrier;

		// Token: 0x04000032 RID: 50
		public GoodReserver _goodReserver;

		// Token: 0x04000033 RID: 51
		public Navigator _navigator;

		// Token: 0x04000034 RID: 52
		public Citizen _citizen;
	}
}
