using System;
using Bindito.Core;

namespace Timberborn.SoilContaminationSystem
{
	// Token: 0x02000013 RID: 19
	[Context("Game")]
	[Context("MapEditor")]
	public class SoilContaminationSystemConfigurator : Configurator
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003FA0 File Offset: 0x000021A0
		public override void Configure()
		{
			base.Bind<ContaminatedObject>().AsTransient();
			base.Bind<SoilContaminationSimulator>().AsSingleton();
			base.Bind<ISoilContaminationService>().To<SoilContaminationService>().AsSingleton();
			base.Bind<SoilContaminationSimulationTaskStarter>().AsSingleton();
		}
	}
}
