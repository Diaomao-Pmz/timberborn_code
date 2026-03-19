using System;
using System.Collections.Generic;
using Timberborn.TickSystem;

namespace Timberborn.ModularShafts
{
	// Token: 0x0200000A RID: 10
	public class ModularShaftAnimatorUpdater : ITickableSingleton
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002534 File Offset: 0x00000734
		public void Tick()
		{
			foreach (ModularShaftAnimator modularShaftAnimator in this._modularShaftAnimators)
			{
				modularShaftAnimator.UpdateAnimation();
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002584 File Offset: 0x00000784
		public void Register(ModularShaftAnimator modularShaftAnimator)
		{
			this._modularShaftAnimators.Add(modularShaftAnimator);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002593 File Offset: 0x00000793
		public void Unregister(ModularShaftAnimator modularShaftAnimator)
		{
			this._modularShaftAnimators.Remove(modularShaftAnimator);
		}

		// Token: 0x04000018 RID: 24
		public readonly HashSet<ModularShaftAnimator> _modularShaftAnimators = new HashSet<ModularShaftAnimator>();
	}
}
