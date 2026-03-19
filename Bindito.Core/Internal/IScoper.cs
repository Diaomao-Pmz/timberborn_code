using System;

namespace Bindito.Core.Internal
{
	// Token: 0x020000AA RID: 170
	public interface IScoper
	{
		// Token: 0x060001BD RID: 445
		Func<object> PlaceInScope(Func<object> provider, Scope scope);
	}
}
