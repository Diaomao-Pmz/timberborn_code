using System;
using Timberborn.BlueprintSystem;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x0200001C RID: 28
	public class KeyboardCameraController : IInputProcessor, ILoadableSingleton
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00004B3C File Offset: 0x00002D3C
		public KeyboardCameraController(InputService inputService, CameraService cameraService, CameraMovementInput cameraMovementInput, InputSettings inputSettings, ISpecService specService)
		{
			this._inputService = inputService;
			this._cameraService = cameraService;
			this._cameraMovementInput = cameraMovementInput;
			this._inputSettings = inputSettings;
			this._specService = specService;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004B69 File Offset: 0x00002D69
		public void Load()
		{
			this._keyboardCameraControllerSpec = this._specService.GetSingleSpec<KeyboardCameraControllerSpec>();
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004B88 File Offset: 0x00002D88
		public bool ProcessInput()
		{
			this.MovementUpdate();
			this.RotationUpdate();
			this.ZoomUpdate();
			return false;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004B9D File Offset: 0x00002D9D
		public float MovementSpeed
		{
			get
			{
				return (this._inputSettings.KeyboardCameraMovementSpeed * 50f + 1f) * (float)(this._cameraMovementInput.MoveCameraFast ? 2 : 1);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004BC9 File Offset: 0x00002DC9
		public float RotationSpeed
		{
			get
			{
				return (this._inputSettings.KeyboardCameraRotationSpeed * 175f + 1f) * (float)(this._cameraMovementInput.MoveCameraFast ? 2 : 1);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004BF5 File Offset: 0x00002DF5
		public float ZoomSpeed
		{
			get
			{
				return this._keyboardCameraControllerSpec.BaseZoomSpeed * this._inputSettings.KeyboardCameraZoomSpeed;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004C10 File Offset: 0x00002E10
		public void MovementUpdate()
		{
			Vector2 cameraMovementAxes = this._cameraMovementInput.CameraMovementAxes;
			Vector3 normalized = new Vector3(cameraMovementAxes.x, 0f, cameraMovementAxes.y).normalized;
			Vector3 delta = this.MovementSpeed * this._cameraService.ZoomSpeedScale * CappedTime.CappedUnscaledDeltaTime() * normalized;
			this._cameraService.MoveCameraBy(delta);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004C74 File Offset: 0x00002E74
		public void RotationUpdate()
		{
			float rotationSpeed = CappedTime.CappedUnscaledDeltaTime() * this.RotationSpeed;
			this.SmoothRotationUpdate(rotationSpeed);
			this.JumpRotationUpdate(rotationSpeed);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004C9C File Offset: 0x00002E9C
		public void SmoothRotationUpdate(float rotationSpeed)
		{
			Vector2 cameraRotationAxes = this._cameraMovementInput.GetCameraRotationAxes();
			this._cameraService.ModifyHorizontalAngle(-cameraRotationAxes.x * rotationSpeed);
			this._cameraService.ModifyVerticalAngle(cameraRotationAxes.y * rotationSpeed);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004CDC File Offset: 0x00002EDC
		public void JumpRotationUpdate(float rotationSpeed)
		{
			this._remainingHorizontalJumpAngle += this.KeyboardJumpRotationAngle();
			this.SmoothlyJumpHorizontally(rotationSpeed);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004CF8 File Offset: 0x00002EF8
		public float KeyboardJumpRotationAngle()
		{
			return this._cameraMovementInput.GetCameraJumpRotationAxes().x * (float)this._keyboardCameraControllerSpec.JumpRotationAngle;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004D18 File Offset: 0x00002F18
		public void SmoothlyJumpHorizontally(float rotationSpeed)
		{
			float num = (float)this._keyboardCameraControllerSpec.JumpRotationSpeedInAnglePerUpdate * rotationSpeed;
			float num2 = (Math.Abs(this._remainingHorizontalJumpAngle) > num) ? (num * (float)Math.Sign(this._remainingHorizontalJumpAngle)) : this._remainingHorizontalJumpAngle;
			this._remainingHorizontalJumpAngle -= num2;
			this._cameraService.ModifyHorizontalAngle(-num2);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004D74 File Offset: 0x00002F74
		public void ZoomUpdate()
		{
			float num = this.ZoomSpeed * CappedTime.CappedUnscaledDeltaTime();
			if (this._inputService.IsKeyHeld(KeyboardCameraController.ZoomInKey))
			{
				this._cameraService.ModifyZoomLevel(num);
			}
			if (this._inputService.IsKeyHeld(KeyboardCameraController.ZoomOutKey))
			{
				this._cameraService.ModifyZoomLevel(-num);
			}
		}

		// Token: 0x04000085 RID: 133
		public static readonly string ZoomInKey = "ZoomIn";

		// Token: 0x04000086 RID: 134
		public static readonly string ZoomOutKey = "ZoomOut";

		// Token: 0x04000087 RID: 135
		public readonly InputService _inputService;

		// Token: 0x04000088 RID: 136
		public readonly CameraService _cameraService;

		// Token: 0x04000089 RID: 137
		public readonly CameraMovementInput _cameraMovementInput;

		// Token: 0x0400008A RID: 138
		public readonly InputSettings _inputSettings;

		// Token: 0x0400008B RID: 139
		public readonly ISpecService _specService;

		// Token: 0x0400008C RID: 140
		public KeyboardCameraControllerSpec _keyboardCameraControllerSpec;

		// Token: 0x0400008D RID: 141
		public float _remainingHorizontalJumpAngle;
	}
}
