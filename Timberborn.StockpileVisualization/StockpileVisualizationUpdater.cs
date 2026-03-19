using System;
using Timberborn.BaseComponentSystem;
using Timberborn.InventorySystem;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x02000020 RID: 32
	public class StockpileVisualizationUpdater : BaseComponent, IAwakableComponent
	{
		// Token: 0x060000EE RID: 238 RVA: 0x000047EA File Offset: 0x000029EA
		public void Awake()
		{
			this._stockpileBannerSetter = base.GetComponent<StockpileBannerSetter>();
			this._stockpileVisualizers = base.GetComponent<StockpileVisualizers>();
			this._singleGoodAllower = base.GetComponent<SingleGoodAllower>();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004810 File Offset: 0x00002A10
		public void UpdateVisualization()
		{
			StockpileVisualizers stockpileVisualizers = this._stockpileVisualizers;
			if (stockpileVisualizers != null)
			{
				stockpileVisualizers.SetCurrentVisualizer(this._singleGoodAllower.AllowedGood);
			}
			this._stockpileBannerSetter.UpdateProperties();
		}

		// Token: 0x04000078 RID: 120
		public StockpileBannerSetter _stockpileBannerSetter;

		// Token: 0x04000079 RID: 121
		public StockpileVisualizers _stockpileVisualizers;

		// Token: 0x0400007A RID: 122
		public SingleGoodAllower _singleGoodAllower;
	}
}
