using System;
using Bindito.Core;
using Timberborn.Beavers;
using Timberborn.EnterableSystem;
using Timberborn.GameDistricts;
using Timberborn.TemplateInstantiation;

namespace Timberborn.DwellingSystem
{
	// Token: 0x02000010 RID: 16
	[Context("Game")]
	public class DwellingSystemConfigurator : Configurator
	{
		// Token: 0x0600007A RID: 122 RVA: 0x0000316C File Offset: 0x0000136C
		public override void Configure()
		{
			base.Bind<UnreachableHomeUnassigner>().AsTransient();
			base.Bind<Dwelling>().AsTransient();
			base.Bind<Dweller>().AsTransient();
			base.Bind<AutoAssignableDwelling>().AsTransient();
			base.Bind<DistrictDwellingStatisticsProvider>().AsTransient();
			base.Bind<DwellerCounter>().AsTransient();
			base.Bind<DwellerHomeAssigner>().AsSingleton();
			base.Bind<GlobalDwellingStatisticsProvider>().AsSingleton();
			base.Bind<StaleAssignableDwellingService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(DwellingSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003202 File Offset: 0x00001402
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BeaverSpec, Dweller>();
			builder.AddDecorator<Dweller, UnreachableHomeUnassigner>();
			builder.AddDecorator<DwellingSpec, Dwelling>();
			builder.AddDecorator<Dwelling, AutoAssignableDwelling>();
			builder.AddDecorator<Dwelling, DwellerCounter>();
			builder.AddDecorator<Dwelling, EnterableSounds>();
			builder.AddDecorator<DistrictCenter, DistrictDwellingStatisticsProvider>();
			return builder.Build();
		}
	}
}
