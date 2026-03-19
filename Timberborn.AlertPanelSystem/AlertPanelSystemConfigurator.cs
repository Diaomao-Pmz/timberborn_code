using System;
using Bindito.Core;

namespace Timberborn.AlertPanelSystem
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class AlertPanelSystemConfigurator : Configurator
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002490 File Offset: 0x00000690
		public override void Configure()
		{
			base.Bind<AlertPanel>().AsSingleton();
			base.Bind<AlertPanelRowFactory>().AsSingleton();
		}
	}
}
