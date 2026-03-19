using System;
using Bindito.Core;
using Timberborn.BlockSystemNavigation;
using Timberborn.TemplateInstantiation;

namespace Timberborn.PathSystemUI
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class PathSystemUIConfigurator : Configurator
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021C5 File Offset: 0x000003C5
		public override void Configure()
		{
			base.Bind<PathEntityBadge>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(PathSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021F0 File Offset: 0x000003F0
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BlockObjectWithPathRangeSpec, PathEntityBadge>();
			return builder.Build();
		}
	}
}
