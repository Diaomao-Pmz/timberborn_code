using System;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x02000008 RID: 8
	public class ColumnCountTracker
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002186 File Offset: 0x00000386
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000218E File Offset: 0x0000038E
		public int MaxCount { get; private set; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002198 File Offset: 0x00000398
		public void Update(int maxIndex)
		{
			int lastMaxCount = this._lastMaxCount;
			this._lastMaxCount = maxIndex;
			this.MaxCount = Math.Max(lastMaxCount, this._lastMaxCount);
		}

		// Token: 0x0400000B RID: 11
		public int _lastMaxCount;
	}
}
