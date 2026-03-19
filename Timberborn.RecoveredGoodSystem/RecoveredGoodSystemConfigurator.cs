using System;
using Bindito.Core;
using Timberborn.BuilderHubSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.ModelHiding;
using Timberborn.StatusSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x0200001A RID: 26
	[Context("Game")]
	public class RecoveredGoodSystemConfigurator : Configurator
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003A20 File Offset: 0x00001C20
		public override void Configure()
		{
			base.Bind<RecoveredGoodStackCarryingBehavior>().AsTransient();
			base.Bind<NoStorageStatus>().AsTransient();
			base.Bind<PrioritizedRecoveredGoodStackRegistrar>().AsTransient();
			base.Bind<RecoveredGoodStack>().AsTransient();
			base.Bind<RecoveredGoodStackAccessible>().AsTransient();
			base.Bind<RecoveredGoodStackDisintegration>().AsTransient();
			base.Bind<RecoveredGoodStackModel>().AsTransient();
			base.Bind<RecoveredGoodStackMover>().AsTransient();
			base.Bind<BuildingGoodsRecoveryService>().AsSingleton();
			base.Bind<RecoveredGoodStackCoordinatesFinder>().AsSingleton();
			base.Bind<RecoveredGoodStackFactory>().AsSingleton();
			base.Bind<PrioritizedRecoveredGoodStackRegistry>().AsSingleton();
			base.Bind<RecoveredGoodStackSpawner>().AsSingleton();
			base.MultiBind<IBuilderJobProvider>().To<RecoverGoodStackJobProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(RecoveredGoodSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<RecoveredGoodStackSpec, RecoveredGoodStack>();
			builder.AddDecorator<RecoveredGoodStack, BuilderPrioritizable>();
			builder.AddDecorator<RecoveredGoodStack, NoStorageStatus>();
			builder.AddDecorator<RecoveredGoodStack, RecoveredGoodStackAccessible>();
			builder.AddDecorator<RecoveredGoodStack, RecoveredGoodStackMover>();
			builder.AddDecorator<RecoveredGoodStack, PrioritizedRecoveredGoodStackRegistrar>();
			builder.AddDecorator<RecoveredGoodStack, StatusSubject>();
			builder.AddDecorator<RecoveredGoodStack, HidabilityPositionUpdater>();
			builder.AddDecorator<RecoveredGoodStackDisintegrationSpec, RecoveredGoodStackDisintegration>();
			builder.AddDecorator<Worker, RecoveredGoodStackCarryingBehavior>();
			builder.AddDecorator<RecoveredGoodStackModelSpec, RecoveredGoodStackModel>();
			return builder.Build();
		}
	}
}
