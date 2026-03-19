using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.EnterableSystem
{
	// Token: 0x02000012 RID: 18
	public class Enterer : BaseComponent, IAwakableComponent, IStartableComponent, IPersistentEntity, IDeletableEntity
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000081 RID: 129 RVA: 0x00002FF4 File Offset: 0x000011F4
		// (remove) Token: 0x06000082 RID: 130 RVA: 0x0000302C File Offset: 0x0000122C
		public event EventHandler<EnteredEnterableEventArgs> EnteredEnterable;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000083 RID: 131 RVA: 0x00003064 File Offset: 0x00001264
		// (remove) Token: 0x06000084 RID: 132 RVA: 0x0000309C File Offset: 0x0000129C
		public event EventHandler ExitedEnterable;

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000030D1 File Offset: 0x000012D1
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000030D9 File Offset: 0x000012D9
		public Enterable CurrentBuilding { get; private set; }

		// Token: 0x06000087 RID: 135 RVA: 0x000030E2 File Offset: 0x000012E2
		public Enterer(ReferenceSerializer referenceSerializer)
		{
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000030F1 File Offset: 0x000012F1
		public bool IsInside
		{
			get
			{
				return this.CurrentBuilding != null;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000030FC File Offset: 0x000012FC
		public void Awake()
		{
			this._characterModel = base.GetComponent<CharacterModel>();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000310A File Offset: 0x0000130A
		public void Start()
		{
			this.ResolveLoadedState();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003112 File Offset: 0x00001312
		public void DeleteEntity()
		{
			this.UnreserveSlotAndExit();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000311C File Offset: 0x0000131C
		public void Save(IEntitySaver entitySaver)
		{
			if (this.HasReservedSlot || this.IsInside)
			{
				IObjectSaver component = entitySaver.GetComponent(Enterer.EntererKey);
				if (this.HasReservedSlot)
				{
					component.Set<Enterable>(Enterer.ReservedBuildingKey, this._reservedBuilding, this._referenceSerializer.Of<Enterable>());
				}
				if (this.IsInside)
				{
					component.Set<Enterable>(Enterer.CurrentBuildingKey, this.CurrentBuilding, this._referenceSerializer.Of<Enterable>());
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003190 File Offset: 0x00001390
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Enterer.EntererKey, out objectLoader))
			{
				Enterable loadedReservedBuilding;
				if (objectLoader.Has<Enterable>(Enterer.ReservedBuildingKey) && objectLoader.GetObsoletable<Enterable>(Enterer.ReservedBuildingKey, this._referenceSerializer.Of<Enterable>(), out loadedReservedBuilding))
				{
					this._loadedReservedBuilding = loadedReservedBuilding;
				}
				Enterable loadedCurrentBuilding;
				if (objectLoader.Has<Enterable>(Enterer.CurrentBuildingKey) && objectLoader.GetObsoletable<Enterable>(Enterer.CurrentBuildingKey, this._referenceSerializer.Of<Enterable>(), out loadedCurrentBuilding))
				{
					this._loadedCurrentBuilding = loadedCurrentBuilding;
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003208 File Offset: 0x00001408
		public void ReserveSlot(Enterable enterable)
		{
			this.UnreserveSlot();
			enterable.ReserveSlot();
			this._reservedBuilding = enterable;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000321D File Offset: 0x0000141D
		public void UnreserveSlot()
		{
			if (this.HasReservedSlot)
			{
				this._reservedBuilding.UnreserveSlot();
				this._reservedBuilding = null;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000323C File Offset: 0x0000143C
		public void Enter(Enterable enterable)
		{
			if (this.CurrentBuilding)
			{
				throw new InvalidOperationException(string.Format("{0} tried to enter {1} while already inside {2}", this, enterable, this.CurrentBuilding));
			}
			this.UnreserveSlot();
			if (enterable.CanEnter)
			{
				this._characterModel.Hide();
				this.CurrentBuilding = enterable;
				enterable.Add(this);
				EventHandler<EnteredEnterableEventArgs> enteredEnterable = this.EnteredEnterable;
				if (enteredEnterable == null)
				{
					return;
				}
				enteredEnterable(this, new EnteredEnterableEventArgs(enterable));
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000032AC File Offset: 0x000014AC
		public void Exit()
		{
			if (this.CurrentBuilding)
			{
				this.CurrentBuilding.Remove(this);
				this.Abandon();
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000032D0 File Offset: 0x000014D0
		public void Abandon()
		{
			Enterable currentBuilding = this.CurrentBuilding;
			this.CurrentBuilding = null;
			this._characterModel.Rotation = currentBuilding.ExitWorldSpaceRotation;
			this._characterModel.Show();
			if (currentBuilding != null)
			{
				EventHandler exitedEnterable = this.ExitedEnterable;
				if (exitedEnterable == null)
				{
					return;
				}
				exitedEnterable(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003320 File Offset: 0x00001520
		public void UnreserveSlotAndExit()
		{
			this.UnreserveSlot();
			this.Exit();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000332E File Offset: 0x0000152E
		public bool HasReservedSlot
		{
			get
			{
				return this._reservedBuilding;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000333B File Offset: 0x0000153B
		public void ResolveLoadedState()
		{
			if (this._loadedReservedBuilding)
			{
				this.ReserveSlot(this._loadedReservedBuilding);
			}
			if (this._loadedCurrentBuilding)
			{
				this.Enter(this._loadedCurrentBuilding);
			}
		}

		// Token: 0x04000022 RID: 34
		public static readonly ComponentKey EntererKey = new ComponentKey("Enterer");

		// Token: 0x04000023 RID: 35
		public static readonly PropertyKey<Enterable> ReservedBuildingKey = new PropertyKey<Enterable>("ReservedBuilding");

		// Token: 0x04000024 RID: 36
		public static readonly PropertyKey<Enterable> CurrentBuildingKey = new PropertyKey<Enterable>("CurrentBuilding");

		// Token: 0x04000028 RID: 40
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x04000029 RID: 41
		public CharacterModel _characterModel;

		// Token: 0x0400002A RID: 42
		public Enterable _reservedBuilding;

		// Token: 0x0400002B RID: 43
		public Enterable _loadedReservedBuilding;

		// Token: 0x0400002C RID: 44
		public Enterable _loadedCurrentBuilding;
	}
}
