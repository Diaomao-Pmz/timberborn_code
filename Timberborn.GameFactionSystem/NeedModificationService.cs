using System;
using Timberborn.GameSceneLoading;
using Timberborn.NeedSpecs;
using Timberborn.NewGameConfigurationSystem;
using Timberborn.Persistence;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.GameFactionSystem
{
	// Token: 0x02000013 RID: 19
	public class NeedModificationService : ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002A71 File Offset: 0x00000C71
		public NeedModificationService(ISceneLoader sceneLoader, ISingletonLoader singletonLoader)
		{
			this._sceneLoader = sceneLoader;
			this._singletonLoader = singletonLoader;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A88 File Offset: 0x00000C88
		public void Load()
		{
			GameSceneParameters sceneParameters = this._sceneLoader.GetSceneParameters<GameSceneParameters>();
			if (sceneParameters.NewGame)
			{
				GameModeSpec gameMode = sceneParameters.NewGameConfiguration.GameMode;
				this._foodConsumption = gameMode.FoodConsumption;
				this._waterConsumption = gameMode.WaterConsumption;
				return;
			}
			IObjectLoader singleton = this._singletonLoader.GetSingleton(NeedModificationService.NeedModificationServiceKey);
			this._foodConsumption = singleton.Get(NeedModificationService.FoodConsumptionKey);
			this._waterConsumption = singleton.Get(NeedModificationService.WaterConsumptionKey);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B01 File Offset: 0x00000D01
		public void Save(ISingletonSaver singletonSaver)
		{
			IObjectSaver singleton = singletonSaver.GetSingleton(NeedModificationService.NeedModificationServiceKey);
			singleton.Set(NeedModificationService.FoodConsumptionKey, this._foodConsumption);
			singleton.Set(NeedModificationService.WaterConsumptionKey, this._waterConsumption);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B30 File Offset: 0x00000D30
		public NeedSpec ModifyIfEligible(NeedSpec needSpec)
		{
			if (needSpec.NeedGroupId == NeedModificationService.FoodGroupId)
			{
				NeedSpec needSpec2 = (NeedSpec)needSpec.<Clone>$();
				needSpec2.DailyDelta = this._foodConsumption * needSpec.DailyDelta;
				return needSpec2;
			}
			if (needSpec.Id == NeedModificationService.FoodNeedId)
			{
				NeedSpec needSpec3 = (NeedSpec)needSpec.<Clone>$();
				needSpec3.DailyDelta = this._foodConsumption * needSpec.DailyDelta;
				return needSpec3;
			}
			if (needSpec.Id == NeedModificationService.WaterNeedId)
			{
				NeedSpec needSpec4 = (NeedSpec)needSpec.<Clone>$();
				needSpec4.DailyDelta = this._waterConsumption * needSpec.DailyDelta;
				return needSpec4;
			}
			return needSpec;
		}

		// Token: 0x04000035 RID: 53
		public static readonly SingletonKey NeedModificationServiceKey = new SingletonKey("NeedModificationService");

		// Token: 0x04000036 RID: 54
		public static readonly PropertyKey<float> FoodConsumptionKey = new PropertyKey<float>("FoodConsumption");

		// Token: 0x04000037 RID: 55
		public static readonly PropertyKey<float> WaterConsumptionKey = new PropertyKey<float>("WaterConsumption");

		// Token: 0x04000038 RID: 56
		public static readonly string FoodNeedId = "Hunger";

		// Token: 0x04000039 RID: 57
		public static readonly string WaterNeedId = "Thirst";

		// Token: 0x0400003A RID: 58
		public static readonly string FoodGroupId = "Nutrition";

		// Token: 0x0400003B RID: 59
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x0400003C RID: 60
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400003D RID: 61
		public float _foodConsumption;

		// Token: 0x0400003E RID: 62
		public float _waterConsumption;
	}
}
