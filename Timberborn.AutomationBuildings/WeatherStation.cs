using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.GameCycleSystem;
using Timberborn.HazardousWeatherSystem;
using Timberborn.Persistence;
using Timberborn.WeatherSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000053 RID: 83
	public class WeatherStation : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<WeatherStation>, IDuplicable, ISamplingTransmitter, ITransmitter
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000098B6 File Offset: 0x00007AB6
		// (set) Token: 0x06000376 RID: 886 RVA: 0x000098BE File Offset: 0x00007ABE
		public WeatherStationMode Mode { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000098C7 File Offset: 0x00007AC7
		// (set) Token: 0x06000378 RID: 888 RVA: 0x000098CF File Offset: 0x00007ACF
		public bool EarlyActivationEnabled { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000098D8 File Offset: 0x00007AD8
		// (set) Token: 0x0600037A RID: 890 RVA: 0x000098E0 File Offset: 0x00007AE0
		public int EarlyActivationHours { get; private set; }

		// Token: 0x0600037B RID: 891 RVA: 0x000098E9 File Offset: 0x00007AE9
		public WeatherStation(WeatherService weatherService, HazardousWeatherService hazardousWeatherService, GameCycleService gameCycleService)
		{
			this._weatherService = weatherService;
			this._hazardousWeatherService = hazardousWeatherService;
			this._gameCycleService = gameCycleService;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00009906 File Offset: 0x00007B06
		public float MaxEarlyActivationHours
		{
			get
			{
				return (float)this._weatherStationSpec.MaxEarlyActivationHours;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00009914 File Offset: 0x00007B14
		public void Awake()
		{
			this._weatherStationSpec = base.GetComponent<WeatherStationSpec>();
			this._automator = base.GetComponent<Automator>();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000992E File Offset: 0x00007B2E
		public void SetMode(WeatherStationMode value)
		{
			if (this.Mode != value)
			{
				this.Mode = value;
				this.UpdateOutputState();
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00009946 File Offset: 0x00007B46
		public void SetEarlyActivationEnabled(bool value)
		{
			if (this.EarlyActivationEnabled != value)
			{
				this.EarlyActivationEnabled = value;
				this.UpdateOutputState();
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000995E File Offset: 0x00007B5E
		public void SetEarlyActivationHours(int value)
		{
			if (this.EarlyActivationHours != value)
			{
				this.EarlyActivationHours = value;
				this.UpdateOutputState();
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00009976 File Offset: 0x00007B76
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(WeatherStation.WeatherStationKey);
			component.Set<WeatherStationMode>(WeatherStation.ModeKey, this.Mode);
			component.Set(WeatherStation.EarlyActivationEnabledKey, this.EarlyActivationEnabled);
			component.Set(WeatherStation.EarlyActivationHoursKey, this.EarlyActivationHours);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000099B8 File Offset: 0x00007BB8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(WeatherStation.WeatherStationKey, out objectLoader))
			{
				this.Mode = (objectLoader.Has<WeatherStationMode>(WeatherStation.ModeKey) ? objectLoader.Get<WeatherStationMode>(WeatherStation.ModeKey) : WeatherStationMode.Temperate);
				this.EarlyActivationEnabled = (objectLoader.Has<bool>(WeatherStation.EarlyActivationEnabledKey) && objectLoader.Get(WeatherStation.EarlyActivationEnabledKey));
				this.EarlyActivationHours = (objectLoader.Has<int>(WeatherStation.EarlyActivationHoursKey) ? objectLoader.Get(WeatherStation.EarlyActivationHoursKey) : 0);
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00009A37 File Offset: 0x00007C37
		public void DuplicateFrom(WeatherStation source)
		{
			this.Mode = source.Mode;
			this.EarlyActivationEnabled = source.EarlyActivationEnabled;
			this.EarlyActivationHours = source.EarlyActivationHours;
			this.UpdateOutputState();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00009A64 File Offset: 0x00007C64
		public void Sample()
		{
			this._sampledPartialCycleDay = this._gameCycleService.PartialCycleDay;
			this._sampledCycleLengthInDays = this._weatherService.CycleLengthInDays;
			this._sampledHazardousWeatherStartCycleDay = this._weatherService.HazardousWeatherStartCycleDay;
			this._sampledIsDrought = (this._hazardousWeatherService.CurrentCycleHazardousWeather is DroughtWeather);
			this._sampledIsBadtide = (this._hazardousWeatherService.CurrentCycleHazardousWeather is BadtideWeather);
			this.UpdateOutputState();
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00009ADC File Offset: 0x00007CDC
		public float EffectiveEarlyActivationDays
		{
			get
			{
				if (!this.EarlyActivationEnabled)
				{
					return 0f;
				}
				return (float)this.EarlyActivationHours / 24f;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00009AF9 File Offset: 0x00007CF9
		public bool WithinActivationTimeTemperate
		{
			get
			{
				return this._sampledPartialCycleDay < (float)this._sampledHazardousWeatherStartCycleDay || this._sampledPartialCycleDay >= (float)(this._sampledCycleLengthInDays + 1) - this.EffectiveEarlyActivationDays;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00009B27 File Offset: 0x00007D27
		public bool WithinActivationTimeHazardous
		{
			get
			{
				return this._sampledPartialCycleDay >= (float)this._sampledHazardousWeatherStartCycleDay - this.EffectiveEarlyActivationDays;
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00009B44 File Offset: 0x00007D44
		public void UpdateOutputState()
		{
			Automator automator = this._automator;
			bool state;
			switch (this.Mode)
			{
			case WeatherStationMode.Temperate:
				state = this.WithinActivationTimeTemperate;
				break;
			case WeatherStationMode.Drought:
				state = (this._sampledIsDrought && this.WithinActivationTimeHazardous);
				break;
			case WeatherStationMode.Badtide:
				state = (this._sampledIsBadtide && this.WithinActivationTimeHazardous);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			automator.SetState(state);
		}

		// Token: 0x040001A7 RID: 423
		public static readonly ComponentKey WeatherStationKey = new ComponentKey("WeatherStation");

		// Token: 0x040001A8 RID: 424
		public static readonly PropertyKey<WeatherStationMode> ModeKey = new PropertyKey<WeatherStationMode>("Mode");

		// Token: 0x040001A9 RID: 425
		public static readonly PropertyKey<bool> EarlyActivationEnabledKey = new PropertyKey<bool>("EarlyActivationEnabled");

		// Token: 0x040001AA RID: 426
		public static readonly PropertyKey<int> EarlyActivationHoursKey = new PropertyKey<int>("EarlyActivationHours");

		// Token: 0x040001AE RID: 430
		public readonly WeatherService _weatherService;

		// Token: 0x040001AF RID: 431
		public readonly HazardousWeatherService _hazardousWeatherService;

		// Token: 0x040001B0 RID: 432
		public readonly GameCycleService _gameCycleService;

		// Token: 0x040001B1 RID: 433
		public WeatherStationSpec _weatherStationSpec;

		// Token: 0x040001B2 RID: 434
		public Automator _automator;

		// Token: 0x040001B3 RID: 435
		public float _sampledPartialCycleDay;

		// Token: 0x040001B4 RID: 436
		public int _sampledCycleLengthInDays;

		// Token: 0x040001B5 RID: 437
		public int _sampledHazardousWeatherStartCycleDay;

		// Token: 0x040001B6 RID: 438
		public bool _sampledIsDrought;

		// Token: 0x040001B7 RID: 439
		public bool _sampledIsBadtide;
	}
}
