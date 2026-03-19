using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class DebuggingUIConfigurator : Configurator
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002450 File Offset: 0x00000650
		public override void Configure()
		{
			base.Bind<DebugPanelMover>().AsTransient();
			base.Bind<DevPanel>().AsSingleton();
			base.Bind<DebuggingPanel>().AsSingleton();
			base.Bind<ObjectDebuggingPanel>().AsSingleton();
			base.Bind<ObjectSelector>().AsSingleton();
			base.Bind<ObjectViewer>().AsSingleton();
			base.Bind<ObjectViewerNodeFactory>().AsSingleton();
			base.MultiBind<IDevModule>().To<DebuggingPanelResetter>().AsSingleton();
		}
	}
}
