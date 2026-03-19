using System;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000021 RID: 33
	public class WalkingEnforcerToggle
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000C9 RID: 201 RVA: 0x00003F1C File Offset: 0x0000211C
		// (remove) Token: 0x060000CA RID: 202 RVA: 0x00003F54 File Offset: 0x00002154
		public event EventHandler ForcedWalkingChanged;

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003F89 File Offset: 0x00002189
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00003F91 File Offset: 0x00002191
		public bool ForcedWalking { get; private set; }

		// Token: 0x060000CD RID: 205 RVA: 0x00003F9A File Offset: 0x0000219A
		public void EnableForcedWalking()
		{
			this.ForcedWalking = true;
			this.InvokeForcedWalkingChanged();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003FA9 File Offset: 0x000021A9
		public void DisableForcedWalking()
		{
			this.ForcedWalking = false;
			this.InvokeForcedWalkingChanged();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003FB8 File Offset: 0x000021B8
		public void InvokeForcedWalkingChanged()
		{
			EventHandler forcedWalkingChanged = this.ForcedWalkingChanged;
			if (forcedWalkingChanged == null)
			{
				return;
			}
			forcedWalkingChanged(this, EventArgs.Empty);
		}
	}
}
