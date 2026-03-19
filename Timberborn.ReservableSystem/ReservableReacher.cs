using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WalkingSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.ReservableSystem
{
	// Token: 0x02000005 RID: 5
	public abstract class ReservableReacher : BaseComponent, INamedComponent
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002104 File Offset: 0x00000304
		public string ComponentName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11
		public abstract IDestination Destination { get; }

		// Token: 0x0600000C RID: 12
		public abstract void NotifyReservableReached(BaseComponent agent);

		// Token: 0x0600000D RID: 13 RVA: 0x000020FC File Offset: 0x000002FC
		public ReservableReacher()
		{
		}
	}
}
