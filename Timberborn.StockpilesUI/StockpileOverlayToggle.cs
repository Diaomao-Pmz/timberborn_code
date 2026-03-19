using System;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000028 RID: 40
	public class StockpileOverlayToggle
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000EB RID: 235 RVA: 0x000049A8 File Offset: 0x00002BA8
		// (remove) Token: 0x060000EC RID: 236 RVA: 0x000049E0 File Offset: 0x00002BE0
		public event EventHandler StateChanged;

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004A15 File Offset: 0x00002C15
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004A1D File Offset: 0x00002C1D
		public bool Enabled { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004A26 File Offset: 0x00002C26
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00004A2E File Offset: 0x00002C2E
		public bool Hidden { get; private set; }

		// Token: 0x060000F1 RID: 241 RVA: 0x00004A37 File Offset: 0x00002C37
		public void EnableOverlay()
		{
			this.Enabled = true;
			EventHandler stateChanged = this.StateChanged;
			if (stateChanged == null)
			{
				return;
			}
			stateChanged(this, EventArgs.Empty);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004A56 File Offset: 0x00002C56
		public void DisableOverlay()
		{
			this.Enabled = false;
			EventHandler stateChanged = this.StateChanged;
			if (stateChanged == null)
			{
				return;
			}
			stateChanged(this, EventArgs.Empty);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004A75 File Offset: 0x00002C75
		public void HideOverlay()
		{
			this.Hidden = true;
			EventHandler stateChanged = this.StateChanged;
			if (stateChanged == null)
			{
				return;
			}
			stateChanged(this, EventArgs.Empty);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004A94 File Offset: 0x00002C94
		public void ShowOverlay()
		{
			this.Hidden = false;
			EventHandler stateChanged = this.StateChanged;
			if (stateChanged == null)
			{
				return;
			}
			stateChanged(this, EventArgs.Empty);
		}
	}
}
