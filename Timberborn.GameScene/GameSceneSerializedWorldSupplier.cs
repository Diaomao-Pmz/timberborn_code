using System;
using Timberborn.ApplicationLifetime;
using Timberborn.GameSaveRuntimeSystem;
using Timberborn.GameSceneLoading;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;
using Timberborn.WorldSerialization;
using UnityEngine;

namespace Timberborn.GameScene
{
	// Token: 0x02000008 RID: 8
	public class GameSceneSerializedWorldSupplier : ISerializedWorldSupplier, ILoadableSingleton, INonSingletonPostLoader
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002348 File Offset: 0x00000548
		public GameSceneSerializedWorldSupplier(GameLoader gameLoader, ISceneLoader sceneLoader)
		{
			this._gameLoader = gameLoader;
			this._sceneLoader = sceneLoader;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002360 File Offset: 0x00000560
		public void Load()
		{
			GameSceneParameters sceneParameters = this._sceneLoader.GetSceneParameters<GameSceneParameters>();
			this._serializedWorld = this.LoadGame(sceneParameters);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002386 File Offset: 0x00000586
		public void PostLoadNonSingletons()
		{
			this._serializedWorld = null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000238F File Offset: 0x0000058F
		public SerializedWorld Get()
		{
			return this._serializedWorld;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002398 File Offset: 0x00000598
		public SerializedWorld LoadGame(GameSceneParameters gameSceneParameters)
		{
			SerializedWorld result;
			try
			{
				result = (gameSceneParameters.NewGame ? this._gameLoader.LoadNew(gameSceneParameters.NewGameConfiguration) : this._gameLoader.Load(gameSceneParameters.SaveReference));
			}
			catch
			{
				if (Application.isEditor)
				{
					GameQuitter.Quit();
				}
				throw;
			}
			return result;
		}

		// Token: 0x04000011 RID: 17
		public readonly GameLoader _gameLoader;

		// Token: 0x04000012 RID: 18
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x04000013 RID: 19
		public SerializedWorld _serializedWorld;
	}
}
