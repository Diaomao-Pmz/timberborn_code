using System;

namespace Timberborn.SingletonSystem
{
	// Token: 0x0200000C RID: 12
	[Singleton]
	public interface INonSingletonPostLoader
	{
		// Token: 0x0600001A RID: 26
		void PostLoadNonSingletons();
	}
}
