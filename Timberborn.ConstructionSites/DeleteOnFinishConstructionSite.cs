using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000022 RID: 34
	public class DeleteOnFinishConstructionSite : BaseComponent
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000E4 RID: 228 RVA: 0x000042D0 File Offset: 0x000024D0
		// (remove) Token: 0x060000E5 RID: 229 RVA: 0x00004308 File Offset: 0x00002508
		public event EventHandler Deleted;

		// Token: 0x060000E6 RID: 230 RVA: 0x0000433D File Offset: 0x0000253D
		public void NotifyDeleted()
		{
			EventHandler deleted = this.Deleted;
			if (deleted == null)
			{
				return;
			}
			deleted(this, EventArgs.Empty);
		}
	}
}
