using System;
using Bindito.Core;

namespace Timberborn.PopulationUI
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	public class PopulationUIConfigurator : Configurator
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000029BF File Offset: 0x00000BBF
		public override void Configure()
		{
			base.Bind<HousingDataRowFactory>().AsSingleton();
			base.Bind<PopulationDataRowFactory>().AsSingleton();
			base.Bind<PopulationPanel>().AsSingleton();
			base.Bind<PopulationServiceDistrictSwitcher>().AsSingleton();
			base.Bind<WorkplaceDataRowFactory>().AsSingleton();
		}
	}
}
