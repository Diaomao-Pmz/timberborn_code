using System;
using System.Collections.Generic;
using Timberborn.Coordinates;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x0200000F RID: 15
	public class TerrainHighlightingService : ILoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002A84 File Offset: 0x00000C84
		public TerrainHighlightingService(MarkerDrawerFactory meshDrawerFactory, ILevelVisibilityService levelVisibilityService, ITerrainService terrainService)
		{
			this._meshDrawerFactory = meshDrawerFactory;
			this._levelVisibilityService = levelVisibilityService;
			this._terrainService = terrainService;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002AC2 File Offset: 0x00000CC2
		public void Load()
		{
			this._markerDrawer = this._meshDrawerFactory.CreateTerrainTileDrawer();
			this._topMarkerDrawer = this._meshDrawerFactory.CreateTopTerrainTileDrawer();
			this._levelVisibilityService.MaxVisibleLevelChanged += this.OnMaxVisibleLevelChanged;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002AFD File Offset: 0x00000CFD
		public void UpdateHighlight(IEnumerable<Vector3Int> highlightedTerrain)
		{
			this.ClearHighlight();
			this._coordinatesToHighlight.AddRange(highlightedTerrain);
			this.UpdateHighlightMatrices();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B17 File Offset: 0x00000D17
		public void ClearHighlight()
		{
			this._coordinatesToHighlight.Clear();
			this._highlightedCoords.Clear();
			this._topHighlightedCoords.Clear();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B3A File Offset: 0x00000D3A
		public void LateUpdateSingleton()
		{
			if (this._highlightedCoords.Count > 0)
			{
				this._markerDrawer.DrawMultipleInstanced(this._highlightedCoords);
				this._topMarkerDrawer.DrawMultipleInstanced(this._topHighlightedCoords);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B6C File Offset: 0x00000D6C
		public void OnMaxVisibleLevelChanged(object sender, int maxVisibleLevel)
		{
			this._topHighlightedCoords.Clear();
			if (!this._levelVisibilityService.TerrainLevelIsAtMax)
			{
				foreach (Vector3Int coordinates in this._coordinatesToHighlight)
				{
					Vector3 position = CoordinateSystem.GridToWorldCentered(coordinates);
					this.AddTerrainStumpMarker(position);
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void UpdateHighlightMatrices()
		{
			foreach (Vector3Int coordinates in this._coordinatesToHighlight)
			{
				Vector3 vector = CoordinateSystem.GridToWorldCentered(coordinates);
				if (this._terrainService.IsVisible(coordinates))
				{
					Matrix4x4 item = Matrix4x4.TRS(vector, Quaternion.identity, Vector3.one);
					this._highlightedCoords.Add(item);
				}
				this.AddTerrainStumpMarker(vector);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002C64 File Offset: 0x00000E64
		public void AddTerrainStumpMarker(Vector3 position)
		{
			float y = position.y;
			if (y >= (float)this._levelVisibilityService.MaxVisibleLevel && y < (float)(this._levelVisibilityService.MaxVisibleLevel + 1))
			{
				this._topHighlightedCoords.Add(Matrix4x4.TRS(position + TerrainHighlightingService.StumpMarkerOffset, Quaternion.identity, TerrainHighlightingService.StumpMarkerSize));
			}
		}

		// Token: 0x04000020 RID: 32
		public static readonly Vector3 StumpMarkerOffset = Vector3.up * TerrainMeshManager.TerrainStumpHeight;

		// Token: 0x04000021 RID: 33
		public static readonly Vector3 StumpMarkerSize = Vector3.one * 0.9f;

		// Token: 0x04000022 RID: 34
		public readonly MarkerDrawerFactory _meshDrawerFactory;

		// Token: 0x04000023 RID: 35
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000024 RID: 36
		public readonly ITerrainService _terrainService;

		// Token: 0x04000025 RID: 37
		public readonly List<Vector3Int> _coordinatesToHighlight = new List<Vector3Int>();

		// Token: 0x04000026 RID: 38
		public readonly List<Matrix4x4> _highlightedCoords = new List<Matrix4x4>();

		// Token: 0x04000027 RID: 39
		public readonly List<Matrix4x4> _topHighlightedCoords = new List<Matrix4x4>();

		// Token: 0x04000028 RID: 40
		public MeshDrawer _markerDrawer;

		// Token: 0x04000029 RID: 41
		public MeshDrawer _topMarkerDrawer;
	}
}
