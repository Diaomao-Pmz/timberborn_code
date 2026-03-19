using System;
using Bindito.Core;
using Timberborn.Rendering;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Illumination
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	[Context("MapEditor")]
	public class IlluminationConfigurator : Configurator
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002A60 File Offset: 0x00000C60
		public override void Configure()
		{
			base.Bind<Illuminator>().AsTransient();
			base.Bind<IlluminatorLightObjects>().AsTransient();
			base.Bind<BlockableIlluminator>().AsTransient();
			base.Bind<CustomizableIlluminator>().AsTransient();
			base.Bind<DefaultIlluminatorColor>().AsTransient();
			base.Bind<IlluminationService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(IlluminationConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002AD2 File Offset: 0x00000CD2
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<IlluminatorLightObjectsSpec, IlluminatorLightObjects>();
			builder.AddDecorator<DefaultIlluminatorColorSpec, DefaultIlluminatorColor>();
			builder.AddDecorator<DefaultIlluminatorColor, Illuminator>();
			builder.AddDecorator<BlockableIlluminatorSpec, BlockableIlluminator>();
			builder.AddDecorator<BlockableIlluminator, Illuminator>();
			builder.AddDecorator<CustomizableIlluminator, Illuminator>();
			builder.AddDecorator<CustomizableIlluminatorSpec, CustomizableIlluminator>();
			builder.AddDecorator<Illuminator, MaterialLightingRenderers>();
			return builder.Build();
		}
	}
}
