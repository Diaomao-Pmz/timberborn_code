using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using UnityEngine;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x02000015 RID: 21
	public class StockpileBannerSetter : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000036A1 File Offset: 0x000018A1
		public StockpileBannerSetter(GoodIconVisualizer goodIconVisualizer, IGoodService goodService)
		{
			this._goodIconVisualizer = goodIconVisualizer;
			this._goodService = goodService;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000036B8 File Offset: 0x000018B8
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._singleGoodAllower = base.GetComponent<SingleGoodAllower>();
			BuildingModel component = base.GetComponent<BuildingModel>();
			this._meshRenderer = component.FinishedModel.GetComponentInChildren<MeshRenderer>();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000036F5 File Offset: 0x000018F5
		public void OnEnterFinishedState()
		{
			this._singleGoodAllower.DisallowedGoodsChanged += this.OnDisallowedGoodsChanged;
			this.UpdateProperties();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003714 File Offset: 0x00001914
		public void OnExitFinishedState()
		{
			this._singleGoodAllower.DisallowedGoodsChanged -= this.OnDisallowedGoodsChanged;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003730 File Offset: 0x00001930
		public void UpdateProperties()
		{
			if (this._singleGoodAllower.HasAllowedGood)
			{
				string allowedGood = this._singleGoodAllower.AllowedGood;
				GoodSpec good = this._goodService.GetGood(allowedGood);
				this._goodIconVisualizer.ShowColoredIcon(this._meshRenderer.material, good, this._blockObject.FlipMode.IsFlipped, StockpileBannerSetter.BannerIconColor);
				return;
			}
			this._goodIconVisualizer.HideColoredIcon(this._meshRenderer.material);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000037A9 File Offset: 0x000019A9
		public void OnDisallowedGoodsChanged(object sender, DisallowedGoodsChangedEventArgs e)
		{
			this.UpdateProperties();
		}

		// Token: 0x0400004A RID: 74
		public static readonly Color BannerIconColor = new Color(0.33f, 0.33f, 0.33f);

		// Token: 0x0400004B RID: 75
		public readonly GoodIconVisualizer _goodIconVisualizer;

		// Token: 0x0400004C RID: 76
		public readonly IGoodService _goodService;

		// Token: 0x0400004D RID: 77
		public BlockObject _blockObject;

		// Token: 0x0400004E RID: 78
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x0400004F RID: 79
		public MeshRenderer _meshRenderer;
	}
}
