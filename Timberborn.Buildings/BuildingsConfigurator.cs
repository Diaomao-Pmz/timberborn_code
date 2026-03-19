using System;
using Bindito.Core;
using Timberborn.BlockingSystem;
using Timberborn.Particles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Buildings
{
	// Token: 0x02000013 RID: 19
	[Context("Game")]
	[Context("MapEditor")]
	public class BuildingsConfigurator : Configurator
	{
		// Token: 0x0600009A RID: 154 RVA: 0x0000330C File Offset: 0x0000150C
		public override void Configure()
		{
			base.Bind<BuildingAccessible>().AsTransient();
			base.Bind<BuildingBlockedAccessible>().AsTransient();
			base.Bind<BuildingModel>().AsTransient();
			base.Bind<BuildingModelGroundCutoff>().AsTransient();
			base.Bind<BuildingSelectionSound>().AsTransient();
			base.Bind<BuildingSounds>().AsTransient();
			base.Bind<BuildingTerrainCutout>().AsTransient();
			base.Bind<Fire>().AsTransient();
			base.Bind<FireIntensityController>().AsTransient();
			base.Bind<UncoveredModelSwitcher>().AsTransient();
			base.Bind<Building>().AsTransient();
			base.Bind<PausableBuilding>().AsTransient();
			base.Bind<BuildingSoundController>().AsTransient();
			base.Bind<BuildingDetailTexture>().AsTransient();
			base.Bind<BuildingService>().AsSingleton();
			base.Bind<BuildingModelUpdater>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BuildingsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000033F8 File Offset: 0x000015F8
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BuildingSpec, Building>();
			builder.AddDecorator<BuildingSpec, BuildingSelectionSound>();
			builder.AddDecorator<BuildingSpec, BuildingSounds>();
			builder.AddDecorator<BuildingAccessibleSpec, BuildingAccessible>();
			builder.AddDecorator<BuildingAccessible, BuildingBlockedAccessible>();
			builder.AddDecorator<BuildingModelSpec, BuildingModel>();
			builder.AddDecorator<BuildingModelGroundCutoffSpec, BuildingModelGroundCutoff>();
			builder.AddDecorator<BuildingTerrainCutoutSpec, BuildingTerrainCutout>();
			builder.AddDecorator<FireSpec, Fire>();
			builder.AddDecorator<Fire, ParticlesCache>();
			builder.AddDecorator<UncoveredModelSwitcherSpec, UncoveredModelSwitcher>();
			builder.AddDecorator<BuildingSpec, BlockableObject>();
			builder.AddDecorator<BuildingSpec, PausableBuilding>();
			builder.AddDecorator<BuildingDetailTextureSpec, BuildingDetailTexture>();
			return builder.Build();
		}
	}
}
