using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.StockpilePriorityUISystem
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class StockpilePriorityUISystemConfigurator : Configurator
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000025F8 File Offset: 0x000007F8
		public override void Configure()
		{
			base.Bind<StockpilePriorityFragment>().AsSingleton();
			base.Bind<StockpilePriorityToggleFactory>().AsSingleton();
			base.Bind<StockpilePriorityBatchControlRowItemFactory>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<StockpilePriorityUISystemConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000A RID: 10
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000026 RID: 38 RVA: 0x00002637 File Offset: 0x00000837
			public EntityPanelModuleProvider(StockpilePriorityFragment stockpilePriorityFragment)
			{
				this._stockpilePriorityFragment = stockpilePriorityFragment;
			}

			// Token: 0x06000027 RID: 39 RVA: 0x00002646 File Offset: 0x00000846
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._stockpilePriorityFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000024 RID: 36
			public readonly StockpilePriorityFragment _stockpilePriorityFragment;
		}
	}
}
