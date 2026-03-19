using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Navigation;

namespace Timberborn.BlockingSystem
{
	// Token: 0x02000007 RID: 7
	public class BlockableObject : BaseComponent, IAccessibleValidator
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler ObjectBlocked;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000009 RID: 9 RVA: 0x00002170 File Offset: 0x00000370
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
		public event EventHandler ObjectUnblocked;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021DD File Offset: 0x000003DD
		public bool IsUnblocked
		{
			get
			{
				return this._blockers.IsEmpty<object>();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021EA File Offset: 0x000003EA
		public bool ValidAccessible
		{
			get
			{
				return this.IsUnblocked;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021F2 File Offset: 0x000003F2
		public void Block(object blocker)
		{
			if (this._blockers.Add(blocker) && this._blockers.Count == 1)
			{
				EventHandler objectBlocked = this.ObjectBlocked;
				if (objectBlocked == null)
				{
					return;
				}
				objectBlocked(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002226 File Offset: 0x00000426
		public void Unblock(object blocker)
		{
			if (this._blockers.Remove(blocker) && this._blockers.Count == 0)
			{
				EventHandler objectUnblocked = this.ObjectUnblocked;
				if (objectUnblocked == null)
				{
					return;
				}
				objectUnblocked(this, EventArgs.Empty);
			}
		}

		// Token: 0x0400000A RID: 10
		public readonly HashSet<object> _blockers = new HashSet<object>();
	}
}
