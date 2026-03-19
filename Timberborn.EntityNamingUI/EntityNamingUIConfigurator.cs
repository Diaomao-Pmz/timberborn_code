using System;
using Bindito.Core;
using Timberborn.EntityNaming;
using Timberborn.TemplateInstantiation;

namespace Timberborn.EntityNamingUI
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class EntityNamingUIConfigurator : Configurator
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002225 File Offset: 0x00000425
		public override void Configure()
		{
			base.Bind<DuplicateEntityNameStatus>().AsTransient();
			base.Bind<EntityNameDialog>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(EntityNamingUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000225C File Offset: 0x0000045C
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<UniquelyNamedEntity, DuplicateEntityNameStatus>();
			return builder.Build();
		}
	}
}
