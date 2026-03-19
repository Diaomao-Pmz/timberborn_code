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
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Reproduction
{
	// Token: 0x02000007 RID: 7
	public class BreedingPod : BaseComponent, IAwakableComponent, IPersistentEntity, IFinishedStateListener, IRegisteredComponent, IFinishedPausable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public Inventory Inventory { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002111 File Offset: 0x00000311
		public BreedingPod(ITimeTriggerFactory timeTriggerFactory, NewbornSpawner newbornSpawner)
		{
			this._timeTriggerFactory = timeTriggerFactory;
			this._newbornSpawner = newbornSpawner;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002127 File Offset: 0x00000327
		public ImmutableArray<GoodAmountSpec> NutrientsPerCycle
		{
			get
			{
				return this._breedingPodSpec.NutrientsPerCycle;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002134 File Offset: 0x00000334
		public int CyclesUntilFullyGrown
		{
			get
			{
				return this._breedingPodSpec.CyclesUntilFullyGrown;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002141 File Offset: 0x00000341
		public bool NeedsNutrients
		{
			get
			{
				return this._blockableObject.IsUnblocked && !this.Inventory.IsFullyReserved;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002160 File Offset: 0x00000360
		public bool ProgressHalted
		{
			get
			{
				return base.Enabled && this._blockableObject.IsUnblocked && !this._timeTrigger.InProgress;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002188 File Offset: 0x00000388
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._breedingPodSpec = base.GetComponent<BreedingPodSpec>();
			this._building = base.GetComponent<Building>();
			this._embryo = base.GameObject.FindChild(this._breedingPodSpec.EmbryoName);
			this._embryo.SetActive(false);
			this._timeTrigger = this._timeTriggerFactory.Create(new Action(this.FinishGrowthCycle), this._breedingPodSpec.CycleLengthInDays);
			this._timeTrigger.FastForwardProgress(1f);
			this.RestartGrowth();
			base.DisableComponent();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002225 File Offset: 0x00000425
		public void InitializeInventory(Inventory inventory)
		{
			Asserts.FieldIsNull<BreedingPod>(this, this.Inventory, "Inventory");
			this.Inventory = inventory;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002240 File Offset: 0x00000440
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(BreedingPod.BreedingPodKey);
			component.Set(BreedingPod.CyclesRemaining, this._cyclesRemaining);
			if (!this._timeTrigger.Finished)
			{
				component.Set(BreedingPod.GrowthProgressKey, this._timeTrigger.Progress);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002290 File Offset: 0x00000490
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(BreedingPod.BreedingPodKey);
			this._cyclesRemaining = component.Get(BreedingPod.CyclesRemaining);
			if (component.Has<float>(BreedingPod.GrowthProgressKey))
			{
				this._timeTrigger.Reset();
				this._timeTrigger.FastForwardProgress(component.Get(BreedingPod.GrowthProgressKey));
				this._embryo.SetActive(true);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022F4 File Offset: 0x000004F4
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.Inventory.Enable();
			this.Inventory.InventoryChanged += this.OnInventoryChanged;
			this._blockableObject.ObjectBlocked += this.OnObjectBlocked;
			this._blockableObject.ObjectUnblocked += this.OnObjectUnblocked;
			if (this._blockableObject.IsUnblocked)
			{
				this._timeTrigger.Resume();
				return;
			}
			this._timeTrigger.Pause();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000237C File Offset: 0x0000057C
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this.Inventory.Disable();
			this.Inventory.InventoryChanged -= this.OnInventoryChanged;
			this._blockableObject.ObjectBlocked -= this.OnObjectBlocked;
			this._blockableObject.ObjectUnblocked -= this.OnObjectUnblocked;
			this._timeTrigger.Reset();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023EC File Offset: 0x000005EC
		public bool HasResourcesToFinish()
		{
			foreach (GoodAmountSpec goodAmountSpec in this.NutrientsPerCycle)
			{
				if (goodAmountSpec.Amount * this._cyclesRemaining > this.Inventory.AmountInStock(goodAmountSpec.Id))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002440 File Offset: 0x00000640
		public float CalculateProgress()
		{
			float num = 1f / (float)this.CyclesUntilFullyGrown;
			float num2 = 1f - num * (float)this._cyclesRemaining;
			float num3 = this._timeTrigger.Finished ? 0f : (this._timeTrigger.Progress * num);
			return Mathf.Clamp01(num2 + num3);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002493 File Offset: 0x00000693
		public IEnumerable<GoodAmount> Nutrients
		{
			get
			{
				return from good in this.NutrientsPerCycle
				select good.ToGoodAmount();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024C0 File Offset: 0x000006C0
		public void FinishGrowthCycle()
		{
			this._cyclesRemaining--;
			if (this._cyclesRemaining == 0)
			{
				if (this._breedingPodSpec.SpawnAdults)
				{
					this._newbornSpawner.SpawnAdult(this._building);
				}
				else
				{
					this._newbornSpawner.SpawnChild(this._building);
				}
				this._embryo.SetActive(false);
				this.RestartGrowth();
			}
			this.RestartGrowthCycle();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000252C File Offset: 0x0000072C
		public void RestartGrowth()
		{
			this._cyclesRemaining = this.CyclesUntilFullyGrown;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000253C File Offset: 0x0000073C
		public void RestartGrowthCycle()
		{
			if (this.ShouldRestartGrowthCycle())
			{
				this._timeTrigger.Reset();
				this._timeTrigger.Resume();
				foreach (GoodAmount good in this.Nutrients)
				{
					this.Inventory.Take(good);
				}
				this._embryo.SetActive(true);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025B8 File Offset: 0x000007B8
		public bool ShouldRestartGrowthCycle()
		{
			return this._timeTrigger.Finished && this.Nutrients.All((GoodAmount nutrient) => this.Inventory.HasUnreservedStock(nutrient));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025E0 File Offset: 0x000007E0
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			this.RestartGrowthCycle();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025E8 File Offset: 0x000007E8
		public void OnObjectBlocked(object sender, EventArgs e)
		{
			this._timeTrigger.Pause();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025F5 File Offset: 0x000007F5
		public void OnObjectUnblocked(object sender, EventArgs e)
		{
			this._timeTrigger.Resume();
		}

		// Token: 0x04000008 RID: 8
		public static readonly ComponentKey BreedingPodKey = new ComponentKey("BreedingPod");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<int> CyclesRemaining = new PropertyKey<int>("CyclesRemaining");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<float> GrowthProgressKey = new PropertyKey<float>("GrowthProgress");

		// Token: 0x0400000C RID: 12
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x0400000D RID: 13
		public readonly NewbornSpawner _newbornSpawner;

		// Token: 0x0400000E RID: 14
		public BlockableObject _blockableObject;

		// Token: 0x0400000F RID: 15
		public BreedingPodSpec _breedingPodSpec;

		// Token: 0x04000010 RID: 16
		public Building _building;

		// Token: 0x04000011 RID: 17
		public GameObject _embryo;

		// Token: 0x04000012 RID: 18
		public ITimeTrigger _timeTrigger;

		// Token: 0x04000013 RID: 19
		public int _cyclesRemaining;
	}
}
