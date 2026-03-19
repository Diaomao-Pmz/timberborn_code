using System;
using Bindito.Core;
using Timberborn.Debugging;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MortalSystem
{
	// Token: 0x02000012 RID: 18
	[Context("Game")]
	public class MortalSystemConfigurator : Configurator
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00002E44 File Offset: 0x00001044
		public override void Configure()
		{
			base.Bind<DeadRootBehavior>().AsTransient();
			base.Bind<Mortal>().AsTransient();
			base.Bind<DeadStatus>().AsTransient();
			base.Bind<MortalNeeder>().AsTransient();
			base.Bind<Temporal>().AsTransient();
			base.Bind<LongLastingCorpsesService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MortalSystemConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<IDevModule>().To<CharacterKiller>().AsSingleton();
			base.MultiBind<IDevModule>().To<LongLastingCorpsesDevModule>().AsSingleton();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002ED8 File Offset: 0x000010D8
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<MortalSpec, Mortal>();
			builder.AddDecorator<Mortal, MortalNeeder>();
			builder.AddDecorator<DeadStatusSpec, DeadStatus>();
			builder.AddDecorator<TemporalSpec, Temporal>();
			return builder.Build();
		}
	}
}
