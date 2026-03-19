using System;
using Timberborn.BlueprintSystem;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000016 RID: 22
	public class EdgePanningCameraTargetPicker : ILoadableSingleton
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00004279 File Offset: 0x00002479
		public EdgePanningCameraTargetPicker(CameraMovementInput cameraMovementInput, InputSettings inputSettings, ISpecService specService)
		{
			this._cameraMovementInput = cameraMovementInput;
			this._inputSettings = inputSettings;
			this._specService = specService;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004298 File Offset: 0x00002498
		public void Load()
		{
			EdgePanningCameraTargetPickerSpec singleSpec = this._specService.GetSingleSpec<EdgePanningCameraTargetPickerSpec>();
			this._minBaseSpeed = singleSpec.MinBaseSpeed;
			this._maxBaseSpeed = singleSpec.MaxBaseSpeed;
			this._fastMovementSpeedBonus = singleSpec.FastMovementSpeedBonus;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000042D8 File Offset: 0x000024D8
		public Vector3 CameraPositionDelta(float zoomSpeedScale)
		{
			if (!this._suspended)
			{
				Vector3 movementDirection = this.MovementDirection();
				return this.CameraPositionDelta(zoomSpeedScale, movementDirection);
			}
			return Vector3.zero;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004302 File Offset: 0x00002502
		public void Suspend()
		{
			this._suspended = true;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000430B File Offset: 0x0000250B
		public float Speed
		{
			get
			{
				return this.BaseSpeed + this.SpeedBonus;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000431A File Offset: 0x0000251A
		public float BaseSpeed
		{
			get
			{
				return this._minBaseSpeed + (this._maxBaseSpeed - this._minBaseSpeed) * this._inputSettings.EdgePanCameraSpeed;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000433C File Offset: 0x0000253C
		public float SpeedBonus
		{
			get
			{
				if (!this._cameraMovementInput.MoveCameraFast)
				{
					return 0f;
				}
				return this._fastMovementSpeedBonus;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004358 File Offset: 0x00002558
		public Vector3 MovementDirection()
		{
			ScreenEdges mouseScreenEdges = this._cameraMovementInput.GetMouseScreenEdges();
			Vector3 vector = Vector3.zero;
			if (mouseScreenEdges.HasFlag(ScreenEdges.Down))
			{
				vector += Vector3.back;
			}
			if (mouseScreenEdges.HasFlag(ScreenEdges.Left))
			{
				vector += Vector3.left;
			}
			if (mouseScreenEdges.HasFlag(ScreenEdges.Up))
			{
				vector += Vector3.forward;
			}
			if (mouseScreenEdges.HasFlag(ScreenEdges.Right))
			{
				vector += Vector3.right;
			}
			return vector;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000043F4 File Offset: 0x000025F4
		public Vector3 CameraPositionDelta(float zoomSpeedScale, Vector3 movementDirection)
		{
			if (!movementDirection.Equals(Vector3.zero))
			{
				float num = zoomSpeedScale * this.Speed * CappedTime.CappedUnscaledDeltaTime();
				return movementDirection * num;
			}
			return Vector3.zero;
		}

		// Token: 0x0400006C RID: 108
		public readonly CameraMovementInput _cameraMovementInput;

		// Token: 0x0400006D RID: 109
		public readonly InputSettings _inputSettings;

		// Token: 0x0400006E RID: 110
		public readonly ISpecService _specService;

		// Token: 0x0400006F RID: 111
		public EdgePanningCameraTargetPickerSpec _edgePanningCameraTargetPickerSpec;

		// Token: 0x04000070 RID: 112
		public bool _suspended;

		// Token: 0x04000071 RID: 113
		public float _minBaseSpeed;

		// Token: 0x04000072 RID: 114
		public float _maxBaseSpeed;

		// Token: 0x04000073 RID: 115
		public float _fastMovementSpeedBonus;
	}
}
