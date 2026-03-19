using System;
using Bindito.Core;
using Timberborn.BlockSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BlockObjectAccesses
{
	// Token: 0x02000010 RID: 16
	[Context("Game")]
	public class BlockObjectAccessesConfigurator : Configurator
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000030DC File Offset: 0x000012DC
		public override void Configure()
		{
			base.Bind<BlockObjectAccessGenerator>().AsTransient();
			base.Bind<BlockObjectAccessible>().AsTransient();
			base.Bind<HighBlockObjectAccessesAdder>().AsTransient();
			base.Bind<BlockObjectAccesses>().AsTransient();
			base.Bind<ParentedNeighborCalculator>().AsTransient();
			base.Bind<NeighborCalculator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BlockObjectAccessesConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000314E File Offset: 0x0000134E
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BlockObject, ParentedNeighborCalculator>();
			builder.AddDecorator<BlockObject, BlockObjectAccessGenerator>();
			builder.AddDecorator<BlockObjectAccessesSpec, BlockObjectAccesses>();
			return builder.Build();
		}
	}
}
