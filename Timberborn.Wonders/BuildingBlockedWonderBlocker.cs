using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;

namespace Timberborn.Wonders
{
	// Token: 0x02000008 RID: 8
	public class BuildingBlockedWonderBlocker : BaseComponent, IAwakableComponent, IWonderBlocker
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000213E File Offset: 0x0000033E
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000214C File Offset: 0x0000034C
		public bool IsWonderBlocked()
		{
			return !this._blockableObject.IsUnblocked;
		}

		// Token: 0x0400000A RID: 10
		public BlockableObject _blockableObject;
	}
}
