using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.WaterSystemUI
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class WaterSystemUIConfigurator : Configurator
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000026AF File Offset: 0x000008AF
		public override void Configure()
		{
			base.Bind<WaterColumnDebuggingPanel>().AsSingleton();
			base.Bind<WaterOpacityTogglePanel>().AsSingleton();
			base.MultiBind<IDevModule>().To<WaterSystemDevModule>().AsSingleton();
		}
	}
}
