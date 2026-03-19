using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Goods;
using Timberborn.Persistence;
using Timberborn.ReservableSystem;
using Timberborn.TemplateSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Yielding
{
	// Token: 0x02000011 RID: 17
	public class Yielder : BaseComponent, IAwakableComponent, IPersistentEntity, INamedComponent
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000052 RID: 82 RVA: 0x00002C3C File Offset: 0x00000E3C
		// (remove) Token: 0x06000053 RID: 83 RVA: 0x00002C74 File Offset: 0x00000E74
		public event EventHandler YieldAdded;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000054 RID: 84 RVA: 0x00002CAC File Offset: 0x00000EAC
		// (remove) Token: 0x06000055 RID: 85 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public event EventHandler YieldDecreased;

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002D19 File Offset: 0x00000F19
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002D21 File Offset: 0x00000F21
		public Reservable Reservable { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002D2A File Offset: 0x00000F2A
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002D32 File Offset: 0x00000F32
		public YielderSpec YielderSpec { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002D3B File Offset: 0x00000F3B
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002D43 File Offset: 0x00000F43
		public IRemoveYieldStrategy RemoveYieldStrategy { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002D4C File Offset: 0x00000F4C
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002D54 File Offset: 0x00000F54
		public string Animation { get; private set; }

		// Token: 0x0600005E RID: 94 RVA: 0x00002D5D File Offset: 0x00000F5D
		public Yielder(GoodAmountSerializer goodAmountSerializer)
		{
			this._goodAmountSerializer = goodAmountSerializer;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002D6C File Offset: 0x00000F6C
		public string ComponentName
		{
			get
			{
				return this.YielderSpec.YielderComponentName;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002D79 File Offset: 0x00000F79
		public float RemovalTimeInHours
		{
			get
			{
				return this.YielderSpec.RemovalTimeInHours;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002D86 File Offset: 0x00000F86
		public bool IsYieldRemoved
		{
			get
			{
				return this._yield.Amount == 0;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002D96 File Offset: 0x00000F96
		public Vector3 CenterPosition
		{
			get
			{
				return this._blockObjectCenter.WorldCenterGrounded;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002DA3 File Offset: 0x00000FA3
		public Vector3Int Coordinates
		{
			get
			{
				return this._blockObject.Coordinates;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public bool IsYielding
		{
			get
			{
				return this.Yield.Amount > 0;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002DCE File Offset: 0x00000FCE
		public int InstantiationOrder
		{
			get
			{
				return this._instantiatedTemplate.InstantiationOrder;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002DDB File Offset: 0x00000FDB
		public GoodAmount Yield
		{
			get
			{
				if (!base.Enabled)
				{
					return new GoodAmount(this._yield.GoodId, 0);
				}
				return this._yield;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002DFD File Offset: 0x00000FFD
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
			this._instantiatedTemplate = base.GetComponent<InstantiatedTemplate>();
			this.Reservable = base.GetComponent<Reservable>();
			base.DisableComponent();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E35 File Offset: 0x00001035
		public void Initialize(YielderSpec yielderSpec, GoodAmount yield, IRemoveYieldStrategy removeYieldStrategy, string animation)
		{
			this.YielderSpec = yielderSpec;
			this.RemoveYieldStrategy = removeYieldStrategy;
			this.Animation = animation;
			this._initialYield = yield;
			this._yield = yield;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E5B File Offset: 0x0000105B
		public void ResetYield()
		{
			this.Enable();
			this._yield = this._initialYield;
			EventHandler yieldAdded = this.YieldAdded;
			if (yieldAdded == null)
			{
				return;
			}
			yieldAdded(this, EventArgs.Empty);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002E88 File Offset: 0x00001088
		public void DecreaseYield(GoodAmount decreasedYield)
		{
			this._yield = new GoodAmount(this._yield.GoodId, this._yield.Amount - decreasedYield.Amount);
			EventHandler yieldDecreased = this.YieldDecreased;
			if (yieldDecreased == null)
			{
				return;
			}
			yieldDecreased(this, EventArgs.Empty);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002ED4 File Offset: 0x000010D4
		public void RemoveRemainingYield()
		{
			this.SetYieldToZero();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002EDC File Offset: 0x000010DC
		public void Enable()
		{
			base.EnableComponent();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002EE4 File Offset: 0x000010E4
		public void Disable()
		{
			base.DisableComponent();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002EEC File Offset: 0x000010EC
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(Yielder.YielderKey, this.ComponentName).Set<GoodAmount>(Yielder.YieldKey, this._yield, this._goodAmountSerializer);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F18 File Offset: 0x00001118
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			GoodAmount yield;
			if (entityLoader.TryGetComponent(Yielder.YielderKey, this.ComponentName, out objectLoader) && objectLoader.GetObsoletable<GoodAmount>(Yielder.YieldKey, this._goodAmountSerializer, out yield) && yield.GoodId == this.YielderSpec.Yield.Id)
			{
				this._yield = yield;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F74 File Offset: 0x00001174
		public void SetYieldToZero()
		{
			this.Enable();
			this._yield = new GoodAmount(this._initialYield.GoodId, 0);
		}

		// Token: 0x04000022 RID: 34
		public static readonly ComponentKey YielderKey = new ComponentKey("Yielder");

		// Token: 0x04000023 RID: 35
		public static readonly PropertyKey<GoodAmount> YieldKey = new PropertyKey<GoodAmount>("Yield");

		// Token: 0x0400002A RID: 42
		public readonly GoodAmountSerializer _goodAmountSerializer;

		// Token: 0x0400002B RID: 43
		public BlockObject _blockObject;

		// Token: 0x0400002C RID: 44
		public BlockObjectCenter _blockObjectCenter;

		// Token: 0x0400002D RID: 45
		public InstantiatedTemplate _instantiatedTemplate;

		// Token: 0x0400002E RID: 46
		public GoodAmount _yield;

		// Token: 0x0400002F RID: 47
		public GoodAmount _initialYield;
	}
}
