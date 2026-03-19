using System;
using System.Linq;
using System.Threading;

namespace Timberborn.MultithreadingAnalysis
{
	// Token: 0x02000008 RID: 8
	public readonly struct TaskSample
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002337 File Offset: 0x00000537
		public int Run { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000233F File Offset: 0x0000053F
		public int TotalRuns { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002347 File Offset: 0x00000547
		public long StartTime { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000234F File Offset: 0x0000054F
		public long EndTime { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002357 File Offset: 0x00000557
		public Thread Thread { get; }

		// Token: 0x0600001B RID: 27 RVA: 0x0000235F File Offset: 0x0000055F
		public TaskSample(int run, int totalRuns, long startTime, long endTime, Thread thread, Type type)
		{
			this.Run = run;
			this.TotalRuns = totalRuns;
			this.StartTime = startTime;
			this.EndTime = endTime;
			this.Thread = thread;
			this._type = type;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000238E File Offset: 0x0000058E
		public Type GenericType
		{
			get
			{
				return this._type.GenericTypeArguments.First<Type>();
			}
		}

		// Token: 0x04000019 RID: 25
		public readonly Type _type;
	}
}
