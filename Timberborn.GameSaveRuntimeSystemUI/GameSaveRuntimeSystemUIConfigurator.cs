using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.GameSaveRuntimeSystemUI
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class GameSaveRuntimeSystemUIConfigurator : Configurator
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000219E File Offset: 0x0000039E
		public override void Configure()
		{
			base.Bind<SaveGameBox>().AsSingleton();
			base.Bind<SaveNameProvider>().AsSingleton();
			base.MultiBind<IDevModule>().To<GameSaverDevModule>().AsSingleton();
		}
	}
}
