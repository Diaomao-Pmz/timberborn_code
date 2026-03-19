using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.Wonders
{
	// Token: 0x02000007 RID: 7
	public class AlreadyActivatedWonderBlocker : BaseComponent, IAwakableComponent, IWonderBlocker
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void Awake()
		{
			this._wonder = base.GetComponent<Wonder>();
			this._wonderAnimationController = base.GetComponent<WonderAnimationController>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211A File Offset: 0x0000031A
		public bool IsWonderBlocked()
		{
			return this._wonder.IsActive || this._wonderAnimationController.IsAnimating;
		}

		// Token: 0x04000008 RID: 8
		public Wonder _wonder;

		// Token: 0x04000009 RID: 9
		public WonderAnimationController _wonderAnimationController;
	}
}
