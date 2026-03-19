using System;
using Timberborn.BaseComponentSystem;
using Timberborn.DeconstructionSystem;
using Timberborn.EntitySystem;
using Timberborn.InventorySystem;
using Timberborn.RecoverableGoodSystem;
using Timberborn.Stockpiles;

namespace Timberborn.GameStockpiles
{
	// Token: 0x02000004 RID: 4
	public class FixedStockpileRemover : BaseComponent, IInitializableEntity, IRecoverableGoodMultiplier
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public FixedStockpileRemover(EntityService entityService)
		{
			this._entityService = entityService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		public void InitializeEntity()
		{
			if (base.GetComponent<FixedStockpile>().IsFixedGoodInvalid)
			{
				base.GetComponent<Deconstructible>().DisableDeconstruction();
				this._isRemoved = true;
				this._entityService.Delete(this);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020FA File Offset: 0x000002FA
		public float GetMultiplierForInventory(Inventory inventory)
		{
			return (float)(this._isRemoved ? 0 : 1);
		}

		// Token: 0x04000006 RID: 6
		public readonly EntityService _entityService;

		// Token: 0x04000007 RID: 7
		public bool _isRemoved;
	}
}
