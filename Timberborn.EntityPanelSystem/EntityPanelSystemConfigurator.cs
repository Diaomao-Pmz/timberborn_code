using System;
using Bindito.Core;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x02000012 RID: 18
	[Context("Game")]
	[Context("MapEditor")]
	public class EntityPanelSystemConfigurator : Configurator
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003AEC File Offset: 0x00001CEC
		public override void Configure()
		{
			base.Bind<LabeledEntityBadge>().AsTransient();
			base.Bind<DebugFragmentFactory>().AsSingleton();
			base.Bind<EntityBadgeService>().AsSingleton();
			base.Bind<EntityDescriptionService>().AsSingleton();
			base.Bind<IEntityPanel>().To<EntityPanel>().AsSingleton();
			base.Bind<UnselectObjectFragment>().AsSingleton();
			base.Bind<FollowObjectFragment>().AsSingleton();
			base.Bind<ProductionItemFactory>().AsSingleton();
			base.Bind<DiagnosticFragmentController>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<EntityPanelSystemConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000013 RID: 19
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000096 RID: 150 RVA: 0x00003B83 File Offset: 0x00001D83
			public EntityPanelModuleProvider(UnselectObjectFragment unselectObjectFragment, FollowObjectFragment followObjectFragment)
			{
				this._unselectObjectFragment = unselectObjectFragment;
				this._followObjectFragment = followObjectFragment;
			}

			// Token: 0x06000097 RID: 151 RVA: 0x00003B99 File Offset: 0x00001D99
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddRightHeaderFragment(this._unselectObjectFragment, 0);
				builder.AddRightHeaderFragment(this._followObjectFragment, 10);
				return builder.Build();
			}

			// Token: 0x0400006E RID: 110
			public readonly UnselectObjectFragment _unselectObjectFragment;

			// Token: 0x0400006F RID: 111
			public readonly FollowObjectFragment _followObjectFragment;
		}
	}
}
