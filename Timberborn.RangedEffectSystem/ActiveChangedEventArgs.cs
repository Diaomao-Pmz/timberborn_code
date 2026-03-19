using System;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x02000007 RID: 7
	public readonly struct ActiveChangedEventArgs
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public bool State { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public ActiveChangedEventArgs(bool state)
		{
			this.State = state;
		}
	}
}
