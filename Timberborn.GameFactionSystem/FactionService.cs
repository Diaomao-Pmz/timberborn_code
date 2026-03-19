using System;
using Timberborn.FactionSystem;
using Timberborn.GameSceneLoading;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.GameFactionSystem
{
	// Token: 0x02000010 RID: 16
	public class FactionService : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000289B File Offset: 0x00000A9B
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000028A3 File Offset: 0x00000AA3
		public FactionSpec Current { get; private set; }

		// Token: 0x06000044 RID: 68 RVA: 0x000028AC File Offset: 0x00000AAC
		public FactionService(ISingletonLoader singletonLoader, FactionSpecService factionSpecService, MapEditorMode mapEditorMode, ISceneLoader sceneLoader, FactionBlueprintModifierProvider factionBlueprintModifierProvider)
		{
			this._singletonLoader = singletonLoader;
			this._factionSpecService = factionSpecService;
			this._mapEditorMode = mapEditorMode;
			this._sceneLoader = sceneLoader;
			this._factionBlueprintModifierProvider = factionBlueprintModifierProvider;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000028DC File Offset: 0x00000ADC
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(FactionService.FactionServiceKey, out objectLoader))
			{
				this.SetCurrentFaction(objectLoader.Get(FactionService.IdKey));
				return;
			}
			GameSceneParameters sceneParameters = this._sceneLoader.GetSceneParameters<GameSceneParameters>();
			this.SetCurrentFaction(sceneParameters.NewGameConfiguration.FactionId);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000292C File Offset: 0x00000B2C
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				singletonSaver.GetSingleton(FactionService.FactionServiceKey).Set(FactionService.IdKey, this.Current.Id);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000295B File Offset: 0x00000B5B
		public void SetCurrentFaction(string factionId)
		{
			this.Current = this._factionSpecService.GetFaction(factionId);
			this._factionBlueprintModifierProvider.Initialize(this.Current.BlueprintModifiers);
		}

		// Token: 0x0400002C RID: 44
		public static readonly SingletonKey FactionServiceKey = new SingletonKey("FactionService");

		// Token: 0x0400002D RID: 45
		public static readonly PropertyKey<string> IdKey = new PropertyKey<string>("Id");

		// Token: 0x0400002F RID: 47
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000030 RID: 48
		public readonly FactionSpecService _factionSpecService;

		// Token: 0x04000031 RID: 49
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000032 RID: 50
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x04000033 RID: 51
		public readonly FactionBlueprintModifierProvider _factionBlueprintModifierProvider;
	}
}
