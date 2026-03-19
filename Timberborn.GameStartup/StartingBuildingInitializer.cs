using System;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.CameraSystem;
using Timberborn.Coordinates;
using Timberborn.Localization;
using Timberborn.NotificationSystem;
using Timberborn.SelectionSystem;
using Timberborn.StartingLocationSystem;

namespace Timberborn.GameStartup
{
	// Token: 0x0200000E RID: 14
	public class StartingBuildingInitializer
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000024D8 File Offset: 0x000006D8
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000024E0 File Offset: 0x000006E0
		public Placement? InitialPlacement { get; private set; }

		// Token: 0x06000026 RID: 38 RVA: 0x000024E9 File Offset: 0x000006E9
		public StartingBuildingInitializer(StartingLocationService startingLocationService, CameraService cameraService, StartingBuildingSpawner startingBuildingSpawner, CameraTargeter cameraTargeter, NotificationBus notificationBus, ILoc loc)
		{
			this._startingLocationService = startingLocationService;
			this._cameraService = cameraService;
			this._startingBuildingSpawner = startingBuildingSpawner;
			this._cameraTargeter = cameraTargeter;
			this._notificationBus = notificationBus;
			this._loc = loc;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002520 File Offset: 0x00000720
		public void Initialize()
		{
			if (this._startingLocationService.HasStartingLocation())
			{
				this.InitialPlacement = new Placement?(this._startingLocationService.GetPlacement());
			}
			this._startingBuildingSpawner.Place(this.InitialPlacement);
			this.SetCamera();
			this._startingLocationService.DeleteStartingLocations();
			this.Notify();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002578 File Offset: 0x00000778
		public void SetCamera()
		{
			Building startingBuilding = this._startingBuildingSpawner.StartingBuilding;
			if (startingBuilding != null)
			{
				this._cameraService.VerticalAngle = StartingBuildingInitializer.StartingVerticalCameraAngle;
				BlockObject component = startingBuilding.GetComponent<BlockObject>();
				this._cameraService.HorizontalAngle = component.Orientation.ToAngle() + StartingBuildingInitializer.StartingHorizontalCameraAngleOffset;
				this._cameraService.ZoomLevel = (float)component.Coordinates.z * StartingBuildingInitializer.ZoomIncreasePerLevel;
				this._cameraTargeter.CenterCameraOn(startingBuilding.GetComponent<SelectableObject>());
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025F8 File Offset: 0x000007F8
		public void Notify()
		{
			Building startingBuilding = this._startingBuildingSpawner.StartingBuilding;
			if (startingBuilding != null)
			{
				this._notificationBus.Post(this._loc.T(StartingBuildingInitializer.NewGameLocKey), startingBuilding);
			}
		}

		// Token: 0x0400001E RID: 30
		public static readonly float ZoomIncreasePerLevel = 0.1f;

		// Token: 0x0400001F RID: 31
		public static readonly float StartingVerticalCameraAngle = 60f;

		// Token: 0x04000020 RID: 32
		public static readonly float StartingHorizontalCameraAngleOffset = 35f;

		// Token: 0x04000021 RID: 33
		public static readonly string NewGameLocKey = "NewGame.Notification";

		// Token: 0x04000023 RID: 35
		public readonly StartingLocationService _startingLocationService;

		// Token: 0x04000024 RID: 36
		public readonly CameraService _cameraService;

		// Token: 0x04000025 RID: 37
		public readonly StartingBuildingSpawner _startingBuildingSpawner;

		// Token: 0x04000026 RID: 38
		public readonly CameraTargeter _cameraTargeter;

		// Token: 0x04000027 RID: 39
		public readonly NotificationBus _notificationBus;

		// Token: 0x04000028 RID: 40
		public readonly ILoc _loc;
	}
}
