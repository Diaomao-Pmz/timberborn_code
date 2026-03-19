using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.SlotSystem
{
	// Token: 0x0200001C RID: 28
	[Context("Game")]
	public class SlotSystemConfigurator : Configurator
	{
		// Token: 0x060000BB RID: 187 RVA: 0x000038E8 File Offset: 0x00001AE8
		public override void Configure()
		{
			base.Bind<FixedSlotManager>().AsTransient();
			base.Bind<PatrollingSlotInitializer>().AsTransient();
			base.Bind<PrioritySlotRetriever>().AsTransient();
			base.Bind<SlotAnimationSynchronizer>().AsTransient();
			base.Bind<SlotManager>().AsTransient();
			base.Bind<TransformSlotInitializer>().AsTransient();
			base.Bind<UnfinishedStateSlotDisabler>().AsTransient();
			base.Bind<SlotRetriever>().AsSingleton();
			base.Bind<TransformSlotFactory>().AsSingleton();
			base.Bind<PatrollingSlotFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(SlotSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000398A File Offset: 0x00001B8A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<FixedSlotManagerSpec, FixedSlotManager>();
			builder.AddDecorator<FixedSlotManager, SlotManager>();
			builder.AddDecorator<PrioritySlotRetrieverSpec, PrioritySlotRetriever>();
			builder.AddDecorator<SlotAnimationSynchronizerSpec, SlotAnimationSynchronizer>();
			builder.AddDecorator<UnfinishedStateSlotDisablerSpec, UnfinishedStateSlotDisabler>();
			builder.AddDecorator<TransformSlotInitializerSpec, TransformSlotInitializer>();
			builder.AddDecorator<PatrollingSlotInitializerSpec, PatrollingSlotInitializer>();
			return builder.Build();
		}
	}
}
