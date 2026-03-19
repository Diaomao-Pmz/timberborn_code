using System;

namespace Bindito.Core.Internal
{
	// Token: 0x0200009C RID: 156
	public interface IInstanceProviderFuncFactory
	{
		// Token: 0x06000185 RID: 389
		Func<object> CreateInstanceProviderFunc(ProvisionBinding provisionBinding);
	}
}
