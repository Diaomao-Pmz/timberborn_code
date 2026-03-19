using System;
using Timberborn.BaseComponentSystem;
using Timberborn.ReservableSystem;
using Timberborn.Yielding;

namespace Timberborn.UncuttableYielding
{
	// Token: 0x02000005 RID: 5
	public class UncuttableRemoveYieldStrategy : BaseComponent, IAwakableComponent, IRemoveYieldStrategy
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002117 File Offset: 0x00000317
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000211F File Offset: 0x0000031F
		public ReservableReacher Reacher { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002128 File Offset: 0x00000328
		public string Id
		{
			get
			{
				return "Uncuttable";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000212F File Offset: 0x0000032F
		public bool IsStillRemovable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002132 File Offset: 0x00000332
		public void Awake()
		{
			this.Reacher = base.GetComponent<UncuttableReacher>();
		}
	}
}
