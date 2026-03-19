using System;
using Timberborn.SingletonSystem;

namespace Timberborn.WorldPersistence
{
	// Token: 0x02000011 RID: 17
	[Singleton]
	public interface ISaveableSingleton
	{
		// Token: 0x0600002D RID: 45
		void Save(ISingletonSaver singletonSaver);
	}
}
