using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.ReservableSystem;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Demolishing
{
	// Token: 0x02000008 RID: 8
	public class Demolishable : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity, IDuplicable<Demolishable>, IDuplicable, IInitializableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000B RID: 11 RVA: 0x00002160 File Offset: 0x00000360
		// (remove) Token: 0x0600000C RID: 12 RVA: 0x00002198 File Offset: 0x00000398
		public event EventHandler Marked;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600000D RID: 13 RVA: 0x000021D0 File Offset: 0x000003D0
		// (remove) Token: 0x0600000E RID: 14 RVA: 0x00002208 File Offset: 0x00000408
		public event EventHandler Unmarked;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000223D File Offset: 0x0000043D
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002245 File Offset: 0x00000445
		public bool IsMarked { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000224E File Offset: 0x0000044E
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002256 File Offset: 0x00000456
		public Reservable Reservable { get; private set; }

		// Token: 0x06000013 RID: 19 RVA: 0x0000225F File Offset: 0x0000045F
		public Demolishable(EventBus eventBus, EntityService entityService)
		{
			this._eventBus = eventBus;
			this._entityService = entityService;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002275 File Offset: 0x00000475
		public float DemolishingProgress
		{
			get
			{
				return 1f - this._demolishTimeLeft / this._demolishableSpec.DemolishTimeInHours;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000228F File Offset: 0x0000048F
		public bool ShowDemolishButtonInEntityPanel
		{
			get
			{
				return this._demolishableSpec.ShowDemolishButtonInEntityPanel;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000229C File Offset: 0x0000049C
		public void Awake()
		{
			this.Reservable = base.GetComponent<Reservable>();
			this._demolishJob = base.GetComponent<DemolishJob>();
			this._demolishableSpec = base.GetComponent<DemolishableSpec>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._demolishTimeLeft = this._demolishableSpec.DemolishTimeInHours;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022EA File Offset: 0x000004EA
		public void InitializeEntity()
		{
			if (this._markPostLoad)
			{
				this.Mark();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022FA File Offset: 0x000004FA
		public void DeleteEntity()
		{
			this.Unmark();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002302 File Offset: 0x00000502
		public void Save(IEntitySaver entitySaver)
		{
			if (this.IsMarked)
			{
				IObjectSaver component = entitySaver.GetComponent(Demolishable.DemolishableKey);
				component.Set(Demolishable.IsMarkedKey, this.IsMarked);
				component.Set(Demolishable.DemolishTimeLeft, this._demolishTimeLeft);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002338 File Offset: 0x00000538
		[BackwardCompatible(2025, 8, 20, Compatibility.Map)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Demolishable.DemolishableKey, out objectLoader))
			{
				this._markPostLoad = (objectLoader.Has<bool>(Demolishable.IsMarkedKey) && objectLoader.Get(Demolishable.IsMarkedKey));
				if (objectLoader.Has<float>(Demolishable.DemolishTimeLeft))
				{
					this._demolishTimeLeft = Math.Clamp(objectLoader.Get(Demolishable.DemolishTimeLeft), 0f, this._demolishableSpec.DemolishTimeInHours);
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023A8 File Offset: 0x000005A8
		public void DuplicateFrom(Demolishable source)
		{
			if (!this.IsMarked && source.IsMarked)
			{
				this.Mark();
				return;
			}
			if (this.IsMarked && !source.IsMarked)
			{
				this.Unmark();
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023D8 File Offset: 0x000005D8
		public void Mark()
		{
			if (this._blockObject.Overridable)
			{
				this._entityService.Delete(this);
				return;
			}
			if (!this.IsMarked)
			{
				this.IsMarked = true;
				this._demolishJob.Enable();
				this._blockObject.OverridableChanged += this.OnOverridableChanged;
				EventHandler marked = this.Marked;
				if (marked != null)
				{
					marked(this, EventArgs.Empty);
				}
				this._eventBus.Post(new DemolishableMarkedEvent(this));
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002458 File Offset: 0x00000658
		public void Unmark()
		{
			if (this.IsMarked)
			{
				this.IsMarked = false;
				this._demolishJob.Disable();
				this._blockObject.OverridableChanged -= this.OnOverridableChanged;
				EventHandler unmarked = this.Unmarked;
				if (unmarked != null)
				{
					unmarked(this, EventArgs.Empty);
				}
				this._eventBus.Post(new DemolishableUnmarkedEvent(this));
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024BE File Offset: 0x000006BE
		public void ProgressDemolition(float deltaTime)
		{
			this._demolishTimeLeft -= deltaTime;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024CE File Offset: 0x000006CE
		public void OnOverridableChanged(object sender, bool overridable)
		{
			if (overridable)
			{
				this._entityService.Delete(this);
			}
		}

		// Token: 0x04000009 RID: 9
		public static readonly ComponentKey DemolishableKey = new ComponentKey("Demolishable");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<bool> IsMarkedKey = new PropertyKey<bool>("IsMarked");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<float> DemolishTimeLeft = new PropertyKey<float>("DemolishTimeLeft");

		// Token: 0x04000010 RID: 16
		public readonly EventBus _eventBus;

		// Token: 0x04000011 RID: 17
		public readonly EntityService _entityService;

		// Token: 0x04000012 RID: 18
		public DemolishJob _demolishJob;

		// Token: 0x04000013 RID: 19
		public DemolishableSpec _demolishableSpec;

		// Token: 0x04000014 RID: 20
		public BlockObject _blockObject;

		// Token: 0x04000015 RID: 21
		public bool _markPostLoad;

		// Token: 0x04000016 RID: 22
		public float _demolishTimeLeft;
	}
}
