using System;
using Timberborn.GameCycleSystem;
using Timberborn.GameSaveRepositorySystem;

namespace Timberborn.Autosaving
{
	// Token: 0x02000008 RID: 8
	public class AutosaveNameService
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002136 File Offset: 0x00000336
		public AutosaveNameService(GameCycleService gameCycleService)
		{
			this._gameCycleService = gameCycleService;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002145 File Offset: 0x00000345
		public string GetAutosaveName()
		{
			return this.Timestamp() + AutosaveNameService.NameSuffix;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002157 File Offset: 0x00000357
		public bool IsAutosaveName(string name)
		{
			return name.EndsWith(AutosaveNameService.NameSuffix);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002164 File Offset: 0x00000364
		public string Timestamp()
		{
			string str = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH\\hmm\\m");
			string str2 = string.Format(AutosaveNameService.GameDatePattern, this._gameCycleService.Cycle, this._gameCycleService.CycleDay);
			return str + ", " + str2;
		}

		// Token: 0x0400000A RID: 10
		public static readonly string NameSuffix = GameSaveRepository.AutosaveNameSuffix;

		// Token: 0x0400000B RID: 11
		public static readonly string GameDatePattern = "Day {0}-{1}";

		// Token: 0x0400000C RID: 12
		public readonly GameCycleService _gameCycleService;
	}
}
