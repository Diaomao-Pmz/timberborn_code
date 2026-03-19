using System;
using Timberborn.CameraSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x02000014 RID: 20
	public class DistrictConnectionLineRotator : IUpdatableSingleton
	{
		// Token: 0x0600007D RID: 125 RVA: 0x0000329D File Offset: 0x0000149D
		public DistrictConnectionLineRotator(CameraService cameraService)
		{
			this._cameraService = cameraService;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000032AC File Offset: 0x000014AC
		public void UpdateSingleton()
		{
			if (this._enabled)
			{
				if (this._simpleRotation)
				{
					this.SimpleRotation();
					return;
				}
				this.Rotation();
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000032CB File Offset: 0x000014CB
		public void StartRotatingSimple(Vector3 start, Vector3 end, Transform transformToRotate)
		{
			this._transformToRotate = transformToRotate;
			this.SetLineDirection(start, end);
			this._start = start;
			this._simpleRotation = true;
			this._enabled = true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000032F4 File Offset: 0x000014F4
		public void StartRotating(Vector3 start, Vector3 end, Transform transformToRotate)
		{
			this._transformToRotate = transformToRotate;
			this.SetLineDirection(start, end);
			this._rightVector = this._transformToRotate.right;
			this._upVector = this._transformToRotate.up;
			this._forwardVector = this._transformToRotate.forward;
			this._simpleRotation = false;
			this._enabled = true;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003351 File Offset: 0x00001551
		public void StopRotating()
		{
			this._transformToRotate = null;
			this._enabled = false;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003364 File Offset: 0x00001564
		public void SetLineDirection(Vector3 start, Vector3 end)
		{
			Vector3 normalized = (end - start).normalized;
			Vector3 vector = Vector3.Cross(normalized, Vector3.up);
			Vector3 vector2 = Vector3.Cross(normalized, vector);
			this._startingRotation = Quaternion.LookRotation(vector2, Vector3.Cross(Vector3.up, vector));
			this._transformToRotate.rotation = this._startingRotation;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000033BC File Offset: 0x000015BC
		public void SimpleRotation()
		{
			Vector3 vector = this._cameraService.Transform.position - this._start;
			vector.y = 0f;
			this._transformToRotate.rotation = Quaternion.LookRotation(vector);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003404 File Offset: 0x00001604
		public void Rotation()
		{
			Vector3 normalized = this._cameraService.Transform.forward.normalized;
			float num = (Vector3.SignedAngle(this._forwardVector, normalized, Vector3.Cross(this._forwardVector, normalized)) + DistrictConnectionLineRotator.TiltStartingAngle) / (180f - DistrictConnectionLineRotator.TiltStartingAngle);
			float num2 = Mathf.Sign(Vector3.Dot(-this._rightVector, normalized));
			float xztiltRatio = this.GetXZTiltRatio(normalized);
			float num3 = Mathf.Lerp(0f, DistrictConnectionLineRotator.MaxTilt, num) * num2 * xztiltRatio;
			this._transformToRotate.rotation = Quaternion.AngleAxis(-num3, this._upVector) * this._startingRotation;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000034B0 File Offset: 0x000016B0
		public float GetXZTiltRatio(Vector3 cameraForward)
		{
			Vector3 vector = cameraForward;
			vector.y = 0f;
			Vector3 upVector = this._upVector;
			upVector.y = 0f;
			float num = Vector3.Dot(vector.normalized, upVector.normalized);
			return 1f - (float)Math.Pow((double)num, 4.0);
		}

		// Token: 0x0400003E RID: 62
		public static readonly float TiltStartingAngle = 35f;

		// Token: 0x0400003F RID: 63
		public static readonly float MaxTilt = 45f;

		// Token: 0x04000040 RID: 64
		public readonly CameraService _cameraService;

		// Token: 0x04000041 RID: 65
		public Transform _transformToRotate;

		// Token: 0x04000042 RID: 66
		public Quaternion _startingRotation;

		// Token: 0x04000043 RID: 67
		public Vector3 _upVector;

		// Token: 0x04000044 RID: 68
		public Vector3 _rightVector;

		// Token: 0x04000045 RID: 69
		public Vector3 _forwardVector;

		// Token: 0x04000046 RID: 70
		public Vector3 _start;

		// Token: 0x04000047 RID: 71
		public bool _simpleRotation;

		// Token: 0x04000048 RID: 72
		public bool _enabled;
	}
}
