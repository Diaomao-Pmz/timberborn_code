using System;
using Timberborn.CameraSystem;
using Timberborn.Debugging;
using Timberborn.GridTraversing;
using Timberborn.InputSystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainQueryingSystem;
using UnityEngine;

namespace Timberborn.CameraWorldState
{
	// Token: 0x02000006 RID: 6
	public class CameraWorldStateResetter : IDevModule, IInputProcessor, ILoadableSingleton
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000215B File Offset: 0x0000035B
		public CameraWorldStateResetter(CameraService cameraService, CameraTargeter cameraTargeter, TerrainPicker terrainPicker, InputService inputService)
		{
			this._cameraService = cameraService;
			this._cameraTargeter = cameraTargeter;
			this._terrainPicker = terrainPicker;
			this._inputService = inputService;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002180 File Offset: 0x00000380
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.CreateBindable("Camera: Reset", CameraWorldStateResetter.ResetCameraKey, new Action(this.ResetCamera))).Build();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021AC File Offset: 0x000003AC
		public void Load()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021BA File Offset: 0x000003BA
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(CameraWorldStateResetter.ResetCameraKey))
			{
				this.ResetCamera();
				return true;
			}
			return false;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D8 File Offset: 0x000003D8
		public void ResetCamera()
		{
			this._cameraTargeter.StopFollowing();
			this._cameraService.VerticalAngle = CameraWorldStateResetter.DefaultVerticalCameraAngle;
			Vector2 screenPoint = new Vector2((float)Screen.width, (float)Screen.height) * 0.5f;
			Ray ray = this._cameraService.ScreenPointToRayInGridSpace(screenPoint);
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinates(ray);
			int num = (traversedCoordinates != null) ? traversedCoordinates.Value.Coordinates.z : 0;
			this._cameraService.ZoomLevel = (float)num * CameraWorldStateResetter.DefaultZoomPerLevel;
		}

		// Token: 0x04000008 RID: 8
		public static readonly string ResetCameraKey = "ResetCamera";

		// Token: 0x04000009 RID: 9
		public static readonly float DefaultZoomPerLevel = 0.1f;

		// Token: 0x0400000A RID: 10
		public static readonly float DefaultVerticalCameraAngle = 60f;

		// Token: 0x0400000B RID: 11
		public readonly CameraService _cameraService;

		// Token: 0x0400000C RID: 12
		public readonly CameraTargeter _cameraTargeter;

		// Token: 0x0400000D RID: 13
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x0400000E RID: 14
		public readonly InputService _inputService;
	}
}
