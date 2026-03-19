using System;
using Bindito.Core;

namespace Timberborn.SimulationSystem
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	[Context("MapEditor")]
	public class SimulationSystemConfigurator : Configurator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002104 File Offset: 0x00000304
		public override void Configure()
		{
			base.Bind<SimulationController>().AsSingleton();
		}
	}
}
