using System;
using Bindito.Core;

namespace Timberborn.GameWonderCompletion
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class GameWonderCompletionConfigurator : Configurator
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002438 File Offset: 0x00000638
		public override void Configure()
		{
			base.Bind<MapNameService>().AsSingleton();
			base.Bind<GameWonderCompletionService>().AsSingleton();
			base.Bind<WonderCompletionCountdownStarter>().AsSingleton();
			base.Bind<GameWonderCompletionRestorer>().AsSingleton();
		}
	}
}
