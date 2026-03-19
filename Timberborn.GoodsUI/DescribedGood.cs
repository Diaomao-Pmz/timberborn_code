using System;
using UnityEngine;

namespace Timberborn.GoodsUI
{
	// Token: 0x02000004 RID: 4
	public readonly struct DescribedGood
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public string DisplayName { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public Sprite Icon { get; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		public DescribedGood(string displayName, Sprite icon)
		{
			this.DisplayName = displayName;
			this.Icon = icon;
		}
	}
}
