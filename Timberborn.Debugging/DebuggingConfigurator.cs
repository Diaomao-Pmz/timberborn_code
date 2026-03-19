using System;
using Bindito.Core;
using Timberborn.KeyBindingSystem;

namespace Timberborn.Debugging
{
	// Token: 0x02000004 RID: 4
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class DebuggingConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public override void Configure()
		{
			base.Bind<DebugModeManager>().AsSingleton();
			base.Bind<DebugModeController>().AsSingleton();
			base.Bind<DevModeManager>().AsSingleton();
			base.Bind<DevModeController>().AsSingleton();
			base.Bind<IKeyBindingBlocker>().To<DevModeKeyBindingBlocker>().AsSingleton();
			base.MultiBind<IDevModule>().To<TestExceptionDevModule>().AsSingleton();
		}
	}
}
