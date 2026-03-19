using System;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.Localization;
using Timberborn.MapRepositorySystem;
using Timberborn.NewGameConfigurationSystem;
using Timberborn.SceneLoading;

namespace Timberborn.GameSceneLoading
{
	// Token: 0x02000007 RID: 7
	public class GameSceneLoader
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public GameSceneLoader(GameSaveRepository gameSaveRepository, ILoc loc, ISceneLoader sceneLoader, GameModeSpecService gameModeSpecService, ISpecService specService, IRandomNumberGenerator randomNumberGenerator)
		{
			this._gameSaveRepository = gameSaveRepository;
			this._loc = loc;
			this._sceneLoader = sceneLoader;
			this._gameModeSpecService = gameModeSpecService;
			this._specService = specService;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002133 File Offset: 0x00000333
		public void StartNewGame(NewGameConfiguration newGameConfiguration)
		{
			this._sceneLoader.LoadScene(GameSceneParameters.CreateNewGameParameters(newGameConfiguration), this.GetTip());
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000214C File Offset: 0x0000034C
		public void StartSaveGame(SaveReference saveReference)
		{
			this._sceneLoader.LoadScene(GameSceneParameters.CreateGameSaveParameters(saveReference), this.GetTip());
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002168 File Offset: 0x00000368
		public void StartNewGameInstantly(string factionId, MapFileReference mapFileReference, string settlementName)
		{
			GameModeSpec defaultSpec = this._gameModeSpecService.GetDefaultSpec();
			this._sceneLoader.LoadSceneInstantly(GameSceneParameters.CreateNewGameParameters(new NewGameConfiguration(factionId, mapFileReference, defaultSpec, settlementName)), this.GetTip());
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021A0 File Offset: 0x000003A0
		public void StartSaveGameInstantly(SaveReference saveReference)
		{
			this._sceneLoader.LoadSceneInstantly(GameSceneParameters.CreateGameSaveParameters(saveReference), this.GetTip());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021BC File Offset: 0x000003BC
		public void StartMostRecentSaveInstantly()
		{
			SaveReference mostRecentSave = this._gameSaveRepository.GetMostRecentSave();
			if (this._gameSaveRepository.SaveExists(mostRecentSave))
			{
				this.StartSaveGameInstantly(mostRecentSave);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021EC File Offset: 0x000003EC
		public string GetTip()
		{
			GameTipSpec singleSpec = this._specService.GetSingleSpec<GameTipSpec>();
			string listElement = this._randomNumberGenerator.GetListElement<string>(singleSpec.Tips);
			return this._loc.T(listElement);
		}

		// Token: 0x04000008 RID: 8
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x04000009 RID: 9
		public readonly ILoc _loc;

		// Token: 0x0400000A RID: 10
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x0400000B RID: 11
		public readonly GameModeSpecService _gameModeSpecService;

		// Token: 0x0400000C RID: 12
		public readonly ISpecService _specService;

		// Token: 0x0400000D RID: 13
		public readonly IRandomNumberGenerator _randomNumberGenerator;
	}
}
