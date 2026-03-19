using System;

namespace Bindito.Core.Internal
{
	// Token: 0x0200009F RID: 159
	public interface IMultiBindingService
	{
		// Token: 0x06000188 RID: 392
		bool IsMultiBound(Type parameterType, out Type multiBoundType);
	}
}
