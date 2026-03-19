using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.GameDistricts;
using Timberborn.Persistence;
using Timberborn.TemplateSystem;
using Timberborn.WorkerTypes;
using Timberborn.WorldPersistence;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200002B RID: 43
	public class WorkplaceWorkerType : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<WorkplaceWorkerType>, IDuplicable, IUnfinishedStateListener, IFinishedStateListener
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000150 RID: 336 RVA: 0x00004CD0 File Offset: 0x00002ED0
		// (remove) Token: 0x06000151 RID: 337 RVA: 0x00004D08 File Offset: 0x00002F08
		public event EventHandler<WorkerTypeChangedEventArgs> WorkerTypeChanged;

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00004D3D File Offset: 0x00002F3D
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00004D45 File Offset: 0x00002F45
		public string WorkerType { get; private set; }

		// Token: 0x06000154 RID: 340 RVA: 0x00004D4E File Offset: 0x00002F4E
		public WorkplaceWorkerType(WorkerTypeService workerTypeService, WorkplaceUnlockingService workplaceUnlockingService)
		{
			this._workerTypeService = workerTypeService;
			this._workplaceUnlockingService = workplaceUnlockingService;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004D64 File Offset: 0x00002F64
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._templateSpec = base.GetComponent<TemplateSpec>();
			this._workplaceSpec = base.GetComponent<WorkplaceSpec>();
			this._workplace = base.GetComponent<Workplace>();
			this.WorkerType = this._workplaceSpec.DefaultWorkerType;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004DB4 File Offset: 0x00002FB4
		public void SetWorkerType(string workerType)
		{
			if (workerType != this.WorkerType && this.IsWorkerTypeAllowed(workerType) && this.IsWorkerTypeUnlocked(workerType))
			{
				this._workplace.UnassignAllWorkers();
				string workerType2 = this.WorkerType;
				this.WorkerType = workerType;
				EventHandler<WorkerTypeChangedEventArgs> workerTypeChanged = this.WorkerTypeChanged;
				if (workerTypeChanged != null)
				{
					workerTypeChanged(this, new WorkerTypeChangedEventArgs(workerType2, this.WorkerType));
				}
			}
			this._wasSet = true;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00004E1F File Offset: 0x0000301F
		public void OnEnterUnfinishedState()
		{
			if (!this._wasSet)
			{
				this._districtBuilding.ReassignedConstructionDistrict += this.OnReassignedConstructionDistrict;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004E40 File Offset: 0x00003040
		public void OnExitUnfinishedState()
		{
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004E44 File Offset: 0x00003044
		public void OnEnterFinishedState()
		{
			if (this._districtBuilding.InstantDistrict)
			{
				this.SetWorkerTypeToDistrict(this._districtBuilding.InstantDistrict);
				return;
			}
			if (!this._wasSet)
			{
				this._districtBuilding.ReassignedInstantDistrict += this.OnReassignedInstantDistrict;
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004E40 File Offset: 0x00003040
		public void OnExitFinishedState()
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004E94 File Offset: 0x00003094
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(WorkplaceWorkerType.WorkplaceWorkerTypeKey);
			component.Set(WorkplaceWorkerType.WorkerTypeKey, this.WorkerType);
			component.Set(WorkplaceWorkerType.WasSetKey, this._wasSet);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004EC4 File Offset: 0x000030C4
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(WorkplaceWorkerType.WorkplaceWorkerTypeKey);
			string workerType = this._workerTypeService.GetWorkerType(component.Get(WorkplaceWorkerType.WorkerTypeKey));
			if (this.IsWorkerTypeAllowed(workerType) && this.IsWorkerTypeUnlocked(workerType))
			{
				this.WorkerType = workerType;
			}
			this._wasSet = component.Get(WorkplaceWorkerType.WasSetKey);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004F20 File Offset: 0x00003120
		public void DuplicateFrom(WorkplaceWorkerType source)
		{
			string workerType = source.WorkerType;
			if (this.IsWorkerTypeAllowed(workerType) && this.IsWorkerTypeUnlocked(workerType))
			{
				this.SetWorkerType(workerType);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00004F4D File Offset: 0x0000314D
		public void OnReassignedConstructionDistrict(object sender, EventArgs e)
		{
			this.SetWorkerTypeToDistrict(this._districtBuilding.ConstructionDistrict);
			this._districtBuilding.ReassignedConstructionDistrict -= this.OnReassignedConstructionDistrict;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004F77 File Offset: 0x00003177
		public void OnReassignedInstantDistrict(object sender, EventArgs e)
		{
			this.SetWorkerTypeToDistrict(this._districtBuilding.InstantDistrict);
			this._districtBuilding.ReassignedInstantDistrict -= this.OnReassignedInstantDistrict;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004FA1 File Offset: 0x000031A1
		public bool IsWorkerTypeAllowed(string workerType)
		{
			return !this._workplaceSpec.DisallowOtherWorkerTypes || this._workplaceSpec.DefaultWorkerType == workerType;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004FC4 File Offset: 0x000031C4
		public bool IsWorkerTypeUnlocked(string workerType)
		{
			UnlockableWorkerType unlockableWorkerType = new UnlockableWorkerType(this._templateSpec.TemplateName, workerType);
			return this._workplaceUnlockingService.Unlocked(unlockableWorkerType);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004FF0 File Offset: 0x000031F0
		public void SetWorkerTypeToDistrict(DistrictCenter districtCenter)
		{
			if (!this._wasSet && districtCenter)
			{
				DistrictDefaultWorkerType component = districtCenter.GetComponent<DistrictDefaultWorkerType>();
				this.SetWorkerType(component.WorkerType);
			}
		}

		// Token: 0x04000078 RID: 120
		public static readonly ComponentKey WorkplaceWorkerTypeKey = new ComponentKey("WorkplaceWorkerType");

		// Token: 0x04000079 RID: 121
		public static readonly PropertyKey<string> WorkerTypeKey = new PropertyKey<string>("WorkerType");

		// Token: 0x0400007A RID: 122
		public static readonly PropertyKey<bool> WasSetKey = new PropertyKey<bool>("WasSet");

		// Token: 0x0400007D RID: 125
		public readonly WorkerTypeService _workerTypeService;

		// Token: 0x0400007E RID: 126
		public readonly WorkplaceUnlockingService _workplaceUnlockingService;

		// Token: 0x0400007F RID: 127
		public DistrictBuilding _districtBuilding;

		// Token: 0x04000080 RID: 128
		public TemplateSpec _templateSpec;

		// Token: 0x04000081 RID: 129
		public WorkplaceSpec _workplaceSpec;

		// Token: 0x04000082 RID: 130
		public Workplace _workplace;

		// Token: 0x04000083 RID: 131
		public bool _wasSet;
	}
}
