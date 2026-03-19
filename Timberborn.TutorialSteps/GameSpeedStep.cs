using System;
using Timberborn.TimeSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200002B RID: 43
	public class GameSpeedStep : ITutorialStep
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000497B File Offset: 0x00002B7B
		public GameSpeedStep(SpeedManager speedManager, string description, int speed, bool onlyOnce)
		{
			this._speedManager = speedManager;
			this._description = description;
			this._speed = speed;
			this._onlyOnce = onlyOnce;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000049A0 File Offset: 0x00002BA0
		public string Description()
		{
			return this._description;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000049A8 File Offset: 0x00002BA8
		public bool Achieved()
		{
			bool flag = (double)Math.Abs((float)this._speed - this._speedManager.CurrentSpeed) < 0.0001;
			this._wasNotAchieved = (this._wasNotAchieved || !flag);
			this._wasAchieved = (flag || this._wasAchieved);
			if (!this._onlyOnce)
			{
				return this._wasNotAchieved && flag;
			}
			return this._wasAchieved;
		}

		// Token: 0x0400008E RID: 142
		public readonly SpeedManager _speedManager;

		// Token: 0x0400008F RID: 143
		public readonly string _description;

		// Token: 0x04000090 RID: 144
		public readonly int _speed;

		// Token: 0x04000091 RID: 145
		public readonly bool _onlyOnce;

		// Token: 0x04000092 RID: 146
		public bool _wasAchieved;

		// Token: 0x04000093 RID: 147
		public bool _wasNotAchieved;
	}
}
