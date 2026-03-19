using System;
using Bindito.Core;

namespace Timberborn.PlatformUtilities
{
	// Token: 0x02000008 RID: 8
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class PlatformUtilitiesConfigurator : Configurator
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021F8 File Offset: 0x000003F8
		public override void Configure()
		{
			base.Bind<IExplorerOpener>().To<ExplorerOpener>().AsSingleton();
		}
	}
}
