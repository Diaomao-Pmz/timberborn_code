using System;
using Timberborn.BaseComponentSystem;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.ReservableSystem;

namespace Timberborn.Forestry
{
	// Token: 0x02000011 RID: 17
	public class TreeComponent : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000288C File Offset: 0x00000A8C
		public bool CanBeReplaced
		{
			get
			{
				return this._livingNaturalResource.IsDead && !this._reservable.Reserved;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000028AB File Offset: 0x00000AAB
		public void Awake()
		{
			this._livingNaturalResource = base.GetComponent<LivingNaturalResource>();
			this._reservable = base.GetComponent<Reservable>();
		}

		// Token: 0x0400001C RID: 28
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x0400001D RID: 29
		public Reservable _reservable;
	}
}
