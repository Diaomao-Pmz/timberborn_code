using System;
using Bindito.Core;

namespace Timberborn.QuickNotificationSystem
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	[Context("MapEditor")]
	public class QuickNotificationSystemConfigurator : Configurator
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002555 File Offset: 0x00000755
		public override void Configure()
		{
			base.Bind<QuickNotificationService>().AsSingleton();
			base.Bind<QuickNotificationPanel>().AsSingleton();
		}
	}
}
