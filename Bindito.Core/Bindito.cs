using System;
using System.Collections.Generic;
using System.Linq;
using Bindito.Core.Internal;

namespace Bindito.Core
{
	// Token: 0x0200006C RID: 108
	public static class Bindito
	{
		// Token: 0x060000DC RID: 220 RVA: 0x0000291E File Offset: 0x00000B1E
		public static IContainer CreateContainer(params IConfigurator[] configurators)
		{
			return Bindito.CreateContainer(configurators.AsEnumerable<IConfigurator>());
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000292B File Offset: 0x00000B2B
		public static IContainer CreateContainer(IEnumerable<IConfigurator> configurators)
		{
			return new ContainerCreator().CreateContainer(configurators);
		}
	}
}
