using System;
using Timberborn.BaseComponentSystem;
using Timberborn.ConstructionSites;
using Timberborn.InventorySystem;
using Timberborn.RecoverableGoodSystem;

namespace Timberborn.LinkedBuildingSystem
{
	// Token: 0x0200000C RID: 12
	public class LinkedConstructionSiteRecoverableGoodMultiplier : BaseComponent, IAwakableComponent, IRecoverableGoodMultiplier
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002940 File Offset: 0x00000B40
		public void Awake()
		{
			this._constructionSite = base.GetComponent<ConstructionSite>();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000294E File Offset: 0x00000B4E
		public float GetMultiplierForInventory(Inventory inventory)
		{
			if (inventory != this._constructionSite.Inventory)
			{
				return 1f;
			}
			return 0.5f;
		}

		// Token: 0x04000019 RID: 25
		public ConstructionSite _constructionSite;
	}
}
