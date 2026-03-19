using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x02000096 RID: 150
	public interface IDependencyRetriever
	{
		// Token: 0x06000177 RID: 375
		IEnumerable<Type> GetDependencies(ProvisionBinding provisionBinding);
	}
}
