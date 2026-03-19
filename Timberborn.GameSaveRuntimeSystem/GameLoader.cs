using System;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.MapSystem;
using Timberborn.NewGameConfigurationSystem;
using Timberborn.WorldSerialization;
using UnityEngine;

namespace Timberborn.GameSaveRuntimeSystem
{
	// Token: 0x02000004 RID: 4
	public class GameLoader
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public bool IsNewGame { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020D7 File Offset: 0x000002D7
		public SaveReference LoadedSave { get; private set; }

		// Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		public GameLoader(GameSaveDeserializer gameSaveDeserializer, MapLoader mapLoader)
		{
			this._gameSaveDeserializer = gameSaveDeserializer;
			this._mapLoader = mapLoader;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F6 File Offset: 0x000002F6
		public SerializedWorld Load(SaveReference saveReference)
		{
			Debug.Log(string.Format("Loading saved game {0} at {1:u}", saveReference, DateTime.Now));
			this.LoadedSave = saveReference;
			return this._gameSaveDeserializer.Load(saveReference);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002125 File Offset: 0x00000325
		public SerializedWorld LoadNew(NewGameConfiguration newGameConfiguration)
		{
			Debug.Log(string.Format("Starting new game at {0:u}:\n{1}", DateTime.Now, newGameConfiguration));
			this.IsNewGame = true;
			return this._mapLoader.Load(newGameConfiguration.MapFileReference);
		}

		// Token: 0x04000008 RID: 8
		public readonly GameSaveDeserializer _gameSaveDeserializer;

		// Token: 0x04000009 RID: 9
		public readonly MapLoader _mapLoader;
	}
}
