using System;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x0200001E RID: 30
	public class MouseCameraController : IInputProcessor, ILoadableSingleton
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00005028 File Offset: 0x00003228
		public MouseCameraController(InputService inputService, InputSettings inputSettings, CameraActionMarker cameraActionMarker, CameraService cameraService, EventBus eventBus, DraggingCameraTargetPicker draggingCameraTargetPicker, GrabbingCameraTargetPicker grabbingCameraTargetPicker, EdgePanningCameraTargetPicker edgePanningCameraTargetPicker, ISpecService specService)
		{
			this._inputService = inputService;
			this._inputSettings = inputSettings;
			this._cameraActionMarker = cameraActionMarker;
			this._cameraService = cameraService;
			this._eventBus = eventBus;
			this._draggingCameraTargetPicker = draggingCameraTargetPicker;
			this._grabbingCameraTargetPicker = grabbingCameraTargetPicker;
			this._edgePanningCameraTargetPicker = edgePanningCameraTargetPicker;
			this._specService = specService;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005080 File Offset: 0x00003280
		public void Load()
		{
			this._mouseCameraControllerSpec = this._specService.GetSingleSpec<MouseCameraControllerSpec>();
			this._inputService.AddInputProcessor(this);
			this._eventBus.Register(this);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000050AB File Offset: 0x000032AB
		public bool ProcessInput()
		{
			this.ScrollWheelUpdate();
			this.MovementUpdate();
			this.RotationUpdate();
			return false;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000050C0 File Offset: 0x000032C0
		[OnEvent]
		public void OnPanelShown(PanelShownEvent panelShownEvent)
		{
			if (this._rotating)
			{
				this.StopRotatingCamera();
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000050D0 File Offset: 0x000032D0
		public void ScrollWheelUpdate()
		{
			if (!this._inputService.MouseOverUI)
			{
				float mouseZoom = this._inputService.MouseZoom;
				this._cameraService.ModifyZoomLevel(mouseZoom);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005102 File Offset: 0x00003302
		public void MovementUpdate()
		{
			if (this._inputSettings.DragCamera)
			{
				this.MoveCameraByDragging();
			}
			else
			{
				this.MoveCameraByGrabbingTerrain();
			}
			if (this._inputSettings.EdgePanCamera)
			{
				this.MoveCameraByEdgePanning();
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005134 File Offset: 0x00003334
		public void MoveCameraByDragging()
		{
			Vector3 delta = this._draggingCameraTargetPicker.CameraPositionDelta();
			this._cameraService.MoveCameraBy(delta);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000515C File Offset: 0x0000335C
		public void MoveCameraByGrabbingTerrain()
		{
			Vector3 point = this._grabbingCameraTargetPicker.PickCameraTarget();
			this._cameraService.MoveTargetTo(point);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005184 File Offset: 0x00003384
		public void MoveCameraByEdgePanning()
		{
			float zoomSpeedScale = this._cameraService.ZoomSpeedScale;
			Vector3 delta = this._edgePanningCameraTargetPicker.CameraPositionDelta(zoomSpeedScale);
			this._cameraService.MoveCameraBy(delta);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000051B8 File Offset: 0x000033B8
		public void RotationUpdate()
		{
			if (this._inputService.RotateButtonHeld && !this._inputService.MoveButtonHeld)
			{
				if (!this._rotating && this._inputService.MouseXYAxes != Vector2.zero)
				{
					this.StartRotatingCamera();
				}
				if (this._rotating)
				{
					this.RotateCamera();
					return;
				}
			}
			else if (this._rotating)
			{
				this.StopRotatingCamera();
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005221 File Offset: 0x00003421
		public void StartRotatingCamera()
		{
			this._rotating = true;
			this._rotationDistanceAccumulator = 0f;
			this._inputService.LockCursor();
			this._inputService.HideCursor();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000524B File Offset: 0x0000344B
		public void StopRotatingCamera()
		{
			this._rotating = false;
			this._rotationDistanceAccumulator = 0f;
			this._cameraActionMarker.Hide();
			this._inputService.UnlockCursor();
			this._inputService.ShowCursor();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005280 File Offset: 0x00003480
		public void RotateCamera()
		{
			Vector2 mouseXYAxes = this._inputService.MouseXYAxes;
			this._rotationDistanceAccumulator += mouseXYAxes.magnitude;
			if (this._rotationDistanceAccumulator > this._mouseCameraControllerSpec.RmbRotationMinDistance || this._inputService.RotateButtonLongHeld)
			{
				Vector2 vector = this._mouseCameraControllerSpec.RmbRotationSpeed * mouseXYAxes * this._inputSettings.MouseCameraRotationSpeed;
				this._cameraService.ModifyHorizontalAngle(vector.x);
				this._cameraService.ModifyVerticalAngle(-vector.y);
			}
		}

		// Token: 0x04000091 RID: 145
		public readonly InputService _inputService;

		// Token: 0x04000092 RID: 146
		public readonly InputSettings _inputSettings;

		// Token: 0x04000093 RID: 147
		public readonly CameraActionMarker _cameraActionMarker;

		// Token: 0x04000094 RID: 148
		public readonly CameraService _cameraService;

		// Token: 0x04000095 RID: 149
		public readonly EventBus _eventBus;

		// Token: 0x04000096 RID: 150
		public readonly DraggingCameraTargetPicker _draggingCameraTargetPicker;

		// Token: 0x04000097 RID: 151
		public readonly GrabbingCameraTargetPicker _grabbingCameraTargetPicker;

		// Token: 0x04000098 RID: 152
		public readonly EdgePanningCameraTargetPicker _edgePanningCameraTargetPicker;

		// Token: 0x04000099 RID: 153
		public readonly ISpecService _specService;

		// Token: 0x0400009A RID: 154
		public bool _rotating;

		// Token: 0x0400009B RID: 155
		public float _rotationDistanceAccumulator;

		// Token: 0x0400009C RID: 156
		public MouseCameraControllerSpec _mouseCameraControllerSpec;
	}
}
