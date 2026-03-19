using System;
using Timberborn.Debugging;
using Timberborn.MultithreadingAnalysis;
using Timberborn.QuickNotificationSystem;

namespace Timberborn.MultithreadingAnalysisUI
{
	// Token: 0x0200000C RID: 12
	public class TaskSnapshotDevModule : IDevModule
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002B72 File Offset: 0x00000D72
		public TaskSnapshotDevModule(SnapshotCollector snapshotCollector, QuickNotificationService quickNotificationService)
		{
			this._snapshotCollector = snapshotCollector;
			this._quickNotificationService = quickNotificationService;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B88 File Offset: 0x00000D88
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Multithreading: Snapshot (1 tick)", delegate
			{
				this.ScheduleSnapshot(1);
			})).AddMethod(DevMethod.Create("Multithreading: Snapshot (2 ticks)", delegate
			{
				this.ScheduleSnapshot(2);
			})).AddMethod(DevMethod.Create("Multithreading: Snapshot (3 ticks)", delegate
			{
				this.ScheduleSnapshot(3);
			})).Build();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public void ScheduleSnapshot(int ticks)
		{
			this._quickNotificationService.SendNotification("Collecting snapshot...");
			this._snapshotCollector.ScheduleCollection(ticks);
		}

		// Token: 0x0400001F RID: 31
		public readonly SnapshotCollector _snapshotCollector;

		// Token: 0x04000020 RID: 32
		public readonly QuickNotificationService _quickNotificationService;
	}
}
