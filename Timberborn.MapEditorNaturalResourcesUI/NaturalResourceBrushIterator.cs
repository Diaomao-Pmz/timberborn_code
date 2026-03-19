using System;
using System.Collections.Generic;
using Timberborn.Brushes;
using Timberborn.CameraSystem;
using Timberborn.GridTraversing;
using Timberborn.InputSystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.TerrainQueryingSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.MapEditorNaturalResourcesUI
{
	// Token: 0x02000009 RID: 9
	public class NaturalResourceBrushIterator
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000218B File Offset: 0x0000038B
		public NaturalResourceBrushIterator(InputService inputService, BrushShapeIterator brushShapeIterator, ILevelVisibilityService levelVisibilityService, TerrainPicker terrainPicker, CameraService cameraService, ITerrainService terrainService)
		{
			this._inputService = inputService;
			this._brushShapeIterator = brushShapeIterator;
			this._levelVisibilityService = levelVisibilityService;
			this._terrainPicker = terrainPicker;
			this._cameraService = cameraService;
			this._terrainService = terrainService;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021C0 File Offset: 0x000003C0
		public IEnumerable<Vector3Int> Iterate(int size, BrushShape shape)
		{
			bool wasDrawing = this._isDrawing;
			this._isDrawing = this._inputService.MainMouseButtonHeld;
			if (!this._isDrawing)
			{
				this._originHeight = null;
			}
			bool originSet = false;
			foreach (Vector3Int vector3Int in this.IterateTerrain(size, shape))
			{
				if (!originSet && this._isDrawing && !wasDrawing)
				{
					this._originHeight = new int?(vector3Int.z);
					originSet = true;
				}
				if (vector3Int.z < this._levelVisibilityService.MaxVisibleLevel + 1)
				{
					yield return vector3Int;
				}
			}
			IEnumerator<Vector3Int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021DE File Offset: 0x000003DE
		public void Reset()
		{
			this._isDrawing = false;
			this._originHeight = null;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021F3 File Offset: 0x000003F3
		public IEnumerable<Vector3Int> IterateTerrain(int size, BrushShape brushShape)
		{
			Ray ray = this._cameraService.ScreenPointToRayInGridSpace(this._inputService.MousePosition);
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinates(ray);
			if (traversedCoordinates != null)
			{
				TraversedCoordinates valueOrDefault = traversedCoordinates.GetValueOrDefault();
				if (valueOrDefault.Face.z == 1)
				{
					Vector3Int center = valueOrDefault.Coordinates + valueOrDefault.Face;
					foreach (Vector3Int coordinates in this._brushShapeIterator.IterateShape(center, size, brushShape))
					{
						int num;
						if (this._terrainService.TryGetRelativeHeight(coordinates, out num))
						{
							int num2 = coordinates.z + num;
							int num3 = this._originHeight ?? center.z;
							if (num2 == num3)
							{
								yield return new Vector3Int(coordinates.x, coordinates.y, num2);
							}
						}
					}
					IEnumerator<Vector3Int> enumerator = null;
					center = default(Vector3Int);
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x04000009 RID: 9
		public readonly InputService _inputService;

		// Token: 0x0400000A RID: 10
		public readonly BrushShapeIterator _brushShapeIterator;

		// Token: 0x0400000B RID: 11
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x0400000C RID: 12
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x0400000D RID: 13
		public readonly CameraService _cameraService;

		// Token: 0x0400000E RID: 14
		public readonly ITerrainService _terrainService;

		// Token: 0x0400000F RID: 15
		public int? _originHeight;

		// Token: 0x04000010 RID: 16
		public bool _isDrawing;
	}
}
