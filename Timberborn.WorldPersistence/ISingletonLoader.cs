using System;
using Timberborn.Persistence;

namespace Timberborn.WorldPersistence
{
	// Token: 0x02000013 RID: 19
	public interface ISingletonLoader
	{
		// Token: 0x0600002F RID: 47
		IObjectLoader GetSingleton(SingletonKey key);

		// Token: 0x06000030 RID: 48
		bool TryGetSingleton(SingletonKey key, out IObjectLoader objectLoader);
	}
}
