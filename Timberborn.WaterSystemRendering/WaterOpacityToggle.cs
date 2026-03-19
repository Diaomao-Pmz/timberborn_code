using System;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x0200001A RID: 26
	public class WaterOpacityToggle
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600009F RID: 159 RVA: 0x00004E44 File Offset: 0x00003044
		// (remove) Token: 0x060000A0 RID: 160 RVA: 0x00004E7C File Offset: 0x0000307C
		public event EventHandler StateChanged;

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004EB1 File Offset: 0x000030B1
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00004EB9 File Offset: 0x000030B9
		public bool Hidden { get; private set; }

		// Token: 0x060000A3 RID: 163 RVA: 0x00004EC2 File Offset: 0x000030C2
		public void HideWater()
		{
			this.Hidden = true;
			EventHandler stateChanged = this.StateChanged;
			if (stateChanged == null)
			{
				return;
			}
			stateChanged(this, EventArgs.Empty);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004EE1 File Offset: 0x000030E1
		public void ShowWater()
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
