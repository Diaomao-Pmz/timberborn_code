using System;

namespace Timberborn.TickSystem
{
	// Token: 0x0200000C RID: 12
	public interface ITickingMode
	{
		// Token: 0x06000016 RID: 22
		bool SingletonIsActiveInThisMode(object singleton);
	}
}
