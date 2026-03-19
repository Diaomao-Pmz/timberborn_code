using System;
using Bindito.Core;

namespace Timberborn.SettingsSystem
{
	// Token: 0x02000007 RID: 7
	[Context("Bootstrapper")]
	public class SettingsSystemConfigurator : Configurator
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000022AE File Offset: 0x000004AE
		public override void Configure()
		{
			base.Bind<ISettings>().To<Settings>().AsSingleton().AsExported();
		}
	}
}
