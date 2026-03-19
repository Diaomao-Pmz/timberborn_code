using System;
using Bindito.Core;
using Timberborn.Beavers;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Healthcare
{
	// Token: 0x0200000F RID: 15
	[Context("Game")]
	public class HealthcareConfigurator : Configurator
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002C04 File Offset: 0x00000E04
		public override void Configure()
		{
			base.Bind<BeaverInjuryTextureSetter>().AsTransient();
			base.Bind<BeaverNeedShaderPropertySetter>().AsTransient();
			base.Bind<ChippedTeethNeedChangeListener>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(HealthcareConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002C52 File Offset: 0x00000E52
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BeaverSpec, ChippedTeethNeedChangeListener>();
			builder.AddDecorator<BeaverNeedShaderPropertySetterSpec, BeaverNeedShaderPropertySetter>();
			builder.AddDecorator<BeaverInjuryTextureSetterSpec, BeaverInjuryTextureSetter>();
			return builder.Build();
		}
	}
}
