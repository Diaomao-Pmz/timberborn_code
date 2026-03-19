using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000011 RID: 17
	public class RegulatorTransform
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002936 File Offset: 0x00000B36
		public RegulatorTransform(Transform transform, Vector3 regulatingPosition, Quaternion regulatingRotation, Vector3 nonRegulatingPosition, Quaternion nonRegulatingRotation)
		{
			this._transform = transform;
			this._regulatingPosition = regulatingPosition;
			this._regulatingRotation = regulatingRotation;
			this._nonRegulatingPosition = nonRegulatingPosition;
			this._nonRegulatingRotation = nonRegulatingRotation;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002964 File Offset: 0x00000B64
		public static RegulatorTransform Create(GameObject parent, RegulatorTransformSpec spec, bool isRegulating)
		{
			Transform transform = parent.FindChildTransform(spec.TransformName);
			Vector3 localPosition = transform.localPosition;
			Quaternion localRotation = transform.localRotation;
			Vector3 nonRegulatingPosition = localPosition + spec.TargetOffset;
			Quaternion nonRegulatingRotation = Quaternion.Euler(spec.TargetRotation);
			RegulatorTransform regulatorTransform = new RegulatorTransform(transform, localPosition, localRotation, nonRegulatingPosition, nonRegulatingRotation);
			regulatorTransform.UpdateInstantly(isRegulating);
			return regulatorTransform;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000029B4 File Offset: 0x00000BB4
		public void UpdateSmoothly(bool isRegulating)
		{
			this._animationProgress = Mathf.MoveTowards(this._animationProgress, RegulatorTransform.GetAnimationProgress(isRegulating), RegulatorTransform.AnimationSpeed * Time.deltaTime);
			ValueTuple<Vector3, Quaternion> targetPositionAndRotation = this.GetTargetPositionAndRotation();
			Vector3 item = targetPositionAndRotation.Item1;
			Quaternion item2 = targetPositionAndRotation.Item2;
			this._transform.SetLocalPositionAndRotation(item, item2);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002A04 File Offset: 0x00000C04
		public void UpdateInstantly(bool isRegulating)
		{
			this._animationProgress = RegulatorTransform.GetAnimationProgress(isRegulating);
			ValueTuple<Vector3, Quaternion> targetPositionAndRotation = this.GetTargetPositionAndRotation();
			Vector3 item = targetPositionAndRotation.Item1;
			Quaternion item2 = targetPositionAndRotation.Item2;
			this._transform.SetLocalPositionAndRotation(item, item2);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002A3D File Offset: 0x00000C3D
		public static float GetAnimationProgress(bool isRegulating)
		{
			return (float)(isRegulating ? 1 : 0);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002A47 File Offset: 0x00000C47
		public ValueTuple<Vector3, Quaternion> GetTargetPositionAndRotation()
		{
			return new ValueTuple<Vector3, Quaternion>(Vector3.Lerp(this._regulatingPosition, this._nonRegulatingPosition, this._animationProgress), Quaternion.Lerp(this._regulatingRotation, this._nonRegulatingRotation, this._animationProgress));
		}

		// Token: 0x0400001F RID: 31
		public static readonly float AnimationSpeed = 0.25f;

		// Token: 0x04000020 RID: 32
		public readonly Transform _transform;

		// Token: 0x04000021 RID: 33
		public readonly Vector3 _regulatingPosition;

		// Token: 0x04000022 RID: 34
		public readonly Quaternion _regulatingRotation;

		// Token: 0x04000023 RID: 35
		public readonly Vector3 _nonRegulatingPosition;

		// Token: 0x04000024 RID: 36
		public readonly Quaternion _nonRegulatingRotation;

		// Token: 0x04000025 RID: 37
		public float _animationProgress;
	}
}
