using System;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.NewGameConfigurationSystem;
using Timberborn.SceneLoading;

namespace Timberborn.GameSceneLoading
{
	// Token: 0x02000009 RID: 9
	public class GameSceneParameters : ISceneParameters
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000223E File Offset: 0x0000043E
		public NewGameConfiguration NewGameConfiguration { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002246 File Offset: 0x00000446
		public SaveReference SaveReference { get; }

		// Token: 0x06000012 RID: 18 RVA: 0x0000224E File Offset: 0x0000044E
		public GameSceneParameters(NewGameConfiguration newGameConfiguration, SaveReference saveReference)
		{
			this.NewGameConfiguration = newGameConfiguration;
			this.SaveReference = saveReference;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002264 File Offset: 0x00000464
		public static GameSceneParameters CreateNewGameParameters(NewGameConfiguration newGameConfiguration)
		{
			return new GameSceneParameters(newGameConfiguration, null);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000226D File Offset: 0x0000046D
		public static GameSceneParameters CreateGameSaveParameters(SaveReference saveReference)
		{
			return new GameSceneParameters(null, saveReference);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002276 File Offset: 0x00000476
		public bool NewGame
		{
			get
			{
				return this.NewGameConfiguration != null;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002281 File Offset: 0x00000481
		public int SceneIndex
		{
			get
			{
				return 2;
			}
		}
	}
}
