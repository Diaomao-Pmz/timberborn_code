using System;
using Timberborn.GameCycleSystem;
using Timberborn.GameSound;
using Timberborn.HazardousWeatherSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.HazardousWeatherSystemUI
{
	// Token: 0x0200000C RID: 12
	public class HazardousWeatherSoundPlayer : ILoadableSingleton
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000264D File Offset: 0x0000084D
		public HazardousWeatherSoundPlayer(EventBus eventBus, GameUISoundController gameUISoundController)
		{
			this._eventBus = eventBus;
			this._gameUISoundController = gameUISoundController;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002663 File Offset: 0x00000863
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002674 File Offset: 0x00000874
		[OnEvent]
		public void OnHazardousWeatherStarted(HazardousWeatherStartedEvent hazardousWeatherStartedEvent)
		{
			if (hazardousWeatherStartedEvent.HazardousWeather is BadtideWeather)
			{
				this._gameUISoundController.PlayBadtideStartedSound();
				return;
			}
			if (hazardousWeatherStartedEvent.HazardousWeather is DroughtWeather)
			{
				this._gameUISoundController.PlayDroughtStartedSound();
				return;
			}
			throw new ArgumentException("No start sound for weather type: " + hazardousWeatherStartedEvent.HazardousWeather.Id);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026CD File Offset: 0x000008CD
		[OnEvent]
		public void OnCycleStarted(CycleStartedEvent cycleStartedEvent)
		{
			if (cycleStartedEvent.Cycle > 1)
			{
				this._gameUISoundController.PlayTemperateWeatherStartedSound();
			}
		}

		// Token: 0x04000022 RID: 34
		public readonly EventBus _eventBus;

		// Token: 0x04000023 RID: 35
		public readonly GameUISoundController _gameUISoundController;
	}
}
