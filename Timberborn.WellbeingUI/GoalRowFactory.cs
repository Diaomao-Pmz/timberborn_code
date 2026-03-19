using System;
using Timberborn.CoreUI;
using Timberborn.FactionSystem;
using Timberborn.GameFactionSystem;
using Timberborn.Localization;
using Timberborn.Wellbeing;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000009 RID: 9
	public class GoalRowFactory
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002589 File Offset: 0x00000789
		public GoalRowFactory(VisualElementLoader visualElementLoader, FactionUnlockConditionDescriber factionUnlockConditionDescriber, ILoc loc, FactionUnlockingService factionUnlockingService, FactionService factionService, WellbeingService wellbeingService)
		{
			this._visualElementLoader = visualElementLoader;
			this._factionUnlockConditionDescriber = factionUnlockConditionDescriber;
			this._loc = loc;
			this._factionUnlockingService = factionUnlockingService;
			this._factionService = factionService;
			this._wellbeingService = wellbeingService;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025C0 File Offset: 0x000007C0
		public VisualElement CreateRow(UnlockableFactionSpec unlockableFactionSpec)
		{
			FactionSpec spec = unlockableFactionSpec.GetSpec<FactionSpec>();
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/Population/GoalRow");
			StyleBackground backgroundImage;
			backgroundImage..ctor(spec.Avatar.Asset);
			UQueryExtensions.Q<VisualElement>(visualElement, "Icon", null).style.backgroundImage = backgroundImage;
			UQueryExtensions.Q<Label>(visualElement, "Header", null).text = spec.DisplayName.Value;
			string text = this._factionUnlockConditionDescriber.Describe(spec);
			UQueryExtensions.Q<Label>(visualElement, "Description", null).text = text;
			this.UpdateProgress(spec, unlockableFactionSpec, visualElement);
			return visualElement;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002654 File Offset: 0x00000854
		public void UpdateProgress(FactionSpec factionSpec, UnlockableFactionSpec unlockableFactionSpec, VisualElement goalRowElement)
		{
			if (!this._factionUnlockingService.IsLocked(factionSpec))
			{
				this.UpdateProgress(this._loc.T(GoalRowFactory.UnlockedLocKey), goalRowElement);
				return;
			}
			if (this._factionService.Current.Id == unlockableFactionSpec.PrerequisiteFaction)
			{
				string progress = string.Format("{0} ", this._wellbeingService.AverageGlobalWellbeing) + string.Format("/ {0}", unlockableFactionSpec.AverageWellbeingToUnlock);
				this.UpdateProgress(progress, goalRowElement);
				return;
			}
			this.UpdateProgress(this._loc.T(GoalRowFactory.NotEligibleLocKey), goalRowElement);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026F9 File Offset: 0x000008F9
		public void UpdateProgress(string progress, VisualElement goalRowElement)
		{
			UQueryExtensions.Q<Label>(goalRowElement, "Progress", null).text = this._loc.T(GoalRowFactory.ProgressLocKey) + " " + progress;
		}

		// Token: 0x0400002B RID: 43
		public static readonly string ProgressLocKey = "Goals.Progress";

		// Token: 0x0400002C RID: 44
		public static readonly string NotEligibleLocKey = "Goals.NotEligible";

		// Token: 0x0400002D RID: 45
		public static readonly string UnlockedLocKey = "Goals.Unlocked";

		// Token: 0x0400002E RID: 46
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002F RID: 47
		public readonly FactionUnlockConditionDescriber _factionUnlockConditionDescriber;

		// Token: 0x04000030 RID: 48
		public readonly ILoc _loc;

		// Token: 0x04000031 RID: 49
		public readonly FactionUnlockingService _factionUnlockingService;

		// Token: 0x04000032 RID: 50
		public readonly FactionService _factionService;

		// Token: 0x04000033 RID: 51
		public readonly WellbeingService _wellbeingService;
	}
}
