using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.DeconstructionSystemUI
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class DeconstructionSystemUIConfigurator : Configurator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000236D File Offset: 0x0000056D
		public override void Configure()
		{
			base.Bind<BuildingDeconstructionTool>().AsSingleton();
			base.Bind<DeconstructionSoundPlayer>().AsSingleton();
			base.MultiBind<IDevModule>().To<BuildingDeconstructionToolPreviewDisabler>().AsSingleton();
		}
	}
}
