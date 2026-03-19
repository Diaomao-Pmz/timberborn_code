using System;
using Timberborn.GameCycleSystem;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.HazardousWeatherSystem
{
	// Token: 0x0200000D RID: 13
	public class HazardousWeatherService : ISaveableSingleton, ILoadableSingleton, ICycleDuration
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002A2E File Offset: 0x00000C2E
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002A36 File Offset: 0x00000C36
		public IHazardousWeather CurrentCycleHazardousWeather { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A3F File Offset: 0x00000C3F
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002A47 File Offset: 0x00000C47
		public int HazardousWeatherDuration { get; private set; }

		// Token: 0x0600003A RID: 58 RVA: 0x00002A50 File Offset: 0x00000C50
		public HazardousWeatherService(EventBus eventBus, ISingletonLoader singletonLoader, MapEditorMode mapEditorMode, DroughtWeather droughtWeather, BadtideWeather badtideWeather, HazardousWeatherRandomizer hazardousWeatherRandomizer, HazardousWeatherHistory hazardousWeatherHistory)
		{
			this._eventBus = eventBus;
			this._singletonLoader = singletonLoader;
			this._mapEditorMode = mapEditorMode;
			this._droughtWeather = droughtWeather;
			this._badtideWeather = badtideWeather;
			this._hazardousWeatherRandomizer = hazardousWeatherRandomizer;
			this._hazardousWeatherHistory = hazardousWeatherHistory;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A8D File Offset: 0x00000C8D
		public int DurationInDays
		{
			get
			{
				return this.HazardousWeatherDuration;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A98 File Offset: 0x00000C98
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				IObjectSaver singleton = singletonSaver.GetSingleton(HazardousWeatherService.HazardousWeatherServiceKey);
				singleton.Set(HazardousWeatherService.HazardousWeatherDurationKey, this.HazardousWeatherDuration);
				singleton.Set(HazardousWeatherService.IsDroughtKey, this.CurrentCycleHazardousWeather == this._droughtWeather);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public void Load()
		{
			IObjectLoader objectLoader;
			if (!this._mapEditorMode.IsMapEditor && this._singletonLoader.TryGetSingleton(HazardousWeatherService.HazardousWeatherServiceKey, out objectLoader))
			{
				this.HazardousWeatherDuration = objectLoader.Get(HazardousWeatherService.HazardousWeatherDurationKey);
				if (objectLoader.Get(HazardousWeatherService.IsDroughtKey))
				{
					this.CurrentCycleHazardousWeather = this._droughtWeather;
					return;
				}
				this.CurrentCycleHazardousWeather = this._badtideWeather;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B50 File Offset: 0x00000D50
		public void SetForCycle(int cycle)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				this.CurrentCycleHazardousWeather = this._hazardousWeatherRandomizer.GetRandomWeatherForCycle(cycle);
				int cyclesCount = this._hazardousWeatherHistory.GetCyclesCount(this.CurrentCycleHazardousWeather.Id);
				this.HazardousWeatherDuration = this.CurrentCycleHazardousWeather.GetDurationAtCycle(cyclesCount + 1);
				this._eventBus.Post(new HazardousWeatherSelectedEvent(this.CurrentCycleHazardousWeather, this.HazardousWeatherDuration));
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BC3 File Offset: 0x00000DC3
		public void StartHazardousWeather()
		{
			this._eventBus.Post(new HazardousWeatherStartedEvent(this.CurrentCycleHazardousWeather));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BDB File Offset: 0x00000DDB
		public void EndHazardousWeather()
		{
			this._eventBus.Post(new HazardousWeatherEndedEvent(this.CurrentCycleHazardousWeather));
		}

		// Token: 0x0400003B RID: 59
		public static readonly SingletonKey HazardousWeatherServiceKey = new SingletonKey("HazardousWeatherService");

		// Token: 0x0400003C RID: 60
		public static readonly PropertyKey<int> HazardousWeatherDurationKey = new PropertyKey<int>("HazardousWeatherDuration");

		// Token: 0x0400003D RID: 61
		public static readonly PropertyKey<bool> IsDroughtKey = new PropertyKey<bool>("IsDrought");

		// Token: 0x04000040 RID: 64
		public readonly EventBus _eventBus;

		// Token: 0x04000041 RID: 65
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000042 RID: 66
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000043 RID: 67
		public readonly DroughtWeather _droughtWeather;

		// Token: 0x04000044 RID: 68
		public readonly BadtideWeather _badtideWeather;

		// Token: 0x04000045 RID: 69
		public readonly HazardousWeatherRandomizer _hazardousWeatherRandomizer;

		// Token: 0x04000046 RID: 70
		public readonly HazardousWeatherHistory _hazardousWeatherHistory;
	}
}
