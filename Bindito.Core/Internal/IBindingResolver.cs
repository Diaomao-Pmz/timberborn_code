using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x02000091 RID: 145
	public interface IBindingResolver
	{
		// Token: 0x06000171 RID: 369
		bool ResolveBindings(Type type, out IEnumerable<Binding> ownBindings);
	}
}
