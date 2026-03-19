using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.WorkerOutfitSystem
{
	// Token: 0x02000011 RID: 17
	[Context("Game")]
	public class WorkerOutfitSystemConfigurator : Configurator
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002F8C File Offset: 0x0000118C
		public override void Configure()
		{
			base.Bind<WorkerOutfitAnimationAttachmentVisibility>().AsTransient();
			base.Bind<WorkerOutfitAttachmentVisualizer>().AsTransient();
			base.Bind<WorkerOutfitChangeNotifier>().AsTransient();
			base.Bind<WorkerOutfitTextureSetter>().AsTransient();
			base.Bind<WorkerOutfitService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WorkerOutfitSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002FF2 File Offset: 0x000011F2
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Worker, WorkerOutfitChangeNotifier>();
			builder.AddDecorator<WorkerOutfitChangeNotifier, WorkerOutfitAttachmentVisualizer>();
			builder.AddDecorator<WorkerOutfitChangeNotifier, WorkerOutfitTextureSetter>();
			builder.AddDecorator<WorkerOutfitAnimationAttachmentVisibilitySpec, WorkerOutfitAnimationAttachmentVisibility>();
			return builder.Build();
		}
	}
}
