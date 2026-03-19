using System;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x0200000A RID: 10
	public class IncrementalStatistic
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002204 File Offset: 0x00000404
		public string Id { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000220C File Offset: 0x0000040C
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002214 File Offset: 0x00000414
		public int Value { get; private set; }

		// Token: 0x06000018 RID: 24 RVA: 0x0000221D File Offset: 0x0000041D
		public IncrementalStatistic(string id, int value)
		{
			this.Id = id;
			this.Value = value;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002234 File Offset: 0x00000434
		public void Increment()
		{
			int value = this.Value;
			this.Value = value + 1;
		}
	}
}
