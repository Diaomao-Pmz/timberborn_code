using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;

namespace Timberborn.ReservableSystem
{
	// Token: 0x02000004 RID: 4
	public class Reservable : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public bool Reserved { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		public bool IsDeleted
		{
			get
			{
				return this._entityComponent.Deleted;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DC File Offset: 0x000002DC
		public void Awake()
		{
			this._entityComponent = base.GetComponent<EntityComponent>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020EA File Offset: 0x000002EA
		public void Reserve()
		{
			this.Reserved = true;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F3 File Offset: 0x000002F3
		public void Unreserve()
		{
			this.Reserved = false;
		}

		// Token: 0x04000007 RID: 7
		public EntityComponent _entityComponent;
	}
}
