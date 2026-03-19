using System;
using System.Linq;
using Timberborn.GameFactionSystem;
using Timberborn.WonderCompletion;

namespace Timberborn.GameWonderCompletion
{
	// Token: 0x0200000A RID: 10
	public class GameWonderCompletionService
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000024B7 File Offset: 0x000006B7
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000024BF File Offset: 0x000006BF
		public bool WasCompletedFirstTimeForMap { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000024C8 File Offset: 0x000006C8
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000024D0 File Offset: 0x000006D0
		public bool WasCompletedFirstTimeForFaction { get; private set; }

		// Token: 0x06000028 RID: 40 RVA: 0x000024D9 File Offset: 0x000006D9
		public GameWonderCompletionService(MapNameService mapNameService, FactionService factionService, WonderCompletionService wonderCompletionService)
		{
			this._mapNameService = mapNameService;
			this._factionService = factionService;
			this._wonderCompletionService = wonderCompletionService;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024F6 File Offset: 0x000006F6
		public bool IsWonderCompletedWithAnyFaction()
		{
			return this._wonderCompletionService.IsWonderCompletedWithAnyFaction(this._mapNameService.Name, this._mapNameService.IsResource);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000251C File Offset: 0x0000071C
		public void CompleteWonder()
		{
			if (this._mapNameService.HasMapName && !this.IsWonderCompletedWithCurrentFaction())
			{
				bool flag = this.IsWonderCompletedWithAnyFaction();
				this.WasCompletedFirstTimeForFaction = flag;
				this.WasCompletedFirstTimeForMap = !flag;
				this._wonderCompletionService.CompleteWonder(this._mapNameService.Name, this._mapNameService.IsResource, this._factionService.Current.Id);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002587 File Offset: 0x00000787
		public void RevokeWonderCompletionForAllFactions()
		{
			this._wonderCompletionService.RevokeWonderCompletionForAllFactions(this._mapNameService.Name, this._mapNameService.IsResource);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025AA File Offset: 0x000007AA
		public bool IsWonderCompletedWithCurrentFaction()
		{
			return this._wonderCompletionService.GetWonderCompletionFactionIds(this._mapNameService.Name, this._mapNameService.IsResource).Contains(this._factionService.Current.Id);
		}

		// Token: 0x04000012 RID: 18
		public readonly MapNameService _mapNameService;

		// Token: 0x04000013 RID: 19
		public readonly FactionService _factionService;

		// Token: 0x04000014 RID: 20
		public readonly WonderCompletionService _wonderCompletionService;
	}
}
