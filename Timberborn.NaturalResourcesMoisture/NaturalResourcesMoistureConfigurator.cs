using System;
using Bindito.Core;
using Timberborn.NaturalResources;
using Timberborn.SoilMoistureSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.NaturalResourcesMoisture
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	[Context("MapEditor")]
	public class NaturalResourcesMoistureConfigurator : Configurator
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002988 File Offset: 0x00000B88
		public override void Configure()
		{
			base.Bind<LivingWaterNaturalResource>().AsTransient();
			base.Bind<LivingWaterObject>().AsTransient();
			base.Bind<WateredNaturalResource>().AsTransient();
			base.Bind<FloodableNaturalResourceService>().AsSingleton();
			base.MultiBind<ISpawnValidator>().To<WateredNaturalResourceSpawnValidator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(NaturalResourcesMoistureConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029F3 File Offset: 0x00000BF3
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<WateredNaturalResourceSpec, WateredNaturalResource>();
			builder.AddDecorator<WateredNaturalResource, DryObject>();
			builder.AddDecorator<FloodableNaturalResourceSpec, LivingWaterObject>();
			builder.AddDecorator<LivingWaterObject, LivingWaterNaturalResource>();
			return builder.Build();
		}
	}
}
