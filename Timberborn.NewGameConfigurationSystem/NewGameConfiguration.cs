using System;
using Timberborn.MapRepositorySystem;

namespace Timberborn.NewGameConfigurationSystem
{
	// Token: 0x0200000C RID: 12
	public class NewGameConfiguration
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000030C1 File Offset: 0x000012C1
		public string FactionId { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000030C9 File Offset: 0x000012C9
		public MapFileReference MapFileReference { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000030D1 File Offset: 0x000012D1
		public GameModeSpec GameMode { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000030D9 File Offset: 0x000012D9
		public string SettlementName { get; }

		// Token: 0x0600005F RID: 95 RVA: 0x000030E1 File Offset: 0x000012E1
		public NewGameConfiguration(string factionId, MapFileReference mapFileReference, GameModeSpec gameMode, string settlementName)
		{
			this.FactionId = factionId;
			this.MapFileReference = mapFileReference;
			this.GameMode = gameMode;
			this.SettlementName = settlementName;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003108 File Offset: 0x00001308
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"FactionId: ",
				this.FactionId,
				", ",
				string.Format("{0}: {1}, ", "MapFileReference", this.MapFileReference),
				string.Format("{0}: {1}", "GameMode", this.GameMode)
			});
		}
	}
}
