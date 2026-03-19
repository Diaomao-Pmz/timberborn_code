using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.InventorySystem;

namespace Timberborn.Stockpiles
{
	// Token: 0x02000009 RID: 9
	public class Stockpile : BaseComponent, IAwakableComponent, IRegisteredComponent, IFinishedStateListener
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002278 File Offset: 0x00000478
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002280 File Offset: 0x00000480
		public Inventory Inventory { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002289 File Offset: 0x00000489
		public int MaxCapacity
		{
			get
			{
				return this._stockpileSpec.MaxCapacity;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002296 File Offset: 0x00000496
		public string WhitelistedGoodType
		{
			get
			{
				return this._stockpileSpec.WhitelistedGoodType;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022A3 File Offset: 0x000004A3
		public void Awake()
		{
			this._stockpileSpec = base.GetComponent<StockpileSpec>();
			base.DisableComponent();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022B7 File Offset: 0x000004B7
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.Inventory.Enable();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022CA File Offset: 0x000004CA
		public void OnExitFinishedState()
		{
			this.Inventory.Disable();
			base.DisableComponent();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022DD File Offset: 0x000004DD
		public void InitializeInventory(Inventory inventory)
		{
			Asserts.FieldIsNull<Stockpile>(this, this.Inventory, "Inventory");
			this.Inventory = inventory;
		}

		// Token: 0x0400000D RID: 13
		public StockpileSpec _stockpileSpec;
	}
}
