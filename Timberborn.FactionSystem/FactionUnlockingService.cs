using System;
using Timberborn.BlueprintSystem;
using Timberborn.PlayerDataSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.FactionSystem
{
	// Token: 0x0200000E RID: 14
	public class FactionUnlockingService
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002F3B File Offset: 0x0000113B
		public FactionUnlockingService(FactionSpecService factionSpecService, IPlayerDataService playerDataService, EventBus eventBus)
		{
			this._factionSpecService = factionSpecService;
			this._playerDataService = playerDataService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F58 File Offset: 0x00001158
		public void UnlockFaction(FactionSpec factionSpec)
		{
			this.Unlock(factionSpec);
			this._eventBus.Post(new FactionUnlockedEvent(factionSpec));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F74 File Offset: 0x00001174
		public void UnlockAllFactions()
		{
			foreach (UnlockableFactionSpec unlockableFactionSpec in this._factionSpecService.UnlockableFactions)
			{
				this.Unlock(unlockableFactionSpec.GetSpec<FactionSpec>());
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002FCC File Offset: 0x000011CC
		public void LockAllFactions()
		{
			foreach (UnlockableFactionSpec unlockableFactionSpec in this._factionSpecService.UnlockableFactions)
			{
				this._playerDataService.Remove(FactionUnlockingService.ComposeFactionUnlockKey(unlockableFactionSpec.GetSpec<FactionSpec>().Id));
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003034 File Offset: 0x00001234
		public bool IsLocked(ComponentSpec componentSpec)
		{
			return componentSpec.HasSpec<UnlockableFactionSpec>() && !this._playerDataService.GetBool(FactionUnlockingService.ComposeFactionUnlockKey(componentSpec.GetSpec<FactionSpec>().Id), false);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000305F File Offset: 0x0000125F
		public void Unlock(FactionSpec factionSpec)
		{
			this._playerDataService.SetBool(FactionUnlockingService.ComposeFactionUnlockKey(factionSpec.Id), true);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003078 File Offset: 0x00001278
		public static string ComposeFactionUnlockKey(string factionId)
		{
			return FactionUnlockingService.FactionUnlockKeyPrefix + factionId;
		}

		// Token: 0x0400002E RID: 46
		public static readonly string FactionUnlockKeyPrefix = "FactionUnlocked_";

		// Token: 0x0400002F RID: 47
		public readonly FactionSpecService _factionSpecService;

		// Token: 0x04000030 RID: 48
		public readonly IPlayerDataService _playerDataService;

		// Token: 0x04000031 RID: 49
		public readonly EventBus _eventBus;
	}
}
