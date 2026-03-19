using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.ScienceSystem;

namespace Timberborn.Demolishing
{
	// Token: 0x02000011 RID: 17
	public class DemolishableScienceReward : BaseComponent, IAwakableComponent, IDeletableEntity
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002B4A File Offset: 0x00000D4A
		public DemolishableScienceReward(ScienceService scienceService)
		{
			this._scienceService = scienceService;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002B59 File Offset: 0x00000D59
		public void Awake()
		{
			this._demolishableScienceRewardSpec = base.GetComponent<DemolishableScienceRewardSpec>();
			this._demolishable = base.GetComponent<Demolishable>();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002B73 File Offset: 0x00000D73
		public void DeleteEntity()
		{
			if (this._demolishable.DemolishingProgress >= 1f)
			{
				this._scienceService.AddPoints(this._demolishableScienceRewardSpec.SciencePoints);
			}
		}

		// Token: 0x04000022 RID: 34
		public readonly ScienceService _scienceService;

		// Token: 0x04000023 RID: 35
		public DemolishableScienceRewardSpec _demolishableScienceRewardSpec;

		// Token: 0x04000024 RID: 36
		public Demolishable _demolishable;
	}
}
