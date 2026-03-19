using System;
using Bindito.Core;
using Timberborn.Emptying;
using Timberborn.LaborSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.LaborSystemInitialization
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class LaborSystemInitializationConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		public override void Configure()
		{
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(LaborSystemInitializationConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D7 File Offset: 0x000002D7
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			LaborSystemInitializationConfigurator.AddDecoratingBehaviors(builder);
			return builder.Build();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E9 File Offset: 0x000002E9
		public static void AddDecoratingBehaviors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<LaborWorkplaceBehavior, EmptyInventoriesLaborBehavior>();
			builder.AddDecorator<LaborWorkplaceBehavior, RemoveUnwantedStockLaborBehavior>();
		}
	}
}
