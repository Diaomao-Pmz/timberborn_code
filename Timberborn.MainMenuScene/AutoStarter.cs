using System;
using System.IO;
using Timberborn.CommandLine;
using Timberborn.ExperimentalModeSystem;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.GameSceneLoading;
using Timberborn.MapEditorSceneLoading;
using Timberborn.MapRepositorySystem;
using Timberborn.PlatformUtilities;
using UnityEngine;

namespace Timberborn.MainMenuScene
{
	// Token: 0x02000005 RID: 5
	public class AutoStarter
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020E3 File Offset: 0x000002E3
		public AutoStarter(ExperimentalMode experimentalMode, GameSceneLoader gameSceneLoader, ICommandLineArguments commandLineArguments, MapEditorSceneLoader mapEditorSceneLoader)
		{
			this._commandLineArguments = commandLineArguments;
			this._experimentalMode = experimentalMode;
			this._gameSceneLoader = gameSceneLoader;
			this._mapEditorSceneLoader = mapEditorSceneLoader;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002108 File Offset: 0x00000308
		public void CheckAutoStarting(Action nextAction)
		{
			if (this.AutoStartingInEditor)
			{
				this.StartInEditorMode();
				return;
			}
			if (!this.AutoStartingInStandalone)
			{
				nextAction();
				return;
			}
			if (this._experimentalMode.IsExperimental)
			{
				this.LoadSave(new SaveReference(this.SaveName, new SettlementReference(this.SettlementName, Path.Combine(UserDataFolder.Folder, "ExperimentalSaves"))));
				return;
			}
			this.LoadSave(new SaveReference(this.SaveName, new SettlementReference(this.SettlementName, Path.Combine(UserDataFolder.Folder, "Saves"))));
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002197 File Offset: 0x00000397
		public string SettlementName
		{
			get
			{
				return this._commandLineArguments.GetString(AutoStarter.SettlementNameCommandLineArgumentKey);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021A9 File Offset: 0x000003A9
		public string SaveName
		{
			get
			{
				return this._commandLineArguments.GetString(AutoStarter.SaveNameCommandLineArgumentKey);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021BB File Offset: 0x000003BB
		public bool AutoStartingInEditor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021BE File Offset: 0x000003BE
		public bool AutoStartingInStandalone
		{
			get
			{
				return !Application.isEditor && this._commandLineArguments.Has(AutoStarter.SaveNameCommandLineArgumentKey);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D9 File Offset: 0x000003D9
		public void StartInEditorMode()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021DB File Offset: 0x000003DB
		public void LoadMostRecentSave()
		{
			this._gameSceneLoader.StartMostRecentSaveInstantly();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021E8 File Offset: 0x000003E8
		public void LoadSave(SaveReference saveReference)
		{
			this._gameSceneLoader.StartSaveGameInstantly(saveReference);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021F6 File Offset: 0x000003F6
		public void StartNewMap()
		{
			this._mapEditorSceneLoader.StartNewMapInstantly(new Vector2Int(128, 128));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002212 File Offset: 0x00000412
		public void EditMap(MapFileReference mapFileReference)
		{
			this._mapEditorSceneLoader.LoadMapInstantly(mapFileReference);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string SettlementNameCommandLineArgumentKey = "settlementName";

		// Token: 0x04000007 RID: 7
		public static readonly string SaveNameCommandLineArgumentKey = "saveName";

		// Token: 0x04000008 RID: 8
		public readonly ExperimentalMode _experimentalMode;

		// Token: 0x04000009 RID: 9
		public readonly GameSceneLoader _gameSceneLoader;

		// Token: 0x0400000A RID: 10
		public readonly ICommandLineArguments _commandLineArguments;

		// Token: 0x0400000B RID: 11
		public readonly MapEditorSceneLoader _mapEditorSceneLoader;
	}
}
