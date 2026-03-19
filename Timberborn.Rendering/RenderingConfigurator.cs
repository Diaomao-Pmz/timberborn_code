using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Rendering
{
	// Token: 0x02000020 RID: 32
	[Context("Game")]
	[Context("MapEditor")]
	public class RenderingConfigurator : Configurator
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00004454 File Offset: 0x00002654
		public override void Configure()
		{
			base.Bind<EntityMaterials>().AsTransient();
			base.Bind<MapBottomGroundCutoff>().AsTransient();
			base.Bind<MarkerPosition>().AsTransient();
			base.Bind<StartableMarkerPositionUpdater>().AsTransient();
			base.Bind<FinishedStateLightingEnforcer>().AsTransient();
			base.Bind<MaterialLightingRenderers>().AsTransient();
			base.Bind<LightingEnabler>().AsTransient();
			base.Bind<ColoredMaterialCache>().AsSingleton();
			base.Bind<MaterialColorer>().AsSingleton();
			base.Bind<MeshDrawerFactory>().AsSingleton();
			base.Bind<MaterialHeightCutoffSetter>().AsSingleton();
			base.Bind<TickProgressPropertyUpdater>().AsSingleton();
			base.Bind<MarkerDrawerFactory>().AsSingleton();
			base.Bind<AreaTileDrawerFactory>().AsSingleton();
			base.Bind<PostprocessingService>().AsSingleton();
			base.Bind<MaterialLightingEnabler>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(RenderingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000453E File Offset: 0x0000273E
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<StartableMarkerPositionUpdaterSpec, StartableMarkerPositionUpdater>();
			builder.AddDecorator<StartableMarkerPositionUpdater, MarkerPosition>();
			builder.AddDecorator<MapBottomGroundCutoffSpec, MapBottomGroundCutoff>();
			builder.AddDecorator<FinishedStateLightingEnforcerSpec, FinishedStateLightingEnforcer>();
			builder.AddDecorator<FinishedStateLightingEnforcer, EntityMaterials>();
			builder.AddDecorator<EntityMaterials, MaterialLightingRenderers>();
			builder.AddDecorator<LightingEnablerSpec, LightingEnabler>();
			return builder.Build();
		}
	}
}
