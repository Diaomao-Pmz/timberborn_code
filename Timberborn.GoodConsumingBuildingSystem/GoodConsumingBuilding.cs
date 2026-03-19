using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Persistence;
using Timberborn.ResourceCountingSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.GoodConsumingBuildingSystem
{
	// Token: 0x02000008 RID: 8
	public class GoodConsumingBuilding : TickableComponent, IAwakableComponent, IFinishedStateListener, IPostInitializableEntity, IPersistentEntity, IRegisteredComponent, IBuildingEfficiencyProvider, IFinishedPausable, IGoodProcessor
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022C5 File Offset: 0x000004C5
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000022CD File Offset: 0x000004CD
		public Inventory Inventory { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022D6 File Offset: 0x000004D6
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000022DE File Offset: 0x000004DE
		public bool IsConsuming { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022E7 File Offset: 0x000004E7
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000022EF File Offset: 0x000004EF
		public bool ConsumptionPaused { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022F8 File Offset: 0x000004F8
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002300 File Offset: 0x00000500
		public float MaximumWorkingTime { get; private set; }

		// Token: 0x0600001F RID: 31 RVA: 0x00002309 File Offset: 0x00000509
		public GoodConsumingBuilding(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002339 File Offset: 0x00000539
		public bool CanUse
		{
			get
			{
				return this.CanUseInternal();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002341 File Offset: 0x00000541
		public float Efficiency
		{
			get
			{
				return (float)(this.CanUse ? 1 : 0);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002350 File Offset: 0x00000550
		public ImmutableArray<ConsumedGoodSpec> ConsumedGoods
		{
			get
			{
				return this._goodConsumingBuildingSpec.ConsumedGoods;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002360 File Offset: 0x00000560
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._goodConsumingBuildingSpec = base.GetComponent<GoodConsumingBuildingSpec>();
			for (int i = 0; i < this.ConsumedGoods.Length; i++)
			{
				this._suppliesLeft.Add(0f);
			}
			this.MaximumWorkingTime = (float)this._goodConsumingBuildingSpec.FullInventoryWorkHours + 1f / this.ConsumedGoods.Max((ConsumedGoodSpec good) => good.GoodPerHour);
			base.DisableComponent();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023FC File Offset: 0x000005FC
		public void InitializeInventory(Inventory inventory)
		{
			Asserts.FieldIsNull<GoodConsumingBuilding>(this, this.Inventory, "Inventory");
			this.Inventory = inventory;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002416 File Offset: 0x00000616
		public override void Tick()
		{
			this.UpdateIsConsuming();
			this.ConsumeSupplies();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002424 File Offset: 0x00000624
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.Inventory.Enable();
			this.Inventory.InventoryStockChanged += this.OnInventoryStockChanged;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000244E File Offset: 0x0000064E
		public void OnExitFinishedState()
		{
			this.Inventory.InventoryStockChanged -= this.OnInventoryStockChanged;
			this.Inventory.Disable();
			base.DisableComponent();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002478 File Offset: 0x00000678
		public void PostInitializeEntity()
		{
			this.UpdateIsConsuming();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002480 File Offset: 0x00000680
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(GoodConsumingBuilding.GoodConsumingBuildingKey).Set(GoodConsumingBuilding.SuppliesLeftKey, this._suppliesLeft);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024A0 File Offset: 0x000006A0
		[BackwardCompatible(2025, 10, 23, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(GoodConsumingBuilding.GoodConsumingBuildingKey);
			if (component.Has<float>(GoodConsumingBuilding.SuppliesLeftKey))
			{
				List<float> list = component.Get(GoodConsumingBuilding.SuppliesLeftKey);
				for (int i = 0; i < this._suppliesLeft.Count; i++)
				{
					if (i >= list.Count)
					{
						return;
					}
					this._suppliesLeft[i] = list[i];
				}
			}
			else
			{
				float value = component.Get(new PropertyKey<float>("SupplyLeft"));
				for (int j = 0; j < this._suppliesLeft.Count; j++)
				{
					this._suppliesLeft[j] = value;
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002540 File Offset: 0x00000740
		public GoodConsumingToggle GetGoodConsumingToggle()
		{
			GoodConsumingToggle goodConsumingToggle = new GoodConsumingToggle();
			this._toggles.Add(goodConsumingToggle);
			goodConsumingToggle.StateChanged += delegate(object _, EventArgs _)
			{
				this.UpdateConsumptionState();
			};
			return goodConsumingToggle;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002574 File Offset: 0x00000774
		public float HoursUntilNoSupply()
		{
			float num = float.MaxValue;
			for (int i = 0; i < this.ConsumedGoods.Length; i++)
			{
				ConsumedGoodSpec consumedGoodSpec = this.ConsumedGoods[i];
				float val = ((float)this.Inventory.UnreservedAmountInStock(consumedGoodSpec.GoodId) + this._suppliesLeft[i]) / consumedGoodSpec.GoodPerHour;
				num = Math.Min(num, val);
			}
			return num;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025E2 File Offset: 0x000007E2
		public GoodRegistry GetProcessingGoods()
		{
			return this._processingGoods;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025EA File Offset: 0x000007EA
		public void OnInventoryStockChanged(object sender, InventoryAmountChangedEventArgs e)
		{
			this.UpdateProcessingGoods();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025F4 File Offset: 0x000007F4
		public void UpdateProcessingGoods()
		{
			this._processingGoods.Clear();
			foreach (GoodAmount good in this.Inventory.Stock)
			{
				this._processingGoods.Add(good);
			}
			for (int i = 0; i < this._suppliesLeft.Count; i++)
			{
				if (this._suppliesLeft[i] > 0f)
				{
					string goodId = this.ConsumedGoods[i].GoodId;
					this._processingGoods.Add(new GoodAmount(goodId, 1));
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026B4 File Offset: 0x000008B4
		public bool CanUseInternal()
		{
			for (int i = 0; i < this.ConsumedGoods.Length; i++)
			{
				if (this._suppliesLeft[i] <= 0f && this.Inventory.UnreservedAmountInStock(this.ConsumedGoods[i].GoodId) == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002711 File Offset: 0x00000911
		public void UpdateConsumptionState()
		{
			this.ConsumptionPaused = this._toggles.FastAny((GoodConsumingToggle toggle) => toggle.Paused);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002743 File Offset: 0x00000943
		public void UpdateIsConsuming()
		{
			this.IsConsuming = (!this.ConsumptionPaused && this._blockableObject.IsUnblocked && this.HasSupplies());
			this.UpdateProcessingGoods();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002770 File Offset: 0x00000970
		public void ConsumeSupplies()
		{
			if (this.IsConsuming)
			{
				for (int i = 0; i < this._suppliesLeft.Count; i++)
				{
					List<float> suppliesLeft = this._suppliesLeft;
					int index = i;
					suppliesLeft[index] -= this._dayNightCycle.FixedDeltaTimeInHours * this.ConsumedGoods[i].GoodPerHour;
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027D4 File Offset: 0x000009D4
		public bool HasSupplies()
		{
			for (int i = 0; i < this._suppliesLeft.Count; i++)
			{
				if (this._suppliesLeft[i] <= 0f)
				{
					string goodId = this.ConsumedGoods[i].GoodId;
					if (this.Inventory.UnreservedAmountInStock(goodId) <= 0)
					{
						return false;
					}
					this.Inventory.Take(new GoodAmount(goodId, 1));
					this._suppliesLeft[i] = 1f;
				}
			}
			return true;
		}

		// Token: 0x0400000A RID: 10
		public static readonly ComponentKey GoodConsumingBuildingKey = new ComponentKey("GoodConsumingBuilding");

		// Token: 0x0400000B RID: 11
		public static readonly ListKey<float> SuppliesLeftKey = new ListKey<float>("SuppliesLeft");

		// Token: 0x04000010 RID: 16
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000011 RID: 17
		public BlockableObject _blockableObject;

		// Token: 0x04000012 RID: 18
		public GoodConsumingBuildingSpec _goodConsumingBuildingSpec;

		// Token: 0x04000013 RID: 19
		public readonly List<GoodConsumingToggle> _toggles = new List<GoodConsumingToggle>();

		// Token: 0x04000014 RID: 20
		public readonly List<float> _suppliesLeft = new List<float>();

		// Token: 0x04000015 RID: 21
		public readonly GoodRegistry _processingGoods = new GoodRegistry();
	}
}
