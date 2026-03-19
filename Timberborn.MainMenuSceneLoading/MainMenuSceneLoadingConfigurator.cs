using System;
using Bindito.Core;

namespace Timberborn.MainMenuSceneLoading
{
	// Token: 0x02000005 RID: 5
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class MainMenuSceneLoadingConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002150 File Offset: 0x00000350
		public override void Configure()
		{
			base.Bind<MainMenuSceneLoader>().AsSingleton();
		}
	}
}
