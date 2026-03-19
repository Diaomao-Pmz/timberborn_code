using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.BehaviorSystem
{
	// Token: 0x02000004 RID: 4
	public abstract class Behavior : BaseComponent, INamedComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public string ComponentName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x06000004 RID: 4
		public abstract Decision Decide(BehaviorAgent agent);

		// Token: 0x06000005 RID: 5 RVA: 0x000020CD File Offset: 0x000002CD
		public Behavior()
		{
		}
	}
}
