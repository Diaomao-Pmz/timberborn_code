using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.Hauling;

namespace Timberborn.HaulingUI
{
	// Token: 0x02000004 RID: 4
	public class HaulCandidateBatchControlRowItemFactory
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public HaulCandidateBatchControlRowItemFactory(ToggleButtonBatchControlRowItemFactory toggleButtonBatchControlRowItemFactory)
		{
			this._toggleButtonBatchControlRowItemFactory = toggleButtonBatchControlRowItemFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			HaulCandidate component = entity.GetComponent<HaulCandidate>();
			if (component)
			{
				HaulPrioritizable haulPrioritizable = component.GetComponent<HaulPrioritizable>();
				return this._toggleButtonBatchControlRowItemFactory.Create(HaulCandidateBatchControlRowItemFactory.ButtonClass, delegate
				{
					haulPrioritizable.Prioritized = !haulPrioritizable.Prioritized;
				}, () => haulPrioritizable.Prioritized, HaulCandidateBatchControlRowItemFactory.PrioritizeLocKey);
			}
			return null;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string ButtonClass = "haul-candidate-batch-control-row-item";

		// Token: 0x04000007 RID: 7
		public static readonly string PrioritizeLocKey = "Hauling.Prioritize";

		// Token: 0x04000008 RID: 8
		public readonly ToggleButtonBatchControlRowItemFactory _toggleButtonBatchControlRowItemFactory;
	}
}
