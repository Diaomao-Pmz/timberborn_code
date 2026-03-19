using System;
using Bindito.Core;
using Timberborn.BlockObjectAccesses;
using Timberborn.Ruins;
using Timberborn.TemplateInstantiation;

namespace Timberborn.RuinsNavigation
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class RuinsNavigationConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		public override void Configure()
		{
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(RuinsNavigationConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D7 File Offset: 0x000002D7
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Ruin, BlockObjectAccessible>();
			return builder.Build();
		}
	}
}
