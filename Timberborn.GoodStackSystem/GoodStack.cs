using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;

namespace Timberborn.GoodStackSystem
{
	// Token: 0x02000007 RID: 7
	public class GoodStack : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity, IGoodStackInventory
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler GoodStackDisabled;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000216D File Offset: 0x0000036D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002175 File Offset: 0x00000375
		public Inventory Inventory { get; private set; }

		// Token: 0x0600000B RID: 11 RVA: 0x0000217E File Offset: 0x0000037E
		public GoodStack(GoodStackModelFactory goodStackModelFactory)
		{
			this._goodStackModelFactory = goodStackModelFactory;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000218D File Offset: 0x0000038D
		public void Awake()
		{
			this._goodStackAccessible = base.GetComponent<GoodStackAccessible>();
			this._goodStackModel = base.GetComponent<GoodStackModel>();
			base.DisableComponent();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021AD File Offset: 0x000003AD
		public void InitializeInventory(Inventory inventory)
		{
			Asserts.FieldIsNull<GoodStack>(this, this.Inventory, "Inventory");
			this.Inventory = inventory;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021C7 File Offset: 0x000003C7
		public void InitializeEntity()
		{
			this._goodStackModelFactory.Create(this);
			if (!this.Inventory.IsEmpty)
			{
				this.EnableGoodStack();
				this._goodStackModel.UpdateModel(this.Inventory);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021F9 File Offset: 0x000003F9
		public void DeleteEntity()
		{
			this.DisableGoodStack();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002201 File Offset: 0x00000401
		public void EnableGoodStack(GoodAmount goodAmount)
		{
			this.EnableGoodStack();
			this.Inventory.Give(goodAmount);
			this._goodStackModel.UpdateModel(this.Inventory);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002226 File Offset: 0x00000426
		public void EnableGoodStack()
		{
			this.Inventory.Enable();
			this.Inventory.InventoryChanged += this.UpdateGoodStack;
			this._goodStackAccessible.Enable();
			base.EnableComponent();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000225C File Offset: 0x0000045C
		public void DisableGoodStack()
		{
			this.Inventory.Disable();
			this.Inventory.InventoryChanged -= this.UpdateGoodStack;
			this._goodStackAccessible.Disable();
			EventHandler goodStackDisabled = this.GoodStackDisabled;
			if (goodStackDisabled != null)
			{
				goodStackDisabled(this, EventArgs.Empty);
			}
			base.DisableComponent();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022B3 File Offset: 0x000004B3
		public void UpdateGoodStack(object sender, InventoryChangedEventArgs e)
		{
			this._goodStackModel.UpdateModel(this.Inventory);
			if (this.Inventory.IsEmpty)
			{
				this.DisableGoodStack();
			}
		}

		// Token: 0x0400000A RID: 10
		public readonly GoodStackModelFactory _goodStackModelFactory;

		// Token: 0x0400000B RID: 11
		public GoodStackAccessible _goodStackAccessible;

		// Token: 0x0400000C RID: 12
		public GoodStackModel _goodStackModel;
	}
}
