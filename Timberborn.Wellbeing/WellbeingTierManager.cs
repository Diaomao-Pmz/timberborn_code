using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;

namespace Timberborn.Wellbeing
{
	// Token: 0x02000016 RID: 22
	public class WellbeingTierManager : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002A05 File Offset: 0x00000C05
		public WellbeingTierManager(IWellbeingTierService wellbeingTierService)
		{
			this._wellbeingTierService = wellbeingTierService;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A14 File Offset: 0x00000C14
		public void Awake()
		{
			this._bonusManager = base.GetComponent<BonusManager>();
			this._wellbeingTracker = base.GetComponent<WellbeingTracker>();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A30 File Offset: 0x00000C30
		public void Start()
		{
			this._wellbeingTracker.WellbeingChanged += delegate(object _, WellbeingChangedEventArgs e)
			{
				this.UpdateBonuses(e.OldWellbeing, e.NewWellbeing);
			};
			foreach (string bonusId in this._wellbeingTierService.GetTierableBonuses(this._wellbeingTracker))
			{
				this.AddBonus(this._wellbeingTracker.Wellbeing, bonusId);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002AAC File Offset: 0x00000CAC
		public void UpdateBonuses(int oldWellbeing, int newWellbeing)
		{
			foreach (string bonusId in this._wellbeingTierService.GetTierableBonuses(this._wellbeingTracker))
			{
				this.RemoveBonus(oldWellbeing, bonusId);
				this.AddBonus(newWellbeing, bonusId);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B10 File Offset: 0x00000D10
		public void AddBonus(int wellbeing, string bonusId)
		{
			WellbeingTierBonus wellbeingTierBonus;
			if (this._wellbeingTierService.TryGetTierBonus(this._wellbeingTracker, bonusId, wellbeing, out wellbeingTierBonus))
			{
				this._bonusManager.AddBonus(bonusId, wellbeingTierBonus.Bonus);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B48 File Offset: 0x00000D48
		public void RemoveBonus(int wellbeing, string bonusId)
		{
			WellbeingTierBonus wellbeingTierBonus;
			if (this._wellbeingTierService.TryGetTierBonus(this._wellbeingTracker, bonusId, wellbeing, out wellbeingTierBonus))
			{
				this._bonusManager.RemoveBonus(bonusId, wellbeingTierBonus.Bonus);
			}
		}

		// Token: 0x0400002B RID: 43
		public readonly IWellbeingTierService _wellbeingTierService;

		// Token: 0x0400002C RID: 44
		public BonusManager _bonusManager;

		// Token: 0x0400002D RID: 45
		public WellbeingTracker _wellbeingTracker;
	}
}
