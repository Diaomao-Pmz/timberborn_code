using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.WorkSystem;

namespace Timberborn.Wonders
{
	// Token: 0x0200000C RID: 12
	public class WaitForInactiveWonderWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002289 File Offset: 0x00000489
		public void Awake()
		{
			this._wonder = base.GetComponent<Wonder>();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002297 File Offset: 0x00000497
		public override Decision Decide(BehaviorAgent agent)
		{
			if (!this._wonder.IsActive)
			{
				return Decision.ReleaseNow();
			}
			return Decision.ReturnNextTick();
		}

		// Token: 0x04000011 RID: 17
		public Wonder _wonder;
	}
}
