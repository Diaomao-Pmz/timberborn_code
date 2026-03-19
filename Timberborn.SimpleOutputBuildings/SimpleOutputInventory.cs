using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.InventorySystem;

namespace Timberborn.SimpleOutputBuildings
{
	// Token: 0x02000009 RID: 9
	public class SimpleOutputInventory : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002178 File Offset: 0x00000378
		public Inventory Inventory { get; private set; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002181 File Offset: 0x00000381
		public void Awake()
		{
			base.DisableComponent();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002189 File Offset: 0x00000389
		public void InitializeInventory(Inventory inventory)
		{
			Asserts.FieldIsNull<SimpleOutputInventory>(this, this.Inventory, "Inventory");
			this.Inventory = inventory;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021A3 File Offset: 0x000003A3
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.Inventory.Enable();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021B6 File Offset: 0x000003B6
		public void OnExitFinishedState()
		{
			this.Inventory.Disable();
			base.DisableComponent();
		}
	}
}
