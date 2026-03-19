using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.BuildingRange;
using Timberborn.Common;
using Timberborn.Rendering;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.RangedEffectBuildingUI
{
	// Token: 0x0200000C RID: 12
	public class RangeTileMarkerService : ILoadableSingleton
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002884 File Offset: 0x00000A84
		public RangeTileMarkerService(AreaTileDrawerFactory areaTileDrawerFactory, ISpecService specService, RootObjectProvider rootObjectProvider)
		{
			this._areaTileDrawerFactory = areaTileDrawerFactory;
			this._specService = specService;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000028C4 File Offset: 0x00000AC4
		public void Load()
		{
			this._parent = this._rootObjectProvider.CreateRootObject("RangeTileMarkerService");
			RangedEffectBuildingColorsSpec singleSpec = this._specService.GetSingleSpec<RangedEffectBuildingColorsSpec>();
			this._areaTileDrawer = this._areaTileDrawerFactory.Create(singleSpec.BuildingRangeTile, this._parent);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002910 File Offset: 0x00000B10
		public void AddBuildingWithRange(IBuildingWithRange buildingWithRange)
		{
			this._buildingsWithRanges.GetOrAdd(buildingWithRange.RangeName).Add(buildingWithRange);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000292A File Offset: 0x00000B2A
		public void RemoveBuildingWithRange(IBuildingWithRange buildingWithRange)
		{
			this._buildingsWithRanges[buildingWithRange.RangeName].Remove(buildingWithRange);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002944 File Offset: 0x00000B44
		public void AddPreviewBuildingWithRange(IBuildingWithRange buildingWithRange, Preview preview)
		{
			this._previewBuildingWithRange = buildingWithRange;
			this._preview = preview;
			this.DrawArea();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000295A File Offset: 0x00000B5A
		public void RemovePreviewBuildingWithRange()
		{
			this._previewBuildingWithRange = null;
			this._preview = null;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000296C File Offset: 0x00000B6C
		public void DrawArea()
		{
			this._showableRange.Clear();
			this._showableRange.UnionWith(this._currentRange);
			if (this._previewBuildingWithRange != null && this._preview.GameObject.activeInHierarchy)
			{
				this._showableRange.UnionWith(this._previewBuildingWithRange.GetBlocksInRange());
			}
			this._areaTileDrawer.UpdateArea(this._showableRange);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029D8 File Offset: 0x00000BD8
		public void RecalculateArea(string rangeName)
		{
			this._currentRange.Clear();
			foreach (IBuildingWithRange buildingWithRange in this._buildingsWithRanges.GetOrAdd(rangeName))
			{
				this._currentRange.UnionWith(buildingWithRange.GetBlocksInRange());
			}
			this.DrawArea();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A4C File Offset: 0x00000C4C
		public void ShowArea()
		{
			this._areaTileDrawer.ShowAllTiles();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A59 File Offset: 0x00000C59
		public void HideArea()
		{
			this._areaTileDrawer.HideAllTiles();
		}

		// Token: 0x0400001C RID: 28
		public readonly AreaTileDrawerFactory _areaTileDrawerFactory;

		// Token: 0x0400001D RID: 29
		public readonly ISpecService _specService;

		// Token: 0x0400001E RID: 30
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400001F RID: 31
		public GameObject _parent;

		// Token: 0x04000020 RID: 32
		public AreaTileDrawer _areaTileDrawer;

		// Token: 0x04000021 RID: 33
		public IBuildingWithRange _previewBuildingWithRange;

		// Token: 0x04000022 RID: 34
		public Preview _preview;

		// Token: 0x04000023 RID: 35
		public readonly HashSet<Vector3Int> _currentRange = new HashSet<Vector3Int>();

		// Token: 0x04000024 RID: 36
		public readonly HashSet<Vector3Int> _showableRange = new HashSet<Vector3Int>();

		// Token: 0x04000025 RID: 37
		public readonly Dictionary<string, HashSet<IBuildingWithRange>> _buildingsWithRanges = new Dictionary<string, HashSet<IBuildingWithRange>>();
	}
}
