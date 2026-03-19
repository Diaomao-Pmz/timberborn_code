using System;
using Bindito.Core;

namespace Timberborn.ScreenSystem
{
	// Token: 0x0200000C RID: 12
	[Context("Bootstrapper")]
	public class ScreenSystemConfigurator : Configurator
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002ED5 File Offset: 0x000010D5
		public override void Configure()
		{
			base.Bind<ScreenSettings>().AsSingleton().AsExported();
			base.Bind<ScreenSettingsController>().AsSingleton();
			base.Bind<CommandLineScreenSettings>().AsSingleton();
			base.Bind<ScreenSettingsLogger>().AsSingleton();
		}
	}
}
