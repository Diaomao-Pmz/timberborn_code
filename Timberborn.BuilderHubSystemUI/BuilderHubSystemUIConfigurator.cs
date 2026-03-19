using System;
using Bindito.Core;
using Timberborn.BuilderHubSystem;
using Timberborn.SimpleOutputBuildingsUI;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BuilderHubSystemUI
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class BuilderHubSystemUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		public override void Configure()
		{
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BuilderHubSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D7 File Offset: 0x000002D7
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BuilderHubSpec, SimpleOutputInventoryFragmentEnabler>();
			return builder.Build();
		}
	}
}
