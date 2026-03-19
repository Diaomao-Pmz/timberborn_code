using System;
using Bindito.Core;
using Timberborn.BlockSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.UnderstructureSystem
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	[Context("MapEditor")]
	public class UnderstructureSystemConfigurator : Configurator
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002834 File Offset: 0x00000A34
		public override void Configure()
		{
			base.Bind<UnderstructureConstraint>().AsTransient();
			base.Bind<Understructure>().AsTransient();
			base.Bind<UnderstructureConstructionSiteValidator>().AsTransient();
			base.Bind<UnderstructureFinder>().AsSingleton();
			base.MultiBind<IBlockObjectValidator>().To<UnderstructureConstraintValidator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(UnderstructureSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000289F File Offset: 0x00000A9F
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<UnderstructureConstraintSpec, UnderstructureConstraint>();
			builder.AddDecorator<BlockObject, Understructure>();
			builder.AddDecorator<UnderstructureConstraint, UnderstructureConstructionSiteValidator>();
			return builder.Build();
		}
	}
}
