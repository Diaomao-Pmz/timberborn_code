using System;
using Timberborn.GameSaveRuntimeSystem;
using Timberborn.SettlementNameSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.GameStartup
{
	// Token: 0x02000006 RID: 6
	public class GameStarter : IUpdatableSingleton, ILoadableSingleton
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000223F File Offset: 0x0000043F
		public GameStarter(SettlementReferenceService settlementReferenceService, GameInitializer gameInitializer, StartingBuildingInitializer startingBuildingInitializer, ISettlementNamePromptShower settlementNamePromptShower, EventBus eventBus, GameLoader gameLoader)
		{
			this._settlementReferenceService = settlementReferenceService;
			this._gameInitializer = gameInitializer;
			this._startingBuildingInitializer = startingBuildingInitializer;
			this._settlementNamePromptShower = settlementNamePromptShower;
			this._eventBus = eventBus;
			this._gameLoader = gameLoader;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002274 File Offset: 0x00000474
		public void Load()
		{
			if (this._gameLoader.IsNewGame)
			{
				this._shouldSpawnStartingBuilding = true;
				return;
			}
			this.StartGameplay(false);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002292 File Offset: 0x00000492
		public void UpdateSingleton()
		{
			if (this._shouldSpawnStartingBuilding)
			{
				this._shouldSpawnStartingBuilding = false;
				this.SpawnStartingBuilding();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022A9 File Offset: 0x000004A9
		[OnEvent]
		public void OnSettlementNameChanged(SettlementNameChangedEvent settlementNameChangedEvent)
		{
			this._settlementReferenceService.InitializeAndLogSettlementName(settlementNameChangedEvent.SettlementName);
			this.StartGameplay(true);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022C3 File Offset: 0x000004C3
		public void SpawnStartingBuilding()
		{
			this._startingBuildingInitializer.Initialize();
			if (this._settlementReferenceService.SettlementReference == null)
			{
				this._eventBus.Register(this);
				this._settlementNamePromptShower.PromptDisallowingCancelling(false);
				return;
			}
			this.StartGameplay(true);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002303 File Offset: 0x00000503
		public void StartGameplay(bool forNewGame)
		{
			this._eventBus.Unregister(this);
			if (forNewGame)
			{
				this._gameInitializer.InitializeNewGame();
				return;
			}
			this._gameInitializer.InitializeGameFromSave();
		}

		// Token: 0x04000013 RID: 19
		public readonly SettlementReferenceService _settlementReferenceService;

		// Token: 0x04000014 RID: 20
		public readonly GameInitializer _gameInitializer;

		// Token: 0x04000015 RID: 21
		public readonly StartingBuildingInitializer _startingBuildingInitializer;

		// Token: 0x04000016 RID: 22
		public readonly ISettlementNamePromptShower _settlementNamePromptShower;

		// Token: 0x04000017 RID: 23
		public readonly EventBus _eventBus;

		// Token: 0x04000018 RID: 24
		public readonly GameLoader _gameLoader;

		// Token: 0x04000019 RID: 25
		public bool _shouldSpawnStartingBuilding;
	}
}
