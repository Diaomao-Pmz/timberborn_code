using System;

namespace Timberborn.BehaviorSystem
{
	// Token: 0x0200000C RID: 12
	public struct ExecutorInfo
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002739 File Offset: 0x00000939
		public readonly string Name { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002741 File Offset: 0x00000941
		public readonly float ElapsedTime { get; }

		// Token: 0x06000032 RID: 50 RVA: 0x00002749 File Offset: 0x00000949
		public ExecutorInfo(string name, float elapsedTime)
		{
			this.Name = name;
			this.ElapsedTime = elapsedTime;
		}
	}
}
