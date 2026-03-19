using System;

namespace Timberborn.TemplateSystem
{
	// Token: 0x02000008 RID: 8
	public class TemplateInstantiationOrderService
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002134 File Offset: 0x00000334
		public int GetOrder()
		{
			int num = this._order + 1;
			this._order = num;
			return num;
		}

		// Token: 0x0400000A RID: 10
		public int _order;
	}
}
