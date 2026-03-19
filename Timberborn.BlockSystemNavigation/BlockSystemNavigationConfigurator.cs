using System;
using Bindito.Core;
using Timberborn.BlockSystem;
using Timberborn.Navigation;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x02000013 RID: 19
	[Context("Game")]
	public class BlockSystemNavigationConfigurator : Configurator
	{
		// Token: 0x06000099 RID: 153 RVA: 0x0000326C File Offset: 0x0000146C
		public override void Configure()
		{
			base.Bind<BlockObjectNavMesh>().AsTransient();
			base.Bind<BlockObjectNavMeshAdder>().AsTransient();
			base.Bind<BlockObjectPreviewNavMesh>().AsTransient();
			base.Bind<NavMeshObjectUpdater>().AsSingleton();
			base.Bind<BlockObjectNavMeshGroupInitializer>().AsSingleton();
			base.Bind<INavMeshSizeProvider>().To<BlockSystemNavMeshSizeProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BlockSystemNavigationConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000032E3 File Offset: 0x000014E3
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BlockObject, BlockObjectNavMesh>();
			builder.AddDecorator<Preview, BlockObjectPreviewNavMesh>();
			builder.AddDecorator<BlockObjectNavMeshAdderSpec, BlockObjectNavMeshAdder>();
			return builder.Build();
		}
	}
}
