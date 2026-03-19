using System;
using Timberborn.Common;
using Timberborn.GameSceneLoading;
using Timberborn.MapStateSystem;
using Timberborn.NewGameConfigurationSystem;
using Timberborn.Persistence;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.HazardousWeatherSystem
{
	// Token: 0x02000005 RID: 5
	public class DroughtWeather : ISaveableSingleton, ILoadableSingleton, IHazardousWeather
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000238E File Offset: 0x0000058E
		public DroughtWeather(ISingletonLoader singletonLoader, IRandomNumberGenerator randomNumberGenerator, MapEditorMode mapEditorMode, ISceneLoader sceneLoader)
		{
			this._singletonLoader = singletonLoader;
			this._randomNumberGenerator = randomNumberGenerator;
			this._mapEditorMode = mapEditorMode;
			this._sceneLoader = sceneLoader;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000023B3 File Offset: 0x000005B3
		public string Id
		{
			get
			{
				return DroughtWeather.DroughtWeatherKey.Name;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023BF File Offset: 0x000005BF
		public void Initialize(int minDroughtDuration, int maxDroughtDuration, float handicapMultiplier, int handicapCycles)
		{
			this._minDroughtDuration = minDroughtDuration;
			this._maxDroughtDuration = maxDroughtDuration;
			this._handicapMultiplier = handicapMultiplier;
			this._handicapCycles = handicapCycles;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023E0 File Offset: 0x000005E0
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				IObjectSaver singleton = singletonSaver.GetSingleton(DroughtWeather.DroughtWeatherKey);
				singleton.Set(DroughtWeather.MinDroughtDurationKey, this._minDroughtDuration);
				singleton.Set(DroughtWeather.MaxDroughtDurationKey, this._maxDroughtDuration);
				singleton.Set(DroughtWeather.HandicapMultiplierKey, this._handicapMultiplier);
				singleton.Set(DroughtWeather.HandicapCyclesKey, this._handicapCycles);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002448 File Offset: 0x00000648
		public void Load()
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				IObjectLoader objectLoader;
				if (this._singletonLoader.TryGetSingleton(DroughtWeather.DroughtWeatherKey, out objectLoader))
				{
					this.Initialize(objectLoader.Get(DroughtWeather.MinDroughtDurationKey), objectLoader.Get(DroughtWeather.MaxDroughtDurationKey), objectLoader.Get(DroughtWeather.HandicapMultiplierKey), objectLoader.Get(DroughtWeather.HandicapCyclesKey));
					return;
				}
				GameModeSpec gameMode = this._sceneLoader.GetSceneParameters<GameSceneParameters>().NewGameConfiguration.GameMode;
				this.Initialize(gameMode.DroughtDuration.Min, gameMode.DroughtDuration.Max, gameMode.DroughtDurationHandicapMultiplier, gameMode.DroughtDurationHandicapCycles);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024EC File Offset: 0x000006EC
		public int GetDurationAtCycle(int cycle)
		{
			float handicapMultiplier = HazardousWeatherHelper.GetHandicapMultiplier(cycle, this._handicapMultiplier, (float)this._handicapCycles);
			float inclusiveMin = handicapMultiplier * (float)this._minDroughtDuration;
			float inclusiveMax = handicapMultiplier * (float)this._maxDroughtDuration;
			int num = (int)Math.Round((double)this._randomNumberGenerator.Range(inclusiveMin, inclusiveMax), MidpointRounding.AwayFromZero);
			if (this._minDroughtDuration > 0)
			{
				num = Math.Max(num, 1);
			}
			return num;
		}

		// Token: 0x04000018 RID: 24
		public static readonly SingletonKey DroughtWeatherKey = new SingletonKey("DroughtWeather");

		// Token: 0x04000019 RID: 25
		public static readonly PropertyKey<int> MinDroughtDurationKey = new PropertyKey<int>("MinDroughtDuration");

		// Token: 0x0400001A RID: 26
		public static readonly PropertyKey<int> MaxDroughtDurationKey = new PropertyKey<int>("MaxDroughtDuration");

		// Token: 0x0400001B RID: 27
		public static readonly PropertyKey<float> HandicapMultiplierKey = new PropertyKey<float>("HandicapMultiplier");

		// Token: 0x0400001C RID: 28
		public static readonly PropertyKey<int> HandicapCyclesKey = new PropertyKey<int>("HandicapCycles");

		// Token: 0x0400001D RID: 29
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400001E RID: 30
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400001F RID: 31
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000020 RID: 32
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x04000021 RID: 33
		public int _minDroughtDuration;

		// Token: 0x04000022 RID: 34
		public int _maxDroughtDuration;

		// Token: 0x04000023 RID: 35
		public float _handicapMultiplier;

		// Token: 0x04000024 RID: 36
		public int _handicapCycles;
	}
}
