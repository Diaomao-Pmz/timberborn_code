using System;
using Bindito.Core;
using Timberborn.BlockSystemUI;
using Timberborn.EntityPanelSystem;
using Timberborn.RecoveredGoodSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.RecoveredGoodSystemUI
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class RecoveredGoodSystemUIConfigurator : Configurator
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002550 File Offset: 0x00000750
		public override void Configure()
		{
			base.Bind<DeleteRecoveredGoodStackFragment>().AsSingleton();
			base.Bind<RecoveredGoodStackFragment>().AsSingleton();
			base.Bind<RecoveredGoodStackDisintegrationFragment>().AsSingleton();
			base.Bind<RecoveredGoodStackDeletionTool>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<RecoveredGoodSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(RecoveredGoodSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025BB File Offset: 0x000007BB
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<RecoveredGoodStack, LabeledEntityBadge>();
			builder.AddDecorator<RecoveredGoodStack, PlaceableBlockObjectDescriber>();
			builder.AddDecorator<RecoveredGoodStackDisintegration, RecoveredGoodStackDisintegrationFragment>();
			return builder.Build();
		}

		// Token: 0x02000009 RID: 9
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000021 RID: 33 RVA: 0x000025E1 File Offset: 0x000007E1
			public EntityPanelModuleProvider(DeleteRecoveredGoodStackFragment deleteRecoveredGoodStackFragment, RecoveredGoodStackFragment recoveredGoodStackFragment, RecoveredGoodStackDisintegrationFragment recoveredGoodStackDisintegrationFragment)
			{
				this._deleteRecoveredGoodStackFragment = deleteRecoveredGoodStackFragment;
				this._recoveredGoodStackFragment = recoveredGoodStackFragment;
				this._recoveredGoodStackDisintegrationFragment = recoveredGoodStackDisintegrationFragment;
			}

			// Token: 0x06000022 RID: 34 RVA: 0x000025FE File Offset: 0x000007FE
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddLeftHeaderFragment(this._deleteRecoveredGoodStackFragment, 0);
				builder.AddTopFragment(this._recoveredGoodStackFragment, 0);
				builder.AddMiddleFragment(this._recoveredGoodStackDisintegrationFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000023 RID: 35
			public readonly DeleteRecoveredGoodStackFragment _deleteRecoveredGoodStackFragment;

			// Token: 0x04000024 RID: 36
			public readonly RecoveredGoodStackFragment _recoveredGoodStackFragment;

			// Token: 0x04000025 RID: 37
			public readonly RecoveredGoodStackDisintegrationFragment _recoveredGoodStackDisintegrationFragment;
		}
	}
}
