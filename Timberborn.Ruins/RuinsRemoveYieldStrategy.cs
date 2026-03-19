using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Demolishing;
using Timberborn.ReservableSystem;
using Timberborn.Yielding;

namespace Timberborn.Ruins
{
	// Token: 0x02000013 RID: 19
	public class RuinsRemoveYieldStrategy : BaseComponent, IAwakableComponent, IRemoveYieldStrategy
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003139 File Offset: 0x00001339
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003141 File Offset: 0x00001341
		public ReservableReacher Reacher { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000314A File Offset: 0x0000134A
		public string Id
		{
			get
			{
				return "Ruins";
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003151 File Offset: 0x00001351
		public bool IsStillRemovable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003154 File Offset: 0x00001354
		public void Awake()
		{
			this.Reacher = base.GetComponent<AccessibleDemolishableReacher>();
		}
	}
}
