using System;

namespace Timberborn.GoodConsumingBuildingSystem
{
	// Token: 0x02000011 RID: 17
	public class GoodConsumingToggle
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600005F RID: 95 RVA: 0x00002DA4 File Offset: 0x00000FA4
		// (remove) Token: 0x06000060 RID: 96 RVA: 0x00002DDC File Offset: 0x00000FDC
		public event EventHandler StateChanged;

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002E11 File Offset: 0x00001011
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002E19 File Offset: 0x00001019
		public bool Paused { get; private set; }

		// Token: 0x06000063 RID: 99 RVA: 0x00002E22 File Offset: 0x00001022
		public void ResumeConsumption()
		{
			if (this.Paused)
			{
				this.Paused = false;
				EventHandler stateChanged = this.StateChanged;
				if (stateChanged == null)
				{
					return;
				}
				stateChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E49 File Offset: 0x00001049
		public void PauseConsumption()
		{
			if (!this.Paused)
			{
				this.Paused = true;
				EventHandler stateChanged = this.StateChanged;
				if (stateChanged == null)
				{
					return;
				}
				stateChanged(this, EventArgs.Empty);
			}
		}
	}
}
