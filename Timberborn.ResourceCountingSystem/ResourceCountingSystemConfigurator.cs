using System;
using Bindito.Core;
using Timberborn.GameDistricts;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ResourceCountingSystem
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class ResourceCountingSystemConfigurator : Configurator
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002A80 File Offset: 0x00000C80
		public override void Configure()
		{
			base.Bind<DistrictResourceCounter>().AsTransient();
			base.Bind<GoodProcessorRegistrar>().AsTransient();
			base.Bind<ResourceCountingService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ResourceCountingSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002ACE File Offset: 0x00000CCE
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DistrictCenter, DistrictResourceCounter>();
			builder.AddDecorator<IGoodProcessor, GoodProcessorRegistrar>();
			return builder.Build();
		}
	}
}
