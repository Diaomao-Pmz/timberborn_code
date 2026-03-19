using System;

namespace Timberborn.ConstructionGuidelines
{
	// Token: 0x02000010 RID: 16
	public class ConstructionGuidelinesToggle
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003196 File Offset: 0x00001396
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000319E File Offset: 0x0000139E
		public bool Visible { get; private set; }

		// Token: 0x0600004D RID: 77 RVA: 0x000031A7 File Offset: 0x000013A7
		public void ShowGuidelines()
		{
			this.Visible = true;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000031B0 File Offset: 0x000013B0
		public void HideGuidelines()
		{
			this.Visible = false;
		}
	}
}
