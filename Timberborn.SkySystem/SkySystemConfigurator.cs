using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.SkySystem
{
	// Token: 0x02000011 RID: 17
	[Context("Game")]
	[Context("MapEditor")]
	public class SkySystemConfigurator : Configurator
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003244 File Offset: 0x00001444
		public override void Configure()
		{
			base.Bind<SunStopper>().AsSingleton();
			base.Bind<Sun>().AsSingleton();
			base.Bind<SkyboxPositioner>().AsSingleton();
			base.Bind<DayStageCycle>().AsSingleton();
			base.MultiBind<IDevModule>().To<SkySystemDevModule>().AsSingleton();
		}
	}
}
