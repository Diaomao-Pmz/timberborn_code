using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.StockpileVisualizationUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class StockpileVisualizationUIConfigurator : Configurator
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000221E File Offset: 0x0000041E
		public override void Configure()
		{
			base.Bind<StockpileGoodColumnVisualizerDebugFragment>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<StockpileVisualizationUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000006 RID: 6
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600000C RID: 12 RVA: 0x00002245 File Offset: 0x00000445
			public EntityPanelModuleProvider(StockpileGoodColumnVisualizerDebugFragment stockpileGoodColumnVisualizerDebugFragment)
			{
				this._stockpileGoodColumnVisualizerDebugFragment = stockpileGoodColumnVisualizerDebugFragment;
			}

			// Token: 0x0600000D RID: 13 RVA: 0x00002254 File Offset: 0x00000454
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddDiagnosticFragment(this._stockpileGoodColumnVisualizerDebugFragment);
				return builder.Build();
			}

			// Token: 0x0400000B RID: 11
			public readonly StockpileGoodColumnVisualizerDebugFragment _stockpileGoodColumnVisualizerDebugFragment;
		}
	}
}
