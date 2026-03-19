using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.TooltipSystem
{
	// Token: 0x02000008 RID: 8
	public class TooltipBlocker
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025E4 File Offset: 0x000007E4
		public bool IsUnblocked
		{
			get
			{
				return this._blockers.IsEmpty<object>();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025F1 File Offset: 0x000007F1
		public void AddBlocker(object blocker)
		{
			this._blockers.Add(blocker);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002600 File Offset: 0x00000800
		public void RemoveBlocker(object blocker)
		{
			this._blockers.Remove(blocker);
		}

		// Token: 0x0400001A RID: 26
		public readonly HashSet<object> _blockers = new HashSet<object>();
	}
}
