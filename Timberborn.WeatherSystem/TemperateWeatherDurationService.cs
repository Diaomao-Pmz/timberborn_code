using System;
using Timberborn.Common;
using Timberborn.GameCycleSystem;
using Timberborn.GameSceneLoading;
using Timberborn.MapStateSystem;
using Timberborn.NewGameConfigurationSystem;
using Timberborn.Persistence;
using Timberborn.SceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.WeatherSystem
{
	// Token: 0x02000004 RID: 4
	public class TemperateWeatherDurationService : ISaveableSingleton, ILoadableSingleton, ICycleDuration
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public int TemperateWeatherDuration { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		public TemperateWeatherDurationService(IRandomNumberGenerator randomNumberGenerator, ISceneLoader sceneLoader, ISingletonLoader singletonLoader, MapEditorMode mapEditorMode)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._sceneLoader = sceneLoader;
			this._singletonLoader = singletonLoader;
			this._mapEditorMode = mapEditorMode;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020F4 File Offset: 0x000002F4
		public int DurationInDays
		{
			get
			{
				return this.TemperateWeatherDuration;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020FC File Offset: 0x000002FC
		public void Initialize(int minTemperateWeatherDuration, int maxTemperateWeatherDuration)
		{
			this._minTemperateWeatherDuration = minTemperateWeatherDuration;
			this._maxTemperateWeatherDuration = maxTemperateWeatherDuration;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000210C File Offset: 0x0000030C
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				IObjectSaver singleton = singletonSaver.GetSingleton(TemperateWeatherDurationService.TemperateWeatherDurationServiceKey);
				singleton.Set(TemperateWeatherDurationService.MinTemperateWeatherDurationKey, this._minTemperateWeatherDuration);
				singleton.Set(TemperateWeatherDurationService.MaxTemperateWeatherDurationKey, this._maxTemperateWeatherDuration);
				singleton.Set(TemperateWeatherDurationService.TemperateWeatherDurationKey, this.TemperateWeatherDuration);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002164 File Offset: 0x00000364
		public void Load()
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				IObjectLoader objectLoader;
				if (this._singletonLoader.TryGetSingleton(TemperateWeatherDurationService.TemperateWeatherDurationServiceKey, out objectLoader))
				{
					this.Initialize(objectLoader.Get(TemperateWeatherDurationService.MinTemperateWeatherDurationKey), objectLoader.Get(TemperateWeatherDurationService.MaxTemperateWeatherDurationKey));
					this.TemperateWeatherDuration = objectLoader.Get(TemperateWeatherDurationService.TemperateWeatherDurationKey);
					return;
				}
				MinMaxSpec<int> temperateWeatherDuration = this._sceneLoader.GetSceneParameters<GameSceneParameters>().NewGameConfiguration.GameMode.TemperateWeatherDuration;
				this.Initialize(temperateWeatherDuration.Min, temperateWeatherDuration.Max);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021ED File Offset: 0x000003ED
		public void SetForCycle(int cycle)
		{
			this.TemperateWeatherDuration = this.GenerateDuration();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FB File Offset: 0x000003FB
		public int GenerateDuration()
		{
			return this._randomNumberGenerator.Range(this._minTemperateWeatherDuration, this._maxTemperateWeatherDuration + 1);
		}

		// Token: 0x04000006 RID: 6
		public static readonly SingletonKey TemperateWeatherDurationServiceKey = new SingletonKey("TemperateWeatherDurationService");

		// Token: 0x04000007 RID: 7
		public static readonly PropertyKey<int> TemperateWeatherDurationKey = new PropertyKey<int>("TemperateWeatherDuration");

		// Token: 0x04000008 RID: 8
		public static readonly PropertyKey<int> MinTemperateWeatherDurationKey = new PropertyKey<int>("MinTemperateWeatherDuration");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<int> MaxTemperateWeatherDurationKey = new PropertyKey<int>("MaxTemperateWeatherDuration");

		// Token: 0x0400000B RID: 11
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000C RID: 12
		public readonly ISceneLoader _sceneLoader;

		// Token: 0x0400000D RID: 13
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400000E RID: 14
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x0400000F RID: 15
		public int _minTemperateWeatherDuration;

		// Token: 0x04000010 RID: 16
		public int _maxTemperateWeatherDuration;
	}
}
