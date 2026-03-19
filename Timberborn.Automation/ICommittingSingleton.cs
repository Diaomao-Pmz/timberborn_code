using System;
using Timberborn.SingletonSystem;

namespace Timberborn.Automation
{
	// Token: 0x0200001D RID: 29
	[Singleton]
	public interface ICommittingSingleton
	{
		// Token: 0x060000D2 RID: 210
		void CommitTick();
	}
}
