using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bindito.Core.Internal
{
	// Token: 0x0200009E RID: 158
	public interface IMethodRetriever
	{
		// Token: 0x06000187 RID: 391
		IEnumerable<MethodInfo> GetInjectedMethods(Type type);
	}
}
