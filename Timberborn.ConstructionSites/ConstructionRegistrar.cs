using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.PrioritySystem;
using Timberborn.TemplateSystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x0200000C RID: 12
	public class ConstructionRegistrar : BaseComponent, IAwakableComponent, IUnfinishedStateListener
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000289C File Offset: 0x00000A9C
		public ConstructionRegistrar(ConstructionRegistry constructionRegistry)
		{
			this._constructionRegistry = constructionRegistry;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028AB File Offset: 0x00000AAB
		public void Awake()
		{
			this._constructionJob = base.GetComponent<ConstructionJob>();
			this._instantiatedTemplate = base.GetComponent<InstantiatedTemplate>();
			this._builderPrioritizable = base.GetComponent<BuilderPrioritizable>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028D1 File Offset: 0x00000AD1
		public void OnEnterUnfinishedState()
		{
			this._constructionRegistry.AddJob(this._constructionJob, this.Priority, this.InstantiationOrder);
			this._builderPrioritizable.PriorityChanged += this.OnPriorityChanged;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002907 File Offset: 0x00000B07
		public void OnExitUnfinishedState()
		{
			this._constructionRegistry.RemoveJob(this.Priority, this.InstantiationOrder);
			this._builderPrioritizable.PriorityChanged -= this.OnPriorityChanged;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002937 File Offset: 0x00000B37
		public int InstantiationOrder
		{
			get
			{
				return this._instantiatedTemplate.InstantiationOrder;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002944 File Offset: 0x00000B44
		public Priority Priority
		{
			get
			{
				return this._builderPrioritizable.Priority;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002954 File Offset: 0x00000B54
		public void OnPriorityChanged(object sender, PriorityChangedEventArgs priorityChangedEventArgs)
		{
			Priority previousPriority = priorityChangedEventArgs.PreviousPriority;
			this._constructionRegistry.RemoveJob(previousPriority, this.InstantiationOrder);
			this._constructionRegistry.AddJob(this._constructionJob, this.Priority, this.InstantiationOrder);
		}

		// Token: 0x04000023 RID: 35
		public readonly ConstructionRegistry _constructionRegistry;

		// Token: 0x04000024 RID: 36
		public ConstructionJob _constructionJob;

		// Token: 0x04000025 RID: 37
		public InstantiatedTemplate _instantiatedTemplate;

		// Token: 0x04000026 RID: 38
		public BuilderPrioritizable _builderPrioritizable;
	}
}
