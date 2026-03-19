using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;

namespace Timberborn.BeaverContaminationSystem
{
	// Token: 0x02000009 RID: 9
	public class ContaminateRootBehavior : RootBehavior, IAwakableComponent
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002405 File Offset: 0x00000605
		public void Awake()
		{
			this._contaminable = base.GetComponent<Contaminable>();
			this._contaminationIncubator = base.GetComponent<ContaminationIncubator>();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000241F File Offset: 0x0000061F
		public override Decision Decide(BehaviorAgent agent)
		{
			if (this._contaminationIncubator.IncubationFinished && !this._contaminable.IsContaminated)
			{
				this._contaminable.Contaminate();
				this._contaminationIncubator.ResetIncubation();
				return Decision.ReleaseNextTick();
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x0400000F RID: 15
		public Contaminable _contaminable;

		// Token: 0x04000010 RID: 16
		public ContaminationIncubator _contaminationIncubator;
	}
}
