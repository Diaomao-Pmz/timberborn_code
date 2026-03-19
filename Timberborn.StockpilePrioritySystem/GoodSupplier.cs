using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.StockpilePrioritySystem
{
	// Token: 0x02000006 RID: 6
	public class GoodSupplier : BaseComponent, IPersistentEntity
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000013 RID: 19 RVA: 0x000022A0 File Offset: 0x000004A0
		// (remove) Token: 0x06000014 RID: 20 RVA: 0x000022D8 File Offset: 0x000004D8
		public event EventHandler GoodSupplyingChanged;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000230D File Offset: 0x0000050D
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002315 File Offset: 0x00000515
		public bool IsSupplying { get; private set; }

		// Token: 0x06000017 RID: 23 RVA: 0x0000231E File Offset: 0x0000051E
		public void Save(IEntitySaver entitySaver)
		{
			if (this.IsSupplying)
			{
				entitySaver.GetComponent(GoodSupplier.GoodSupplierKey).Set(GoodSupplier.IsSupplyingKey, this.IsSupplying);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002344 File Offset: 0x00000544
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(GoodSupplier.GoodSupplierKey, out objectLoader))
			{
				this.IsSupplying = objectLoader.Get(GoodSupplier.IsSupplyingKey);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002371 File Offset: 0x00000571
		public void EnableSupplying()
		{
			this.IsSupplying = true;
			EventHandler goodSupplyingChanged = this.GoodSupplyingChanged;
			if (goodSupplyingChanged == null)
			{
				return;
			}
			goodSupplyingChanged(this, EventArgs.Empty);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002390 File Offset: 0x00000590
		public void DisableSupplying()
		{
			this.IsSupplying = false;
			EventHandler goodSupplyingChanged = this.GoodSupplyingChanged;
			if (goodSupplyingChanged == null)
			{
				return;
			}
			goodSupplyingChanged(this, EventArgs.Empty);
		}

		// Token: 0x0400000C RID: 12
		public static readonly ComponentKey GoodSupplierKey = new ComponentKey("GoodSupplier");

		// Token: 0x0400000D RID: 13
		public static readonly PropertyKey<bool> IsSupplyingKey = new PropertyKey<bool>("IsSupplying");
	}
}
