using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.MultithreadingAnalysisUI
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class MultithreadingConfigurator : Configurator
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021D0 File Offset: 0x000003D0
		public override void Configure()
		{
			base.Bind<TaskSnapshotPanel>().AsSingleton();
			base.Bind<ThreadViewFactory>().AsSingleton();
			base.Bind<TaskViewFactory>().AsSingleton();
			base.Bind<MarkerViewFactory>().AsSingleton();
			base.Bind<TaskColorProvider>().AsSingleton();
			base.Bind<SnapshotTimeline>().AsSingleton();
			base.MultiBind<IDevModule>().To<TaskSnapshotDevModule>().AsSingleton();
		}
	}
}
