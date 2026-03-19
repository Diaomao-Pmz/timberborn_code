using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.TemplateSystem
{
	// Token: 0x02000007 RID: 7
	public class InstantiatedTemplate : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public int InstantiationOrder { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x0000210F File Offset: 0x0000030F
		public InstantiatedTemplate(TemplateInstantiationOrderService templateInstantiationOrderService)
		{
			this._templateInstantiationOrderService = templateInstantiationOrderService;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000211E File Offset: 0x0000031E
		public void Awake()
		{
			this.InstantiationOrder = this._templateInstantiationOrderService.GetOrder();
		}

		// Token: 0x04000009 RID: 9
		public readonly TemplateInstantiationOrderService _templateInstantiationOrderService;
	}
}
