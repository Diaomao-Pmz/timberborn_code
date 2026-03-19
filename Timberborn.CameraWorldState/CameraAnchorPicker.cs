using System;
using Timberborn.CameraSystem;
using Timberborn.GridTraversing;
using Timberborn.TerrainQueryingSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.CameraWorldState
{
	// Token: 0x02000004 RID: 4
	public class CameraAnchorPicker : ICameraAnchorPicker
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public CameraAnchorPicker(TerrainPicker terrainPicker, IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._terrainPicker = terrainPicker;
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public Vector3? PickAnchorPoint(Ray ray)
		{
			if (this._terrainPicker.PickTerrainCoordinates(ray, new Predicate<Vector3Int>(this.IsWaterVoxel)) == null)
			{
				return null;
			}
			TraversedCoordinates? traversedCoordinates;
			return new Vector3?(traversedCoordinates.GetValueOrDefault().Intersection);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002121 File Offset: 0x00000321
		public bool IsWaterVoxel(Vector3Int coordinates)
		{
			return this._threadSafeWaterMap.CellIsUnderwater(coordinates);
		}

		// Token: 0x04000006 RID: 6
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x04000007 RID: 7
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;
	}
}
