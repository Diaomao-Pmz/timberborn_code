using System;
using Timberborn.BlueprintSystem;
using Timberborn.Localization;
using Timberborn.TimeSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200004D RID: 77
	public class SetPauseStepDeserializer : IStepDeserializer
	{
		// Token: 0x0600020F RID: 527 RVA: 0x00006672 File Offset: 0x00004872
		public SetPauseStepDeserializer(SpeedManager speedManager, ILoc loc)
		{
			this._speedManager = speedManager;
			this._loc = loc;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006688 File Offset: 0x00004888
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			SetPauseStepSpec setPauseStepSpec = step.Specs[0] as SetPauseStepSpec;
			if (setPauseStepSpec != null)
			{
				tutorialStep = this.Create(setPauseStepSpec.Pause, setPauseStepSpec.OnlyOnce);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000066C8 File Offset: 0x000048C8
		public TutorialStep Create(bool pause, bool onlyOnce)
		{
			string description = pause ? this._loc.T("Tutorial.Basics.Pause") : this._loc.T("Tutorial.Basics.Unpause");
			return TutorialStep.Create(new SetPauseStep(this._speedManager, description, pause, onlyOnce), "Speed0", null);
		}

		// Token: 0x04000106 RID: 262
		public readonly SpeedManager _speedManager;

		// Token: 0x04000107 RID: 263
		public readonly ILoc _loc;
	}
}
