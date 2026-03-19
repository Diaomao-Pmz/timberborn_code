using System;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.MapStateSystem;
using UnityEngine;

namespace Timberborn.MapThumbnailCapturing
{
	// Token: 0x0200000F RID: 15
	public class ThumbnailCameraDefaultPositionProvider
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002677 File Offset: 0x00000877
		public ThumbnailCameraDefaultPositionProvider(MapSize mapSize)
		{
			this._mapSize = mapSize;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002688 File Offset: 0x00000888
		public CameraConfiguration GetDefaultPosition()
		{
			Vector3 vector = CoordinateSystem.GridToWorld((this._mapSize.TerrainSize.XY() / 2).XYZ());
			int x = this._mapSize.TerrainSize.x;
			return new CameraConfiguration(vector + ThumbnailCameraDefaultPositionProvider.DefaultRotation * Vector3.back * (float)x, ThumbnailCameraDefaultPositionProvider.DefaultRotation, (float)ShadowDistanceUpdater.MaxDistance);
		}

		// Token: 0x04000026 RID: 38
		public static readonly Quaternion DefaultRotation = Quaternion.Euler(40f, 0f, 0f);

		// Token: 0x04000027 RID: 39
		public readonly MapSize _mapSize;
	}
}
