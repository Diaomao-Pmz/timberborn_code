using System;
using Timberborn.SingletonSystem;

namespace Timberborn.TickSystem
{
	// Token: 0x0200000A RID: 10
	[Singleton]
	public interface ITickableSingleton
	{
		// Token: 0x0600000E RID: 14
		void Tick();
	}
}
