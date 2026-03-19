using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.DiagnosticsUI
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class DiagnosticsUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public override void Configure()
		{
			base.Bind<MeshMetricsDebuggingPanel>().AsSingleton();
			base.Bind<FramesPerSecondPanel>().AsSingleton();
			base.MultiBind<IDevModule>().To<GCToggler>().AsSingleton();
			base.MultiBind<IDevModule>().To<GCTrigger>().AsSingleton();
			base.MultiBind<IDevModule>().To<EmptySceneLoader>().AsSingleton();
		}
	}
}
