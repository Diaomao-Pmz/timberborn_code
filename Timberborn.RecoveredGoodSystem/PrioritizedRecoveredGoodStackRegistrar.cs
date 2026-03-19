using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.EntitySystem;
using Timberborn.PrioritySystem;
using Timberborn.TemplateSystem;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x02000009 RID: 9
	public class PrioritizedRecoveredGoodStackRegistrar : BaseComponent, IAwakableComponent, IStartableComponent, IDeletableEntity
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000022EB File Offset: 0x000004EB
		public PrioritizedRecoveredGoodStackRegistrar(PrioritizedRecoveredGoodStackRegistry prioritizedRecoveredGoodStackRegistry)
		{
			this._prioritizedRecoveredGoodStackRegistry = prioritizedRecoveredGoodStackRegistry;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022FA File Offset: 0x000004FA
		public void Awake()
		{
			this._recoveredGoodStack = base.GetComponent<RecoveredGoodStack>();
			this._instantiatedTemplate = base.GetComponent<InstantiatedTemplate>();
			this._builderPrioritizable = base.GetComponent<BuilderPrioritizable>();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002320 File Offset: 0x00000520
		public void Start()
		{
			this._builderPrioritizable.Enable();
			this._builderPrioritizable.PriorityChanged += this.OnPriorityChanged;
			this._prioritizedRecoveredGoodStackRegistry.AddStack(this._recoveredGoodStack, this.PrioritizablePriority, this.InstantiationOrder);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000236C File Offset: 0x0000056C
		public void DeleteEntity()
		{
			this._prioritizedRecoveredGoodStackRegistry.RemoveStack(this.PrioritizablePriority, this.InstantiationOrder);
			this._builderPrioritizable.Disable();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002390 File Offset: 0x00000590
		public Priority PrioritizablePriority
		{
			get
			{
				return this._builderPrioritizable.Priority;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000239D File Offset: 0x0000059D
		public int InstantiationOrder
		{
			get
			{
				return this._instantiatedTemplate.InstantiationOrder;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023AC File Offset: 0x000005AC
		public void OnPriorityChanged(object sender, PriorityChangedEventArgs priorityChangedEventArgs)
		{
			Priority previousPriority = priorityChangedEventArgs.PreviousPriority;
			this._prioritizedRecoveredGoodStackRegistry.RemoveStack(previousPriority, this.InstantiationOrder);
			this._prioritizedRecoveredGoodStackRegistry.AddStack(this._recoveredGoodStack, this.PrioritizablePriority, this.InstantiationOrder);
		}

		// Token: 0x04000011 RID: 17
		public readonly PrioritizedRecoveredGoodStackRegistry _prioritizedRecoveredGoodStackRegistry;

		// Token: 0x04000012 RID: 18
		public RecoveredGoodStack _recoveredGoodStack;

		// Token: 0x04000013 RID: 19
		public InstantiatedTemplate _instantiatedTemplate;

		// Token: 0x04000014 RID: 20
		public BuilderPrioritizable _builderPrioritizable;
	}
}
