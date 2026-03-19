using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.SoilBarrierSystem
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	[Context("MapEditor")]
	public class SoilBarrierSystemConfigurator : Configurator
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002841 File Offset: 0x00000A41
		public override void Configure()
		{
			base.Bind<SoilBarrier>().AsTransient();
			base.Bind<SoilBarrierMap>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(SoilBarrierSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002878 File Offset: 0x00000A78
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<SoilBarrierSpec, SoilBarrier>();
			return builder.Build();
		}
	}
}
