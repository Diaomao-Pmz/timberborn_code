using System;
using Bindito.Core;
using Timberborn.Debugging;
using Timberborn.NaturalResources;
using Timberborn.NaturalResourcesModelSystem;
using Timberborn.Rendering;
using Timberborn.TemplateInstantiation;

namespace Timberborn.NaturalResourcesUI
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	[Context("MapEditor")]
	public class NaturalResourcesUIConfigurator : Configurator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000023CC File Offset: 0x000005CC
		public override void Configure()
		{
			base.Bind<NaturalResourceDescriber>().AsTransient();
			base.Bind<NaturalResourceEntityBadge>().AsTransient();
			base.Bind<NaturalResourceMarkerPositionUpdater>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(NaturalResourcesUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<IDevModule>().To<NaturalResourcesModelToggler>().AsSingleton();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000242B File Offset: 0x0000062B
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<NaturalResourceSpec, NaturalResourceEntityBadge>();
			builder.AddDecorator<NaturalResourceSpec, NaturalResourceDescriber>();
			builder.AddDecorator<NaturalResourceModel, MarkerPosition>();
			builder.AddDecorator<NaturalResourceModel, NaturalResourceMarkerPositionUpdater>();
			return builder.Build();
		}
	}
}
