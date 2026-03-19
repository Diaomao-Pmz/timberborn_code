using System;

namespace Timberborn.Wellbeing
{
	// Token: 0x0200000A RID: 10
	public class NewWellbeingHighscoreEvent
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002136 File Offset: 0x00000336
		public NewWellbeingHighscoreEvent(int wellbeingHighscore)
		{
			this.WellbeingHighscore = wellbeingHighscore;
		}

		// Token: 0x0400000A RID: 10
		public readonly int WellbeingHighscore;
	}
}
