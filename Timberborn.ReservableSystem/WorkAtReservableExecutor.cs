using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.ReservableSystem
{
	// Token: 0x02000008 RID: 8
	public abstract class WorkAtReservableExecutor : BaseComponent, IAwakableComponent, IPostInitializableEntity, IExecutor
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000018 RID: 24 RVA: 0x00002270 File Offset: 0x00000470
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x000022A8 File Offset: 0x000004A8
		public event EventHandler WorkStarted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001A RID: 26 RVA: 0x000022E0 File Offset: 0x000004E0
		// (remove) Token: 0x0600001B RID: 27 RVA: 0x00002318 File Offset: 0x00000518
		public event EventHandler<WorkFinishedEventArgs> WorkFinished;

		// Token: 0x0600001C RID: 28 RVA: 0x0000234D File Offset: 0x0000054D
		public WorkAtReservableExecutor(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000235C File Offset: 0x0000055C
		public void Awake()
		{
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			this._worker = base.GetComponent<Worker>();
			this.Initialize();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000237C File Offset: 0x0000057C
		public void PostInitializeEntity()
		{
			if (this._startWorkingPostLoad)
			{
				if (this.Reservable != null)
				{
					this.StartWorking();
				}
				this._startWorkingPostLoad = false;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000239B File Offset: 0x0000059B
		public bool IsLoadedLegacyExecutor()
		{
			return this._startWorkingPostLoad && this._isLegacyExecutor;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023B0 File Offset: 0x000005B0
		public ExecutorStatus Tick(float deltaTimeInHours)
		{
			if (this.Reservable == null)
			{
				return this.Stop();
			}
			if (this._dayNightCycle.PartialDayNumber < this._finishTimestamp)
			{
				this.PerformActionOnTick(this._dayNightCycle.FixedDeltaTimeInHours);
				return ExecutorStatus.Running;
			}
			if (!this.Reservable.IsDeleted && this.CanComplete())
			{
				return this.Complete();
			}
			return this.Stop();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002414 File Offset: 0x00000614
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(WorkAtReservableExecutor.WorkAtReservableExecutorKey);
			component.Set(WorkAtReservableExecutor.FinishTimestampKey, this._finishTimestamp);
			component.Set(WorkAtReservableExecutor.IsLegacyExecutorKey, false);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002440 File Offset: 0x00000640
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(WorkAtReservableExecutor.WorkAtReservableExecutorKey);
			this._finishTimestamp = component.Get(WorkAtReservableExecutor.FinishTimestampKey);
			this._isLegacyExecutor = !component.Has<bool>(WorkAtReservableExecutor.IsLegacyExecutorKey);
			this._startWorkingPostLoad = true;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35
		public abstract string Animation { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36
		public abstract Reservable Reservable { get; }

		// Token: 0x06000025 RID: 37
		public abstract void Initialize();

		// Token: 0x06000026 RID: 38
		public abstract bool CanComplete();

		// Token: 0x06000027 RID: 39
		public abstract void PerformActionOnTick(float deltaTime);

		// Token: 0x06000028 RID: 40
		public abstract void PerformActionOnComplete();

		// Token: 0x06000029 RID: 41
		public abstract void Unreserve();

		// Token: 0x0600002A RID: 42 RVA: 0x00002485 File Offset: 0x00000685
		public void Launch(float unscaledTimeInHours)
		{
			this._finishTimestamp = this.CalculateFinishTimestamp(unscaledTimeInHours);
			this.StartWorking();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000249A File Offset: 0x0000069A
		public void StartWorking()
		{
			this.TurnOnAnimation();
			EventHandler workStarted = this.WorkStarted;
			if (workStarted == null)
			{
				return;
			}
			workStarted(this, EventArgs.Empty);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024B8 File Offset: 0x000006B8
		public ExecutorStatus Stop()
		{
			EventHandler<WorkFinishedEventArgs> workFinished = this.WorkFinished;
			if (workFinished != null)
			{
				workFinished(this, new WorkFinishedEventArgs(false));
			}
			this.TurnOffAnimation();
			this.Unreserve();
			return ExecutorStatus.Failure;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024DF File Offset: 0x000006DF
		public ExecutorStatus Complete()
		{
			EventHandler<WorkFinishedEventArgs> workFinished = this.WorkFinished;
			if (workFinished != null)
			{
				workFinished(this, new WorkFinishedEventArgs(true));
			}
			this.TurnOffAnimation();
			this.PerformActionOnComplete();
			return ExecutorStatus.Success;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002506 File Offset: 0x00000706
		public void TurnOnAnimation()
		{
			this._animationTurnedOn = this.Animation;
			this._characterAnimator.SetBool(this._animationTurnedOn, true);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002526 File Offset: 0x00000726
		public void TurnOffAnimation()
		{
			if (!string.IsNullOrEmpty(this._animationTurnedOn))
			{
				this._characterAnimator.SetBool(this._animationTurnedOn, false);
				this._animationTurnedOn = null;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002550 File Offset: 0x00000750
		public float CalculateFinishTimestamp(float unscaledTimeInHours)
		{
			float hours = unscaledTimeInHours / this._worker.WorkingSpeedMultiplier;
			return this._dayNightCycle.DayNumberHoursFromNow(hours);
		}

		// Token: 0x0400000E RID: 14
		public static readonly ComponentKey WorkAtReservableExecutorKey = new ComponentKey("WorkAtReservableExecutor");

		// Token: 0x0400000F RID: 15
		public static readonly PropertyKey<float> FinishTimestampKey = new PropertyKey<float>("FinishTimestamp");

		// Token: 0x04000010 RID: 16
		public static readonly PropertyKey<bool> IsLegacyExecutorKey = new PropertyKey<bool>("IsLegacyExecutor");

		// Token: 0x04000013 RID: 19
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000014 RID: 20
		public CharacterAnimator _characterAnimator;

		// Token: 0x04000015 RID: 21
		public Worker _worker;

		// Token: 0x04000016 RID: 22
		public float _finishTimestamp;

		// Token: 0x04000017 RID: 23
		public bool _startWorkingPostLoad;

		// Token: 0x04000018 RID: 24
		public bool _isLegacyExecutor;

		// Token: 0x04000019 RID: 25
		public string _animationTurnedOn;
	}
}
