using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.UILayoutSystem
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	[Context("MapEditor")]
	public class UILayoutConfigurator : Configurator
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000024A9 File Offset: 0x000006A9
		public override void Configure()
		{
			base.Bind<UIVisibilityManager>().AsSingleton();
			base.Bind<OverlayPanelSpeedLocker>().AsSingleton();
			base.Bind<UILayout>().AsSingleton();
			base.MultiBind<IDevModule>().To<DebugUIScaleChanger>().AsSingleton();
		}
	}
}
