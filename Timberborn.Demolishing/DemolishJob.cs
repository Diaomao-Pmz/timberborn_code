using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BlockSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.EntitySystem;
using Timberborn.PrioritySystem;

namespace Timberborn.Demolishing
{
	// Token: 0x0200001B RID: 27
	public class DemolishJob : BaseComponent, IAwakableComponent, IPostInitializableEntity
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00003667 File Offset: 0x00001867
		public DemolishJob(DemolishJobs demolishJobs)
		{
			this._demolishJobs = demolishJobs;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003676 File Offset: 0x00001876
		public void Awake()
		{
			this._demolishable = base.GetComponent<Demolishable>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._builderPrioritizable = base.GetComponent<BuilderPrioritizable>();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000369C File Offset: 0x0000189C
		public void PostInitializeEntity()
		{
			this._canBeEnabled = true;
			if (this._enableAfterInitialization)
			{
				this.Enable();
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000036B3 File Offset: 0x000018B3
		public void Enable()
		{
			if (this._canBeEnabled)
			{
				this._demolishJobs.AddJob(this, this.Priority);
				this._builderPrioritizable.PriorityChanged += this.OnPriorityChanged;
				return;
			}
			this._enableAfterInitialization = true;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000036EE File Offset: 0x000018EE
		public void Disable()
		{
			this._enableAfterInitialization = false;
			this._demolishJobs.RemoveJob(this, this.Priority);
			this._builderPrioritizable.PriorityChanged -= this.OnPriorityChanged;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003720 File Offset: 0x00001920
		public bool CanStartJob(Demolisher demolisher)
		{
			return this._blockObject.CanDelete() && ((!this._demolishable.Reservable.Reserved && !demolisher.HasReservedDemolishable) || demolisher.IsReserved(this._demolishable));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000375C File Offset: 0x0000195C
		public ValueTuple<Behavior, Decision> StartBuilderJob(Demolisher demolisher)
		{
			if (!demolisher.IsReserved(this._demolishable))
			{
				demolisher.Reserve(this._demolishable);
			}
			DemolishBehavior component = demolisher.GetComponent<DemolishBehavior>();
			BehaviorAgent component2 = demolisher.GetComponent<BehaviorAgent>();
			return new ValueTuple<Behavior, Decision>(component, component.Decide(component2));
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000379E File Offset: 0x0000199E
		public Priority Priority
		{
			get
			{
				return this._builderPrioritizable.Priority;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000037AC File Offset: 0x000019AC
		public void OnPriorityChanged(object sender, PriorityChangedEventArgs priorityChangedEventArgs)
		{
			Priority previousPriority = priorityChangedEventArgs.PreviousPriority;
			this._demolishJobs.RemoveJob(this, previousPriority);
			this._demolishJobs.AddJob(this, this.Priority);
		}

		// Token: 0x0400003A RID: 58
		public readonly DemolishJobs _demolishJobs;

		// Token: 0x0400003B RID: 59
		public Demolishable _demolishable;

		// Token: 0x0400003C RID: 60
		public BlockObject _blockObject;

		// Token: 0x0400003D RID: 61
		public BuilderPrioritizable _builderPrioritizable;

		// Token: 0x0400003E RID: 62
		public bool _canBeEnabled;

		// Token: 0x0400003F RID: 63
		public bool _enableAfterInitialization;
	}
}
