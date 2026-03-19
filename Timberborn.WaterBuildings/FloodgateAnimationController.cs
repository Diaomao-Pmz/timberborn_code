using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000011 RID: 17
	public class FloodgateAnimationController : BaseComponent, IAwakableComponent, IUpdatableComponent
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003587 File Offset: 0x00001787
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000358F File Offset: 0x0000178F
		public Transform Gate { get; private set; }

		// Token: 0x060000A7 RID: 167 RVA: 0x00003598 File Offset: 0x00001798
		public void Awake()
		{
			string gateName = base.GetComponent<FloodgateAnimationControllerSpec>().GateName;
			this.Gate = base.GameObject.FindChildTransform(gateName);
			base.DisableComponent();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000035CC File Offset: 0x000017CC
		public void Update()
		{
			float height = this.GetHeight();
			this.SetHeight(height);
			if (Mathf.Abs(height - this._targetHeight) < 0.001f)
			{
				base.DisableComponent();
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003601 File Offset: 0x00001801
		public void MoveGateInstantly(float height)
		{
			this.SetHeight(height);
			base.DisableComponent();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003610 File Offset: 0x00001810
		public void MoveGateSmoothly(float height)
		{
			this._targetHeight = height;
			base.EnableComponent();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000361F File Offset: 0x0000181F
		public float GetHeight()
		{
			return Mathf.MoveTowards(this.Gate.transform.localPosition.y, this._targetHeight, Time.deltaTime * 3f);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000364C File Offset: 0x0000184C
		public void SetHeight(float height)
		{
			this.Gate.transform.localPosition = new Vector3(0f, height, 0f);
		}

		// Token: 0x04000041 RID: 65
		public float _targetHeight;
	}
}
