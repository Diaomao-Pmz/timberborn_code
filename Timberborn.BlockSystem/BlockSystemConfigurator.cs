using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;
using Timberborn.TransformControl;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000033 RID: 51
	[Context("Game")]
	[Context("MapEditor")]
	public class BlockSystemConfigurator : Configurator
	{
		// Token: 0x06000177 RID: 375 RVA: 0x00005AAC File Offset: 0x00003CAC
		public override void Configure()
		{
			base.Bind<BlockObject>().AsTransient();
			base.Bind<BlockObjectAtopDeletionBlocker>().AsTransient();
			base.Bind<BlockObjectCenter>().AsTransient();
			base.Bind<BlockObjectRange>().AsTransient();
			base.Bind<BlockObjectState>().AsTransient();
			base.Bind<BlockObjectPostLoadState>().AsTransient();
			base.Bind<BlockObjectTerrainCutout>().AsTransient();
			base.Bind<BlockOccupant>().AsTransient();
			base.Bind<PlacementChangeNotifier>().AsTransient();
			base.Bind<Preview>().AsTransient();
			base.Bind<PreviewBlockObject>().AsTransient();
			base.Bind<AreaClamper>().AsSingleton();
			base.Bind<AreaIterator>().AsSingleton();
			base.Bind<BlockService>().AsSingleton();
			base.Bind<IBlockService>().ToExisting<BlockService>();
			base.Bind<BlockValidator>().AsSingleton();
			base.Bind<MatterBelowValidator>().AsSingleton();
			base.Bind<StackableBlockService>().AsSingleton();
			base.Bind<IBlockOccupancyService>().To<BlockOccupancyService>().AsSingleton();
			base.Bind<OverridenBlockObjectService>().AsSingleton();
			base.Bind<BlockObjectFactory>().AsSingleton();
			base.Bind<PreviewBlockService>().AsSingleton();
			base.Bind<BlockObjectBatchLoader>().AsSingleton();
			base.Bind<BlockObjectValidationService>().AsSingleton();
			base.MultiBind<IBlockObjectValidator>().To<NoTerrainRemoverBelowValidator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BlockSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005C0C File Offset: 0x00003E0C
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BlockObjectSpec, BlockObject>();
			builder.AddDecorator<BlockObject, TransformController>();
			builder.AddDecorator<BlockObject, PlacementChangeNotifier>();
			builder.AddDecorator<BlockObject, BlockObjectCenter>();
			builder.AddDecorator<BlockObject, BlockObjectRange>();
			builder.AddDecorator<BlockObject, BlockObjectState>();
			builder.AddDecorator<BlockObject, BlockObjectPostLoadState>();
			builder.AddDecorator<BlockObject, Preview>();
			builder.AddDecorator<BlockObject, PreviewBlockObject>();
			builder.AddDecorator<BlockObject, BlockObjectAtopDeletionBlocker>();
			builder.AddDecorator<BlockObjectTerrainCutoutSpec, BlockObjectTerrainCutout>();
			return builder.Build();
		}
	}
}
