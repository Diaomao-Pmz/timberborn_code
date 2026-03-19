using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x02000098 RID: 152
	public interface IInstanceBank
	{
		// Token: 0x0600017B RID: 379
		bool TryGetInstance(Type type, out object instance);

		// Token: 0x0600017C RID: 380
		bool TryGetExportedInstance(Type type, out object instance);

		// Token: 0x0600017D RID: 381
		IEnumerable<object> GetInstances(Type type);

		// Token: 0x0600017E RID: 382
		IEnumerable<object> GetExportedInstances(Type type);
	}
}
