using System;
using Bindito.Core;
using Timberborn.BehaviorSystem;
using Timberborn.Bots;
using Timberborn.Carrying;
using Timberborn.CharacterControlSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.ConstructionSites;
using Timberborn.DeathSystem;
using Timberborn.Demolishing;
using Timberborn.MortalSystem;
using Timberborn.NeedBehaviorSystem;
using Timberborn.Planting;
using Timberborn.ReservableSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WalkingSystem;
using Timberborn.Wandering;
using Timberborn.Workshops;
using Timberborn.WorkSystem;
using Timberborn.Yielding;

namespace Timberborn.BotBehavior
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class BotBehaviorConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<BotBehaviorInitializer>().AsTransient();
			base.Bind<BotNeedBehaviorPicker>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BotBehaviorConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F5 File Offset: 0x000002F5
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BotSpec, BotBehaviorInitializer>();
			builder.AddDecorator<BotSpec, BotNeedBehaviorPicker>();
			builder.AddDecorator<BotSpec, BehaviorManager>();
			BotBehaviorConfigurator.AddExecutors(builder);
			BotBehaviorConfigurator.AddBehaviors(builder);
			return builder.Build();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002120 File Offset: 0x00000320
		public static void AddExecutors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<BotSpec, WalkToPositionExecutor>();
			builder.AddDecorator<BotSpec, WalkInsideExecutor>();
			builder.AddDecorator<BotSpec, ApplyEffectExecutor>();
			builder.AddDecorator<BotSpec, WalkToAccessibleExecutor>();
			builder.AddDecorator<BotSpec, AnimateExecutor>();
			builder.AddDecorator<BotSpec, WaitExecutor>();
			builder.AddDecorator<BotSpec, WorkExecutor>();
			builder.AddDecorator<BotSpec, ProduceExecutor>();
			builder.AddDecorator<BotSpec, PlantExecutor>();
			builder.AddDecorator<BotSpec, WalkToReservableExecutor>();
			builder.AddDecorator<BotSpec, RemoveYieldExecutor>();
			builder.AddDecorator<BotSpec, BuildExecutor>();
			builder.AddDecorator<BotSpec, DemolishExecutor>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000217B File Offset: 0x0000037B
		public static void AddBehaviors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<BotSpec, CharacterControlRootBehavior>();
			builder.AddDecorator<BotSpec, DeadRootBehavior>();
			builder.AddDecorator<BotSpec, CarryRootBehavior>();
			builder.AddDecorator<BotSpec, DieRootBehavior>();
			builder.AddDecorator<BotSpec, CriticalNeederRootBehavior>();
			builder.AddDecorator<BotSpec, StrandedRootBehavior>();
			builder.AddDecorator<BotSpec, NeederRootBehavior>();
			builder.AddDecorator<BotSpec, WorkerRootBehavior>();
			builder.AddDecorator<BotSpec, WanderRootBehavior>();
		}
	}
}
