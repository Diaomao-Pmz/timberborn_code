using System;
using Bindito.Core;

namespace Timberborn.TutorialSettingsSystem
{
	// Token: 0x02000005 RID: 5
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class TutorialSettingsSystemConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002185 File Offset: 0x00000385
		public override void Configure()
		{
			base.Bind<TutorialSettings>().AsSingleton();
		}
	}
}
