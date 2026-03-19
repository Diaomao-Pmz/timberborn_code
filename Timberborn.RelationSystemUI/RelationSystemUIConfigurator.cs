using System;
using Bindito.Core;
using Timberborn.RelationSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.RelationSystemUI
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class RelationSystemUIConfigurator : Configurator
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000023CA File Offset: 0x000005CA
		public override void Configure()
		{
			base.Bind<RelationHighlighter>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(RelationSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023F5 File Offset: 0x000005F5
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<IRelationOwner, RelationHighlighter>();
			return builder.Build();
		}
	}
}
