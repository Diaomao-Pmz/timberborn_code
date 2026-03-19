using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;

namespace Timberborn.Emptying
{
	// Token: 0x02000004 RID: 4
	public class AutoEmptiable : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._emptiable = base.GetComponent<Emptiable>();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DA File Offset: 0x000002DA
		public void OnEnterFinishedState()
		{
			this._blockableObject.ObjectUnblocked += this.OnObjectUnblocked;
			this._blockableObject.ObjectBlocked += this.OnObjectBlocked;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000210A File Offset: 0x0000030A
		public void OnExitFinishedState()
		{
			this._blockableObject.ObjectUnblocked -= this.OnObjectUnblocked;
			this._blockableObject.ObjectBlocked -= this.OnObjectBlocked;
			this._emptiable.UnmarkForEmptying();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002145 File Offset: 0x00000345
		public void OnObjectUnblocked(object sender, EventArgs e)
		{
			this._emptiable.UnmarkForEmptying();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002152 File Offset: 0x00000352
		public void OnObjectBlocked(object sender, EventArgs e)
		{
			this._emptiable.MarkForEmptyingWithoutStatus();
		}

		// Token: 0x04000006 RID: 6
		public BlockableObject _blockableObject;

		// Token: 0x04000007 RID: 7
		public Emptiable _emptiable;
	}
}
