using System;
using Timberborn.CameraSystem;
using Timberborn.InputSystem;
using UnityEngine;

namespace Timberborn.AreaSelectionSystem
{
	// Token: 0x0200001E RID: 30
	public class AreaSelectionController
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000033F5 File Offset: 0x000015F5
		public AreaSelectionController(InputService inputService, CameraService cameraService)
		{
			this._inputService = inputService;
			this._cameraService = cameraService;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000340C File Offset: 0x0000160C
		public bool ProcessInput(AreaSelectionController.RaysCallback previewCallback, AreaSelectionController.RaysCallback actionCallback, Action showNoneCallback)
		{
			if (this._inputService.MainMouseButtonUp && !this._camRotationStartedFirst)
			{
				if (this._startRay != null)
				{
					actionCallback(this._startRay.Value, this._endRay ?? this._startRay.Value, this._selectionStarted);
					this._camRotationStarted = false;
				}
				this._selectionStarted = false;
				this._startRay = null;
				this._endRay = null;
				return true;
			}
			if (this._inputService.RotateButtonUp)
			{
				this._camRotationStartedFirst = false;
				this._camRotationStarted = false;
			}
			Ray value = this._cameraService.ScreenPointToRayInGridSpace(this._inputService.MousePosition);
			bool result = false;
			if (!this._selectionStarted && this._inputService.RotateButtonHeld)
			{
				if (!this._camRotationStartedFirst)
				{
					this._camRotationStartedFirst = true;
					this._startRay = null;
					this._endRay = null;
				}
			}
			else if (!this._camRotationStartedFirst)
			{
				if (this._inputService.MainMouseButtonDown)
				{
					this._selectionStarted = true;
					this._endRay = null;
					this._startRay = (this._inputService.MouseOverUI ? null : new Ray?(value));
				}
				else if (this._selectionStarted && this._inputService.Cancel)
				{
					if (this._startRay == null && this._endRay == null)
					{
						return false;
					}
					this._startRay = null;
					this._endRay = null;
					result = true;
				}
				else if (this._inputService.MainMouseButtonHeld && this._inputService.RotateButtonDown)
				{
					if (!this._camRotationStarted)
					{
						this._camRotationStarted = true;
						if (this._startRay != null)
						{
							this._endRay = new Ray?(value);
						}
					}
				}
				else if (this._inputService.MainMouseButtonHeld && !this._camRotationStarted)
				{
					if (this._startRay != null)
					{
						this._endRay = new Ray?(value);
					}
				}
				else if (!this._camRotationStarted && !this._selectionStarted)
				{
					this._startRay = (this._inputService.MouseOverUI ? null : new Ray?(value));
					this._endRay = null;
				}
			}
			if (this._startRay != null)
			{
				previewCallback(this._startRay.Value, this._endRay ?? this._startRay.Value, this._selectionStarted);
			}
			else
			{
				showNoneCallback();
			}
			return result;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000036C7 File Offset: 0x000018C7
		public void Reset()
		{
			this._startRay = null;
			this._endRay = null;
			this._selectionStarted = false;
			this._camRotationStartedFirst = false;
			this._camRotationStarted = false;
		}

		// Token: 0x04000061 RID: 97
		public readonly InputService _inputService;

		// Token: 0x04000062 RID: 98
		public readonly CameraService _cameraService;

		// Token: 0x04000063 RID: 99
		public bool _camRotationStartedFirst;

		// Token: 0x04000064 RID: 100
		public bool _camRotationStarted;

		// Token: 0x04000065 RID: 101
		public bool _selectionStarted;

		// Token: 0x04000066 RID: 102
		public Ray? _startRay;

		// Token: 0x04000067 RID: 103
		public Ray? _endRay;

		// Token: 0x0200001F RID: 31
		// (Invoke) Token: 0x06000083 RID: 131
		public delegate void RaysCallback(Ray start, Ray end, bool selectionStarted);
	}
}
