using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.RuinsModelShuffling
{
	// Token: 0x02000005 RID: 5
	[Context("MapEditor")]
	public class RuinsModelShufflingConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000219C File Offset: 0x0000039C
		public override void Configure()
		{
			base.Bind<RuinModelShufflingFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<RuinsModelShufflingConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000006 RID: 6
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600000B RID: 11 RVA: 0x000021C3 File Offset: 0x000003C3
			public EntityPanelModuleProvider(RuinModelShufflingFragment ruinModelShufflingFragment)
			{
				this._ruinModelShufflingFragment = ruinModelShufflingFragment;
			}

			// Token: 0x0600000C RID: 12 RVA: 0x000021D2 File Offset: 0x000003D2
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._ruinModelShufflingFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400000C RID: 12
			public readonly RuinModelShufflingFragment _ruinModelShufflingFragment;
		}
	}
}
