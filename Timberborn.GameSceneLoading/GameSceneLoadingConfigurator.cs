using System;
using Bindito.Core;

namespace Timberborn.GameSceneLoading
{
	// Token: 0x02000008 RID: 8
	[Context("MainMenu")]
	[Context("Game")]
	public class GameSceneLoadingConfigurator : Configurator
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002228 File Offset: 0x00000428
		public override void Configure()
		{
			base.Bind<GameSceneLoader>().AsTransient();
		}
	}
}
