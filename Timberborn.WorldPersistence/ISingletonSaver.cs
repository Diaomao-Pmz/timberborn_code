using System;
using Timberborn.Persistence;

namespace Timberborn.WorldPersistence
{
	// Token: 0x02000014 RID: 20
	public interface ISingletonSaver
	{
		// Token: 0x06000031 RID: 49
		IObjectSaver GetSingleton(SingletonKey key);
	}
}
