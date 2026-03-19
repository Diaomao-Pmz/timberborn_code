using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BonusSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.RelationSystem;
using Timberborn.TickSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000015 RID: 21
	public class Worker : TickableComponent, IAwakableComponent, IPersistentEntity, IInitializableEntity, IDeletableEntity, IRegisteredComponent, IRelationOwner
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600005F RID: 95 RVA: 0x00002C74 File Offset: 0x00000E74
		// (remove) Token: 0x06000060 RID: 96 RVA: 0x00002CAC File Offset: 0x00000EAC
		public event EventHandler<EventArgs> GotEmployed;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000061 RID: 97 RVA: 0x00002CE4 File Offset: 0x00000EE4
		// (remove) Token: 0x06000062 RID: 98 RVA: 0x00002D1C File Offset: 0x00000F1C
		public event EventHandler<EventArgs> GotUnemployed;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000063 RID: 99 RVA: 0x00002D54 File Offset: 0x00000F54
		// (remove) Token: 0x06000064 RID: 100 RVA: 0x00002D8C File Offset: 0x00000F8C
		public event EventHandler RelationsChanged;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002DC1 File Offset: 0x00000FC1
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002DC9 File Offset: 0x00000FC9
		public Workplace Workplace { get; private set; }

		// Token: 0x06000067 RID: 103 RVA: 0x00002DD2 File Offset: 0x00000FD2
		public Worker(ReferenceSerializer referenceSerializer)
		{
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002DEC File Offset: 0x00000FEC
		public string WorkerType
		{
			get
			{
				return this._workerSpec.WorkerType;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002DF9 File Offset: 0x00000FF9
		public bool JobRunning
		{
			get
			{
				return this._behaviorManager.IsRunningBehavior<IJobBehavior>();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002E06 File Offset: 0x00001006
		public bool Employed
		{
			get
			{
				return base.Enabled && this.Workplace;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002E1D File Offset: 0x0000101D
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002E25 File Offset: 0x00001025
		public float WorkingSpeedMultiplier
		{
			get
			{
				return this._workingSpeedMultiplier;
			}
			set
			{
				this._workingSpeedMultiplier = value;
				this._characterAnimator.SetFloat("WorkingSpeed", value);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002E40 File Offset: 0x00001040
		public void Awake()
		{
			base.DisableComponent();
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			this._bonusManager = base.GetComponent<BonusManager>();
			this._behaviorManager = base.GetComponent<BehaviorManager>();
			this._workerSpec = base.GetComponent<WorkerSpec>();
			base.GetComponent<Character>().Died += delegate(object _, EventArgs _)
			{
				this.Unemploy();
			};
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002E9A File Offset: 0x0000109A
		public void InitializeEntity()
		{
			this.WorkingSpeedMultiplier = 1f;
			if (this._loaded)
			{
				this.AssignToWorkplaceAfterLoad();
			}
			base.EnableComponent();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002EBB File Offset: 0x000010BB
		public override void Tick()
		{
			this.WorkingSpeedMultiplier = this._bonusManager.Multiplier(Worker.WorkingSpeedBonusId);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002ED3 File Offset: 0x000010D3
		public void DeleteEntity()
		{
			this.Unemploy();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002EDB File Offset: 0x000010DB
		public void EmployAt(Workplace workplace)
		{
			if (this.Workplace != workplace)
			{
				this.Unemploy();
				this.SetWorkplace(workplace);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002EF4 File Offset: 0x000010F4
		public void Unemploy()
		{
			if (this.Workplace != null)
			{
				Workplace workplace = this.Workplace;
				this.Workplace = null;
				workplace.UnassignWorker(this);
				EventHandler<EventArgs> gotUnemployed = this.GotUnemployed;
				if (gotUnemployed != null)
				{
					gotUnemployed(this, EventArgs.Empty);
				}
				EventHandler relationsChanged = this.RelationsChanged;
				if (relationsChanged == null)
				{
					return;
				}
				relationsChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F49 File Offset: 0x00001149
		public void Save(IEntitySaver entitySaver)
		{
			if (this.Workplace)
			{
				entitySaver.GetComponent(Worker.WorkerKey).Set<Workplace>(Worker.WorkplaceKey, this.Workplace, this._referenceSerializer.Of<Workplace>());
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F80 File Offset: 0x00001180
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			Workplace workplace;
			if (entityLoader.TryGetComponent(Worker.WorkerKey, out objectLoader) && objectLoader.GetObsoletable<Workplace>(Worker.WorkplaceKey, this._referenceSerializer.Of<Workplace>(), out workplace))
			{
				this.Workplace = workplace;
				this._loaded = true;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002FC4 File Offset: 0x000011C4
		public IEnumerable<BaseComponent> GetRelations()
		{
			if (!this.Employed)
			{
				return Enumerable.Empty<BaseComponent>();
			}
			return Enumerables.One<Workplace>(this.Workplace);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002FEC File Offset: 0x000011EC
		public void SetWorkplace(Workplace workplace)
		{
			this.Workplace = workplace;
			workplace.AssignWorker(this);
			EventHandler<EventArgs> gotEmployed = this.GotEmployed;
			if (gotEmployed != null)
			{
				gotEmployed(this, EventArgs.Empty);
			}
			EventHandler relationsChanged = this.RelationsChanged;
			if (relationsChanged == null)
			{
				return;
			}
			relationsChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000302C File Offset: 0x0000122C
		public void AssignToWorkplaceAfterLoad()
		{
			if (this.Workplace)
			{
				if (!this.Workplace.GetComponent<EntityComponent>().Deleted && this.Workplace.Understaffed)
				{
					WorkplaceWorkerType component = this.Workplace.GetComponent<WorkplaceWorkerType>();
					if (component != null && component.WorkerType == this.WorkerType)
					{
						this.SetWorkplace(this.Workplace);
						return;
					}
				}
				string firstName = base.GetComponent<Character>().FirstName;
				Debug.LogWarning("After loading " + firstName + " couldn't get employed at their old workplace, " + this.Workplace.Name);
				this.Workplace = null;
			}
		}

		// Token: 0x04000023 RID: 35
		public static readonly string WorkingSpeedBonusId = "WorkingSpeed";

		// Token: 0x04000024 RID: 36
		public static readonly ComponentKey WorkerKey = new ComponentKey("Worker");

		// Token: 0x04000025 RID: 37
		public static readonly PropertyKey<Workplace> WorkplaceKey = new PropertyKey<Workplace>("Workplace");

		// Token: 0x0400002A RID: 42
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x0400002B RID: 43
		public CharacterAnimator _characterAnimator;

		// Token: 0x0400002C RID: 44
		public BonusManager _bonusManager;

		// Token: 0x0400002D RID: 45
		public BehaviorManager _behaviorManager;

		// Token: 0x0400002E RID: 46
		public WorkerSpec _workerSpec;

		// Token: 0x0400002F RID: 47
		public float _workingSpeedMultiplier = 1f;

		// Token: 0x04000030 RID: 48
		public bool _loaded;
	}
}
