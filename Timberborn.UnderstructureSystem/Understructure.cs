using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;

namespace Timberborn.UnderstructureSystem
{
	// Token: 0x02000007 RID: 7
	public class Understructure : BaseComponent, IDeletableEntity, IFinishedStateListener
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler Deleted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000009 RID: 9 RVA: 0x00002170 File Offset: 0x00000370
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
		public event EventHandler EnteredFinishedState;

		// Token: 0x0600000B RID: 11 RVA: 0x000021DD File Offset: 0x000003DD
		public void DeleteEntity()
		{
			EventHandler deleted = this.Deleted;
			if (deleted == null)
			{
				return;
			}
			deleted(this, EventArgs.Empty);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021F5 File Offset: 0x000003F5
		public void OnEnterFinishedState()
		{
			EventHandler enteredFinishedState = this.EnteredFinishedState;
			if (enteredFinishedState == null)
			{
				return;
			}
			enteredFinishedState(this, EventArgs.Empty);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000220D File Offset: 0x0000040D
		public void OnExitFinishedState()
		{
		}
	}
}
