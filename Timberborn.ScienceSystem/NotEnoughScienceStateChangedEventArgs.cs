using System;

namespace Timberborn.ScienceSystem
{
	// Token: 0x0200000A RID: 10
	public class NotEnoughScienceStateChangedEventArgs
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023F9 File Offset: 0x000005F9
		public bool NewState { get; }

		// Token: 0x06000017 RID: 23 RVA: 0x00002401 File Offset: 0x00000601
		public NotEnoughScienceStateChangedEventArgs(bool newState)
		{
			this.NewState = newState;
		}
	}
}
