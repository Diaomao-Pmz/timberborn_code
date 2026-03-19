using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.Goods;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.InventorySystem
{
	// Token: 0x0200000A RID: 10
	public class GoodReserver : BaseComponent, IStartableComponent, IPersistentEntity
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002867 File Offset: 0x00000A67
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000286F File Offset: 0x00000A6F
		public GoodReservation StockReservation { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002878 File Offset: 0x00000A78
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002880 File Offset: 0x00000A80
		public GoodReservation CapacityReservation { get; private set; }

		// Token: 0x0600002D RID: 45 RVA: 0x00002889 File Offset: 0x00000A89
		public GoodReserver(GoodReservationValueSerializer goodReservationValueSerializer)
		{
			this._goodReservationValueSerializer = goodReservationValueSerializer;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002898 File Offset: 0x00000A98
		public bool HasReservedCapacity
		{
			get
			{
				if (this.CapacityReservation.Inventory)
				{
					Inventory inventory = this.CapacityReservation.Inventory;
					return inventory != null && inventory.Enabled;
				}
				return false;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000028D8 File Offset: 0x00000AD8
		public bool HasReservedStock
		{
			get
			{
				if (this.StockReservation.Inventory)
				{
					Inventory inventory = this.StockReservation.Inventory;
					return inventory != null && inventory.Enabled;
				}
				return false;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002916 File Offset: 0x00000B16
		public void Start()
		{
			this.ResolveLoadedReservations();
			base.GetComponent<Character>().Died += this.OnDied;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002935 File Offset: 0x00000B35
		public void ReserveCapacity(Inventory inventory, GoodAmount capacity)
		{
			GoodReserver.ThrowIfInventoryNotEnabled(inventory);
			this.UnreserveCapacity();
			inventory.ReserveCapacity(capacity);
			this.CapacityReservation = new GoodReservation(inventory, capacity, true);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002958 File Offset: 0x00000B58
		public void UnreserveCapacity()
		{
			if (this.CapacityReservation.Inventory != null)
			{
				this.CapacityReservation.Inventory.UnreserveCapacity(this.CapacityReservation.GoodAmount);
			}
			this.CapacityReservation = default(GoodReservation);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029A5 File Offset: 0x00000BA5
		public void ReserveExactStockAmount(Inventory inventory, GoodAmount good)
		{
			this.ReserveStock(inventory, good, true);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000029B0 File Offset: 0x00000BB0
		public void ReserveNotLessThanStockAmount(Inventory inventory, GoodAmount good)
		{
			this.ReserveStock(inventory, good, false);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029BC File Offset: 0x00000BBC
		public void UnreserveStock()
		{
			if (this.StockReservation.Inventory != null)
			{
				this.StockReservation.Inventory.UnreserveStock(this.StockReservation.GoodAmount);
			}
			this.StockReservation = default(GoodReservation);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A0C File Offset: 0x00000C0C
		public void Save(IEntitySaver entitySaver)
		{
			if (this.HasReservedStock || this.HasReservedCapacity)
			{
				IObjectSaver component = entitySaver.GetComponent(GoodReserver.GoodReserverKey);
				if (this.HasReservedStock)
				{
					component.Set<GoodReservation>(GoodReserver.StockReservationKey, this.StockReservation, this._goodReservationValueSerializer);
				}
				if (this.HasReservedCapacity)
				{
					component.Set<GoodReservation>(GoodReserver.CapacityReservationKey, this.CapacityReservation, this._goodReservationValueSerializer);
				}
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A74 File Offset: 0x00000C74
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(GoodReserver.GoodReserverKey, out objectLoader))
			{
				GoodReservation stockReservation;
				if (objectLoader.Has<GoodReservation>(GoodReserver.StockReservationKey) && objectLoader.GetObsoletable<GoodReservation>(GoodReserver.StockReservationKey, this._goodReservationValueSerializer, out stockReservation))
				{
					this.StockReservation = stockReservation;
				}
				GoodReservation capacityReservation;
				if (objectLoader.Has<GoodReservation>(GoodReserver.CapacityReservationKey) && objectLoader.GetObsoletable<GoodReservation>(GoodReserver.CapacityReservationKey, this._goodReservationValueSerializer, out capacityReservation))
				{
					this.CapacityReservation = capacityReservation;
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public void ResolveLoadedReservations()
		{
			if (this.CapacityReservation.Inventory != null)
			{
				if (this.CapacityReservation.Inventory.HasUnreservedCapacity(this.CapacityReservation.GoodAmount))
				{
					this.CapacityReservation.Inventory.ReserveCapacity(this.CapacityReservation.GoodAmount);
				}
				else
				{
					this.LogReservationResolvingWarning(this.CapacityReservation, "capacity");
					this.CapacityReservation = default(GoodReservation);
				}
			}
			if (this.StockReservation.Inventory != null)
			{
				if (this.StockReservation.Inventory.HasUnreservedStock(this.StockReservation.GoodAmount))
				{
					this.StockReservation.Inventory.ReserveStock(this.StockReservation.GoodAmount);
					return;
				}
				this.LogReservationResolvingWarning(this.StockReservation, "good");
				this.StockReservation = default(GoodReservation);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void OnDied(object sender, EventArgs e)
		{
			this.UnreserveCapacity();
			this.UnreserveStock();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002BEA File Offset: 0x00000DEA
		public void ReserveStock(Inventory inventory, GoodAmount good, bool fixedAmount)
		{
			GoodReserver.ThrowIfInventoryNotEnabled(inventory);
			this.UnreserveStock();
			inventory.ReserveStock(good);
			this.StockReservation = new GoodReservation(inventory, good, fixedAmount);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C0D File Offset: 0x00000E0D
		public static void ThrowIfInventoryNotEnabled(Inventory inventory)
		{
			if (!inventory.Enabled)
			{
				throw new ArgumentException(string.Format("Provided {0} is not enabled! {1}", "Inventory", inventory));
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C30 File Offset: 0x00000E30
		public void LogReservationResolvingWarning(GoodReservation goodReservation, string reservationType)
		{
			Debug.LogWarning(string.Concat(new string[]
			{
				"While loading ",
				base.Name,
				" couldn't reserve ",
				reservationType,
				" ",
				string.Format("for {0} at {1} ", goodReservation.GoodAmount, goodReservation.Inventory.Name),
				"and ignored this reservation of ",
				reservationType,
				"."
			}));
		}

		// Token: 0x04000017 RID: 23
		public static readonly ComponentKey GoodReserverKey = new ComponentKey("GoodReserver");

		// Token: 0x04000018 RID: 24
		public static readonly PropertyKey<GoodReservation> StockReservationKey = new PropertyKey<GoodReservation>("StockReservation");

		// Token: 0x04000019 RID: 25
		public static readonly PropertyKey<GoodReservation> CapacityReservationKey = new PropertyKey<GoodReservation>("CapacityReservation");

		// Token: 0x0400001C RID: 28
		public readonly GoodReservationValueSerializer _goodReservationValueSerializer;
	}
}
