using System;
using Bindito.Core;

namespace Timberborn.IntroSettingsSystem
{
	// Token: 0x02000005 RID: 5
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class IntroSettingsConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FF File Offset: 0x000002FF
		public override void Configure()
		{
			base.Bind<IntroSettings>().AsSingleton();
			base.Bind<IntroSettingsController>().AsSingleton();
		}
	}
}
