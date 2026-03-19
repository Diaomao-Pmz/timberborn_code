using System;
using Bindito.Core;
using Timberborn.Debugging;
using Timberborn.EntityNaming;
using Timberborn.Illumination;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Automation
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class AutomationConfigurator : Configurator
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002400 File Offset: 0x00000600
		public override void Configure()
		{
			base.Bind<Automator>().AsTransient();
			base.Bind<Automatable>().AsTransient();
			base.Bind<AutomatorIlluminator>().AsTransient();
			base.Bind<AutomationRunner>().AsSingleton();
			base.Bind<IAutomationRunnerDebugger>().ToExisting<AutomationRunner>();
			base.Bind<AutomationPartitioner>().AsSingleton();
			base.Bind<AutomationPlanVersioner>().AsSingleton();
			base.Bind<AutomatorPartitionFactory>().AsSingleton();
			base.Bind<AutomatorRegistry>().AsSingleton();
			base.Bind<AutomationResetter>().AsSingleton();
			base.Bind<AutomationDebugger>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(AutomationConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<IDevModule>().To<AutomationDevModule>().AsSingleton();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024BF File Offset: 0x000006BF
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ITransmitter, Automator>();
			builder.AddDecorator<ITerminal, Automator>();
			builder.AddDecorator<IAutomatableNeeder, Automatable>();
			builder.AddDecorator<AutomatorIlluminator, Illuminator>();
			builder.AddDecorator<AutomatorIlluminator, CustomizableIlluminator>();
			builder.AddDecorator<ITransmitter, NumberedEntityNamer>();
			return builder.Build();
		}
	}
}
