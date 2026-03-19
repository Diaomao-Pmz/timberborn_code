using System;
using Timberborn.Persistence;

namespace Timberborn.WorldPersistence
{
	// Token: 0x0200000C RID: 12
	public interface IEntityLoader
	{
		// Token: 0x06000021 RID: 33
		IObjectLoader GetComponent(ComponentKey key);

		// Token: 0x06000022 RID: 34
		IObjectLoader GetComponent(ComponentKey key, string suffix);

		// Token: 0x06000023 RID: 35
		bool TryGetComponent(ComponentKey key, out IObjectLoader objectLoader);

		// Token: 0x06000024 RID: 36
		bool TryGetComponent(ComponentKey key, string suffix, out IObjectLoader objectLoader);
	}
}
