using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MechanicalConnectorSystem
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	public class MechanicalConnectorSystemConfigurator : Configurator
	{
		// Token: 0x0600002A RID: 42 RVA: 0x0000266C File Offset: 0x0000086C
		public override void Configure()
		{
			base.Bind<MechanicalConnectorActivator>().AsTransient();
			base.Bind<MechanicalConnectors>().AsTransient();
			base.Bind<MechanicalConnectorFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MechanicalConnectorSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026BA File Offset: 0x000008BA
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<MechanicalConnectorTargetSpec, MechanicalConnectors>();
			builder.AddDecorator<MechanicalConnectors, MechanicalConnectorActivator>();
			return builder.Build();
		}
	}
}
