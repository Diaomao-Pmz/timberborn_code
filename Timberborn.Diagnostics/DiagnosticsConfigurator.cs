using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.Diagnostics
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	[Context("MapEditor")]
	public class DiagnosticsConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<MeshMetricsRetriever>().AsSingleton();
			base.Bind<SelectedMeshMetrics>().AsSingleton();
			base.Bind<FramesPerSecondCounter>().AsSingleton();
			base.MultiBind<IDevModule>().To<MeshMetricsDumper>().AsSingleton();
		}
	}
}
