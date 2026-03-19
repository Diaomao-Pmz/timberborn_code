using System;
using Bindito.Core;

namespace Timberborn.SoundSettingsSystem
{
	// Token: 0x02000006 RID: 6
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class SoundSettingsSystemConfigurator : Configurator
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000251E File Offset: 0x0000071E
		public override void Configure()
		{
			base.Bind<Muter>().AsSingleton();
			base.Bind<SoundSettingsUpdater>().AsSingleton();
			base.Bind<SoundSettings>().AsSingleton();
		}
	}
}
