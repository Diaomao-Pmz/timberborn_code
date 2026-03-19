using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;
using Timberborn.Characters;
using Timberborn.LifeSystem;
using Timberborn.Localization;
using Timberborn.NotificationSystem;
using Timberborn.Persistence;
using Timberborn.SelectionSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Beavers
{
	// Token: 0x02000014 RID: 20
	public class Child : TickableComponent, IAwakableComponent, IPersistentEntity
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000029FA File Offset: 0x00000BFA
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002A02 File Offset: 0x00000C02
		public float GrowthProgress { get; private set; }

		// Token: 0x06000057 RID: 87 RVA: 0x00002A0B File Offset: 0x00000C0B
		public Child(BeaverFactory beaverFactory, NotificationBus notificationBus, LifeService lifeService, IDayNightCycle dayNightCycle, EntitySelectionService entitySelectionService, ILoc loc)
		{
			this._beaverFactory = beaverFactory;
			this._notificationBus = notificationBus;
			this._lifeService = lifeService;
			this._dayNightCycle = dayNightCycle;
			this._entitySelectionService = entitySelectionService;
			this._loc = loc;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002A40 File Offset: 0x00000C40
		public bool IsNewborn
		{
			get
			{
				return this._character.Age == 0;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002A50 File Offset: 0x00000C50
		public void Awake()
		{
			this._character = base.GetComponent<Character>();
			this._bonusManager = base.GetComponent<BonusManager>();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A6A File Offset: 0x00000C6A
		public override void Tick()
		{
			this.UpdateGrowthProgress();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002A72 File Offset: 0x00000C72
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(Child.ChildKey).Set(Child.GrowthProgressKey, this.GrowthProgress);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002A90 File Offset: 0x00000C90
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Child.ChildKey);
			this.GrowthProgress = component.Get(Child.GrowthProgressKey);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002ABA File Offset: 0x00000CBA
		public void FastForwardGrowthProgress(float growthProgress)
		{
			this.GrowthProgress += growthProgress;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002ACA File Offset: 0x00000CCA
		public bool GrowUpIfItIsTime()
		{
			if (!this._grownUp && this.GrowthProgress >= 1f)
			{
				this.GrowUp();
			}
			return this._grownUp;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002AED File Offset: 0x00000CED
		public void UpdateGrowthProgress()
		{
			this.GrowthProgress = Math.Min(this.GrowthProgress + this.GrowthProgressPerTick(), 1f);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002B0C File Offset: 0x00000D0C
		public float GrowthProgressPerTick()
		{
			return this._lifeService.CalculateGrowthProgress(this._dayNightCycle.FixedDeltaTimeInHours) * this._bonusManager.Multiplier(Child.BonusId);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002B38 File Offset: 0x00000D38
		public void GrowUp()
		{
			SelectableObject component = base.GetComponent<SelectableObject>();
			base.GameObject.SetActive(false);
			Beaver beaver = this._beaverFactory.CreateAdultFromChild(this);
			SelectableObject component2 = beaver.GetComponent<SelectableObject>();
			this._entitySelectionService.Replace(component, component2);
			this._character.DestroyCharacter();
			this._notificationBus.Post(this._loc.T<string>(Child.GrewUpLocKey, this._character.FirstName), beaver);
			this._grownUp = true;
		}

		// Token: 0x04000028 RID: 40
		public static readonly string BonusId = "GrowthSpeed";

		// Token: 0x04000029 RID: 41
		public static readonly string GrewUpLocKey = "Beaver.GrewUp";

		// Token: 0x0400002A RID: 42
		public static readonly ComponentKey ChildKey = new ComponentKey("Child");

		// Token: 0x0400002B RID: 43
		public static readonly PropertyKey<float> GrowthProgressKey = new PropertyKey<float>("GrowthProgress");

		// Token: 0x0400002D RID: 45
		public readonly BeaverFactory _beaverFactory;

		// Token: 0x0400002E RID: 46
		public readonly NotificationBus _notificationBus;

		// Token: 0x0400002F RID: 47
		public readonly LifeService _lifeService;

		// Token: 0x04000030 RID: 48
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000031 RID: 49
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000032 RID: 50
		public readonly ILoc _loc;

		// Token: 0x04000033 RID: 51
		public Character _character;

		// Token: 0x04000034 RID: 52
		public BonusManager _bonusManager;

		// Token: 0x04000035 RID: 53
		public bool _grownUp;
	}
}
