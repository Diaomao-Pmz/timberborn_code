using System;
using Timberborn.AchievementSystem;
using Timberborn.GameFactionSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200002C RID: 44
	public class InjuredJustBornBeaverAchievement : Achievement
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003B58 File Offset: 0x00001D58
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003B60 File Offset: 0x00001D60
		public bool CanTrackInjury { get; private set; }

		// Token: 0x060000B9 RID: 185 RVA: 0x00003B69 File Offset: 0x00001D69
		public InjuredJustBornBeaverAchievement(FactionService factionService)
		{
			this._factionService = factionService;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003B78 File Offset: 0x00001D78
		public override string Id
		{
			get
			{
				return "INJURED_JUST_BORN_BEAVER";
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003B7F File Offset: 0x00001D7F
		public override void EnableInternal()
		{
			if (this._factionService.Current.Id == AchievementHelper.IronTeeth)
			{
				this.CanTrackInjury = true;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public override void DisableInternal()
		{
			this.CanTrackInjury = false;
		}

		// Token: 0x04000061 RID: 97
		public readonly FactionService _factionService;
	}
}
