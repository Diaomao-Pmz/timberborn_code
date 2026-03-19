using System;
using Bindito.Core;
using Timberborn.Beavers;
using Timberborn.GameDistricts;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BeaverContaminationSystem
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class BeaverContaminationSystemConfigurator : Configurator
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000218C File Offset: 0x0000038C
		public override void Configure()
		{
			base.Bind<ContaminateRootBehavior>().AsTransient();
			base.Bind<ContaminationApplier>().AsTransient();
			base.Bind<Contaminable>().AsTransient();
			base.Bind<ContaminableAnimator>().AsTransient();
			base.Bind<ContaminationIncubator>().AsTransient();
			base.Bind<ContaminationNeedEnabler>().AsTransient();
			base.Bind<DistrictBeaverContaminationStatisticsProvider>().AsTransient();
			base.Bind<GlobalBeaverContaminationStatisticsProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BeaverContaminationSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002216 File Offset: 0x00000416
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BeaverSpec, Contaminable>();
			builder.AddDecorator<Contaminable, ContaminableAnimator>();
			builder.AddDecorator<Contaminable, ContaminationNeedEnabler>();
			builder.AddDecorator<Contaminable, ContaminationApplier>();
			builder.AddDecorator<Contaminable, ContaminationIncubator>();
			builder.AddDecorator<DistrictCenter, DistrictBeaverContaminationStatisticsProvider>();
			return builder.Build();
		}
	}
}
