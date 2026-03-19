using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;

namespace Timberborn.MortalSystem
{
	// Token: 0x02000009 RID: 9
	public class DeadRootBehavior : RootBehavior, IAwakableComponent
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002493 File Offset: 0x00000693
		public void Awake()
		{
			this._mortal = base.GetComponent<Mortal>();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024A1 File Offset: 0x000006A1
		public override Decision Decide(BehaviorAgent agent)
		{
			if (!this._mortal.Dead)
			{
				return Decision.ReleaseNow();
			}
			return Decision.ReturnNextTick();
		}

		// Token: 0x04000014 RID: 20
		public Mortal _mortal;
	}
}
