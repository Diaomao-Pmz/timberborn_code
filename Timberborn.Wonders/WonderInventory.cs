using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.Wonders
{
	// Token: 0x02000019 RID: 25
	public class WonderInventory : BaseComponent, IAwakableComponent, IStartableComponent, IWonderBlocker, IFinishedStateListener
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003020 File Offset: 0x00001220
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003028 File Offset: 0x00001228
		public Inventory Inventory { get; private set; }

		// Token: 0x06000085 RID: 133 RVA: 0x00003031 File Offset: 0x00001231
		public WonderInventory(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003040 File Offset: 0x00001240
		public ImmutableArray<GoodAmountSpec> RequiredGoods
		{
			get
			{
				return this._wonderInventorySpec.RequiredGoods;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000304D File Offset: 0x0000124D
		public void Awake()
		{
			this._wonder = base.GetComponent<Wonder>();
			this._wonderInventorySpec = base.GetComponent<WonderInventorySpec>();
			this._statusToggle = StatusToggle.CreateNormalStatus("LackOfResources", this._loc.T(WonderInventory.DisallowReasonLocKey));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003087 File Offset: 0x00001287
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000309C File Offset: 0x0000129C
		public void OnEnterFinishedState()
		{
			this.Inventory.Enable();
			this.Inventory.InventoryChanged += delegate(object _, InventoryChangedEventArgs _)
			{
				this.UpdateStatus();
			};
			this._wonder.WonderActivated += this.OnWonderActivated;
			this.UpdateStatus();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000030E8 File Offset: 0x000012E8
		public void OnExitFinishedState()
		{
			this.Inventory.Disable();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000030F5 File Offset: 0x000012F5
		public bool IsWonderBlocked()
		{
			return !this.Inventory.IsFull;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003105 File Offset: 0x00001305
		public void InitializeInventory(Inventory inventory)
		{
			Asserts.FieldIsNull<WonderInventory>(this, this.Inventory, "Inventory");
			this.Inventory = inventory;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000311F File Offset: 0x0000131F
		public void OnWonderActivated(object sender, EventArgs e)
		{
			this.ClearInventory();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003128 File Offset: 0x00001328
		public void ClearInventory()
		{
			foreach (GoodAmountSpec goodAmountSpec in this.RequiredGoods)
			{
				this.Inventory.Take(new GoodAmount(goodAmountSpec.Id, goodAmountSpec.Amount));
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003173 File Offset: 0x00001373
		public void UpdateStatus()
		{
			if (!this.IsWonderBlocked() || this._wonder.IsActive)
			{
				this._statusToggle.Deactivate();
				return;
			}
			this._statusToggle.Activate();
		}

		// Token: 0x0400003C RID: 60
		public static readonly string DisallowReasonLocKey = "Status.Wonder.NotEnoughGoods";

		// Token: 0x0400003E RID: 62
		public readonly ILoc _loc;

		// Token: 0x0400003F RID: 63
		public Wonder _wonder;

		// Token: 0x04000040 RID: 64
		public WonderInventorySpec _wonderInventorySpec;

		// Token: 0x04000041 RID: 65
		public StatusToggle _statusToggle;
	}
}
