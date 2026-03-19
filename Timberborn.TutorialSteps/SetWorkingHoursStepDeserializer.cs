using System;
using Timberborn.BlueprintSystem;
using Timberborn.Localization;
using Timberborn.TutorialSystem;
using Timberborn.WorkSystem;
using Timberborn.WorkSystemUI;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000050 RID: 80
	public class SetWorkingHoursStepDeserializer : IStepDeserializer
	{
		// Token: 0x06000225 RID: 549 RVA: 0x00006923 File Offset: 0x00004B23
		public SetWorkingHoursStepDeserializer(WorkingHoursManager workingHoursManager, WorkingHoursPanel workingHoursPanel, ILoc loc)
		{
			this._workingHoursManager = workingHoursManager;
			this._workingHoursPanel = workingHoursPanel;
			this._loc = loc;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006940 File Offset: 0x00004B40
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			SetWorkingHoursStepSpec setWorkingHoursStepSpec = step.Specs[0] as SetWorkingHoursStepSpec;
			if (setWorkingHoursStepSpec != null)
			{
				tutorialStep = this.Create(setWorkingHoursStepSpec.TargetWorkingHours);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000697C File Offset: 0x00004B7C
		public TutorialStep Create(int targetWorkingHours)
		{
			string description = this._loc.T<int>("Tutorial.SetWorkingHours", targetWorkingHours);
			return TutorialStep.Create(new SetWorkingHoursStep(this._workingHoursManager, targetWorkingHours, description), delegate(bool state)
			{
				this._workingHoursPanel.TogglePanelHighlight(state);
			}, null, null);
		}

		// Token: 0x0400010D RID: 269
		public readonly WorkingHoursManager _workingHoursManager;

		// Token: 0x0400010E RID: 270
		public readonly WorkingHoursPanel _workingHoursPanel;

		// Token: 0x0400010F RID: 271
		public readonly ILoc _loc;
	}
}
