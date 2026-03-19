using System;
using Timberborn.TimeSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200004C RID: 76
	public class SetPauseStep : ITutorialStep
	{
		// Token: 0x0600020C RID: 524 RVA: 0x000065C9 File Offset: 0x000047C9
		public SetPauseStep(SpeedManager speedManager, string description, bool pause, bool onlyOnce)
		{
			this._speedManager = speedManager;
			this._description = description;
			this._pause = pause;
			this._onlyOnce = onlyOnce;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000065EE File Offset: 0x000047EE
		public string Description()
		{
			return this._description;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000065F8 File Offset: 0x000047F8
		public bool Achieved()
		{
			bool flag = this._pause ? (this._speedManager.CurrentSpeed == 0f) : (this._speedManager.CurrentSpeed > 0f);
			this._wasNotAchieved = (this._wasNotAchieved || !flag);
			this._wasAchieved = (flag || this._wasAchieved);
			if (!this._onlyOnce)
			{
				return this._wasNotAchieved && flag;
			}
			return this._wasAchieved;
		}

		// Token: 0x04000100 RID: 256
		public readonly SpeedManager _speedManager;

		// Token: 0x04000101 RID: 257
		public readonly string _description;

		// Token: 0x04000102 RID: 258
		public readonly bool _pause;

		// Token: 0x04000103 RID: 259
		public readonly bool _onlyOnce;

		// Token: 0x04000104 RID: 260
		public bool _wasAchieved;

		// Token: 0x04000105 RID: 261
		public bool _wasNotAchieved;
	}
}
