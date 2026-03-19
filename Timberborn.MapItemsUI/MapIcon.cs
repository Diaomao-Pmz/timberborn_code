using System;
using UnityEngine;

namespace Timberborn.MapItemsUI
{
	// Token: 0x02000005 RID: 5
	public class MapIcon
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C0 File Offset: 0x000002C0
		public Sprite Icon { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020C8 File Offset: 0x000002C8
		public string TooltipLocKey { get; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020D0 File Offset: 0x000002D0
		public MapIcon(Sprite icon, string tooltipLocKey)
		{
			this.Icon = icon;
			this.TooltipLocKey = tooltipLocKey;
		}
	}
}
