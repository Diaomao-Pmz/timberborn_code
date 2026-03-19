using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;
using Timberborn.TerrainSystem;
using Timberborn.ToolSystem;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x02000019 RID: 25
	[Context("Game")]
	[Context("MapEditor")]
	public class BlockObjectToolsConfigurator : Configurator
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003378 File Offset: 0x00001578
		public override void Configure()
		{
			base.Bind<PreviewTerrainCutout>().AsTransient();
			base.Bind<PreviewFactory>().AsSingleton();
			base.Bind<BlockObjectToolDescriber>().AsSingleton();
			base.Bind<EntityBlockObjectDeletionTool>().AsSingleton();
			base.Bind<BlockObjectToolFactory>().AsSingleton();
			base.Bind<PlaceableBlockObjectSpecService>().AsSingleton();
			base.Bind<PreviewPlacement>().AsSingleton();
			base.Bind<BlockObjectPlacerService>().AsSingleton();
			base.Bind<DefaultBlockObjectPlacer>().AsSingleton();
			base.Bind<BlockObjectToolGroupSpecService>().AsSingleton();
			base.Bind<PreviewPlacerFactory>().AsSingleton();
			base.Bind<PreviewShower>().AsSingleton();
			base.Bind<PreviewTerrainCutoutService>().AsSingleton();
			base.MultiBind<IToolFinder>().To<BlockObjectToolFinder>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BlockObjectToolsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000344F File Offset: 0x0000164F
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ICutoutTilesProvider, PreviewTerrainCutout>();
			return builder.Build();
		}
	}
}
