using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BlockingSystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Workshops
{
	// Token: 0x02000029 RID: 41
	public class WorkExecutor : BaseComponent, IAwakableComponent, IStartableComponent, IExecutor
	{
		// Token: 0x06000142 RID: 322 RVA: 0x000052F6 File Offset: 0x000034F6
		public WorkExecutor(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005305 File Offset: 0x00003505
		public void Awake()
		{
			this._worker = base.GetComponent<Worker>();
			this._worker.GotUnemployed += this.OnGotUnemployed;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000532A File Offset: 0x0000352A
		public void Start()
		{
			if (this._workshop)
			{
				this.StartWorking();
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000533F File Offset: 0x0000353F
		public void Launch(float maxWorkingTimeInHours)
		{
			this.Initialize();
			if (this._workshop && this._blockableObject.IsUnblocked)
			{
				this._finishTimestamp = this._dayNightCycle.DayNumberHoursFromNow(maxWorkingTimeInHours);
				this.StartWorking();
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000537C File Offset: 0x0000357C
		public ExecutorStatus Tick(float deltaTimeInHours)
		{
			if (!this._workshop || !this._isWorking)
			{
				return ExecutorStatus.Failure;
			}
			if (!this._blockableObject.IsUnblocked)
			{
				this.StopWorking();
				return ExecutorStatus.Failure;
			}
			if (this._dayNightCycle.PartialDayNumber > this._finishTimestamp)
			{
				this.StopWorking();
				return ExecutorStatus.Success;
			}
			return ExecutorStatus.Running;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000053D1 File Offset: 0x000035D1
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(WorkExecutor.WorkExecutorKey).Set(WorkExecutor.FinishTimestampKey, this._finishTimestamp);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000053F0 File Offset: 0x000035F0
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(WorkExecutor.WorkExecutorKey);
			this._finishTimestamp = component.Get(WorkExecutor.FinishTimestampKey);
			this.Initialize();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005420 File Offset: 0x00003620
		public void Initialize()
		{
			Workplace workplace = this._worker.Workplace;
			if (workplace)
			{
				this._workshop = workplace.GetComponent<Workshop>();
				this._blockableObject = workplace.GetComponent<BlockableObject>();
				return;
			}
			this.Clear();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005460 File Offset: 0x00003660
		public void StartWorking()
		{
			this._workshop.InformOfStartedWorking();
			this._isWorking = true;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005474 File Offset: 0x00003674
		public void StopWorking()
		{
			this._workshop.InformOfStoppedWorking();
			this._isWorking = false;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005488 File Offset: 0x00003688
		public void OnGotUnemployed(object sender, EventArgs e)
		{
			if (this._workshop && this._isWorking)
			{
				this.StopWorking();
				this.Clear();
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000054AB File Offset: 0x000036AB
		public void Clear()
		{
			this._workshop = null;
			this._blockableObject = null;
		}

		// Token: 0x0400008D RID: 141
		public static readonly ComponentKey WorkExecutorKey = new ComponentKey("WorkExecutor");

		// Token: 0x0400008E RID: 142
		public static readonly PropertyKey<float> FinishTimestampKey = new PropertyKey<float>("FinishTimestamp");

		// Token: 0x0400008F RID: 143
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000090 RID: 144
		public Worker _worker;

		// Token: 0x04000091 RID: 145
		public Workshop _workshop;

		// Token: 0x04000092 RID: 146
		public BlockableObject _blockableObject;

		// Token: 0x04000093 RID: 147
		public float _finishTimestamp;

		// Token: 0x04000094 RID: 148
		public bool _isWorking;
	}
}
