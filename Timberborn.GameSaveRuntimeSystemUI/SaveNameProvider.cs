using System;
using System.Linq;
using Timberborn.Common;
using Timberborn.GameSaveRepositorySystemUI;
using Timberborn.SettlementNameSystem;

namespace Timberborn.GameSaveRuntimeSystemUI
{
	// Token: 0x0200000A RID: 10
	public class SaveNameProvider
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000279E File Offset: 0x0000099E
		public SaveNameProvider(SettlementReferenceService settlementReferenceService)
		{
			this._settlementReferenceService = settlementReferenceService;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027B0 File Offset: 0x000009B0
		public string GetDefaultSaveName(ReadOnlyList<GameSaveItem> existingSaves)
		{
			string settlementName = this._settlementReferenceService.SettlementReference.SettlementName;
			int num = 0;
			string saveName = settlementName;
			while (existingSaves.Any((GameSaveItem s) => s.SaveReference.SaveName == saveName))
			{
				saveName = string.Format("{0} ({1})", settlementName, ++num);
			}
			return saveName;
		}

		// Token: 0x04000023 RID: 35
		public readonly SettlementReferenceService _settlementReferenceService;
	}
}
