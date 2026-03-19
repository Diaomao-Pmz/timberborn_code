using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.EntitySystem
{
	// Token: 0x02000011 RID: 17
	[Context("Game")]
	[Context("MapEditor")]
	public class EntitySystemConfigurator : Configurator
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000027FC File Offset: 0x000009FC
		public override void Configure()
		{
			base.Bind<EntityComponent>().AsTransient();
			base.Bind<LabeledEntity>().AsTransient();
			base.Bind<EntityComponentRegistry>().AsSingleton();
			base.Bind<EntityComponentRegistryFactory>().AsSingleton();
			base.Bind<RegisteredComponentService>().AsSingleton();
			base.Bind<EntityRegistry>().AsSingleton();
			base.Bind<EntityService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(EntitySystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000287A File Offset: 0x00000A7A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<LabeledEntitySpec, LabeledEntity>();
			return builder.Build();
		}
	}
}
