using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.DistributionSystemUI
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class DistributionSystemUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<DistrictCrossingFragment>().AsSingleton();
			base.Bind<DistrictCrossingInventoryFragment>().AsSingleton();
			base.Bind<ImportGoodIconFactory>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<DistributionSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000005 RID: 5
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000005 RID: 5 RVA: 0x000020FD File Offset: 0x000002FD
			public EntityPanelModuleProvider(DistrictCrossingFragment districtCrossingFragment, DistrictCrossingInventoryFragment districtCrossingInventoryFragment)
			{
				this._districtCrossingFragment = districtCrossingFragment;
				this._districtCrossingInventoryFragment = districtCrossingInventoryFragment;
			}

			// Token: 0x06000006 RID: 6 RVA: 0x00002113 File Offset: 0x00000313
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddSideFragment(this._districtCrossingFragment);
				builder.AddBottomFragment(this._districtCrossingInventoryFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000006 RID: 6
			public readonly DistrictCrossingFragment _districtCrossingFragment;

			// Token: 0x04000007 RID: 7
			public readonly DistrictCrossingInventoryFragment _districtCrossingInventoryFragment;
		}
	}
}
