using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.BehaviorSystemUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class BehaviorSystemUIConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000223C File Offset: 0x0000043C
		public override void Configure()
		{
			base.Bind<BehaviorManagerDebugFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<BehaviorSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000006 RID: 6
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600000B RID: 11 RVA: 0x00002263 File Offset: 0x00000463
			public EntityPanelModuleProvider(BehaviorManagerDebugFragment behaviorManagerDebugFragment)
			{
				this._behaviorManagerDebugFragment = behaviorManagerDebugFragment;
			}

			// Token: 0x0600000C RID: 12 RVA: 0x00002272 File Offset: 0x00000472
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddDiagnosticFragment(this._behaviorManagerDebugFragment);
				return builder.Build();
			}

			// Token: 0x0400000A RID: 10
			public readonly BehaviorManagerDebugFragment _behaviorManagerDebugFragment;
		}
	}
}
