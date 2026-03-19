using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.PrioritySystem;
using Timberborn.TemplateSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000026 RID: 38
	public class WorkplacePriority : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<WorkplacePriority>, IDuplicable, IFinishedStateListener, IPrioritizable
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600010B RID: 267 RVA: 0x00004334 File Offset: 0x00002534
		// (remove) Token: 0x0600010C RID: 268 RVA: 0x0000436C File Offset: 0x0000256C
		public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000043A1 File Offset: 0x000025A1
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000043A9 File Offset: 0x000025A9
		public Priority Priority { get; private set; } = Priority.Normal;

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000043B2 File Offset: 0x000025B2
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000043BA File Offset: 0x000025BA
		public Workplace Workplace { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000043C3 File Offset: 0x000025C3
		public int InstantiationOrder
		{
			get
			{
				return this._instantiatedTemplate.InstantiationOrder;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000043D0 File Offset: 0x000025D0
		public void Awake()
		{
			this.Workplace = base.GetComponent<Workplace>();
			this._instantiatedTemplate = base.GetComponent<InstantiatedTemplate>();
			base.DisableComponent();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00002630 File Offset: 0x00000830
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000043F0 File Offset: 0x000025F0
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000043F8 File Offset: 0x000025F8
		public void Save(IEntitySaver entitySaver)
		{
			if (this.Priority != Priority.Normal)
			{
				entitySaver.GetComponent(WorkplacePriority.WorkplacePriorityKey).Set<Priority>(WorkplacePriority.PriorityKey, this.Priority);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004420 File Offset: 0x00002620
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(WorkplacePriority.WorkplacePriorityKey, out objectLoader))
			{
				this.Priority = objectLoader.Get<Priority>(WorkplacePriority.PriorityKey);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000444D File Offset: 0x0000264D
		public void DuplicateFrom(WorkplacePriority source)
		{
			this.SetPriority(source.Priority);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000445C File Offset: 0x0000265C
		public void SetPriority(Priority priority)
		{
			if (priority != this.Priority)
			{
				Priority priority2 = this.Priority;
				this.Priority = priority;
				EventHandler<PriorityChangedEventArgs> priorityChanged = this.PriorityChanged;
				if (priorityChanged == null)
				{
					return;
				}
				priorityChanged(this, new PriorityChangedEventArgs(priority2));
			}
		}

		// Token: 0x0400005F RID: 95
		public static readonly ComponentKey WorkplacePriorityKey = new ComponentKey("WorkplacePriority");

		// Token: 0x04000060 RID: 96
		public static readonly PropertyKey<Priority> PriorityKey = new PropertyKey<Priority>("Priority");

		// Token: 0x04000064 RID: 100
		public InstantiatedTemplate _instantiatedTemplate;
	}
}
