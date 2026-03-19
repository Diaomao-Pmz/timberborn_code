using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.Goods;
using UnityEngine;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x02000016 RID: 22
	public class StockpileGoodColumnVisualizer : BaseComponent, IAwakableComponent, IStockpileVisualizer
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000037CC File Offset: 0x000019CC
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000037D4 File Offset: 0x000019D4
		public GoodVisualizationSpec CurrentVisualization { get; private set; }

		// Token: 0x06000086 RID: 134 RVA: 0x000037DD File Offset: 0x000019DD
		public StockpileGoodColumnVisualizer(GoodVisualizationSpecService goodVisualizationSpecService, GoodColumnVariantsService goodColumnVariantsService)
		{
			this._goodVisualizationSpecService = goodVisualizationSpecService;
			this._goodColumnVariantsService = goodColumnVariantsService;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000037F3 File Offset: 0x000019F3
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._goodVisualization = base.GetComponent<GoodVisualization>();
			this._stockpileGoodColumnVisualizerSpec = base.GetComponent<StockpileGoodColumnVisualizerSpec>();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003819 File Offset: 0x00001A19
		public bool CanVisualize(string stockpileVisualization)
		{
			return this.GoodVisualizationId == stockpileVisualization;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003828 File Offset: 0x00001A28
		public void Initialize(GoodSpec goodSpec, int capacity)
		{
			this.CurrentVisualization = this._goodVisualizationSpecService.GetVisualization(this.GoodVisualizationId, this._stockpileGoodColumnVisualizerSpec.GoodVisualizationVariant);
			this._goodSpec = goodSpec;
			this.CalculateAmounts(capacity);
			this._goodVisualization.SetMaterial(this.CurrentVisualization.Material.Asset, this._stockpileGoodColumnVisualizerSpec.CenterOffset.z);
			this._goodVisualization.SetIcon(goodSpec);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000389C File Offset: 0x00001A9C
		public void UpdateAmount(int amountInStock)
		{
			int num = Math.Min(Mathf.CeilToInt((float)amountInStock * this._capacityFactor), this._maxNumberOfVisualizedGoods);
			float num2 = (float)(num / this._perLevelAmount) * this.CurrentVisualization.Offset.z;
			Vector3 localPosition = CoordinateSystem.GridToWorld(this._stockpileGoodColumnVisualizerSpec.CenterOffset) + new Vector3(0f, num2);
			this._goodVisualization.SetLocalPosition(localPosition);
			int amount = num % this._perLevelAmount;
			Mesh variant = this._goodColumnVariantsService.GetVariant(this, amount);
			this._goodVisualization.SetMesh(variant);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000392C File Offset: 0x00001B2C
		public void Clear()
		{
			this.CurrentVisualization = null;
			this._goodSpec = null;
			this._perLevelAmount = 0;
			this._maxNumberOfVisualizedGoods = 0;
			this._capacityFactor = 0f;
			this._goodVisualization.Clear();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003960 File Offset: 0x00001B60
		public void OverrideColor(Color color)
		{
			this._goodVisualization.SetMaterial(this.CurrentVisualization.Material.Asset, this._stockpileGoodColumnVisualizerSpec.CenterOffset.z);
			this._goodVisualization.SetIcon(this._goodSpec, color);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000399F File Offset: 0x00001B9F
		public string GoodVisualizationId
		{
			get
			{
				return this._stockpileGoodColumnVisualizerSpec.GoodVisualizationId;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000039AC File Offset: 0x00001BAC
		public void CalculateAmounts(int capacity)
		{
			int num = this._blockObject.Blocks.GetOccupiedCoordinates().Count((Vector3Int coords) => coords.z == this._blockObject.BaseZ);
			this._perLevelAmount = 9 * num;
			this._maxNumberOfVisualizedGoods = this._perLevelAmount * this.CurrentVisualization.LimitingAmountFlooredToInt;
			this._capacityFactor = (float)this._maxNumberOfVisualizedGoods / (float)capacity;
		}

		// Token: 0x04000051 RID: 81
		public readonly GoodVisualizationSpecService _goodVisualizationSpecService;

		// Token: 0x04000052 RID: 82
		public readonly GoodColumnVariantsService _goodColumnVariantsService;

		// Token: 0x04000053 RID: 83
		public BlockObject _blockObject;

		// Token: 0x04000054 RID: 84
		public GoodVisualization _goodVisualization;

		// Token: 0x04000055 RID: 85
		public StockpileGoodColumnVisualizerSpec _stockpileGoodColumnVisualizerSpec;

		// Token: 0x04000056 RID: 86
		public GoodSpec _goodSpec;

		// Token: 0x04000057 RID: 87
		public int _perLevelAmount;

		// Token: 0x04000058 RID: 88
		public float _capacityFactor;

		// Token: 0x04000059 RID: 89
		public int _maxNumberOfVisualizedGoods;
	}
}
