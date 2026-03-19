using System;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.GameSceneLoading;
using Timberborn.NewGameConfigurationSystem;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.UILayoutSystem;
using UnityEngine;

namespace Timberborn.GameStartup
{
	// Token: 0x02000004 RID: 4
	public class GameInitializer : IUpdatableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public GameInitializer(StartingBuildingSpawner startingBuildingSpawner, StartingBeaversInitializer startingBeaverInitializer, ISceneLoader sceneLoader, SpeedManager speedManager, EventBus eventBus)
		{
			this._startingBuildingSpawner = startingBuildingSpawner;
			this._startingBeaverInitializer = startingBeaverInitializer;
			this._sceneLoader = sceneLoader;
			this._speedManager = speedManager;
			this._eventBus = eventBus;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020ED File Offset: 0x000002ED
		public bool IsGameInitialized
		{
			get
			{
				return this._initializationState == GameInitializer.InitializationState.Finished;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F8 File Offset: 0x000002F8
		public void InitializeNewGame()
		{
			this._initializationState = GameInitializer.InitializationState.SpawnBeavers;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002101 File Offset: 0x00000301
		public void InitializeGameFromSave()
		{
			this._initializationState = GameInitializer.InitializationState.ShowUI;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000210A File Offset: 0x0000030A
		public void UpdateSingleton()
		{
			if (this._initializationState != GameInitializer.InitializationState.Finished)
			{
				this.Initialize();
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211B File Offset: 0x0000031B
		public void Initialize()
		{
			this._initializationState = this.PerformNextInitializationStep();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000212C File Offset: 0x0000032C
		public GameInitializer.InitializationState PerformNextInitializationStep()
		{
			GameInitializer.InitializationState result;
			switch (this._initializationState)
			{
			case GameInitializer.InitializationState.SpawnBeavers:
				result = this.SpawnBeavers();
				break;
			case GameInitializer.InitializationState.PostSpawnBeavers:
				result = this.PostSpawnBeavers();
				break;
			case GameInitializer.InitializationState.UnpauseGame:
				result = this.UnpauseGame();
				break;
			case GameInitializer.InitializationState.ShowUI:
				result = this.ShowPrimaryUI();
				break;
			default:
				result = this._initializationState;
				break;
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002188 File Offset: 0x00000388
		public GameInitializer.InitializationState SpawnBeavers()
		{
			Building startingBuilding = this._startingBuildingSpawner.StartingBuilding;
			if (startingBuilding)
			{
				Vector3? unblockedSingleAccess = startingBuilding.GetComponent<BuildingAccessible>().Accessible.UnblockedSingleAccess;
				if (unblockedSingleAccess != null)
				{
					Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
					GameModeSpec gameMode = this._sceneLoader.GetSceneParameters<GameSceneParameters>().NewGameConfiguration.GameMode;
					this._startingBeaverInitializer.Initialize(valueOrDefault, gameMode.StartingAdults, gameMode.AdultAgeProgress, gameMode.StartingChildren, gameMode.ChildAgeProgress);
				}
			}
			return GameInitializer.InitializationState.PostSpawnBeavers;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002206 File Offset: 0x00000406
		public GameInitializer.InitializationState PostSpawnBeavers()
		{
			this._eventBus.Post(new NewGameInitializedEvent());
			return GameInitializer.InitializationState.UnpauseGame;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002219 File Offset: 0x00000419
		public GameInitializer.InitializationState UnpauseGame()
		{
			this._speedManager.ChangeSpeed(1f);
			return GameInitializer.InitializationState.ShowUI;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000222C File Offset: 0x0000042C
		public GameInitializer.InitializationState ShowPrimaryUI()
		{
			this._eventBus.Post(new ShowPrimaryUIEvent());
			return GameInitializer.InitializationState.Finished;
		}

		// Token: 0x04000006 RID: 6
		public readonly StartingBuildingSpawner _startingBuildingSpawner;

		// Token: 0x04000007 RID: 7
		public readonly StartingBeaversInitializer _startingBeaverInitializer;

		// Token: 0x04000008 RID: 8
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x04000009 RID: 9
		public readonly SpeedManager _speedManager;

		// Token: 0x0400000A RID: 10
		public readonly EventBus _eventBus;

		// Token: 0x0400000B RID: 11
		public GameInitializer.InitializationState _initializationState;

		// Token: 0x02000005 RID: 5
		public enum InitializationState
		{
			// Token: 0x0400000D RID: 13
			Waiting,
			// Token: 0x0400000E RID: 14
			SpawnBeavers,
			// Token: 0x0400000F RID: 15
			PostSpawnBeavers,
			// Token: 0x04000010 RID: 16
			UnpauseGame,
			// Token: 0x04000011 RID: 17
			ShowUI,
			// Token: 0x04000012 RID: 18
			Finished
		}
	}
}
