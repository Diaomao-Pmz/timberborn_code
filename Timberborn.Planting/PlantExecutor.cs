using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.EnterableSystem;
using Timberborn.NaturalResources;
using Timberborn.Persistence;
using Timberborn.TemplateSystem;
using Timberborn.TimeSystem;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x02000018 RID: 24
	public class PlantExecutor : BaseComponent, IAwakableComponent, IExecutor
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600007E RID: 126 RVA: 0x00002FC8 File Offset: 0x000011C8
		// (remove) Token: 0x0600007F RID: 127 RVA: 0x00003000 File Offset: 0x00001200
		public event EventHandler PlantingStarted;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000080 RID: 128 RVA: 0x00003038 File Offset: 0x00001238
		// (remove) Token: 0x06000081 RID: 129 RVA: 0x00003070 File Offset: 0x00001270
		public event EventHandler PlantingFinished;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000030A5 File Offset: 0x000012A5
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000030AD File Offset: 0x000012AD
		public bool IsPlanting { get; private set; }

		// Token: 0x06000084 RID: 132 RVA: 0x000030B6 File Offset: 0x000012B6
		public PlantExecutor(IDayNightCycle dayNightCycle, TemplateNameMapper templateNameMapper, NaturalResourceFactory naturalResourceFactory, PlantingService plantingService, BlockValidator blockValidator)
		{
			this._dayNightCycle = dayNightCycle;
			this._templateNameMapper = templateNameMapper;
			this._naturalResourceFactory = naturalResourceFactory;
			this._plantingService = plantingService;
			this._blockValidator = blockValidator;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000030E3 File Offset: 0x000012E3
		public void Awake()
		{
			this._enterer = base.GetComponent<Enterer>();
			this._worker = base.GetComponent<Worker>();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003100 File Offset: 0x00001300
		public bool Launch(Vector3Int coordinates, string resource)
		{
			if (this._enterer.IsInside || !this._worker.Workplace || string.IsNullOrEmpty(resource))
			{
				return false;
			}
			this._coordinates = coordinates;
			this._naturalResource = resource;
			float hours = this._templateNameMapper.GetTemplate(this._naturalResource).GetSpec<PlantableSpec>().PlantTimeInHours / this._worker.WorkingSpeedMultiplier;
			this._finishTimestamp = this._dayNightCycle.DayNumberHoursFromNow(hours);
			this.StartPlanting();
			return true;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003188 File Offset: 0x00001388
		public ExecutorStatus Tick(float deltaTimeInHours)
		{
			if (!this._worker.Workplace || this._naturalResource == null || this._plantingService.GetResourceAt(this._coordinates) != this._naturalResource)
			{
				this.FinishPlanting();
				return ExecutorStatus.Failure;
			}
			if (this._dayNightCycle.PartialDayNumber > this._finishTimestamp)
			{
				this.SpawnResource();
				this.FinishPlanting();
				return ExecutorStatus.Success;
			}
			return ExecutorStatus.Running;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000031F7 File Offset: 0x000013F7
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(PlantExecutor.PlantExecutorKey);
			component.Set(PlantExecutor.CoordinatesKey, this._coordinates);
			component.Set(PlantExecutor.NaturalResourceKey, this._naturalResource);
			component.Set(PlantExecutor.FinishTimestampKey, this._finishTimestamp);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003238 File Offset: 0x00001438
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(PlantExecutor.PlantExecutorKey);
			this._coordinates = component.Get(PlantExecutor.CoordinatesKey);
			this._naturalResource = component.Get(PlantExecutor.NaturalResourceKey);
			this._finishTimestamp = component.Get(PlantExecutor.FinishTimestampKey);
			this.StartPlanting();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000328C File Offset: 0x0000148C
		public void SpawnResource()
		{
			BlockObjectSpec spec = this._templateNameMapper.GetTemplate(this._naturalResource).GetSpec<BlockObjectSpec>();
			if (this._blockValidator.BlocksValid(spec, new Placement(this._coordinates)))
			{
				this._naturalResourceFactory.PlantNew(this._naturalResource, this._coordinates);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000032E0 File Offset: 0x000014E0
		public void StartPlanting()
		{
			this._plantingService.ReservePlantingCoordinates(this._coordinates);
			EventHandler plantingStarted = this.PlantingStarted;
			if (plantingStarted != null)
			{
				plantingStarted(this, EventArgs.Empty);
			}
			this.IsPlanting = true;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003311 File Offset: 0x00001511
		public void FinishPlanting()
		{
			this._plantingService.UnreservePlantingCoordinates(this._coordinates);
			EventHandler plantingFinished = this.PlantingFinished;
			if (plantingFinished != null)
			{
				plantingFinished(this, EventArgs.Empty);
			}
			this.IsPlanting = false;
		}

		// Token: 0x04000037 RID: 55
		public static readonly ComponentKey PlantExecutorKey = new ComponentKey("PlantExecutor");

		// Token: 0x04000038 RID: 56
		public static readonly PropertyKey<string> NaturalResourceKey = new PropertyKey<string>("NaturalResource");

		// Token: 0x04000039 RID: 57
		public static readonly PropertyKey<Vector3Int> CoordinatesKey = new PropertyKey<Vector3Int>("Coordinates");

		// Token: 0x0400003A RID: 58
		public static readonly PropertyKey<float> FinishTimestampKey = new PropertyKey<float>("FinishTimestamp");

		// Token: 0x0400003E RID: 62
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400003F RID: 63
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x04000040 RID: 64
		public readonly NaturalResourceFactory _naturalResourceFactory;

		// Token: 0x04000041 RID: 65
		public readonly PlantingService _plantingService;

		// Token: 0x04000042 RID: 66
		public readonly BlockValidator _blockValidator;

		// Token: 0x04000043 RID: 67
		public Enterer _enterer;

		// Token: 0x04000044 RID: 68
		public Worker _worker;

		// Token: 0x04000045 RID: 69
		public float _finishTimestamp;

		// Token: 0x04000046 RID: 70
		public Vector3Int _coordinates;

		// Token: 0x04000047 RID: 71
		public string _naturalResource;
	}
}
