using System;
using Bindito.Core;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.SelectionSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BlockSystemUI
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	[Context("MapEditor")]
	public class BlockSystemUIConfigurator : Configurator
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002834 File Offset: 0x00000A34
		public override void Configure()
		{
			base.Bind<BlockObjectCameraTarget>().AsTransient();
			base.Bind<BlockObjectDeletionDescriber>().AsTransient();
			base.Bind<EntranceMarkerDrawer>().AsTransient();
			base.Bind<PlaceableBlockObjectDescriber>().AsTransient();
			base.Bind<UndergroundDepthDescriber>().AsTransient();
			base.Bind<BlockObjectBoundsDrawerFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BlockSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028A6 File Offset: 0x00000AA6
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BlockObject, EntranceMarkerDrawer>();
			builder.AddDecorator<BlockObject, SelectableObject>();
			builder.AddDecorator<BlockObject, BlockObjectDeletionDescriber>();
			builder.AddDecorator<BlockObjectCenter, BlockObjectCameraTarget>();
			builder.AddDecorator<PlaceableBlockObjectSpec, LabeledEntityBadge>();
			builder.AddDecorator<PlaceableBlockObjectSpec, PlaceableBlockObjectDescriber>();
			builder.AddDecorator<UndergroundDepthDescriberSpec, UndergroundDepthDescriber>();
			builder.AddDecorator<IInfiniteUndergroundModel, UndergroundDepthDescriber>();
			return builder.Build();
		}
	}
}
