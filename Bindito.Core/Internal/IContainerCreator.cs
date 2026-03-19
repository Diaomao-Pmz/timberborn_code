using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x02000095 RID: 149
	public interface IContainerCreator
	{
		// Token: 0x06000175 RID: 373
		IContainer CreateContainer(IEnumerable<IConfigurator> configurators);

		// Token: 0x06000176 RID: 374
		IContainer CreateChildContainer(IEnumerable<IConfigurator> configurators);
	}
}
