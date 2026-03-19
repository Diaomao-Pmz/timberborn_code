using System;
using Timberborn.ApplicationLifetime;
using Timberborn.MapEditorPersistence;
using Timberborn.MapEditorSceneLoading;
using Timberborn.MapRepositorySystem;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;
using Timberborn.WorldSerialization;
using UnityEngine;

namespace Timberborn.MapEditorScene
{
	// Token: 0x02000007 RID: 7
	public class MapEditorSerializedWorldSupplier : ISerializedWorldSupplier, ILoadableSingleton, INonSingletonPostLoader
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002280 File Offset: 0x00000480
		public MapEditorSerializedWorldSupplier(MapEditorMapLoader mapEditorMapLoader, ISceneLoader sceneLoader)
		{
			this._mapEditorMapLoader = mapEditorMapLoader;
			this._sceneLoader = sceneLoader;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002298 File Offset: 0x00000498
		public void Load()
		{
			MapEditorSceneParameters sceneParameters = this._sceneLoader.GetSceneParameters<MapEditorSceneParameters>();
			this._serializedWorld = this.LoadGame(sceneParameters);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022BE File Offset: 0x000004BE
		public void PostLoadNonSingletons()
		{
			this._serializedWorld = null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022C7 File Offset: 0x000004C7
		public SerializedWorld Get()
		{
			return this._serializedWorld;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022D0 File Offset: 0x000004D0
		public SerializedWorld LoadGame(MapEditorSceneParameters mapEditorSceneParameters)
		{
			SerializedWorld result;
			try
			{
				if (mapEditorSceneParameters.NewMap)
				{
					Vector2Int? newMapSize = mapEditorSceneParameters.NewMapSize;
					if (newMapSize == null)
					{
						throw new InvalidOperationException("Missing map size");
					}
					Vector2Int valueOrDefault = newMapSize.GetValueOrDefault();
					result = this._mapEditorMapLoader.LoadNew(valueOrDefault);
				}
				else
				{
					MapFileReference? map = mapEditorSceneParameters.Map;
					if (map == null)
					{
						throw new InvalidOperationException("Missing map parameter");
					}
					MapFileReference valueOrDefault2 = map.GetValueOrDefault();
					result = this._mapEditorMapLoader.Load(valueOrDefault2);
				}
			}
			catch (Exception)
			{
				if (Application.isEditor)
				{
					GameQuitter.Quit();
				}
				throw;
			}
			return result;
		}

		// Token: 0x04000009 RID: 9
		public readonly MapEditorMapLoader _mapEditorMapLoader;

		// Token: 0x0400000A RID: 10
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x0400000B RID: 11
		public SerializedWorld _serializedWorld;
	}
}
