using System;
using Timberborn.Debugging;
using Timberborn.QuickNotificationSystem;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000012 RID: 18
	public class CameraSystemDevModule : IDevModule
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003C62 File Offset: 0x00001E62
		public CameraSystemDevModule(CameraService cameraService, CameraStateRestorer cameraStateRestorer, QuickNotificationService quickNotificationService)
		{
			this._cameraService = cameraService;
			this._cameraStateRestorer = cameraStateRestorer;
			this._quickNotificationService = quickNotificationService;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003C80 File Offset: 0x00001E80
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.CreateBindable("Camera state: Save", CameraSystemDevModule.SaveCameraKey, delegate
			{
				this._cameraStateRestorer.SaveCameraState();
			})).AddMethod(DevMethod.CreateBindable("Camera state: Restore", CameraSystemDevModule.RestoreCameraKey, new Action(this._cameraStateRestorer.LoadCameraState))).AddMethod(DevMethod.CreateBindable("Camera state: Copy", CameraSystemDevModule.SaveCameraToClipboardKey, new Action(this._cameraStateRestorer.SaveCameraStateToClipboard))).AddMethod(DevMethod.CreateBindable("Camera state: Paste", CameraSystemDevModule.RestoreCameraFromClipboardKey, new Action(this._cameraStateRestorer.LoadCameraStateFromClipboard))).AddMethod(DevMethod.Create("Camera: Free mode", new Action(this.ToggleFreeMode))).AddMethod(DevMethod.Create("Camera: FOV +", new Action(this.IncreaseFieldOfView))).AddMethod(DevMethod.Create("Camera: FOV -", new Action(this.DecreaseFieldOfView))).AddMethod(DevMethod.Create("Camera: Move target up", new Action(this.MoveTargetUp))).AddMethod(DevMethod.Create("Camera: Move target down", new Action(this.MoveTargetDown))).AddMethod(DevMethod.Create("Camera: Move clip plane nearer", new Action(this.MoveClipPlaneNearer))).AddMethod(DevMethod.Create("Camera: Move clip plane farther", new Action(this.MoveClipPlaneFarther))).Build();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003DE3 File Offset: 0x00001FE3
		public void IncreaseFieldOfView()
		{
			this.ModifyFieldOfView(CameraSystemDevModule.FieldOfViewStep);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003DF0 File Offset: 0x00001FF0
		public void DecreaseFieldOfView()
		{
			this.ModifyFieldOfView(-CameraSystemDevModule.FieldOfViewStep);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003DFE File Offset: 0x00001FFE
		public void MoveTargetUp()
		{
			this.MoveTargetVertically(CameraSystemDevModule.MoveTargetUpDownStep);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003E0B File Offset: 0x0000200B
		public void MoveTargetDown()
		{
			this.MoveTargetVertically(-CameraSystemDevModule.MoveTargetUpDownStep);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003E19 File Offset: 0x00002019
		public void MoveClipPlaneNearer()
		{
			this.MoveClipPlane(1f / CameraSystemDevModule.NearClipPlaneMultiplier);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003E2C File Offset: 0x0000202C
		public void MoveClipPlaneFarther()
		{
			this.MoveClipPlane(CameraSystemDevModule.NearClipPlaneMultiplier);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003E3C File Offset: 0x0000203C
		public void ToggleFreeMode()
		{
			this._cameraService.FreeMode = !this._cameraService.FreeMode;
			this._quickNotificationService.SendNotification("Free camera " + (this._cameraService.FreeMode ? "ON" : "OFF"));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003E90 File Offset: 0x00002090
		public void ModifyFieldOfView(float delta)
		{
			this._cameraService.FieldOfView += delta;
			this._quickNotificationService.SendNotification(string.Format("Field of view: {0}", this._cameraService.FieldOfView));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003ECC File Offset: 0x000020CC
		public void MoveTargetVertically(float delta)
		{
			this._cameraService.MoveCameraBy(new Vector3(0f, delta, 0f));
			this._quickNotificationService.SendNotification(string.Format("Target height: {0:0.0}", this._cameraService.Target.y));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003F1E File Offset: 0x0000211E
		public void MoveClipPlane(float multiplier)
		{
			this._cameraService.NearClipPlane *= multiplier;
			this._quickNotificationService.SendNotification(string.Format("Clip plane: {0:0.0}", this._cameraService.NearClipPlane));
		}

		// Token: 0x0400005B RID: 91
		public static readonly string SaveCameraKey = "SaveCamera";

		// Token: 0x0400005C RID: 92
		public static readonly string RestoreCameraKey = "RestoreCamera";

		// Token: 0x0400005D RID: 93
		public static readonly string SaveCameraToClipboardKey = "SaveCameraToClipboard";

		// Token: 0x0400005E RID: 94
		public static readonly string RestoreCameraFromClipboardKey = "RestoreCameraFromClipboard";

		// Token: 0x0400005F RID: 95
		public static readonly float FieldOfViewStep = 2f;

		// Token: 0x04000060 RID: 96
		public static readonly float MoveTargetUpDownStep = 1f;

		// Token: 0x04000061 RID: 97
		public static readonly float NearClipPlaneMultiplier = 1.2f;

		// Token: 0x04000062 RID: 98
		public readonly CameraService _cameraService;

		// Token: 0x04000063 RID: 99
		public readonly CameraStateRestorer _cameraStateRestorer;

		// Token: 0x04000064 RID: 100
		public readonly QuickNotificationService _quickNotificationService;
	}
}
