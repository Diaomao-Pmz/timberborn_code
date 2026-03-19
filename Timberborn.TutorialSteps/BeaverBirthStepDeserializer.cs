using System;
using Timberborn.BlueprintSystem;
using Timberborn.Localization;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200000B RID: 11
	public class BeaverBirthStepDeserializer : IStepDeserializer
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000023D0 File Offset: 0x000005D0
		public BeaverBirthStepDeserializer(FirstbornService firstbornService, ILoc loc)
		{
			this._firstbornService = firstbornService;
			this._loc = loc;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023E8 File Offset: 0x000005E8
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			if (step.Specs[0] is BeaverBirthStepSpec)
			{
				tutorialStep = this.Create();
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002419 File Offset: 0x00000619
		public TutorialStep Create()
		{
			return TutorialStep.Create(new BeaverBirthStep(this._firstbornService, this._loc.T(BeaverBirthStepDeserializer.BeaverBirthLocKey)), null, null, null);
		}

		// Token: 0x04000015 RID: 21
		public static readonly string BeaverBirthLocKey = "Tutorial.MoreBeavers.BeaverBirth";

		// Token: 0x04000016 RID: 22
		public readonly FirstbornService _firstbornService;

		// Token: 0x04000017 RID: 23
		public readonly ILoc _loc;
	}
}
