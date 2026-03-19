using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BlockObjectModelSystem
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class BlockObjectModelSystemConfigurator : Configurator
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002811 File Offset: 0x00000A11
		public override void Configure()
		{
			base.Bind<BlockObjectModel>().AsTransient();
			base.Bind<BlockObjectModelController>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BlockObjectModelSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002848 File Offset: 0x00000A48
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<IBlockObjectModel, BlockObjectModelController>();
			builder.AddDecorator<BlockObjectModelSpec, BlockObjectModel>();
			return builder.Build();
		}
	}
}
