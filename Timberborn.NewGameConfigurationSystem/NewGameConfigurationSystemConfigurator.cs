using System;
using Bindito.Core;

namespace Timberborn.NewGameConfigurationSystem
{
	// Token: 0x0200000A RID: 10
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class NewGameConfigurationSystemConfigurator : Configurator
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002F1F File Offset: 0x0000111F
		public override void Configure()
		{
			base.Bind<GameModeSpecService>().AsSingleton();
		}
	}
}
