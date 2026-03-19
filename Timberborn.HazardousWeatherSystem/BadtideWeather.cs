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
	// Token: 0x02000004 RID: 4
	public class BadtideWeather : ISaveableSingleton, ILoadableSingleton, IHazardousWeather
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public float ChanceForBadtide { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020D1 File Offset: 0x000002D1
		public BadtideWeather(ISingletonLoader singletonLoader, IRandomNumberGenerator randomNumberGenerator, MapEditorMode mapEditorMode, ISceneLoader sceneLoader, GameModeSpecService gameModeSpecService)
		{
			this._singletonLoader = singletonLoader;
			this._randomNumberGenerator = randomNumberGenerator;
			this._mapEditorMode = mapEditorMode;
			this._sceneLoader = sceneLoader;
			this._gameModeSpecService = gameModeSpecService;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020FE File Offset: 0x000002FE
		public string Id
		{
			get
			{
				return BadtideWeather.BadtideWeatherKey.Name;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000210A File Offset: 0x0000030A
		public void Initialize(GameModeSpec gameMode)
		{
			this.Initialize(gameMode.BadtideDuration.Min, gameMode.BadtideDuration.Max, gameMode.BadtideDurationHandicapMultiplier, gameMode.BadtideDurationHandicapCycles, gameMode.CyclesBeforeRandomizingBadtide, gameMode.ChanceForBadtide);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002140 File Offset: 0x00000340
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				IObjectSaver singleton = singletonSaver.GetSingleton(BadtideWeather.BadtideWeatherKey);
				singleton.Set(BadtideWeather.MinDurationKey, this._minDuration);
				singleton.Set(BadtideWeather.MaxDurationKey, this._maxDuration);
				singleton.Set(BadtideWeather.HandicapMultiplierKey, this._handicapMultiplier);
				singleton.Set(BadtideWeather.HandicapCyclesKey, this._handicapCycles);
				singleton.Set(BadtideWeather.CyclesBeforeRandomizingKey, this._cyclesBeforeRandomizingBadtideWeather);
				singleton.Set(BadtideWeather.ChanceForBadtideWeatherKey, this.ChanceForBadtide);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021CC File Offset: 0x000003CC
		public void Load()
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				IObjectLoader objectLoader;
				if (this._singletonLoader.TryGetSingleton(BadtideWeather.BadtideWeatherKey, out objectLoader))
				{
					this.Initialize(objectLoader.Get(BadtideWeather.MinDurationKey), objectLoader.Get(BadtideWeather.MaxDurationKey), objectLoader.Get(BadtideWeather.HandicapMultiplierKey), objectLoader.Get(BadtideWeather.HandicapCyclesKey), objectLoader.Get(BadtideWeather.CyclesBeforeRandomizingKey), objectLoader.Get(BadtideWeather.ChanceForBadtideWeatherKey));
					return;
				}
				GameSceneParameters gameSceneParameters;
				if (this._sceneLoader.TryGetSceneParameters<GameSceneParameters>(out gameSceneParameters) && gameSceneParameters.NewGame)
				{
					this.Initialize(gameSceneParameters.NewGameConfiguration.GameMode);
					return;
				}
				this.Initialize(this._gameModeSpecService.GetDefaultSpec());
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002280 File Offset: 0x00000480
		public bool CanOccurAtCycle(int cycle)
		{
			return cycle > this._cyclesBeforeRandomizingBadtideWeather;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000228C File Offset: 0x0000048C
		public int GetDurationAtCycle(int cycle)
		{
			float handicapMultiplier = HazardousWeatherHelper.GetHandicapMultiplier(cycle, this._handicapMultiplier, (float)this._handicapCycles);
			float inclusiveMin = handicapMultiplier * (float)this._minDuration;
			float inclusiveMax = handicapMultiplier * (float)this._maxDuration;
			int num = (int)Math.Round((double)this._randomNumberGenerator.Range(inclusiveMin, inclusiveMax), MidpointRounding.AwayFromZero);
			if (this._minDuration > 0)
			{
				num = Math.Max(num, 1);
			}
			return num;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022E7 File Offset: 0x000004E7
		public void Initialize(int minDuration, int maxDuration, float handicapMultiplier, int handicapCycles, int cyclesBeforeRandomizingBadtide, float chanceForBadtide)
		{
			this._minDuration = minDuration;
			this._maxDuration = maxDuration;
			this._handicapMultiplier = handicapMultiplier;
			this._handicapCycles = handicapCycles;
			this._cyclesBeforeRandomizingBadtideWeather = cyclesBeforeRandomizingBadtide;
			this.ChanceForBadtide = chanceForBadtide;
		}

		// Token: 0x04000006 RID: 6
		public static readonly SingletonKey BadtideWeatherKey = new SingletonKey("BadtideWeather");

		// Token: 0x04000007 RID: 7
		public static readonly PropertyKey<int> MinDurationKey = new PropertyKey<int>("MinBadtideWeatherDuration");

		// Token: 0x04000008 RID: 8
		public static readonly PropertyKey<int> MaxDurationKey = new PropertyKey<int>("MaxBadtideWeatherDuration");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<float> HandicapMultiplierKey = new PropertyKey<float>("HandicapMultiplier");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<int> HandicapCyclesKey = new PropertyKey<int>("HandicapCycles");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<int> CyclesBeforeRandomizingKey = new PropertyKey<int>("CyclesBeforeRandomizing");

		// Token: 0x0400000C RID: 12
		public static readonly PropertyKey<float> ChanceForBadtideWeatherKey = new PropertyKey<float>("ChanceBadtideWeather");

		// Token: 0x0400000E RID: 14
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400000F RID: 15
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000010 RID: 16
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000011 RID: 17
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x04000012 RID: 18
		public readonly GameModeSpecService _gameModeSpecService;

		// Token: 0x04000013 RID: 19
		public int _minDuration;

		// Token: 0x04000014 RID: 20
		public int _maxDuration;

		// Token: 0x04000015 RID: 21
		public float _handicapMultiplier;

		// Token: 0x04000016 RID: 22
		public int _handicapCycles;

		// Token: 0x04000017 RID: 23
		public int _cyclesBeforeRandomizingBadtideWeather;
	}
}
