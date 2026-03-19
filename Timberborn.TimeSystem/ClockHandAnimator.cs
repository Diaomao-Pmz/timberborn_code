using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.TimeSystem
{
	// Token: 0x02000007 RID: 7
	public class ClockHandAnimator : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public ClockHandAnimator(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
		public void Awake()
		{
			this._clockHandAnimatorSpec = base.GetComponent<ClockHandAnimatorSpec>();
			this._hand = base.GameObject.FindChildTransform(this._clockHandAnimatorSpec.HandName);
			this._initialRotation = this._hand.localRotation.eulerAngles;
			base.DisableComponent();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002164 File Offset: 0x00000364
		public override void Tick()
		{
			this.UpdateHandRotation();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216C File Offset: 0x0000036C
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.UpdateHandRotation();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000217A File Offset: 0x0000037A
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002184 File Offset: 0x00000384
		public void UpdateHandRotation()
		{
			float num = -this._dayNightCycle.DayProgress * 360f + this._clockHandAnimatorSpec.AngleOffset;
			this._hand.localRotation = Quaternion.Euler(new Vector3(0f, 0f, num) + this._initialRotation);
		}

		// Token: 0x04000008 RID: 8
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000009 RID: 9
		public ClockHandAnimatorSpec _clockHandAnimatorSpec;

		// Token: 0x0400000A RID: 10
		public Transform _hand;

		// Token: 0x0400000B RID: 11
		public Vector3 _initialRotation;
	}
}
