using System;
using Bindito.Core;

namespace Timberborn.SteamOverlaySystem
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	[Context("MainMenu")]
	[Context("MapEditor")]
	public class SteamOverlaySystemConfigurator : Configurator
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000021DC File Offset: 0x000003DC
		public override void Configure()
		{
			base.Bind<SteamOverlayInputBlocker>().AsSingleton();
			base.Bind<SteamOverlayOpener>().AsSingleton();
		}
	}
}
