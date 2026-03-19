using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.InventorySystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.StockpilesUI
{
	// Token: 0x0200000F RID: 15
	public class NoGoodAllowedStatus : BaseComponent, IAwakableComponent, IStartableComponent, IFinishedStateListener
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000029F3 File Offset: 0x00000BF3
		public NoGoodAllowedStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A04 File Offset: 0x00000C04
		public void Awake()
		{
			this._singleGoodAllower = base.GetComponent<SingleGoodAllower>();
			this._noGoodSelectedStatusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon("UnspecifiedGood", this._loc.T(NoGoodAllowedStatus.NoGoodSelectedLocKey), this._loc.T(NoGoodAllowedStatus.NoGoodSelectedShortLocKey), 0f);
			base.DisableComponent();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A58 File Offset: 0x00000C58
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._noGoodSelectedStatusToggle);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A6B File Offset: 0x00000C6B
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.UpdateStatusToggle();
			this._singleGoodAllower.DisallowedGoodsChanged += this.OnDisallowedGoodsChanged;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A90 File Offset: 0x00000C90
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this.UpdateStatusToggle();
			this._singleGoodAllower.DisallowedGoodsChanged -= this.OnDisallowedGoodsChanged;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AB5 File Offset: 0x00000CB5
		public void OnDisallowedGoodsChanged(object sender, DisallowedGoodsChangedEventArgs e)
		{
			this.UpdateStatusToggle();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002ABD File Offset: 0x00000CBD
		public void UpdateStatusToggle()
		{
			if (base.Enabled && !this._singleGoodAllower.HasAllowedGood)
			{
				this._noGoodSelectedStatusToggle.Activate();
				return;
			}
			this._noGoodSelectedStatusToggle.Deactivate();
		}

		// Token: 0x0400002C RID: 44
		public static readonly string NoGoodSelectedLocKey = "Status.Inventory.NoGoodSelected";

		// Token: 0x0400002D RID: 45
		public static readonly string NoGoodSelectedShortLocKey = "Status.Inventory.NoGoodSelected.Short";

		// Token: 0x0400002E RID: 46
		public readonly ILoc _loc;

		// Token: 0x0400002F RID: 47
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x04000030 RID: 48
		public StatusToggle _noGoodSelectedStatusToggle;
	}
}
