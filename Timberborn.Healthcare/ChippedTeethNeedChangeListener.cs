using System;
using Timberborn.BaseComponentSystem;
using Timberborn.NeedSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Healthcare
{
	// Token: 0x0200000E RID: 14
	public class ChippedTeethNeedChangeListener : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002B9D File Offset: 0x00000D9D
		public ChippedTeethNeedChangeListener(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002BAC File Offset: 0x00000DAC
		public void Awake()
		{
			base.GetComponent<NeedManager>().NeedChangedActiveState += this.OnNeedChangedActiveState;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002BC5 File Offset: 0x00000DC5
		public void OnNeedChangedActiveState(object sender, NeedChangedActiveStateEventArgs e)
		{
			if (e.IsActive && e.NeedSpec.Id == ChippedTeethNeedChangeListener.ChippedTeethNeedId)
			{
				this._eventBus.Post(new TeethChippedEvent());
			}
		}

		// Token: 0x04000023 RID: 35
		public static readonly string ChippedTeethNeedId = "ChippedTeeth";

		// Token: 0x04000024 RID: 36
		public readonly EventBus _eventBus;
	}
}
