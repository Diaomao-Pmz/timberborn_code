using System;
using Bindito.Core;
using Timberborn.NaturalResources;
using Timberborn.NaturalResourcesMoisture;
using Timberborn.SoilMoistureSystem;
using Timberborn.StatusSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MapEditorNaturalResources
{
	// Token: 0x02000004 RID: 4
	[Context("MapEditor")]
	internal class MapEditorNaturalResourcesConfigurator : Configurator
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000225C File Offset: 0x0000045C
		protected override void Configure()
		{
			base.Bind<InstantNaturalResource>().AsTransient();
			base.Bind<NaturalResourceLayerService>().AsSingleton();
			base.Bind<NaturalResourceSpawner>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MapEditorNaturalResourcesConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022AA File Offset: 0x000004AA
		private static TemplateModule ProvideTemplateModule()
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
