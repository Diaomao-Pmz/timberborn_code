using System;
using Bindito.Core;
using Timberborn.BlockObjectAccesses;
using Timberborn.BuilderHubSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.Rendering;
using Timberborn.ReservableSystem;
using Timberborn.StatusSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.Demolishing
{
	// Token: 0x0200001A RID: 26
	[Context("Game")]
	public class DemolishingConfigurator : Configurator
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00003510 File Offset: 0x00001710
		public override void Configure()
		{
			base.Bind<DemolishBehavior>().AsTransient();
			base.Bind<DemolishExecutor>().AsTransient();
			base.Bind<Demolishable>().AsTransient();
			base.Bind<DemolishableParticleController>().AsTransient();
			base.Bind<DemolishablePrioritizableEnabler>().AsTransient();
			base.Bind<DemolishableStatusIconOffsetter>().AsTransient();
			base.Bind<Demolisher>().AsTransient();
			base.Bind<DemolishJob>().AsTransient();
			base.Bind<ReachableDemolishable>().AsTransient();
			base.Bind<AccessibleDemolishableReacher>().AsTransient();
			base.Bind<DemolishableScienceReward>().AsTransient();
			base.Bind<DemolishJobs>().AsSingleton();
			base.Bind<ReservedDemolishableSerializer>().AsSingleton();
			base.MultiBind<IBuilderJobProvider>().To<DemolishJobProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(DemolishingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000035E8 File Offset: 0x000017E8
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DemolishableSpec, Demolishable>();
			builder.AddDecorator<Demolishable, DemolishablePrioritizableEnabler>();
			builder.AddDecorator<Demolishable, DemolishJob>();
			builder.AddDecorator<Demolishable, ReachableDemolishable>();
			builder.AddDecorator<Demolishable, Reservable>();
			builder.AddDecorator<Demolishable, StatusSubject>();
			builder.AddDecorator<DemolishablePrioritizableEnabler, BuilderPrioritizable>();
			builder.AddDecorator<Demolisher, DemolishBehavior>();
			builder.AddDecorator<DemolishableFromTopSpec, AccessibleDemolishableReacher>();
			builder.AddDecorator<DemolishableFromTopSpec, BlockObjectAccessible>();
			builder.AddDecorator<DemolishableFromTopSpec, HighBlockObjectAccessesAdder>();
			builder.AddDecorator<Worker, Demolisher>();
			builder.AddDecorator<DemolishableParticleControllerSpec, DemolishableParticleController>();
			builder.AddDecorator<DemolishableStatusIconOffsetter, MarkerPosition>();
			builder.AddDecorator<DemolishableStatusIconOffsetterSpec, DemolishableStatusIconOffsetter>();
			builder.AddDecorator<DemolishableScienceRewardSpec, DemolishableScienceReward>();
			return builder.Build();
		}
	}
}
