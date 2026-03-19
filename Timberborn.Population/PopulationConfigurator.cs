using System;
using Bindito.Core;

namespace Timberborn.Population
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class PopulationConfigurator : Configurator
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002221 File Offset: 0x00000421
		public override void Configure()
		{
			base.Bind<PopulationDataCollector>().AsSingleton();
			base.Bind<PopulationService>().AsSingleton();
		}
	}
}
