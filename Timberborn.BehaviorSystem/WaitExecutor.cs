using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.BehaviorSystem
{
	// Token: 0x02000010 RID: 16
	public class WaitExecutor : BaseComponent, IExecutor
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002761 File Offset: 0x00000961
		public WaitExecutor(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002770 File Offset: 0x00000970
		public void LaunchForIdleTime()
		{
			this.LaunchForSpecifiedTime(WaitExecutor.IdleWaitTimeInHours);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002780 File Offset: 0x00000980
		public void LaunchForSpecifiedTime(float hours)
		{
			float finishTimestamp = this._dayNightCycle.DayNumberHoursFromNow(hours);
			this._finishTimestamp = finishTimestamp;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027A1 File Offset: 0x000009A1
		public ExecutorStatus Tick(float deltaTimeInHours)
		{
			if (this._dayNightCycle.PartialDayNumber > this._finishTimestamp)
			{
				return ExecutorStatus.Success;
			}
			return ExecutorStatus.Running;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027B9 File Offset: 0x000009B9
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(WaitExecutor.WaitExecutorKey).Set(WaitExecutor.FinishTimestampKey, this._finishTimestamp);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027D8 File Offset: 0x000009D8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(WaitExecutor.WaitExecutorKey);
			this._finishTimestamp = component.Get(WaitExecutor.FinishTimestampKey);
		}

		// Token: 0x04000024 RID: 36
		public static readonly ComponentKey WaitExecutorKey = new ComponentKey("WaitExecutor");

		// Token: 0x04000025 RID: 37
		public static readonly PropertyKey<float> FinishTimestampKey = new PropertyKey<float>("FinishTimestamp");

		// Token: 0x04000026 RID: 38
		public static readonly float IdleWaitTimeInHours = 0.15f;

		// Token: 0x04000027 RID: 39
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000028 RID: 40
		public float _finishTimestamp;
	}
}
