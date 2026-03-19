using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.Carrying
{
	// Token: 0x02000007 RID: 7
	public class BackpackCarrier : BaseComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler BackpackChanged;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000216D File Offset: 0x0000036D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002175 File Offset: 0x00000375
		public bool IsBackpackEnabled { get; private set; }

		// Token: 0x0600000B RID: 11 RVA: 0x0000217E File Offset: 0x0000037E
		public void EnableBackpack()
		{
			this.SetBackpack(true);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002187 File Offset: 0x00000387
		public void DisableBackpack()
		{
			this.SetBackpack(false);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002190 File Offset: 0x00000390
		public void SetBackpack(bool useBackpack)
		{
			this.IsBackpackEnabled = useBackpack;
			EventHandler backpackChanged = this.BackpackChanged;
			if (backpackChanged == null)
			{
				return;
			}
			backpackChanged(this, EventArgs.Empty);
		}
	}
}
