using System;
using Timberborn.Persistence;

namespace Timberborn.WorldPersistence
{
	// Token: 0x0200000D RID: 13
	public interface IEntitySaver
	{
		// Token: 0x06000025 RID: 37
		IObjectSaver GetComponent(ComponentKey key);

		// Token: 0x06000026 RID: 38
		IObjectSaver GetComponent(ComponentKey key, string suffix);
	}
}
