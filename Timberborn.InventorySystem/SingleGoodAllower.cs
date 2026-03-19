using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Goods;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.InventorySystem
{
	// Token: 0x0200001E RID: 30
	public class SingleGoodAllower : BaseComponent, IAwakableComponent, IFinishedStateListener, IPersistentEntity, IInitializableGoodDisallower, IGoodDisallower, IDuplicable<SingleGoodAllower>, IDuplicable
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060000E5 RID: 229 RVA: 0x0000480C File Offset: 0x00002A0C
		// (remove) Token: 0x060000E6 RID: 230 RVA: 0x00004844 File Offset: 0x00002A44
		public event EventHandler<DisallowedGoodsChangedEventArgs> DisallowedGoodsChanged;

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004879 File Offset: 0x00002A79
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00004881 File Offset: 0x00002A81
		public string AllowedGood { get; private set; }

		// Token: 0x060000E9 RID: 233 RVA: 0x0000488A File Offset: 0x00002A8A
		public SingleGoodAllower(SerializedGoodValueSerializer serializedGoodValueSerializer)
		{
			this._serializedGoodValueSerializer = serializedGoodValueSerializer;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004899 File Offset: 0x00002A99
		public bool HasAllowedGood
		{
			get
			{
				return this.AllowedGood != null;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000048A4 File Offset: 0x00002AA4
		public void Awake()
		{
			base.DisableComponent();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000048AC File Offset: 0x00002AAC
		public void Initialize(Inventory inventory)
		{
			this._inventory = inventory;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000048B5 File Offset: 0x00002AB5
		public void Allow(string goodId)
		{
			this.Disallow();
			this.AllowedGood = goodId;
			if (goodId != null)
			{
				this.InvokeDisallowedGoodsChangedEvent(goodId);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000048D0 File Offset: 0x00002AD0
		public void Disallow()
		{
			string allowedGood = this.AllowedGood;
			this.AllowedGood = null;
			if (allowedGood != null)
			{
				this.InvokeDisallowedGoodsChangedEvent(allowedGood);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000048F5 File Offset: 0x00002AF5
		public int AllowedAmount(string goodId)
		{
			if (!(this.AllowedGood == goodId) || this.HasOtherGoods())
			{
				return 0;
			}
			return int.MaxValue;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004914 File Offset: 0x00002B14
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000048A4 File Offset: 0x00002AA4
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000491C File Offset: 0x00002B1C
		public void Save(IEntitySaver entitySaver)
		{
			if (this.HasAllowedGood)
			{
				entitySaver.GetComponent(SingleGoodAllower.SingleGoodAllowerKey).Set<SerializedGood>(SingleGoodAllower.AllowedGoodKey, new SerializedGood(this.AllowedGood), this._serializedGoodValueSerializer);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000494C File Offset: 0x00002B4C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			SerializedGood serializedGood;
			if (entityLoader.TryGetComponent(SingleGoodAllower.SingleGoodAllowerKey, out objectLoader) && objectLoader.GetObsoletable<SerializedGood>(SingleGoodAllower.AllowedGoodKey, this._serializedGoodValueSerializer, out serializedGood) && this._inventory.Takes(serializedGood.Id))
			{
				this.AllowedGood = serializedGood.Id;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000499C File Offset: 0x00002B9C
		public void DuplicateFrom(SingleGoodAllower source)
		{
			string allowedGood = source.AllowedGood;
			if (allowedGood == null || this._inventory.Takes(allowedGood))
			{
				this.Allow(allowedGood);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000049C8 File Offset: 0x00002BC8
		public bool HasOtherGoods()
		{
			foreach (GoodAmount goodAmount in this._inventory.Stock)
			{
				if (goodAmount.GoodId != this.AllowedGood)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004A38 File Offset: 0x00002C38
		public void InvokeDisallowedGoodsChangedEvent(string goodId)
		{
			EventHandler<DisallowedGoodsChangedEventArgs> disallowedGoodsChanged = this.DisallowedGoodsChanged;
			if (disallowedGoodsChanged == null)
			{
				return;
			}
			disallowedGoodsChanged(this, new DisallowedGoodsChangedEventArgs(goodId));
		}

		// Token: 0x04000057 RID: 87
		public static readonly ComponentKey SingleGoodAllowerKey = new ComponentKey("SingleGoodAllower");

		// Token: 0x04000058 RID: 88
		public static readonly PropertyKey<SerializedGood> AllowedGoodKey = new PropertyKey<SerializedGood>("AllowedGood");

		// Token: 0x0400005B RID: 91
		public readonly SerializedGoodValueSerializer _serializedGoodValueSerializer;

		// Token: 0x0400005C RID: 92
		public Inventory _inventory;
	}
}
