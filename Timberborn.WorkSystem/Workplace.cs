using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.EnterableSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.RelationSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000020 RID: 32
	public class Workplace : BaseComponent, IAwakableComponent, IStartableComponent, IPersistentEntity, IDuplicable<Workplace>, IDuplicable, IFinishedStateListener, IRegisteredComponent, IFinishedPausable, IRelationOwner, IStatusHider
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000C1 RID: 193 RVA: 0x00003900 File Offset: 0x00001B00
		// (remove) Token: 0x060000C2 RID: 194 RVA: 0x00003938 File Offset: 0x00001B38
		public event EventHandler<WorkerChangedEventArgs> WorkerAssigned;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000C3 RID: 195 RVA: 0x00003970 File Offset: 0x00001B70
		// (remove) Token: 0x060000C4 RID: 196 RVA: 0x000039A8 File Offset: 0x00001BA8
		public event EventHandler<WorkerChangedEventArgs> WorkerUnassigned;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000C5 RID: 197 RVA: 0x000039E0 File Offset: 0x00001BE0
		// (remove) Token: 0x060000C6 RID: 198 RVA: 0x00003A18 File Offset: 0x00001C18
		public event EventHandler RelationsChanged;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060000C7 RID: 199 RVA: 0x00003A50 File Offset: 0x00001C50
		// (remove) Token: 0x060000C8 RID: 200 RVA: 0x00003A88 File Offset: 0x00001C88
		public event EventHandler DesiredWorkersChanged;

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003ABD File Offset: 0x00001CBD
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003AC5 File Offset: 0x00001CC5
		public int DesiredWorkers { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003ACE File Offset: 0x00001CCE
		public ReadOnlyList<Worker> AssignedWorkers
		{
			get
			{
				return this._assignedWorkers.AsReadOnlyList<Worker>();
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003ADB File Offset: 0x00001CDB
		public ReadOnlyList<WorkplaceBehavior> WorkplaceBehaviors
		{
			get
			{
				return this._workplaceBehaviors.AsReadOnlyList<WorkplaceBehavior>();
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public bool Understaffed
		{
			get
			{
				return this._blockableObject.IsUnblocked && this._assignedWorkers.Count < this.DesiredWorkers;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003B0C File Offset: 0x00001D0C
		public bool Overstaffed
		{
			get
			{
				return !this._blockableObject.IsUnblocked || this._assignedWorkers.Count > this.DesiredWorkers;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003B30 File Offset: 0x00001D30
		public int NumberOfAssignedWorkers
		{
			get
			{
				return this._assignedWorkers.Count;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003B3D File Offset: 0x00001D3D
		public int MaxWorkers
		{
			get
			{
				return this._workplaceSpec.MaxWorkers;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003B4A File Offset: 0x00001D4A
		public void Awake()
		{
			this._workplaceSpec = base.GetComponent<WorkplaceSpec>();
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._workplaceWorkerType = base.GetComponent<WorkplaceWorkerType>();
			this.DesiredWorkers = this._workplaceSpec.DefaultWorkers;
			base.DisableComponent();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003B87 File Offset: 0x00001D87
		public void Start()
		{
			this.ValidateWorkplaceSpec();
			this.ValidateWorkplaceBehaviors();
			this.ValidateEnterable();
			this._blockableObject.ObjectBlocked += this.OnObjectBlocked;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002630 File Offset: 0x00000830
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003BB2 File Offset: 0x00001DB2
		public void OnExitFinishedState()
		{
			this.UnassignAllWorkers();
			base.DisableComponent();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public void AssignWorker(Worker worker)
		{
			if (!this._assignedWorkers.Contains(worker))
			{
				if (!this.Understaffed)
				{
					throw new InvalidOperationException(string.Format("Can't assign {0} to {1}, this workplace isn't understaffed", worker, base.Name));
				}
				if (this._workplaceWorkerType.WorkerType != worker.WorkerType)
				{
					string text = "WorkerType";
					throw new InvalidOperationException(string.Concat(new string[]
					{
						string.Format("Can't assign {0} with {1} {2}", worker, text, worker.WorkerType),
						" to ",
						base.Name,
						" with ",
						text,
						" ",
						this._workplaceWorkerType.WorkerType
					}));
				}
				this._assignedWorkers.Add(worker);
				worker.EmployAt(this);
				EventHandler<WorkerChangedEventArgs> workerAssigned = this.WorkerAssigned;
				if (workerAssigned != null)
				{
					workerAssigned(this, new WorkerChangedEventArgs(worker));
				}
				EventHandler relationsChanged = this.RelationsChanged;
				if (relationsChanged == null)
				{
					return;
				}
				relationsChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003CB5 File Offset: 0x00001EB5
		public void IncreaseDesiredWorkers()
		{
			if (this.DesiredWorkers < this._workplaceSpec.MaxWorkers)
			{
				this.SetDesiredWorkers(this.DesiredWorkers + 1);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public void DecreaseDesiredWorkers()
		{
			if (this.DesiredWorkers > 1)
			{
				this.SetDesiredWorkers(this.DesiredWorkers - 1);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public void UnassignWorker(Worker worker)
		{
			if (this._assignedWorkers.Remove(worker))
			{
				worker.Unemploy();
				EventHandler<WorkerChangedEventArgs> workerUnassigned = this.WorkerUnassigned;
				if (workerUnassigned != null)
				{
					workerUnassigned(this, new WorkerChangedEventArgs(worker));
				}
				EventHandler relationsChanged = this.RelationsChanged;
				if (relationsChanged == null)
				{
					return;
				}
				relationsChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003D44 File Offset: 0x00001F44
		public bool AnyWorkerHasJobRunning()
		{
			for (int i = 0; i < this._assignedWorkers.Count; i++)
			{
				if (this._assignedWorkers[i].JobRunning)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003D7D File Offset: 0x00001F7D
		public void UnassignAllWorkers()
		{
			while (!this._assignedWorkers.IsEmpty<Worker>())
			{
				this.UnassignWorker(this._assignedWorkers.First<Worker>());
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003D9F File Offset: 0x00001F9F
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(Workplace.WorkplaceKey).Set(Workplace.DesiredWorkersKey, this.DesiredWorkers);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003DBC File Offset: 0x00001FBC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Workplace.WorkplaceKey, out objectLoader))
			{
				this.DesiredWorkers = Math.Min(objectLoader.Get(Workplace.DesiredWorkersKey), this._workplaceSpec.MaxWorkers);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003DF9 File Offset: 0x00001FF9
		public void DuplicateFrom(Workplace source)
		{
			if (source._workplaceSpec.MaxWorkers == this._workplaceSpec.MaxWorkers)
			{
				this.SetDesiredWorkers(source.DesiredWorkers);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003E1F File Offset: 0x0000201F
		public IEnumerable<BaseComponent> GetRelations()
		{
			return from assignedWorker in this._assignedWorkers
			select assignedWorker;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003E4B File Offset: 0x0000204B
		public void OnObjectBlocked(object sender, EventArgs e)
		{
			this.UnassignAllWorkers();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003E54 File Offset: 0x00002054
		public void ValidateWorkplaceSpec()
		{
			if (this._workplaceSpec.DisallowOtherWorkerTypes && this._workplaceSpec.WorkerTypeUnlockCosts.Length > 0)
			{
				throw new InvalidDataException(base.Name + ". If DisallowOtherWorkerTypes is set, WorkerTypeUnlockCosts must be empty.");
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003E9A File Offset: 0x0000209A
		public void ValidateWorkplaceBehaviors()
		{
			base.GetComponents<WorkplaceBehavior>(this._workplaceBehaviors);
			if (this._workplaceBehaviors.IsEmpty<WorkplaceBehavior>())
			{
				throw new InvalidOperationException(base.Name + " doesn't have any required WorkplaceBehavior component");
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003ECC File Offset: 0x000020CC
		public void ValidateEnterable()
		{
			int maxWorkers = this._workplaceSpec.MaxWorkers;
			EnterableSpec component = base.GetComponent<EnterableSpec>();
			if (component.LimitedCapacityFinished && component.CapacityFinished < maxWorkers)
			{
				throw new InvalidOperationException(string.Format("{0} has insufficient capacity for {1} workers", base.Name, maxWorkers));
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003F19 File Offset: 0x00002119
		public void SetDesiredWorkers(int desiredWorkers)
		{
			this.DesiredWorkers = desiredWorkers;
			EventHandler desiredWorkersChanged = this.DesiredWorkersChanged;
			if (desiredWorkersChanged != null)
			{
				desiredWorkersChanged(this, EventArgs.Empty);
			}
			this.UnassignWorkerIfOverstaffed();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003F3F File Offset: 0x0000213F
		public void UnassignWorkerIfOverstaffed()
		{
			if (this.Overstaffed)
			{
				this.UnassignWorkerIfNonworking();
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003F50 File Offset: 0x00002150
		public void UnassignWorkerIfNonworking()
		{
			Worker worker2 = this._assignedWorkers.FirstOrDefault((Worker worker) => !worker.JobRunning);
			if (worker2 != null)
			{
				this.UnassignWorker(worker2);
			}
		}

		// Token: 0x0400004C RID: 76
		public static readonly ComponentKey WorkplaceKey = new ComponentKey("Workplace");

		// Token: 0x0400004D RID: 77
		public static readonly PropertyKey<int> DesiredWorkersKey = new PropertyKey<int>("DesiredWorkers");

		// Token: 0x04000053 RID: 83
		public WorkplaceSpec _workplaceSpec;

		// Token: 0x04000054 RID: 84
		public BlockableObject _blockableObject;

		// Token: 0x04000055 RID: 85
		public WorkplaceWorkerType _workplaceWorkerType;

		// Token: 0x04000056 RID: 86
		public readonly List<Worker> _assignedWorkers = new List<Worker>();

		// Token: 0x04000057 RID: 87
		public readonly List<WorkplaceBehavior> _workplaceBehaviors = new List<WorkplaceBehavior>();
	}
}
