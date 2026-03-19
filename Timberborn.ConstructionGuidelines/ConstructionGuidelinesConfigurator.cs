using System;
using Bindito.Core;
using Timberborn.BlockSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ConstructionGuidelines
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	[Context("MapEditor")]
	public class ConstructionGuidelinesConfigurator : Configurator
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002354 File Offset: 0x00000554
		public override void Configure()
		{
			base.Bind<BlockObjectGridFootprint>().AsTransient();
			base.Bind<ConstructionGuidelinesRenderingService>().AsSingleton();
			base.Bind<TileDrawerFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ConstructionGuidelinesConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023A2 File Offset: 0x000005A2
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BlockObject, BlockObjectGridFootprint>();
			return builder.Build();
		}
	}
}
