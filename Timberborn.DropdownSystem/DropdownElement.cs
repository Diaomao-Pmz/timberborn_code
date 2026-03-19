using System;
using UnityEngine.UIElements;

namespace Timberborn.DropdownSystem
{
	// Token: 0x02000007 RID: 7
	public readonly struct DropdownElement
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000027EC File Offset: 0x000009EC
		public VisualElement Content { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000027F4 File Offset: 0x000009F4
		public string Tooltip { get; }

		// Token: 0x06000024 RID: 36 RVA: 0x000027FC File Offset: 0x000009FC
		public DropdownElement(VisualElement content, string tooltip)
		{
			this.Content = content;
			this.Tooltip = tooltip;
		}
	}
}
