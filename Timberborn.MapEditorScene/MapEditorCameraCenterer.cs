using System;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.MapEditorScene
{
	// Token: 0x02000003 RID: 3
	public class MapEditorCameraCenterer : ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public MapEditorCameraCenterer(MapSize mapSize, CameraService cameraService)
		{
			this._mapSize = mapSize;
			this._cameraService = cameraService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public void Load()
		{
			Vector3Int coordinates = (this._mapSize.TerrainSize.XY() / 2).XYZ();
			this._cameraService.MoveTargetTo(CoordinateSystem.GridToWorld(coordinates));
		}

		// Token: 0x04000001 RID: 1
		private readonly MapSize _mapSize;

		// Token: 0x04000002 RID: 2
		private readonly CameraService _cameraService;
	}
}
