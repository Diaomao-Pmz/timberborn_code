using System;
using Bindito.Core;

namespace Timberborn.RootProviders
{
	// Token: 0x02000005 RID: 5
	[Context("Bootstrapper")]
	public class RootProvidersConfigurator : Configurator
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020C6 File Offset: 0x000002C6
		public override void Configure()
		{
			base.Bind<RootObjectProvider>().AsSingleton().AsExported();
			base.Bind<RootVisualElementProvider>().AsSingleton().AsExported();
		}
	}
}
