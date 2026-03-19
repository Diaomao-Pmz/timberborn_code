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
	// Token: 0x02000007 RID: 7
	public class NaturalResourceBrushIterator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002162 File Offset: 0x00000362
		public NaturalResourceBrushIterator(InputService inputService, BrushShapeIterator brushShapeIterator, ILevelVisibilityService levelVisibilityService, TerrainPicker terrainPicker, CameraService cameraService, ITerrainService terrainService)
		{
			this._inputService = inputService;
			this._brushShapeIterator = brushShapeIterator;
			this._levelVisibilityService = levelVisibilityService;
			this._terrainPicker = terrainPicker;
			this._cameraService = cameraService;
			this._terrainService = terrainService;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002197 File Offset: 0x00000397
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

		// Token: 0x0600000B RID: 11 RVA: 0x000021B5 File Offset: 0x000003B5
		public void Reset()
		{
			this._isDrawing = false;
			this._originHeight = null;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021CA File Offset: 0x000003CA
		private IEnumerable<Vector3Int> IterateTerrain(int size, BrushShape brushShape)
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

		// Token: 0x04000003 RID: 3
		private readonly InputService _inputService;

		// Token: 0x04000004 RID: 4
		private readonly BrushShapeIterator _brushShapeIterator;

		// Token: 0x04000005 RID: 5
		private readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000006 RID: 6
		private readonly TerrainPicker _terrainPicker;

		// Token: 0x04000007 RID: 7
		private readonly CameraService _cameraService;

		// Token: 0x04000008 RID: 8
		private readonly ITerrainService _terrainService;

		// Token: 0x04000009 RID: 9
		private int? _originHeight;

		// Token: 0x0400000A RID: 10
		private bool _isDrawing;
	}
}
