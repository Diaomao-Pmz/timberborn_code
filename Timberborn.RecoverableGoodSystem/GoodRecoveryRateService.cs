using System;
using Timberborn.GameSceneLoading;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.RecoverableGoodSystem
{
	// Token: 0x02000004 RID: 4
	public class GoodRecoveryRateService : ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public float DemolishableRecoveryRate { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		public GoodRecoveryRateService(MapEditorMode mapEditorMode, ISingletonLoader singletonLoader, ISceneLoader sceneLoader)
		{
			this._mapEditorMode = mapEditorMode;
			this._singletonLoader = singletonLoader;
			this._sceneLoader = sceneLoader;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EC File Offset: 0x000002EC
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				singletonSaver.GetSingleton(GoodRecoveryRateService.GoodRecoveryRateServiceKey).Set(GoodRecoveryRateService.DemolishableRecoveryRateKey, this.DemolishableRecoveryRate);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002118 File Offset: 0x00000318
		public void Load()
		{
			GameSceneParameters gameSceneParameters;
			if (this._sceneLoader.TryGetSceneParameters<GameSceneParameters>(out gameSceneParameters) && gameSceneParameters.NewGame)
			{
				this.DemolishableRecoveryRate = gameSceneParameters.NewGameConfiguration.GameMode.DemolishableRecoveryRate;
				return;
			}
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(GoodRecoveryRateService.GoodRecoveryRateServiceKey, out objectLoader))
			{
				this.DemolishableRecoveryRate = objectLoader.Get(GoodRecoveryRateService.DemolishableRecoveryRateKey);
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly SingletonKey GoodRecoveryRateServiceKey = new SingletonKey("GoodRecoveryRateService");

		// Token: 0x04000007 RID: 7
		public static readonly PropertyKey<float> DemolishableRecoveryRateKey = new PropertyKey<float>("DemolishableRecoveryRate");

		// Token: 0x04000009 RID: 9
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x0400000A RID: 10
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400000B RID: 11
		public readonly ISceneLoader _sceneLoader;
	}
}
