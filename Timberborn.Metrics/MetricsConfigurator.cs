using System;
using Bindito.Core;

namespace Timberborn.Metrics
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	[Context("MapEditor")]
	public class MetricsConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020C0 File Offset: 0x000002C0
		public override void Configure()
		{
			base.Bind<IMetricsService>().To<MetricsService>().AsSingleton();
			base.Bind<MetricsRepository>().AsSingleton();
			base.Bind<MetricsFormatter>().AsSingleton();
		}
	}
}
