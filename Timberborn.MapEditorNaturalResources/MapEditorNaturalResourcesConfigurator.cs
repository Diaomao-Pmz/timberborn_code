using System;
using Bindito.Core;
using Timberborn.NaturalResources;
using Timberborn.NaturalResourcesMoisture;
using Timberborn.SoilMoistureSystem;
using Timberborn.StatusSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MapEditorNaturalResources
{
	// Token: 0x02000005 RID: 5
	[Context("MapEditor")]
	public class MapEditorNaturalResourcesConfigurator : Configurator
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002234 File Offset: 0x00000434
		public override void Configure()
		{
			base.Bind<InstantNaturalResource>().AsTransient();
			base.Bind<NaturalResourceLayerService>().AsSingleton();
			base.Bind<NaturalResourceSpawner>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MapEditorNaturalResourcesConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002282 File Offset: 0x00000482
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<NaturalResourceSpec, StatusSubject>();
			builder.AddDecorator<WateredNaturalResourceSpec, InstantNaturalResource>();
			builder.AddDecorator<WateredNaturalResourceSpec, DryObject>();
			builder.AddDecorator<WateredNaturalResourceSpec, WateredNaturalResource>();
			builder.AddDecorator<FloodableNaturalResourceSpec, InstantNaturalResource>();
			builder.AddDecorator<FloodableNaturalResourceSpec, LivingWaterObject>();
			builder.AddDecorator<LivingWaterObject, LivingWaterNaturalResource>();
			return builder.Build();
		}
	}
}
