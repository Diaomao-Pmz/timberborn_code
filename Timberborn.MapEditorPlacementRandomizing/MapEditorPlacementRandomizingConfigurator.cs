using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MapEditorPlacementRandomizing
{
	// Token: 0x02000009 RID: 9
	[Context("MapEditor")]
	internal class MapEditorPlacementRandomizingConfigurator : Configurator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002288 File Offset: 0x00000488
		protected override void Configure()
		{
			base.Bind<BlockObjectPlacementRandomizer>().AsTransient();
			base.Bind<BlockObjectPlacementRandomizingService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MapEditorPlacementRandomizingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022BF File Offset: 0x000004BF
		private static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BlockObjectRandomizablePlacementSpec, BlockObjectPlacementRandomizer>();
			return builder.Build();
		}
	}
}
