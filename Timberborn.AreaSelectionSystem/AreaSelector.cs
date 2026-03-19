using System;
using Timberborn.BlockObjectPickingSystem;
using Timberborn.BlockSystem;
using Timberborn.GridTraversing;
using Timberborn.TerrainQueryingSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.AreaSelectionSystem
{
	// Token: 0x02000021 RID: 33
	public class AreaSelector
	{
		// Token: 0x06000088 RID: 136 RVA: 0x0000376D File Offset: 0x0000196D
		public AreaSelector(BlockObjectRaycaster blockObjectRaycaster, TerrainPicker terrainPicker, AreaClamper areaClamper, ITerrainService terrainService)
		{
			this._blockObjectRaycaster = blockObjectRaycaster;
			this._terrainPicker = terrainPicker;
			this._areaClamper = areaClamper;
			this._terrainService = terrainService;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003794 File Offset: 0x00001994
		public SelectionStart? GetSelectionStart<T>(Ray ray)
		{
			BlockObjectHit blockObjectHit;
			if (this._blockObjectRaycaster.TryHitBlockObject<T>(ray, out blockObjectHit))
			{
				return new SelectionStart?(new SelectionStart(blockObjectHit));
			}
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinates(ray);
			if (traversedCoordinates != null)
			{
				return new SelectionStart?(new SelectionStart(traversedCoordinates.Value.Coordinates + traversedCoordinates.Value.Face));
			}
			return null;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000380C File Offset: 0x00001A0C
		public Vector3Int GetSelectionEnd(SelectionStart selectionStart, Ray endRay)
		{
			Vector3Int coordinates = selectionStart.Coordinates;
			int referenceTerrainLevel = selectionStart.ReferenceTerrainLevel;
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.FindCoordinatesOnLevelInMap(endRay, (float)referenceTerrainLevel);
			Vector3Int endCoords = (traversedCoordinates != null) ? traversedCoordinates.GetValueOrDefault().Coordinates : coordinates;
			endCoords.z += selectionStart.VerticalOffset;
			return this.ClampSelectionEnd(coordinates, endCoords);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003874 File Offset: 0x00001A74
		public Vector3Int ClampSelectionEnd(Vector3Int startCoords, Vector3Int endCoords)
		{
			Vector3Int vector3Int = this._areaClamper.ClampEnd(startCoords, endCoords, 30);
			if (endCoords != vector3Int)
			{
				int terrainHeight = this._terrainService.GetTerrainHeight(vector3Int);
				vector3Int.z = Mathf.Max(startCoords.z, terrainHeight);
			}
			vector3Int.z = Mathf.Min(startCoords.z, vector3Int.z);
			return vector3Int;
		}

		// Token: 0x04000068 RID: 104
		public readonly BlockObjectRaycaster _blockObjectRaycaster;

		// Token: 0x04000069 RID: 105
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x0400006A RID: 106
		public readonly AreaClamper _areaClamper;

		// Token: 0x0400006B RID: 107
		public readonly ITerrainService _terrainService;
	}
}
