using System;
using Bindito.Core;
using Timberborn.Multithreading;

namespace Timberborn.MultithreadingAnalysis
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	[Context("MapEditor")]
	public class MultithreadingAnalysisConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020ED File Offset: 0x000002ED
		public override void Configure()
		{
			base.Bind<SnapshotCollector>().AsSingleton();
			base.Bind<ISnapshotCollector>().ToExisting<SnapshotCollector>();
		}
	}
}
