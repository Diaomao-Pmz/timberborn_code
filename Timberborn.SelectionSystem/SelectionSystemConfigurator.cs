using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;
using Timberborn.TemplateSystem;

namespace Timberborn.SelectionSystem
{
	// Token: 0x0200001B RID: 27
	[Context("Game")]
	[Context("MapEditor")]
	public class SelectionSystemConfigurator : Configurator
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003910 File Offset: 0x00001B10
		public override void Configure()
		{
			base.Bind<SelectableObject>().AsTransient();
			base.Bind<BoxColliderAdder>().AsTransient();
			base.Bind<HighlightableObject>().AsTransient();
			base.Bind<RollingHighlighter>().AsTransient();
			base.Bind<Highlighter>().AsTransient();
			base.Bind<EntitySelectionService>().AsSingleton();
			base.Bind<CameraTargeter>().AsSingleton();
			base.Bind<SelectableObjectRetriever>().AsSingleton();
			base.Bind<AreaHighlightingService>().AsSingleton();
			base.Bind<HighlightRenderingService>().AsSingleton();
			base.Bind<SelectableObjectRaycaster>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(SelectionSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000039BE File Offset: 0x00001BBE
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<TemplateSpec, HighlightableObject>();
			builder.AddDecorator<BoxColliderAdderSpec, BoxColliderAdder>();
			return builder.Build();
		}
	}
}
