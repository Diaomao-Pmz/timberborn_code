using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.StartingLocationSystem
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	[Context("MapEditor")]
	public class StartingLocationSystemConfigurator : Configurator
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002464 File Offset: 0x00000664
		public override void Configure()
		{
			base.Bind<StartingLocation>().AsTransient();
			base.Bind<StartingLocationRenderer>().AsTransient();
			base.Bind<StartingLocationService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(StartingLocationSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024B2 File Offset: 0x000006B2
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<StartingLocationSpec, StartingLocation>();
			builder.AddDecorator<StartingLocationSpec, StartingLocationRenderer>();
			return builder.Build();
		}
	}
}
