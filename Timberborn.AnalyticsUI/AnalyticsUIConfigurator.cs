using System;
using Bindito.Core;

namespace Timberborn.AnalyticsUI
{
	// Token: 0x02000005 RID: 5
	[Context("MainMenu")]
	public class AnalyticsUIConfigurator : Configurator
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002203 File Offset: 0x00000403
		public override void Configure()
		{
			base.Bind<AnalyticsConsentBox>().AsSingleton();
		}
	}
}
