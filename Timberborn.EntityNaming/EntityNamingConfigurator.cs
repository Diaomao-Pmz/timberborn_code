using System;
using Bindito.Core;
using Timberborn.EntitySystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.EntityNaming
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	[Context("MapEditor")]
	public class EntityNamingConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002118 File Offset: 0x00000318
		public override void Configure()
		{
			base.Bind<NamedEntity>().AsTransient();
			base.Bind<LabeledEntityNamer>().AsTransient();
			base.Bind<NumberedEntityNamer>().AsTransient();
			base.Bind<NamedEntityGameObjectSynchronizer>().AsTransient();
			base.Bind<UniquelyNamedEntity>().AsTransient();
			base.Bind<NumberedEntityNamerService>().AsSingleton();
			base.Bind<SerializedEntityNameNumberSerializer>().AsSingleton();
			base.Bind<UniquelyNamedEntityService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(EntityNamingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A2 File Offset: 0x000003A2
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<NamedEntitySpec, NamedEntity>();
			builder.AddDecorator<IEntityNamer, NamedEntity>();
			builder.AddDecorator<LabeledEntity, LabeledEntityNamer>();
			builder.AddDecorator<NumberedEntityNamerSpec, NumberedEntityNamer>();
			return builder.Build();
		}
	}
}
