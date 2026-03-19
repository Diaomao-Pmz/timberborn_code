using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.WellbeingUI;

namespace Timberborn.BonusSystemUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class BonusSystemUIConfigurator : Configurator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000021D0 File Offset: 0x000003D0
		public override void Configure()
		{
			base.Bind<BonusManagerDebugFragment>().AsSingleton();
			base.Bind<NeedPenaltyEffectDescriber>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<BonusSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<INeedEffectDescriber>().To<NeedPenaltyEffectDescriber>().AsSingleton();
		}

		// Token: 0x02000006 RID: 6
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600000A RID: 10 RVA: 0x00002214 File Offset: 0x00000414
			public EntityPanelModuleProvider(BonusManagerDebugFragment bonusManagerDebugFragment)
			{
				this._bonusManagerDebugFragment = bonusManagerDebugFragment;
			}

			// Token: 0x0600000B RID: 11 RVA: 0x00002223 File Offset: 0x00000423
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddDiagnosticFragment(this._bonusManagerDebugFragment);
				return builder.Build();
			}

			// Token: 0x0400000B RID: 11
			public readonly BonusManagerDebugFragment _bonusManagerDebugFragment;
		}
	}
}
