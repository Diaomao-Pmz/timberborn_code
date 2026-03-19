using System;
using System.Collections.Generic;
using Timberborn.Automation;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200004A RID: 74
	public class SpringReturnService : ICommittingSingleton
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000882C File Offset: 0x00006A2C
		public void CommitTick()
		{
			for (int i = 0; i < this._levers.Count; i++)
			{
				if (this._levers[i])
				{
					this._levers[i].SpringReturnToOff();
				}
			}
			this._levers.Clear();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000887E File Offset: 0x00006A7E
		public void Register(Lever lever)
		{
			this._levers.Add(lever);
		}

		// Token: 0x04000178 RID: 376
		public readonly List<Lever> _levers = new List<Lever>();
	}
}
