using System;
using Bindito.Core;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	public class SettlementStatisticsConfigurator : Configurator
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000241C File Offset: 0x0000061C
		public override void Configure()
		{
			base.Bind<BeaverBornStatisticCollector>().AsSingleton();
			base.Bind<BeaverExplodedStatisticCollector>().AsSingleton();
			base.Bind<BotsManufacturedStatisticCollector>().AsSingleton();
			base.Bind<ChippedTeethStatisticCollector>().AsSingleton();
			base.Bind<DynamiteDetonatedStatisticCollector>().AsSingleton();
			base.Bind<TreeCutStatisticCollector>().AsSingleton();
			base.Bind<WaterConsumedStatisticCollector>().AsSingleton();
			base.Bind<TailsPaintedStatisticCollector>().AsSingleton();
			base.Bind<IncrementalStatisticCollector>().AsSingleton();
			base.Bind<IncrementalStatisticSerializer>().AsSingleton();
			base.Bind<DaysPassedStatisticCollector>().AsSingleton();
		}
	}
}
