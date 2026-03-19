using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.ScienceSystem
{
	// Token: 0x0200000B RID: 11
	public class ScienceNeedingBuilding : TickableComponent, IAwakableComponent, IFinishedStateListener, IPersistentEntity, IFinishedPausable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000018 RID: 24 RVA: 0x00002410 File Offset: 0x00000610
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x00002448 File Offset: 0x00000648
		public event EventHandler<NotEnoughScienceStateChangedEventArgs> NotEnoughScienceStateChanged;

		// Token: 0x0600001A RID: 26 RVA: 0x0000247D File Offset: 0x0000067D
		public ScienceNeedingBuilding(IDayNightCycle dayNightCycle, ScienceService scienceService)
		{
			this._dayNightCycle = dayNightCycle;
			this._scienceService = scienceService;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002493 File Offset: 0x00000693
		public int ScienceUsedPerHour
		{
			get
			{
				return this._scienceNeedingBuildingSpec.ScienceUsedPerHour;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000024A0 File Offset: 0x000006A0
		public float ScienceStoredPercentage
		{
			get
			{
				return this._currentScience / (float)this.ScienceUsedPerHour;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024B0 File Offset: 0x000006B0
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._scienceNeedingBuildingSpec = base.GetComponent<ScienceNeedingBuildingSpec>();
			this._sciencePerTick = this._dayNightCycle.FixedDeltaTimeInHours * (float)this.ScienceUsedPerHour;
			base.DisableComponent();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024E9 File Offset: 0x000006E9
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.UpdateNotEnoughScience();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024F7 File Offset: 0x000006F7
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024FF File Offset: 0x000006FF
		public override void Tick()
		{
			if (this._currentScience <= 0f)
			{
				this.RefillScience();
			}
			if (this._currentScience > 0f && this._blockableObject.IsUnblocked)
			{
				this.UseScience();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002534 File Offset: 0x00000734
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(ScienceNeedingBuilding.ScienceNeedingBuildingKey).Set(ScienceNeedingBuilding.CurrentScienceKey, this._currentScience);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002554 File Offset: 0x00000754
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(ScienceNeedingBuilding.ScienceNeedingBuildingKey);
			this._currentScience = component.Get(ScienceNeedingBuilding.CurrentScienceKey);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000257E File Offset: 0x0000077E
		public void RefillScience()
		{
			if (this._scienceService.SciencePoints >= this.ScienceUsedPerHour)
			{
				this.AddPoints(this.ScienceUsedPerHour);
			}
			this.UpdateNotEnoughScience();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025A5 File Offset: 0x000007A5
		public void AddPoints(int neededScience)
		{
			this._scienceService.SubtractPoints(neededScience);
			this._currentScience += (float)neededScience;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025C2 File Offset: 0x000007C2
		public void UseScience()
		{
			this._currentScience -= this._sciencePerTick;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025D8 File Offset: 0x000007D8
		public void UpdateNotEnoughScience()
		{
			bool flag = this._currentScience <= 0f;
			if (this._notEnoughScience != flag)
			{
				this._notEnoughScience = flag;
				this.UpdateBlockableBuilding(flag);
				EventHandler<NotEnoughScienceStateChangedEventArgs> notEnoughScienceStateChanged = this.NotEnoughScienceStateChanged;
				if (notEnoughScienceStateChanged == null)
				{
					return;
				}
				notEnoughScienceStateChanged(this, new NotEnoughScienceStateChangedEventArgs(flag));
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002624 File Offset: 0x00000824
		public void UpdateBlockableBuilding(bool block)
		{
			if (block)
			{
				this._blockableObject.Block(this);
				return;
			}
			this._blockableObject.Unblock(this);
		}

		// Token: 0x04000017 RID: 23
		public static readonly ComponentKey ScienceNeedingBuildingKey = new ComponentKey("ScienceNeedingBuilding");

		// Token: 0x04000018 RID: 24
		public static readonly PropertyKey<float> CurrentScienceKey = new PropertyKey<float>("CurrentScience");

		// Token: 0x0400001A RID: 26
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400001B RID: 27
		public readonly ScienceService _scienceService;

		// Token: 0x0400001C RID: 28
		public BlockableObject _blockableObject;

		// Token: 0x0400001D RID: 29
		public ScienceNeedingBuildingSpec _scienceNeedingBuildingSpec;

		// Token: 0x0400001E RID: 30
		public float _sciencePerTick;

		// Token: 0x0400001F RID: 31
		public float _currentScience;

		// Token: 0x04000020 RID: 32
		public bool _notEnoughScience;
	}
}
