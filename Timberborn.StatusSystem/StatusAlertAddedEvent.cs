using System;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000010 RID: 16
	public class StatusAlertAddedEvent
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002754 File Offset: 0x00000954
		public string StatusAlert { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000275C File Offset: 0x0000095C
		public Sprite StatusSprite { get; }

		// Token: 0x0600003F RID: 63 RVA: 0x00002764 File Offset: 0x00000964
		public StatusAlertAddedEvent(string statusAlert, Sprite statusSprite)
		{
			this.StatusAlert = statusAlert;
			this.StatusSprite = statusSprite;
		}
	}
}
