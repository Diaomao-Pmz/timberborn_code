using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x0200009A RID: 154
	public interface IInstanceProviderBank
	{
		// Token: 0x06000180 RID: 384
		bool TryGetInstanceProvider(Type type, out InstanceProvider instanceProvider);

		// Token: 0x06000181 RID: 385
		bool TryGetExportedInstanceProvider(Type type, out InstanceProvider instanceProvider);

		// Token: 0x06000182 RID: 386
		IEnumerable<InstanceProvider> GetInstanceProviders(Type type);

		// Token: 0x06000183 RID: 387
		IEnumerable<InstanceProvider> GetExportedInstanceProviders(Type type);
	}
}
