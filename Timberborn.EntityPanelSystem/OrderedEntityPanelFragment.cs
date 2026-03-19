using System;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x0200001A RID: 26
	public class OrderedEntityPanelFragment
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003D2C File Offset: 0x00001F2C
		public IEntityPanelFragment Fragment { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003D34 File Offset: 0x00001F34
		public int Order { get; }

		// Token: 0x060000B3 RID: 179 RVA: 0x00003D3C File Offset: 0x00001F3C
		public OrderedEntityPanelFragment(IEntityPanelFragment fragment, int order)
		{
			this.Fragment = fragment;
			this.Order = order;
		}
	}
}
