using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.HaulingUI
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class HaulingUIConfigurator : Configurator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000024E1 File Offset: 0x000006E1
		public override void Configure()
		{
			base.Bind<HaulCandidateDebugFragment>().AsSingleton();
			base.Bind<HaulCandidateFragment>().AsSingleton();
			base.Bind<HaulCandidateBatchControlRowItemFactory>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<HaulingUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000A RID: 10
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600001E RID: 30 RVA: 0x00002520 File Offset: 0x00000720
			public EntityPanelModuleProvider(HaulCandidateDebugFragment haulCandidateDebugFragment, HaulCandidateFragment haulCandidateFragment)
			{
				this._haulCandidateDebugFragment = haulCandidateDebugFragment;
				this._haulCandidateFragment = haulCandidateFragment;
			}

			// Token: 0x0600001F RID: 31 RVA: 0x00002536 File Offset: 0x00000736
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddDiagnosticFragment(this._haulCandidateDebugFragment);
				builder.AddMiddleFragment(this._haulCandidateFragment, 10);
				return builder.Build();
			}

			// Token: 0x04000019 RID: 25
			public readonly HaulCandidateDebugFragment _haulCandidateDebugFragment;

			// Token: 0x0400001A RID: 26
			public readonly HaulCandidateFragment _haulCandidateFragment;
		}
	}
}
