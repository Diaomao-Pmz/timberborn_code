using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.InventorySystem
{
	// Token: 0x02000011 RID: 17
	public class Inventory : BaseComponent, IAwakableComponent, IPersistentEntity, IAmountProvider, INamedComponent
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600004C RID: 76 RVA: 0x00002DC8 File Offset: 0x00000FC8
		// (remove) Token: 0x0600004D RID: 77 RVA: 0x00002E00 File Offset: 0x00001000
		public event EventHandler<InventoryChangedEventArgs> InventoryChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600004E RID: 78 RVA: 0x00002E38 File Offset: 0x00001038
		// (remove) Token: 0x0600004F RID: 79 RVA: 0x00002E70 File Offset: 0x00001070
		public event EventHandler<InventoryAmountChangedEventArgs> InventoryStockChanged;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000050 RID: 80 RVA: 0x00002EA8 File Offset: 0x000010A8
		// (remove) Token: 0x06000051 RID: 81 RVA: 0x00002EE0 File Offset: 0x000010E0
		public event EventHandler<InventoryAmountChangedEventArgs> InventoryCapacityReservationChanged;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000052 RID: 82 RVA: 0x00002F18 File Offset: 0x00001118
		// (remove) Token: 0x06000053 RID: 83 RVA: 0x00002F50 File Offset: 0x00001150
		public event EventHandler UnwantedStockDisappeared;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000054 RID: 84 RVA: 0x00002F88 File Offset: 0x00001188
		// (remove) Token: 0x06000055 RID: 85 RVA: 0x00002FC0 File Offset: 0x000011C0
		public event EventHandler InventoryEnabled;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000056 RID: 86 RVA: 0x00002FF8 File Offset: 0x000011F8
		// (remove) Token: 0x06000057 RID: 87 RVA: 0x00003030 File Offset: 0x00001230
		public event EventHandler InventoryDisabled;

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003065 File Offset: 0x00001265
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000306D File Offset: 0x0000126D
		public string ComponentName { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003076 File Offset: 0x00001276
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000307E File Offset: 0x0000127E
		public int Capacity { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003087 File Offset: 0x00001287
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000308F File Offset: 0x0000128F
		public bool HasUnwantedStock { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003098 File Offset: 0x00001298
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000030A0 File Offset: 0x000012A0
		public bool PublicInput { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000030A9 File Offset: 0x000012A9
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000030B1 File Offset: 0x000012B1
		public bool PublicOutput { get; private set; }

		// Token: 0x06000062 RID: 98 RVA: 0x000030BA File Offset: 0x000012BA
		public Inventory(GoodRegistryValueSerializer goodRegistryValueSerializer)
		{
			this._goodRegistryValueSerializer = goodRegistryValueSerializer;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000030F5 File Offset: 0x000012F5
		public int TotalAmountInStock
		{
			get
			{
				return this._storage.TotalAmount;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003102 File Offset: 0x00001302
		public bool IsFull
		{
			get
			{
				return this.TotalAmountInStock >= this.Capacity;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003115 File Offset: 0x00001315
		public bool IsFullyReserved
		{
			get
			{
				return this.TotalAmountInStock + this._reservedCapacity.TotalAmount >= this.Capacity;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003134 File Offset: 0x00001334
		public bool HasAnyUnreservedStock
		{
			get
			{
				return this._storage.TotalAmount > this._reservedStock.TotalAmount;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000314E File Offset: 0x0000134E
		public bool IsEmpty
		{
			get
			{
				return this.TotalAmountInStock == 0;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003159 File Offset: 0x00001359
		public bool IsInput
		{
			get
			{
				return this._allowedGoods.HasInputGoods;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003166 File Offset: 0x00001366
		public bool IsOutput
		{
			get
			{
				return this._allowedGoods.HasOutputGoods;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003173 File Offset: 0x00001373
		public ReadOnlyList<StorableGoodAmount> AllowedGoods
		{
			get
			{
				return this._allowedGoods.Goods;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003180 File Offset: 0x00001380
		public ReadOnlyHashSet<string> InputGoods
		{
			get
			{
				return this._allowedGoods.InputGoods;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000318D File Offset: 0x0000138D
		public ReadOnlyHashSet<string> OutputGoods
		{
			get
			{
				return this._allowedGoods.OutputGoods;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000319A File Offset: 0x0000139A
		public ReadOnlyList<GoodAmount> Stock
		{
			get
			{
				return this._storage.Goods;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000031A7 File Offset: 0x000013A7
		public bool IsUnblocked
		{
			get
			{
				return this._blockableObject.IsUnblocked;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000031B4 File Offset: 0x000013B4
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._districtInventoryAssigner = base.GetComponent<DistrictInventoryAssigner>();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000031D0 File Offset: 0x000013D0
		public void Initialize(string componentName, int capacity, IEnumerable<StorableGoodAmount> storableGoodAmounts, bool publicInput, bool publicOutput, bool ignorableCapacity, IGoodDisallower goodDisallower)
		{
			this._allowedGoods.Add(storableGoodAmounts);
			this.Capacity = capacity;
			this.ComponentName = componentName;
			this.PublicInput = publicInput;
			this.PublicOutput = publicOutput;
			this._ignorableCapacity = ignorableCapacity;
			this._goodDisallower = goodDisallower;
			this._goodDisallower.DisallowedGoodsChanged += this.OnDisallowedGoodsChanged;
			base.DisableComponent();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003234 File Offset: 0x00001434
		public void Enable()
		{
			base.EnableComponent();
			DistrictInventoryAssigner districtInventoryAssigner = this._districtInventoryAssigner;
			if (districtInventoryAssigner != null)
			{
				districtInventoryAssigner.StartAssigningInventory(this);
			}
			this.CheckIfUnwantedStockAppeared();
			foreach (GoodAmount goodAmount in this._storage.Goods)
			{
				this.InvokeInventoryChangedEvent(goodAmount.GoodId);
			}
			EventHandler inventoryEnabled = this.InventoryEnabled;
			if (inventoryEnabled == null)
			{
				return;
			}
			inventoryEnabled(this, EventArgs.Empty);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000032CC File Offset: 0x000014CC
		public void Disable()
		{
			base.DisableComponent();
			DistrictInventoryAssigner districtInventoryAssigner = this._districtInventoryAssigner;
			if (districtInventoryAssigner != null)
			{
				districtInventoryAssigner.StopAssigningInventory(this);
			}
			EventHandler inventoryDisabled = this.InventoryDisabled;
			if (inventoryDisabled == null)
			{
				return;
			}
			inventoryDisabled(this, EventArgs.Empty);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000032FC File Offset: 0x000014FC
		public void Give(GoodAmount good)
		{
			this.CheckCapacity(good);
			this.CheckUnreservedCapacity(good);
			this._storage.Add(good);
			this.InvokeInventoryStockChangedEvents(good);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000331F File Offset: 0x0000151F
		public void GiveIgnoringCapacityReservation(GoodAmount good)
		{
			this.CheckCapacity(good);
			this._storage.Add(good);
			this.InvokeInventoryStockChangedEvents(good);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000333B File Offset: 0x0000153B
		public void GiveIgnoringCapacity(GoodAmount good)
		{
			this._storage.Add(good);
			this.InvokeInventoryStockChangedEvents(good);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003350 File Offset: 0x00001550
		public void ReserveCapacity(GoodAmount good)
		{
			this.CheckIfAllows(good);
			if (!this.HasUnreservedCapacity(good))
			{
				this.ThrowArgumentException(string.Format("Can't reserve capacity for {0}. Not enough unreserved capacity", good));
			}
			this._reservedCapacity.Add(good);
			this.InvokeInventoryCapacityReservationChangedEvents(good);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000338C File Offset: 0x0000158C
		public void UnreserveCapacity(GoodAmount good)
		{
			this.CheckIfAllows(good);
			if (this._reservedCapacity.Amount(good.GoodId) < good.Amount)
			{
				this.ThrowArgumentException(string.Format("Can't unreserve capacity for {0}. It wasn't previously reserved.", good));
			}
			this._reservedCapacity.Subtract(good);
			this.InvokeInventoryCapacityReservationChangedEvents(new GoodAmount(good.GoodId, -good.Amount));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000033F7 File Offset: 0x000015F7
		public int ReservedCapacity(string good)
		{
			return this._reservedCapacity.Amount(good);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003405 File Offset: 0x00001605
		public ReadOnlyList<GoodAmount> ReservedCapacity()
		{
			return this._reservedCapacity.Goods;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003414 File Offset: 0x00001614
		public int UnreservedCapacity(string goodId)
		{
			int val = this.GoodCapacity(goodId) - this._reservedCapacity.Amount(goodId);
			int val2 = this.Capacity - this._storage.TotalAmount - this._reservedCapacity.TotalAmount;
			return Math.Max(Math.Min(val, val2), 0);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003460 File Offset: 0x00001660
		public bool HasUnreservedCapacity(GoodAmount good)
		{
			return this.UnreservedCapacity(good.GoodId) >= good.Amount;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000347B File Offset: 0x0000167B
		public bool HasUnreservedCapacity(string goodId)
		{
			return this.Takes(goodId) && this.UnreservedCapacity(goodId) > 0;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003492 File Offset: 0x00001692
		public void Take(GoodAmount good)
		{
			this.CheckIfHasStock(good, "take");
			this._storage.Subtract(good);
			this.CheckIfUnwantedStockDisappeared();
			this.InvokeInventoryStockChangedEvents(new GoodAmount(good.GoodId, -good.Amount));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000034CC File Offset: 0x000016CC
		public void ReserveStock(GoodAmount good)
		{
			this.CheckIfHasStock(good, "reserve");
			this._reservedStock.Add(good);
			this.InvokeInventoryChangedEvent(good.GoodId);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000034F3 File Offset: 0x000016F3
		public bool HasUnreservedStock(GoodAmount good)
		{
			return this.UnreservedAmountInStock(good.GoodId) >= good.Amount;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003510 File Offset: 0x00001710
		public void UnreserveStock(GoodAmount good)
		{
			if (this._reservedStock.Amount(good.GoodId) < good.Amount)
			{
				this.ThrowArgumentException(string.Format("Can't unreserve {0}. It wasn't previously reserved.", good));
			}
			this._reservedStock.Subtract(good);
			this.InvokeInventoryChangedEvent(good.GoodId);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003567 File Offset: 0x00001767
		public bool HasUnreservedStock(string goodId)
		{
			return this.Gives(goodId) && this.UnreservedAmountInStock(goodId) > 0;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000357E File Offset: 0x0000177E
		public int AmountInStock(string goodId)
		{
			return this._storage.Amount(goodId);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000358C File Offset: 0x0000178C
		public int UnreservedAmountInStock(string goodId)
		{
			int num = this._storage.Amount(goodId);
			int num2 = this._reservedStock.Amount(goodId);
			return num - num2;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000035B4 File Offset: 0x000017B4
		public IEnumerable<GoodAmount> UnreservedStock()
		{
			foreach (GoodAmount goodAmount in this._storage.Goods)
			{
				string goodId = goodAmount.GoodId;
				int amount = goodAmount.Amount;
				int num = this._reservedStock.Amount(goodId);
				int num2 = amount - num;
				if (num2 > 0)
				{
					yield return new GoodAmount(goodId, num2);
				}
			}
			List<GoodAmount>.Enumerator enumerator = default(List<GoodAmount>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000035C4 File Offset: 0x000017C4
		public IEnumerable<GoodAmount> UnreservedTakeableStock()
		{
			foreach (GoodAmount goodAmount in this.UnreservedStock())
			{
				if (this.Gives(goodAmount.GoodId))
				{
					yield return goodAmount;
				}
			}
			IEnumerator<GoodAmount> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000035D4 File Offset: 0x000017D4
		public IEnumerable<GoodAmount> UnreservedUnwantedStock()
		{
			foreach (GoodAmount goodAmount in this.UnreservedStock())
			{
				string goodId = goodAmount.GoodId;
				int num = this.LimitedAmount(goodId);
				int num2 = goodAmount.Amount - num;
				if (num2 > 0)
				{
					yield return new GoodAmount(goodId, num2);
				}
			}
			IEnumerator<GoodAmount> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000035E4 File Offset: 0x000017E4
		public int UnwantedStockAmount()
		{
			int num = 0;
			foreach (GoodAmount goodAmount in this._storage.Goods)
			{
				num += Math.Max(0, goodAmount.Amount - this.LimitedAmount(goodAmount.GoodId));
			}
			return num;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000365C File Offset: 0x0000185C
		public int LimitedAmount(string goodId)
		{
			return Math.Min(this._goodDisallower.AllowedAmount(goodId), this._allowedGoods.GetAmount(goodId));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000367C File Offset: 0x0000187C
		public bool Gives(string goodId)
		{
			return this.OutputGoods.Contains(goodId);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003698 File Offset: 0x00001898
		public bool Takes(string goodId)
		{
			return this.InputGoods.Contains(goodId);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000036B4 File Offset: 0x000018B4
		public void Save(IEntitySaver entitySaver)
		{
			if (!this.IsEmpty)
			{
				entitySaver.GetComponent(Inventory.InventoryKey, this.ComponentName).Set<GoodRegistry>(Inventory.StorageKey, this._storage, this._goodRegistryValueSerializer);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000036E5 File Offset: 0x000018E5
		public void Load(IEntityLoader entityLoader)
		{
			this.LoadFromNamedComponent(entityLoader, this.ComponentName);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000036F4 File Offset: 0x000018F4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Concat(new string[]
			{
				"Inventory ",
				base.Name,
				" ",
				this.ComponentName,
				":"
			}));
			Guid entityId = base.GetComponent<EntityComponent>().EntityId;
			stringBuilder.AppendLine(string.Format("{0}: {1}", "entityId", entityId));
			stringBuilder.AppendLine(string.Format("enabled: {0}", base.Enabled));
			stringBuilder.AppendLine(string.Format("finished: {0}", base.GetComponent<BlockObject>().IsFinished));
			stringBuilder.AppendLine(string.Format("{0}: {1}", "Capacity", this.Capacity));
			stringBuilder.AppendLine(string.Format("{0}: {1}", "_allowedGoods", this._allowedGoods));
			stringBuilder.AppendLine(string.Format("{0}: {1}", "_storage", this._storage));
			stringBuilder.AppendLine(string.Format("{0}: {1}", "_reservedCapacity", this._reservedCapacity));
			stringBuilder.AppendLine(string.Format("{0}: {1}", "_reservedStock", this._reservedStock));
			return stringBuilder.ToString();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003840 File Offset: 0x00001A40
		public void GetCapacity(List<GoodAmount> capacity)
		{
			if (!this._ignorableCapacity)
			{
				foreach (StorableGoodAmount storableGoodAmount in this.AllowedGoods)
				{
					string goodId = storableGoodAmount.StorableGood.GoodId;
					int num = this.LimitedAmount(goodId);
					if (num > 0)
					{
						capacity.Add(new GoodAmount(goodId, num));
					}
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000038C4 File Offset: 0x00001AC4
		public void LoadFromNamedComponent(IEntityLoader entityLoader, string componentName)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Inventory.InventoryKey, componentName, out objectLoader))
			{
				this._storage = objectLoader.Get<GoodRegistry>(Inventory.StorageKey, this._goodRegistryValueSerializer);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000038F8 File Offset: 0x00001AF8
		public void CheckUnreservedCapacity(GoodAmount good)
		{
			if (!this.HasUnreservedCapacity(good))
			{
				this.ThrowArgumentException(string.Format("Can't give {0}. There's not enough unreserved capacity.", good));
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003919 File Offset: 0x00001B19
		public void CheckCapacity(GoodAmount good)
		{
			if (!this.HasCapacity(good))
			{
				this.ThrowArgumentException(string.Format("Can't give {0}. There's not enough capacity.", good));
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000393A File Offset: 0x00001B3A
		public bool HasStock(GoodAmount good)
		{
			return this._storage.Amount(good.GoodId) >= good.Amount;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000395A File Offset: 0x00001B5A
		public bool Allows(string goodId)
		{
			return this._allowedGoods.Contains(goodId);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003968 File Offset: 0x00001B68
		public void CheckIfAllows(GoodAmount good)
		{
			string goodId = good.GoodId;
			if (!this.Allows(goodId))
			{
				this.ThrowArgumentException(goodId + " isn't allowed.");
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003998 File Offset: 0x00001B98
		public void CheckIfHasStock(GoodAmount good, string action)
		{
			if (!this.HasStock(good))
			{
				this.ThrowArgumentException(string.Format("Can't {0} {1}. There's not enough stored.", action, good));
			}
			if (!this.HasUnreservedStock(good))
			{
				this.ThrowArgumentException(string.Format("Can't {0} {1}. There's not enough unreserved.", action, good));
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000039E8 File Offset: 0x00001BE8
		public bool HasCapacity(GoodAmount good)
		{
			int num = this.GoodCapacity(good.GoodId);
			int num2 = this.Capacity - this._storage.TotalAmount;
			return num >= good.Amount && num2 >= good.Amount;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A2D File Offset: 0x00001C2D
		public int GoodCapacity(string goodId)
		{
			return this.LimitedAmount(goodId) - this._storage.Amount(goodId);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003A43 File Offset: 0x00001C43
		public void OnDisallowedGoodsChanged(object sender, DisallowedGoodsChangedEventArgs e)
		{
			this.CheckIfUnwantedStockAppeared();
			this.CheckIfUnwantedStockDisappeared();
			this.InvokeInventoryChangedEvent(e.GoodId);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003A5E File Offset: 0x00001C5E
		public void CheckIfUnwantedStockAppeared()
		{
			if (base.Enabled && !this.HasUnwantedStock && this.UnwantedStockAmount() > 0)
			{
				this.HasUnwantedStock = true;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003A80 File Offset: 0x00001C80
		public void CheckIfUnwantedStockDisappeared()
		{
			if (base.Enabled && this.HasUnwantedStock && this.UnwantedStockAmount() == 0)
			{
				this.HasUnwantedStock = false;
				EventHandler unwantedStockDisappeared = this.UnwantedStockDisappeared;
				if (unwantedStockDisappeared == null)
				{
					return;
				}
				unwantedStockDisappeared(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003AB7 File Offset: 0x00001CB7
		public void InvokeInventoryChangedEvent(string goodId)
		{
			EventHandler<InventoryChangedEventArgs> inventoryChanged = this.InventoryChanged;
			if (inventoryChanged == null)
			{
				return;
			}
			inventoryChanged(this, new InventoryChangedEventArgs(goodId));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public void InvokeInventoryStockChangedEvents(GoodAmount goodAmount)
		{
			this.InvokeInventoryChangedEvent(goodAmount.GoodId);
			EventHandler<InventoryAmountChangedEventArgs> inventoryStockChanged = this.InventoryStockChanged;
			if (inventoryStockChanged == null)
			{
				return;
			}
			inventoryStockChanged(this, new InventoryAmountChangedEventArgs(goodAmount));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003AF6 File Offset: 0x00001CF6
		public void InvokeInventoryCapacityReservationChangedEvents(GoodAmount goodAmount)
		{
			this.InvokeInventoryChangedEvent(goodAmount.GoodId);
			EventHandler<InventoryAmountChangedEventArgs> inventoryCapacityReservationChanged = this.InventoryCapacityReservationChanged;
			if (inventoryCapacityReservationChanged == null)
			{
				return;
			}
			inventoryCapacityReservationChanged(this, new InventoryAmountChangedEventArgs(goodAmount));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003B1C File Offset: 0x00001D1C
		public void ThrowArgumentException(string customMessage)
		{
			throw new ArgumentException(customMessage + "\n" + this.ToString());
		}

		// Token: 0x04000021 RID: 33
		public static readonly ComponentKey InventoryKey = new ComponentKey("Inventory");

		// Token: 0x04000022 RID: 34
		public static readonly PropertyKey<GoodRegistry> StorageKey = new PropertyKey<GoodRegistry>("Storage");

		// Token: 0x0400002E RID: 46
		public readonly GoodRegistryValueSerializer _goodRegistryValueSerializer;

		// Token: 0x0400002F RID: 47
		public BlockableObject _blockableObject;

		// Token: 0x04000030 RID: 48
		public DistrictInventoryAssigner _districtInventoryAssigner;

		// Token: 0x04000031 RID: 49
		public readonly StorableGoodRegistry _allowedGoods = new StorableGoodRegistry();

		// Token: 0x04000032 RID: 50
		public GoodRegistry _storage = new GoodRegistry();

		// Token: 0x04000033 RID: 51
		public readonly GoodRegistry _reservedStock = new GoodRegistry();

		// Token: 0x04000034 RID: 52
		public readonly GoodRegistry _reservedCapacity = new GoodRegistry();

		// Token: 0x04000035 RID: 53
		public IGoodDisallower _goodDisallower;

		// Token: 0x04000036 RID: 54
		public bool _ignorableCapacity;
	}
}
