using System;
using Bindito.Core;
using Timberborn.BehaviorSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000022 RID: 34
	[Context("Game")]
	public class WalkingSystemConfigurator : Configurator
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00003FD0 File Offset: 0x000021D0
		public override void Configure()
		{
			base.Bind<WalkInsideExecutor>().AsTransient();
			base.Bind<WalkToAccessibleExecutor>().AsTransient();
			base.Bind<WalkToPositionExecutor>().AsTransient();
			base.Bind<Walker>().AsTransient();
			base.Bind<NavMeshObserver>().AsTransient();
			base.Bind<WalkerMover>().AsTransient();
			base.Bind<WalkerSpeedManager>().AsTransient();
			base.Bind<NavMeshProximityValidator>().AsTransient();
			base.Bind<OccupiedAccessiblePathStart>().AsTransient();
			base.Bind<RunningStateUpdater>().AsTransient();
			base.Bind<SwimmingAnimator>().AsTransient();
			base.Bind<WalkerPathStart>().AsTransient();
			base.Bind<WalkingEnforcer>().AsTransient();
			base.Bind<PositionDestinationFactory>().AsSingleton();
			base.Bind<WalkerService>().AsSingleton();
			base.Bind<RandomDestinationPicker>().AsSingleton();
			base.Bind<DestinationValueSerializer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WalkingSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000040C8 File Offset: 0x000022C8
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<WalkerSpeedManagerSpec, WalkerSpeedManager>();
			builder.AddDecorator<WalkerSpeedManager, Walker>();
			builder.AddDecorator<Walker, NavMeshObserver>();
			builder.AddDecorator<Walker, OccupiedAccessiblePathStart>();
			builder.AddDecorator<Walker, WalkerPathStart>();
			builder.AddDecorator<Walker, NavMeshProximityValidator>();
			builder.AddDecorator<RunningStateUpdaterSpec, RunningStateUpdater>();
			builder.AddDecorator<RunningStateUpdater, WalkingEnforcer>();
			builder.AddDecorator<BehaviorManager, WalkerMover>();
			builder.AddDecorator<SwimmingAnimatorSpec, SwimmingAnimator>();
			return builder.Build();
		}
	}
}
