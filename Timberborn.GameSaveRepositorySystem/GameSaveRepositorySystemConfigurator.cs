using System;
using Bindito.Core;

namespace Timberborn.GameSaveRepositorySystem
{
	// Token: 0x0200000B RID: 11
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class GameSaveRepositorySystemConfigurator : Configurator
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000287A File Offset: 0x00000A7A
		public override void Configure()
		{
			base.Bind<GameSaveRepository>().AsSingleton();
			base.Bind<GameSaveDeserializer>().AsSingleton();
		}
	}
}
