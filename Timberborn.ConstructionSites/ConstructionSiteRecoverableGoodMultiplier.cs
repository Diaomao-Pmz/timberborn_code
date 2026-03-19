using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.InventorySystem;
using Timberborn.RecoverableGoodSystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x0200001B RID: 27
	public class ConstructionSiteRecoverableGoodMultiplier : BaseComponent, IAwakableComponent, IRecoverableGoodMultiplier
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00003DC5 File Offset: 0x00001FC5
		public ConstructionSiteRecoverableGoodMultiplier(GoodRecoveryRateService goodRecoveryRateService)
		{
			this._goodRecoveryRateService = goodRecoveryRateService;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._constructionSite = base.GetComponent<ConstructionSite>();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003DEE File Offset: 0x00001FEE
		public float GetMultiplierForInventory(Inventory inventory)
		{
			if (this._blockObject.IsFinished && inventory == this._constructionSite.Inventory)
			{
				return this._goodRecoveryRateService.DemolishableRecoveryRate;
			}
			return 1f;
		}

		// Token: 0x0400005E RID: 94
		public readonly GoodRecoveryRateService _goodRecoveryRateService;

		// Token: 0x0400005F RID: 95
		public BlockObject _blockObject;

		// Token: 0x04000060 RID: 96
		public ConstructionSite _constructionSite;
	}
}
