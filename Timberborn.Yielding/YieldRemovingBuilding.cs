using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Goods;
using Timberborn.TemplateSystem;

namespace Timberborn.Yielding
{
	// Token: 0x0200001D RID: 29
	public class YieldRemovingBuilding : BaseComponent, IAwakableComponent, IAllowedGoodProvider
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00003D60 File Offset: 0x00001F60
		public YieldRemovingBuilding(TemplateService templateService, IGoodService goodService)
		{
			this._templateService = templateService;
			this._goodService = goodService;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003D76 File Offset: 0x00001F76
		public void Awake()
		{
			this._yieldRemovingBuildingSpec = base.GetComponent<YieldRemovingBuildingSpec>();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003D84 File Offset: 0x00001F84
		public IEnumerable<string> GetAllowedGoods()
		{
			return (from yielderDecorable in this.GetAllowedYielders()
			select yielderDecorable.Yielder.Yield.Id).Distinct<string>();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public IEnumerable<IYielderDecorable> GetAllowedYielders()
		{
			if (this._yieldRemovingBuildingSpec == null)
			{
				this._yieldRemovingBuildingSpec = base.GetComponent<YieldRemovingBuildingSpec>();
			}
			return from yielderDecorable in this._templateService.GetAll<IYielderDecorable>()
			where this.IsAllowed(yielderDecorable.Yielder)
			orderby ((ComponentSpec)yielderDecorable).GetSpec<IOrderableYielder>().Order
			select yielderDecorable;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003E1F File Offset: 0x0000201F
		public bool IsAllowed(YielderSpec yielderSpec)
		{
			return this._goodService.GetGoodOrNull(yielderSpec.Yield.Id) != null && yielderSpec.ResourceGroup == this._yieldRemovingBuildingSpec.ResourceGroup;
		}

		// Token: 0x04000056 RID: 86
		public readonly TemplateService _templateService;

		// Token: 0x04000057 RID: 87
		public readonly IGoodService _goodService;

		// Token: 0x04000058 RID: 88
		public YieldRemovingBuildingSpec _yieldRemovingBuildingSpec;
	}
}
