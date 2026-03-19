using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using Timberborn.Goods;
using UnityEngine;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x0200001B RID: 27
	public class StockpilePlaneVisualizer : BaseComponent, IAwakableComponent, IStockpileVisualizer
	{
		// Token: 0x060000CF RID: 207 RVA: 0x000042ED File Offset: 0x000024ED
		public StockpilePlaneVisualizer(GoodVisualizationSpecService goodVisualizationSpecService)
		{
			this._goodVisualizationSpecService = goodVisualizationSpecService;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000042FC File Offset: 0x000024FC
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
			this._goodVisualization = base.GetComponent<GoodVisualization>();
			this._stockpilePlaneVisualizerSpec = base.GetComponent<StockpilePlaneVisualizerSpec>();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004330 File Offset: 0x00002530
		public bool CanVisualize(string stockpileVisualization)
		{
			return this.Visualizations.Any((StockpilePlaneVisualization v) => v.GoodVisualizationId == stockpileVisualization);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004364 File Offset: 0x00002564
		public void Initialize(GoodSpec goodSpec, int capacity)
		{
			this._currentPlaneVisualization = this.GetPlaneVisualization(goodSpec.StockpileVisualization);
			this._currentVisualization = this._goodVisualizationSpecService.GetVisualization(this._currentPlaneVisualization.GoodVisualizationId, this._currentPlaneVisualization.GoodVisualizationVariant);
			this._maxGoodAmount = capacity;
			this._goodVisualization.SetLocalPosition(CoordinateSystem.GridToWorld(this._currentPlaneVisualization.CenterOffset));
			this.SetMaterial(goodSpec);
			this._goodVisualization.SetMesh(this._currentVisualization.PrimaryMesh.Asset);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000043F0 File Offset: 0x000025F0
		public void UpdateAmount(int amountInStock)
		{
			float num = Mathf.Clamp01((float)amountInStock / (float)this._maxGoodAmount);
			float nonLinearity = this._currentVisualization.NonLinearity;
			if (nonLinearity != 0f)
			{
				num = (float)Math.Pow((double)num, (double)(nonLinearity + 1f));
			}
			this.SetTargetHeight(num);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004439 File Offset: 0x00002639
		public void Clear()
		{
			this._currentVisualization = null;
			this._maxGoodAmount = 0;
			this._goodVisualization.Clear();
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004454 File Offset: 0x00002654
		public ImmutableArray<StockpilePlaneVisualization> Visualizations
		{
			get
			{
				return this._stockpilePlaneVisualizerSpec.StockpilePlaneVisualizations;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004464 File Offset: 0x00002664
		public StockpilePlaneVisualization GetPlaneVisualization(string stockpileVisualization)
		{
			return this.Visualizations.Single((StockpilePlaneVisualization v) => v.GoodVisualizationId == stockpileVisualization);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004498 File Offset: 0x00002698
		public void SetMaterial(GoodSpec goodSpec)
		{
			AssetRef<Material> assetRef = goodSpec.ContainerMaterial ?? this._currentVisualization.Material;
			this._goodVisualization.SetMaterial(assetRef.Asset, this._currentPlaneVisualization.CenterOffset.z);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000044DC File Offset: 0x000026DC
		public void SetTargetHeight(float inventoryFillProgress)
		{
			Vector2 movementRange = this._currentPlaneVisualization.MovementRange;
			float num = (this._currentVisualization.LimitingAmount != 0f) ? Math.Min(movementRange.y, this._currentVisualization.LimitingAmount) : movementRange.y;
			float num2 = Mathf.Lerp(movementRange.x, num, inventoryFillProgress);
			Vector3 position = CoordinateSystem.GridToWorld(this._blockObjectCenter.GridCenterGrounded + this._currentPlaneVisualization.CenterOffset + this._currentVisualization.Offset + Vector3.forward * num2);
			Quaternion quaternion = this._blockObject.Orientation.ToWorldSpaceRotation();
			this._goodVisualization.SetPositionAndRotation(position, quaternion);
		}

		// Token: 0x0400006D RID: 109
		public readonly GoodVisualizationSpecService _goodVisualizationSpecService;

		// Token: 0x0400006E RID: 110
		public BlockObject _blockObject;

		// Token: 0x0400006F RID: 111
		public BlockObjectCenter _blockObjectCenter;

		// Token: 0x04000070 RID: 112
		public GoodVisualization _goodVisualization;

		// Token: 0x04000071 RID: 113
		public StockpilePlaneVisualizerSpec _stockpilePlaneVisualizerSpec;

		// Token: 0x04000072 RID: 114
		public GoodVisualizationSpec _currentVisualization;

		// Token: 0x04000073 RID: 115
		public StockpilePlaneVisualization _currentPlaneVisualization;

		// Token: 0x04000074 RID: 116
		public int _maxGoodAmount;
	}
}
