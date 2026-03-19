using System;
using Timberborn.Debugging;
using Timberborn.GameFactionSystem;
using Timberborn.GameWonderCompletion;
using Timberborn.QuickNotificationSystem;

namespace Timberborn.GameWonderCompletionUI
{
	// Token: 0x02000006 RID: 6
	public class WonderCompletionDevModule : IDevModule
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000219C File Offset: 0x0000039C
		public WonderCompletionDevModule(GameWonderCompletionService gameWonderCompletionService, MapNameService mapNameService, FactionService factionService, QuickNotificationService quickNotificationService)
		{
			this._gameWonderCompletionService = gameWonderCompletionService;
			this._mapNameService = mapNameService;
			this._factionService = factionService;
			this._quickNotificationService = quickNotificationService;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C4 File Offset: 0x000003C4
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Wonders: Complete on this map", new Action(this.CompleteWonder))).AddMethod(DevMethod.Create("Wonders: Revoke all completions", new Action(this.RevokeAllCompletions))).Build();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002214 File Offset: 0x00000414
		public void CompleteWonder()
		{
			this._gameWonderCompletionService.CompleteWonder();
			string text = this._mapNameService.HasMapName ? ("Map " + this._mapNameService.Name + " completed for faction " + this._factionService.Current.Id) : "Unable to complete - missing map name";
			this._quickNotificationService.SendNotification(text);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002278 File Offset: 0x00000478
		public void RevokeAllCompletions()
		{
			this._gameWonderCompletionService.RevokeWonderCompletionForAllFactions();
			string text = this._mapNameService.HasMapName ? ("Completion of map " + this._mapNameService.Name + " revoked for all factions") : "Unable to complete - missing map name";
			this._quickNotificationService.SendNotification(text);
		}

		// Token: 0x04000009 RID: 9
		public readonly GameWonderCompletionService _gameWonderCompletionService;

		// Token: 0x0400000A RID: 10
		public readonly MapNameService _mapNameService;

		// Token: 0x0400000B RID: 11
		public readonly FactionService _factionService;

		// Token: 0x0400000C RID: 12
		public readonly QuickNotificationService _quickNotificationService;
	}
}
