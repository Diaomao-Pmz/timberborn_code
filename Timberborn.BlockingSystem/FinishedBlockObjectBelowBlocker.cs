using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.BlockingSystem
{
	// Token: 0x02000010 RID: 16
	public class FinishedBlockObjectBelowBlocker : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002ACC File Offset: 0x00000CCC
		public void Awake()
		{
			this._blockObjectBelowBlocker = base.GetComponent<BlockObjectBelowBlocker>();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002ADA File Offset: 0x00000CDA
		public void OnEnterFinishedState()
		{
			this._blockObjectBelowBlocker.Block();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002AE7 File Offset: 0x00000CE7
		public void OnExitFinishedState()
		{
			this._blockObjectBelowBlocker.Unblock();
		}

		// Token: 0x04000019 RID: 25
		public BlockObjectBelowBlocker _blockObjectBelowBlocker;
	}
}
