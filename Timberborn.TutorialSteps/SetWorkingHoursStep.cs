using System;
using Timberborn.TutorialSystem;
using Timberborn.WorkSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200004F RID: 79
	public class SetWorkingHoursStep : ITutorialStep
	{
		// Token: 0x06000222 RID: 546 RVA: 0x000068DD File Offset: 0x00004ADD
		public SetWorkingHoursStep(WorkingHoursManager workingHoursManager, int targetWorkingHours, string description)
		{
			this._workingHoursManager = workingHoursManager;
			this._targetWorkingHours = targetWorkingHours;
			this._description = description;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000068FA File Offset: 0x00004AFA
		public string Description()
		{
			return this._description;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006902 File Offset: 0x00004B02
		public bool Achieved()
		{
			return Math.Abs(this._workingHoursManager.EndHours - (float)this._targetWorkingHours) < 0.01f;
		}

		// Token: 0x0400010A RID: 266
		public readonly WorkingHoursManager _workingHoursManager;

		// Token: 0x0400010B RID: 267
		public readonly int _targetWorkingHours;

		// Token: 0x0400010C RID: 268
		public readonly string _description;
	}
}
