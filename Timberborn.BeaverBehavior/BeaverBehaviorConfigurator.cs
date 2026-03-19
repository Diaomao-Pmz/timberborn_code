using System;
using Bindito.Core;
using Timberborn.BeaverContaminationSystem;
using Timberborn.Beavers;
using Timberborn.BehaviorSystem;
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
using Timberborn.SleepSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WalkingSystem;
using Timberborn.Wandering;
using Timberborn.Workshops;
using Timberborn.WorkSystem;
using Timberborn.Yielding;

namespace Timberborn.BeaverBehavior
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class BeaverBehaviorConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<BeaverBehaviorInitializer>().AsTransient();
			base.Bind<BeaverNeedBehaviorPicker>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BeaverBehaviorConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F5 File Offset: 0x000002F5
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BeaverSpec, BeaverBehaviorInitializer>();
			builder.AddDecorator<BeaverSpec, BeaverNeedBehaviorPicker>();
			builder.AddDecorator<BeaverSpec, BehaviorManager>();
			BeaverBehaviorConfigurator.AddExecutors(builder);
			BeaverBehaviorConfigurator.AddBehaviors(builder);
			return builder.Build();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002120 File Offset: 0x00000320
		public static void AddExecutors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<BeaverSpec, WalkToPositionExecutor>();
			builder.AddDecorator<BeaverSpec, WalkInsideExecutor>();
			builder.AddDecorator<BeaverSpec, ApplyEffectExecutor>();
			builder.AddDecorator<BeaverSpec, WalkToAccessibleExecutor>();
			builder.AddDecorator<BeaverSpec, AnimateExecutor>();
			builder.AddDecorator<BeaverSpec, WaitExecutor>();
			builder.AddDecorator<AdultSpec, WorkExecutor>();
			builder.AddDecorator<AdultSpec, ProduceExecutor>();
			builder.AddDecorator<AdultSpec, PlantExecutor>();
			builder.AddDecorator<AdultSpec, WalkToReservableExecutor>();
			builder.AddDecorator<AdultSpec, RemoveYieldExecutor>();
			builder.AddDecorator<AdultSpec, BuildExecutor>();
			builder.AddDecorator<AdultSpec, DemolishExecutor>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000217C File Offset: 0x0000037C
		public static void AddBehaviors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<BeaverSpec, CharacterControlRootBehavior>();
			builder.AddDecorator<BeaverSpec, DeadRootBehavior>();
			builder.AddDecorator<AdultSpec, CarryRootBehavior>();
			builder.AddDecorator<Child, ChildRootBehavior>();
			builder.AddDecorator<BeaverSpec, DieRootBehavior>();
			builder.AddDecorator<BeaverSpec, ContaminateRootBehavior>();
			builder.AddDecorator<BeaverSpec, CriticalNeederRootBehavior>();
			builder.AddDecorator<BeaverSpec, StrandedRootBehavior>();
			builder.AddDecorator<AdultSpec, WorkerRootBehavior>();
			builder.AddDecorator<BeaverSpec, NeederRootBehavior>();
			builder.AddDecorator<BeaverSpec, SleepNeedBehavior>();
			builder.AddDecorator<BeaverSpec, WanderRootBehavior>();
		}
	}
}
