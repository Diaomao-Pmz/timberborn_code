using System;
using System.Collections.Generic;
using Timberborn.Automation;

namespace Timberborn.FireworkSystem
{
	// Token: 0x0200000E RID: 14
	public class FireworkLaunchService : ISamplingSingleton
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000030AC File Offset: 0x000012AC
		public void Sample()
		{
			foreach (FireworkLauncher fireworkLauncher in this._fireworkLaunchers)
			{
				fireworkLauncher.LaunchIfArmed();
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000030FC File Offset: 0x000012FC
		public void Add(FireworkLauncher fireworkLauncher)
		{
			this._fireworkLaunchers.Add(fireworkLauncher);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000310B File Offset: 0x0000130B
		public void Remove(FireworkLauncher fireworkLauncher)
		{
			this._fireworkLaunchers.Remove(fireworkLauncher);
		}

		// Token: 0x0400004A RID: 74
		public readonly HashSet<FireworkLauncher> _fireworkLaunchers = new HashSet<FireworkLauncher>();
	}
}
