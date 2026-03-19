using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.InventorySystem;
using Timberborn.Localization;
using Timberborn.Navigation;
using Timberborn.Persistence;
using Timberborn.StatusSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Emptying
{
	// Token: 0x02000007 RID: 7
	public class Emptiable : BaseComponent, IAwakableComponent, IStartableComponent, IPersistentEntity, IAccessibleValidator, IFinishedStateListener, IInventoryValidator, IRegisteredComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001B RID: 27 RVA: 0x00002384 File Offset: 0x00000584
		// (remove) Token: 0x0600001C RID: 28 RVA: 0x000023BC File Offset: 0x000005BC
		public event EventHandler MarkedForEmptying;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001D RID: 29 RVA: 0x000023F4 File Offset: 0x000005F4
		// (remove) Token: 0x0600001E RID: 30 RVA: 0x0000242C File Offset: 0x0000062C
		public event EventHandler UnmarkedForEmptying;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002461 File Offset: 0x00000661
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002469 File Offset: 0x00000669
		public bool IsMarkedForEmptying { get; private set; }

		// Token: 0x06000021 RID: 33 RVA: 0x00002472 File Offset: 0x00000672
		public Emptiable(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002481 File Offset: 0x00000681
		public bool ValidAccessible
		{
			get
			{
				return !base.Enabled || !this.IsMarkedForEmptying;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002481 File Offset: 0x00000681
		public bool ValidInventory
		{
			get
			{
				return !base.Enabled || !this.IsMarkedForEmptying;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002496 File Offset: 0x00000696
		public void Awake()
		{
			this._emptyStatusToggle = StatusToggle.CreatePriorityStatusWithFloatingIcon("Empty", this._loc.T(Emptiable.EmptyingInProgressLocKey), 0f);
			base.DisableComponent();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024C3 File Offset: 0x000006C3
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._emptyStatusToggle);
			if (this.IsMarkedForEmptying)
			{
				this.MarkForEmptying();
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024E4 File Offset: 0x000006E4
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024EC File Offset: 0x000006EC
		public void OnExitFinishedState()
		{
			this.UnmarkForEmptying();
			base.DisableComponent();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024FA File Offset: 0x000006FA
		public void MarkForEmptyingWithStatus()
		{
			this.MarkForEmptying();
			this._emptyStatusToggle.Activate();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000250D File Offset: 0x0000070D
		public void MarkForEmptyingWithoutStatus()
		{
			this.MarkForEmptying();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002515 File Offset: 0x00000715
		public void UnmarkForEmptying()
		{
			this._emptyStatusToggle.Deactivate();
			this.IsMarkedForEmptying = false;
			EventHandler unmarkedForEmptying = this.UnmarkedForEmptying;
			if (unmarkedForEmptying == null)
			{
				return;
			}
			unmarkedForEmptying(this, EventArgs.Empty);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000253F File Offset: 0x0000073F
		public void Save(IEntitySaver entitySaver)
		{
			if (this.IsMarkedForEmptying)
			{
				IObjectSaver component = entitySaver.GetComponent(Emptiable.EmptiableKey);
				component.Set(Emptiable.IsMarkedForEmptyingKey, this.IsMarkedForEmptying);
				component.Set(Emptiable.StatusIsActiveKey, this._emptyStatusToggle.IsActive);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000257C File Offset: 0x0000077C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Emptiable.EmptiableKey, out objectLoader))
			{
				this.IsMarkedForEmptying = objectLoader.Get(Emptiable.IsMarkedForEmptyingKey);
				if (objectLoader.Get(Emptiable.StatusIsActiveKey))
				{
					this._emptyStatusToggle.Activate();
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025C1 File Offset: 0x000007C1
		public void MarkForEmptying()
		{
			this.IsMarkedForEmptying = true;
			EventHandler markedForEmptying = this.MarkedForEmptying;
			if (markedForEmptying == null)
			{
				return;
			}
			markedForEmptying(this, EventArgs.Empty);
		}

		// Token: 0x0400000A RID: 10
		public static readonly string EmptyingInProgressLocKey = "Status.Emptying.EmptyingInProgress";

		// Token: 0x0400000B RID: 11
		public static readonly ComponentKey EmptiableKey = new ComponentKey("Emptiable");

		// Token: 0x0400000C RID: 12
		public static readonly PropertyKey<bool> IsMarkedForEmptyingKey = new PropertyKey<bool>("IsMarkedForEmptying");

		// Token: 0x0400000D RID: 13
		public static readonly PropertyKey<bool> StatusIsActiveKey = new PropertyKey<bool>("StatusIsActive");

		// Token: 0x04000011 RID: 17
		public readonly ILoc _loc;

		// Token: 0x04000012 RID: 18
		public StatusToggle _emptyStatusToggle;
	}
}
