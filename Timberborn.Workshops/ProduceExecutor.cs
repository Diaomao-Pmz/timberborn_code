using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BlockingSystem;
using Timberborn.MechanicalSystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Workshops
{
	// Token: 0x0200001C RID: 28
	public class ProduceExecutor : BaseComponent, IAwakableComponent, IStartableComponent, IExecutor
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00003B56 File Offset: 0x00001D56
		public ProduceExecutor(IDayNightCycle dayNightCycle, ReferenceSerializer referenceSerializer)
		{
			this._dayNightCycle = dayNightCycle;
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003B6C File Offset: 0x00001D6C
		public void Awake()
		{
			this._worker = base.GetComponent<Worker>();
			this._worker.GotUnemployed += this.OnGotUnemployed;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003B91 File Offset: 0x00001D91
		public void Start()
		{
			if (this._workshop)
			{
				this.StartProducing();
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public bool Launch(float maxProductionTimeInHours)
		{
			this.Initialize(this._worker.Workplace);
			if (!this._workplace || !this._blockableObject.IsUnblocked)
			{
				return false;
			}
			if (!this._manufactory || !this._manufactory.IsReadyToProduce)
			{
				return false;
			}
			if (this._mechanicalBuilding && !this._mechanicalBuilding.ActiveAndPowered)
			{
				return false;
			}
			this._finishTimestamp = this._dayNightCycle.DayNumberHoursFromNow(maxProductionTimeInHours);
			this.StartProducing();
			return true;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003C34 File Offset: 0x00001E34
		public ExecutorStatus Tick(float deltaTimeInHours)
		{
			if (!this._workplace || !this._isProducing)
			{
				return ExecutorStatus.Failure;
			}
			if (!this._blockableObject.IsUnblocked)
			{
				this.StopProducing();
				return ExecutorStatus.Failure;
			}
			if (this._dayNightCycle.PartialDayNumber > this._finishTimestamp || !this._manufactory.IsReadyToProduce)
			{
				this.StopProducing();
				return ExecutorStatus.Success;
			}
			this._manufactory.IncreaseProductionProgress(deltaTimeInHours * this._worker.WorkingSpeedMultiplier);
			return ExecutorStatus.Running;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(ProduceExecutor.ProduceExecutorKey);
			component.Set(ProduceExecutor.FinishTimestampKey, this._finishTimestamp);
			if (this._workplace)
			{
				component.Set<Workplace>(ProduceExecutor.WorkplaceKey, this._workplace, this._referenceSerializer.Of<Workplace>());
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003D04 File Offset: 0x00001F04
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(ProduceExecutor.ProduceExecutorKey);
			this._finishTimestamp = component.Get(ProduceExecutor.FinishTimestampKey);
			Workplace workplace = component.Has<Workplace>(ProduceExecutor.WorkplaceKey) ? component.Get<Workplace>(ProduceExecutor.WorkplaceKey, this._referenceSerializer.Of<Workplace>()) : this._worker.Workplace;
			this.Initialize(workplace);
			this.VerifyLoadedWorkplace();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003D6C File Offset: 0x00001F6C
		public void Initialize(Workplace workplace)
		{
			if (workplace)
			{
				this._workplace = workplace;
				this._workshop = this._workplace.GetComponent<Workshop>();
				this._manufactory = this._workplace.GetComponent<Manufactory>();
				this._blockableObject = this._workplace.GetComponent<BlockableObject>();
				this._mechanicalBuilding = this._workplace.GetComponent<MechanicalBuilding>();
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003DCC File Offset: 0x00001FCC
		public void StartProducing()
		{
			this._workshop.InformOfStartedWorking();
			this._isProducing = true;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public void StopProducing()
		{
			this._workshop.InformOfStoppedWorking();
			this._isProducing = false;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public void VerifyLoadedWorkplace()
		{
			if (!this._workplace || !this._workshop || !this._manufactory)
			{
				this.Clear();
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003E23 File Offset: 0x00002023
		public void OnGotUnemployed(object sender, EventArgs e)
		{
			if (this._workshop && this._isProducing)
			{
				this.StopProducing();
				this.Clear();
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003E46 File Offset: 0x00002046
		public void Clear()
		{
			this._workplace = null;
			this._workshop = null;
			this._manufactory = null;
			this._blockableObject = null;
			this._mechanicalBuilding = null;
		}

		// Token: 0x04000055 RID: 85
		public static readonly ComponentKey ProduceExecutorKey = new ComponentKey("ProduceExecutor");

		// Token: 0x04000056 RID: 86
		public static readonly PropertyKey<float> FinishTimestampKey = new PropertyKey<float>("FinishTimestamp");

		// Token: 0x04000057 RID: 87
		public static readonly PropertyKey<Workplace> WorkplaceKey = new PropertyKey<Workplace>("Workplace");

		// Token: 0x04000058 RID: 88
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000059 RID: 89
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x0400005A RID: 90
		public Worker _worker;

		// Token: 0x0400005B RID: 91
		public Workshop _workshop;

		// Token: 0x0400005C RID: 92
		public MechanicalBuilding _mechanicalBuilding;

		// Token: 0x0400005D RID: 93
		public Workplace _workplace;

		// Token: 0x0400005E RID: 94
		public Manufactory _manufactory;

		// Token: 0x0400005F RID: 95
		public BlockableObject _blockableObject;

		// Token: 0x04000060 RID: 96
		public float _finishTimestamp;

		// Token: 0x04000061 RID: 97
		public bool _isProducing;
	}
}
