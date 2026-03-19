using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Goods;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.Stockpiles
{
	// Token: 0x02000007 RID: 7
	public class FixedStockpile : BaseComponent, IPersistentEntity
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public FixedStockpile(IGoodService goodService)
		{
			this._goodService = goodService;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002118 File Offset: 0x00000318
		public bool IsFixedGoodInvalid
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this._fixedGoodId) && !this._goodService.HasGood(this._fixedGoodId);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213D File Offset: 0x0000033D
		public void SetFixedGood(string goodId)
		{
			this._fixedGoodId = goodId;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002146 File Offset: 0x00000346
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(FixedStockpile.FixedStockpileKey).Set(FixedStockpile.FixedGoodIdKey, this._fixedGoodId);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002164 File Offset: 0x00000364
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(FixedStockpile.FixedStockpileKey);
			this._fixedGoodId = component.Get(FixedStockpile.FixedGoodIdKey);
		}

		// Token: 0x04000008 RID: 8
		public static readonly ComponentKey FixedStockpileKey = new ComponentKey("FixedStockpile");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<string> FixedGoodIdKey = new PropertyKey<string>("FixedGoodId");

		// Token: 0x0400000A RID: 10
		public readonly IGoodService _goodService;

		// Token: 0x0400000B RID: 11
		public string _fixedGoodId = string.Empty;
	}
}
