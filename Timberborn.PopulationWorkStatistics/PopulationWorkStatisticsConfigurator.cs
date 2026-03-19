using System;
using Bindito.Core;
using Timberborn.GameDistricts;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.PopulationWorkStatistics
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class PopulationWorkStatisticsConfigurator : Configurator
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000024A0 File Offset: 0x000006A0
		public override void Configure()
		{
			base.Bind<DistrictEmploymentStatisticsProvider>().AsTransient();
			base.Bind<DistrictWorkRefusingStatisticsProvider>().AsTransient();
			base.Bind<WorkplaceWorkerCounter>().AsTransient();
			base.Bind<GlobalEmploymentStatisticsProvider>().AsSingleton();
			base.Bind<GlobalWorkRefusingStatisticsProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(PopulationWorkStatisticsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002506 File Offset: 0x00000706
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DistrictCenter, DistrictEmploymentStatisticsProvider>();
			builder.AddDecorator<DistrictCenter, DistrictWorkRefusingStatisticsProvider>();
			builder.AddDecorator<Workplace, WorkplaceWorkerCounter>();
			return builder.Build();
		}
	}
}
