using System;
using Bindito.Core;
using Timberborn.AreaSelectionSystem;
using Timberborn.ConstructionSites;
using Timberborn.Rendering;
using Timberborn.TemplateInstantiation;
using Timberborn.TerrainLevelValidation;

namespace Timberborn.Terraforming
{
	// Token: 0x02000013 RID: 19
	[Context("Game")]
	public class TerraformingConfigurator : Configurator
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000033BC File Offset: 0x000015BC
		public override void Configure()
		{
			base.Bind<Drill>().AsTransient();
			base.Bind<DrillHeadVisualizer>().AsTransient();
			base.Bind<DrillScrewBuilder>().AsTransient();
			base.Bind<DrillScrewRotator>().AsTransient();
			base.Bind<GroundRaiser>().AsTransient();
			base.Bind<TerraformingDirectionalBlocker>().AsTransient();
			base.Bind<GroundRaisingService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(TerraformingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000343C File Offset: 0x0000163C
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<GroundRaiserSpec, GroundRaiser>();
			builder.AddDecorator<GroundRaiser, TerraformingDirectionalBlocker>();
			builder.AddDecorator<GroundRaiser, TopTerrainLevelValidationConstraint>();
			builder.AddDecorator<GroundRaiser, DeleteOnFinishConstructionSite>();
			builder.AddDecorator<GroundRaiser, PhysicallySupportedConstructionSite>();
			builder.AddDecorator<DrillScrewBuilderSpec, DrillScrewBuilder>();
			builder.AddDecorator<DrillScrewBuilder, DrillScrewRotator>();
			builder.AddDecorator<DrillScrewBuilder, EntityMaterials>();
			builder.AddDecorator<DrillHeadVisualizerSpec, DrillHeadVisualizer>();
			builder.AddDecorator<DrillSpec, Drill>();
			builder.AddDecorator<Slope, AreaBoundsDrawingBlocker>();
			return builder.Build();
		}
	}
}
