using System;
using Bindito.Core;
using Timberborn.GameDistricts;
using Timberborn.TemplateInstantiation;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class GameDistrictsMigrationConfigurator : Configurator
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002450 File Offset: 0x00000650
		public override void Configure()
		{
			base.Bind<AdultsDistributorTemplate>().AsTransient();
			base.Bind<BotsDistributorTemplate>().AsTransient();
			base.Bind<ChildrenDistributorTemplate>().AsTransient();
			base.Bind<ContaminatedDistributorTemplate>().AsTransient();
			base.Bind<MigrationTrigger>().AsTransient();
			base.Bind<PopulationDistributor>().AsTransient();
			base.Bind<DistributorTemplateInitializer>().AsSingleton();
			base.Bind<MigrationCoordinator>().AsSingleton();
			base.Bind<MigrationNeighbours>().AsSingleton();
			base.Bind<MigrationService>().AsSingleton();
			base.Bind<PopulationDistributorRetriever>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<GameDistrictsMigrationConfigurator.GameDistrictsMigrationTemplateModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000D RID: 13
		public class GameDistrictsMigrationTemplateModuleProvider : IProvider<TemplateModule>
		{
			// Token: 0x0600001F RID: 31 RVA: 0x000024FA File Offset: 0x000006FA
			public GameDistrictsMigrationTemplateModuleProvider(DistributorTemplateInitializer distributorTemplateInitializer)
			{
				this._distributorTemplateInitializer = distributorTemplateInitializer;
			}

			// Token: 0x06000020 RID: 32 RVA: 0x00002509 File Offset: 0x00000709
			public TemplateModule Get()
			{
				TemplateModule.Builder builder = new TemplateModule.Builder();
				builder.AddDecorator<DistrictCenter, AdultsDistributorTemplate>();
				builder.AddDecorator<DistrictCenter, BotsDistributorTemplate>();
				builder.AddDecorator<DistrictCenter, ChildrenDistributorTemplate>();
				builder.AddDecorator<DistrictCenter, ContaminatedDistributorTemplate>();
				builder.AddDecorator<DistrictCenter, MigrationTrigger>();
				builder.AddDedicatedDecorator<IDistributorTemplate, PopulationDistributor>(this._distributorTemplateInitializer);
				return builder.Build();
			}

			// Token: 0x04000013 RID: 19
			public readonly DistributorTemplateInitializer _distributorTemplateInitializer;
		}
	}
}
