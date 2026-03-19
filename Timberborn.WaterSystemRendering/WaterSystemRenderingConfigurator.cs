using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x0200001D RID: 29
	[Context("Game")]
	[Context("MapEditor")]
	public class WaterSystemRenderingConfigurator : Configurator
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00005600 File Offset: 0x00003800
		public override void Configure()
		{
			base.Bind<IWaterMesh>().To<WaterMesh>().AsSingleton();
			base.Bind<IWaterRenderer>().To<WaterRenderer>().AsSingleton();
			base.Bind<WaterColumnPostprocessor>().AsSingleton();
			base.Bind<WaterOpacityService>().AsSingleton();
			base.Bind<WaterBackfacesRenderer>().AsSingleton();
			base.Bind<WaterRenderingTaskStarter>().AsSingleton();
			base.Bind<WaterFlowLimitUpdater>().AsSingleton();
			base.MultiBind<IDevModule>().To<WaterSystemRenderingDevModule>().AsSingleton();
			base.MultiBind<IDevModule>().To<WaterOpacityOverrider>().AsSingleton();
		}
	}
}
