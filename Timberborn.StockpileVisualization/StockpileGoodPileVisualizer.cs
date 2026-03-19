using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Goods;
using UnityEngine;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x02000018 RID: 24
	public class StockpileGoodPileVisualizer : BaseComponent, IAwakableComponent, IStockpileVisualizer
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003C4D File Offset: 0x00001E4D
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003C55 File Offset: 0x00001E55
		public GoodVisualizationSpec CurrentVisualization { get; private set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00003C5E File Offset: 0x00001E5E
		public StockpileGoodPileVisualizer(IRandomNumberGenerator randomNumberGenerator, GoodVisualizationSpecService goodVisualizationSpecService, GoodPileVariantsService goodPileVariantsService)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._goodVisualizationSpecService = goodVisualizationSpecService;
			this._goodPileVariantsService = goodPileVariantsService;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003C7B File Offset: 0x00001E7B
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._goodVisualization = base.GetComponent<GoodVisualization>();
			this._stockpileGoodPileVisualizerSpec = base.GetComponent<StockpileGoodPileVisualizerSpec>();
			this._rotated = this._randomNumberGenerator.CheckProbability(0.5f);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public bool CanVisualize(string stockpileVisualization)
		{
			ImmutableArray<string> goodPileVisualizations = this._stockpileGoodPileVisualizerSpec.GoodPileVisualizations;
			for (int i = 0; i < goodPileVisualizations.Length; i++)
			{
				if (goodPileVisualizations[i] == stockpileVisualization)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public void Initialize(GoodSpec goodSpec, int capacity)
		{
			this.CurrentVisualization = this._goodVisualizationSpecService.GetVisualization(goodSpec.StockpileVisualization, "");
			this.CalculateAmounts(capacity);
			this._goodVisualization.SetMaterial(this.CurrentVisualization.Material.Asset, this._stockpileGoodPileVisualizerSpec.CenterOffset.z);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003D54 File Offset: 0x00001F54
		public void UpdateAmount(int amountInStock)
		{
			int num = Math.Min(amountInStock, this._maxNumberOfVisualizedGoods);
			int num2 = num / this._perLevelAmount;
			float num3 = (float)num2 * this.CurrentVisualization.Offset.z - 0.01f;
			Vector3 localPosition = CoordinateSystem.GridToWorld(this._stockpileGoodPileVisualizerSpec.CenterOffset) + new Vector3(0f, num3);
			this._goodVisualization.SetLocalPosition(localPosition);
			int amount = num % this._perLevelAmount;
			bool rotated = this._rotated ? (num2 % 2 == 0) : (num2 % 2 != 0);
			Mesh variant = this._goodPileVariantsService.GetVariant(this, amount, rotated);
			this._goodVisualization.SetMesh(variant);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003DFB File Offset: 0x00001FFB
		public void Clear()
		{
			this.CurrentVisualization = null;
			this._perLevelAmount = 0;
			this._maxNumberOfVisualizedGoods = 0;
			this._goodVisualization.Clear();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003E20 File Offset: 0x00002020
		public void CalculateAmounts(int capacity)
		{
			int num = this._blockObject.Blocks.GetOccupiedCoordinates().Count((Vector3Int coords) => coords.z == this._blockObject.BaseZ);
			this._perLevelAmount = this.CurrentVisualization.LimitingAmountFlooredToInt * num;
			this._maxNumberOfVisualizedGoods = capacity;
		}

		// Token: 0x0400005E RID: 94
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400005F RID: 95
		public readonly GoodVisualizationSpecService _goodVisualizationSpecService;

		// Token: 0x04000060 RID: 96
		public readonly GoodPileVariantsService _goodPileVariantsService;

		// Token: 0x04000061 RID: 97
		public BlockObject _blockObject;

		// Token: 0x04000062 RID: 98
		public GoodVisualization _goodVisualization;

		// Token: 0x04000063 RID: 99
		public StockpileGoodPileVisualizerSpec _stockpileGoodPileVisualizerSpec;

		// Token: 0x04000064 RID: 100
		public int _maxNumberOfVisualizedGoods;

		// Token: 0x04000065 RID: 101
		public bool _rotated;

		// Token: 0x04000066 RID: 102
		public int _perLevelAmount;
	}
}
