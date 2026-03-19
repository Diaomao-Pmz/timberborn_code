using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.InventorySystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;
using Timberborn.Stockpiles;

namespace Timberborn.StockpilesUI
{
	// Token: 0x0200002C RID: 44
	public class UnwantedStockStatus : BaseComponent, IAwakableComponent, IStartableComponent, IFinishedStateListener
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00004DE3 File Offset: 0x00002FE3
		public UnwantedStockStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004DF4 File Offset: 0x00002FF4
		public void Awake()
		{
			this._singleGoodAllower = base.GetComponent<SingleGoodAllower>();
			this._inventory = base.GetComponent<Stockpile>().Inventory;
			this._unwantedStockStatusToggle = StatusToggle.CreateNormalStatusWithFloatingIcon("Empty", this._loc.T(UnwantedStockStatus.UnwantedStockLocKey), 0f);
			base.DisableComponent();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004E49 File Offset: 0x00003049
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._unwantedStockStatusToggle);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004E5C File Offset: 0x0000305C
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.UpdateStatusToggle();
			this._singleGoodAllower.DisallowedGoodsChanged += this.OnDisallowedGoodsChanged;
			this._inventory.InventoryChanged += this.OnInventoryChanged;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004E98 File Offset: 0x00003098
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this.UpdateStatusToggle();
			this._singleGoodAllower.DisallowedGoodsChanged -= this.OnDisallowedGoodsChanged;
			this._inventory.InventoryChanged -= this.OnInventoryChanged;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004ED4 File Offset: 0x000030D4
		public void OnDisallowedGoodsChanged(object sender, DisallowedGoodsChangedEventArgs e)
		{
			this.UpdateStatusToggle();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004ED4 File Offset: 0x000030D4
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			this.UpdateStatusToggle();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004EDC File Offset: 0x000030DC
		public void UpdateStatusToggle()
		{
			if (base.Enabled && this._inventory.HasUnwantedStock)
			{
				this._unwantedStockStatusToggle.Activate();
				return;
			}
			this._unwantedStockStatusToggle.Deactivate();
		}

		// Token: 0x040000C6 RID: 198
		public static readonly string UnwantedStockLocKey = "Status.Inventory.UnwantedStock";

		// Token: 0x040000C7 RID: 199
		public readonly ILoc _loc;

		// Token: 0x040000C8 RID: 200
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x040000C9 RID: 201
		public Inventory _inventory;

		// Token: 0x040000CA RID: 202
		public StatusToggle _unwantedStockStatusToggle;
	}
}
