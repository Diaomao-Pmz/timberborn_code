using System;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000014 RID: 20
	public class DraggingCameraTargetPicker : ILoadableSingleton
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00003FC9 File Offset: 0x000021C9
		public DraggingCameraTargetPicker(InputService inputService, CameraActionMarker cameraActionMarker, EventBus eventBus, ISpecService specService)
		{
			this._inputService = inputService;
			this._cameraActionMarker = cameraActionMarker;
			this._eventBus = eventBus;
			this._specService = specService;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003FF0 File Offset: 0x000021F0
		public void Load()
		{
			DraggingCameraTargetPickerSpec singleSpec = this._specService.GetSingleSpec<DraggingCameraTargetPickerSpec>();
			this._movementSpeed = singleSpec.MovementSpeed;
			this._eventBus.Register(this);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004024 File Offset: 0x00002224
		public Vector3 CameraPositionDelta()
		{
			if (this._startingMousePosition == null)
			{
				if (this._inputService.MoveButtonHeld)
				{
					this.StartDragging();
				}
			}
			else
			{
				if (this._inputService.MoveButtonHeld)
				{
					return this.CameraPositionDelta(this._startingMousePosition.Value);
				}
				this.StopDragging();
			}
			return Vector3.zero;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000407D File Offset: 0x0000227D
		[OnEvent]
		public void OnPanelShown(PanelShownEvent panelShownEvent)
		{
			if (this._startingMousePosition != null)
			{
				this.StopDragging();
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004094 File Offset: 0x00002294
		public Vector3 CameraPositionDelta(Vector2 startingMousePosition)
		{
			Vector2 vector = this._inputService.MousePositionNdc - startingMousePosition;
			Vector3 normalized = new Vector3(vector.x, 0f, vector.y).normalized;
			float num = vector.magnitude * this._movementSpeed * CappedTime.CappedUnscaledDeltaTime();
			return normalized * num;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000040EC File Offset: 0x000022EC
		public void StartDragging()
		{
			this._startingMousePosition = new Vector2?(this._inputService.MousePositionNdc);
			this._cameraActionMarker.ShowMarker(this._startingMousePosition.Value);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000411A File Offset: 0x0000231A
		public void StopDragging()
		{
			this._startingMousePosition = null;
			this._cameraActionMarker.Hide();
		}

		// Token: 0x04000065 RID: 101
		public readonly InputService _inputService;

		// Token: 0x04000066 RID: 102
		public readonly CameraActionMarker _cameraActionMarker;

		// Token: 0x04000067 RID: 103
		public readonly EventBus _eventBus;

		// Token: 0x04000068 RID: 104
		public readonly ISpecService _specService;

		// Token: 0x04000069 RID: 105
		public Vector2? _startingMousePosition;

		// Token: 0x0400006A RID: 106
		public float _movementSpeed;
	}
}
