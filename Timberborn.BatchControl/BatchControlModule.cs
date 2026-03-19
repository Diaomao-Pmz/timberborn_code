using System;
using System.Collections.Frozen;
using System.Collections.Generic;

namespace Timberborn.BatchControl
{
	// Token: 0x0200000F RID: 15
	public class BatchControlModule
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public FrozenDictionary<int, BatchControlTab> Tabs { get; }

		// Token: 0x06000046 RID: 70 RVA: 0x00002CEC File Offset: 0x00000EEC
		public BatchControlModule(Dictionary<int, BatchControlTab> tabs)
		{
			this.Tabs = tabs.ToFrozenDictionary(null);
		}

		// Token: 0x02000010 RID: 16
		public class Builder
		{
			// Token: 0x06000047 RID: 71 RVA: 0x00002D01 File Offset: 0x00000F01
			public void AddTab(BatchControlTab tab, int order)
			{
				this._tabs.Add(order, tab);
			}

			// Token: 0x06000048 RID: 72 RVA: 0x00002D10 File Offset: 0x00000F10
			public BatchControlModule Build()
			{
				return new BatchControlModule(this._tabs);
			}

			// Token: 0x04000038 RID: 56
			public readonly Dictionary<int, BatchControlTab> _tabs = new Dictionary<int, BatchControlTab>();
		}
	}
}
