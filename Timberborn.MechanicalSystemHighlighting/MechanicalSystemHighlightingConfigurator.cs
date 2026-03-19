using System;
using Bindito.Core;
using Timberborn.MechanicalSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MechanicalSystemHighlighting
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	public class MechanicalSystemHighlightingConfigurator : Configurator
	{
		// Token: 0x0600002C RID: 44 RVA: 0x0000275C File Offset: 0x0000095C
		public override void Configure()
		{
			base.Bind<PreviewMechanicalNodeHighlighter>().AsTransient();
			base.Bind<MechanicalGraphHighlightService>().AsSingleton();
			base.Bind<MechanicalGraphIterator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MechanicalSystemHighlightingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027AA File Offset: 0x000009AA
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<MechanicalNode, PreviewMechanicalNodeHighlighter>();
			return builder.Build();
		}
	}
}
