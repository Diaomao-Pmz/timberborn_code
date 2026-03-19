using System;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x0200000A RID: 10
	public class CameraHorizontalShifter : IUpdatableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002316 File Offset: 0x00000516
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000231E File Offset: 0x0000051E
		public float CurrentOffset { get; private set; }

		// Token: 0x06000015 RID: 21 RVA: 0x00002327 File Offset: 0x00000527
		public CameraHorizontalShifter(CameraService cameraService)
		{
			this._cameraService = cameraService;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002338 File Offset: 0x00000538
		public void UpdateSingleton()
		{
			if (this._targetOffset != null)
			{
				float value = this._targetOffset.Value;
				float num = Math.Min(Time.unscaledDeltaTime, 0.05f);
				this.CurrentOffset = Mathf.Lerp(this.CurrentOffset, value, 6f * num);
				Matrix4x4 projectionMatrix = this._cameraService.ProjectionMatrix;
				projectionMatrix[0, 2] = this.CurrentOffset;
				this._cameraService.SetProjectionMatrix(projectionMatrix);
				if (Math.Abs(this.CurrentOffset - value) < 0.0001f)
				{
					this._targetOffset = null;
					if (value == 0f)
					{
						this._cameraService.ResetProjectionMatrix();
					}
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023E4 File Offset: 0x000005E4
		public void EnableHorizontalCameraShift(float offset)
		{
			Matrix4x4 identity = Matrix4x4.identity;
			identity[0, 2] = offset;
			identity[1, 2] = 0f;
			this._targetOffset = new float?((this._cameraService.ProjectionMatrix * identity)[0, 2]);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002435 File Offset: 0x00000635
		public void DisableCameraShift()
		{
			this._targetOffset = new float?(0f);
		}

		// Token: 0x04000013 RID: 19
		public readonly CameraService _cameraService;

		// Token: 0x04000014 RID: 20
		public float? _targetOffset;
	}
}
