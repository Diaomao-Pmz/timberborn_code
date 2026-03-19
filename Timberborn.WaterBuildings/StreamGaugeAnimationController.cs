using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000024 RID: 36
	public class StreamGaugeAnimationController : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00004DC3 File Offset: 0x00002FC3
		public void Awake()
		{
			this._streamGaugeAnimationControllerSpec = base.GetComponent<StreamGaugeAnimationControllerSpec>();
			this._marker = base.GameObject.FindChildTransform(this._streamGaugeAnimationControllerSpec.MarkerName);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public void SetHeight(float newHeight)
		{
			Vector3 localPosition = this._marker.localPosition;
			float num = Mathf.Min(newHeight, this._streamGaugeAnimationControllerSpec.MaxHeight);
			localPosition..ctor(localPosition.x, num, localPosition.z);
			this._marker.localPosition = localPosition;
		}

		// Token: 0x0400008A RID: 138
		public StreamGaugeAnimationControllerSpec _streamGaugeAnimationControllerSpec;

		// Token: 0x0400008B RID: 139
		public Transform _marker;
	}
}
