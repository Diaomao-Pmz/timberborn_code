using System;
using Bindito.Core;

namespace Timberborn.WebNavigation
{
	// Token: 0x02000005 RID: 5
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class WebNavigationConfigurator : Configurator
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002189 File Offset: 0x00000389
		public override void Configure()
		{
			base.Bind<UrlOpener>().AsSingleton();
		}
	}
}
