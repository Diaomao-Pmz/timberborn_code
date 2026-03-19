using System;
using System.Threading;
using Timberborn.SingletonSystem;

namespace Timberborn.Multithreading
{
	// Token: 0x0200000C RID: 12
	public class MainThreadPrioritySetter : ILoadableSingleton
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000233F File Offset: 0x0000053F
		public void Load()
		{
			Thread.CurrentThread.Priority = ThreadPriority.Highest;
		}
	}
}
