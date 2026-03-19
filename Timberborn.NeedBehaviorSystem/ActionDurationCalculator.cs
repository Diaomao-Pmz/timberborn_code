using System;
using Timberborn.BaseComponentSystem;
using Timberborn.NeedSystem;
using Timberborn.WalkingSystem;
using UnityEngine;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x02000007 RID: 7
	public class ActionDurationCalculator : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void Awake()
		{
			this._walker = base.GetComponent<Walker>();
			this._needManager = base.GetComponent<NeedManager>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211A File Offset: 0x0000031A
		public float DurationWithReturnInHours(Vector3 actionPosition, Vector3 returnPosition)
		{
			return this.TravelTimeBetween(base.Transform.position, actionPosition) + this.TravelTimeBetween(actionPosition, returnPosition) + 0.3f;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213D File Offset: 0x0000033D
		public float FullyEffectiveDurationInHours(in EssentialAction essentialAction)
		{
			return this.TravelTimeBetween(base.Transform.position, essentialAction.Position) + essentialAction.MinDurationInHours + this._needManager.FullyEffectiveDurationInHours(essentialAction.Effect);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216F File Offset: 0x0000036F
		public float TravelTimeBetween(Vector3 actionPosition, Vector3 position)
		{
			return this._walker.CalculateTravelTimeInHours(actionPosition, position);
		}

		// Token: 0x04000008 RID: 8
		public Walker _walker;

		// Token: 0x04000009 RID: 9
		public NeedManager _needManager;
	}
}
