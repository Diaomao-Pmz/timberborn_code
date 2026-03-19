using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.HazardousWeatherSystem;
using Timberborn.WaterSourceSystem;
using Timberborn.WeatherSystem;

namespace Timberborn.GameWaterSourceSystem
{
	// Token: 0x02000008 RID: 8
	public class HazardousWeatherWaterSource : BaseComponent, IAwakableComponent, IInitializableEntity, IWaterStrengthModifier
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021C9 File Offset: 0x000003C9
		public HazardousWeatherWaterSource(WeatherService weatherService, HazardousWeatherService hazardousWeatherService)
		{
			this._weatherService = weatherService;
			this._hazardousWeatherService = hazardousWeatherService;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021DF File Offset: 0x000003DF
		public void Awake()
		{
			this._hazardousWeatherWaterSourceSpec = base.GetComponent<HazardousWeatherWaterSourceSpec>();
			this._waterSource = base.GetComponent<WaterSource>();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021F9 File Offset: 0x000003F9
		public void InitializeEntity()
		{
			this._waterSource.AddWaterStrengthModifier(this);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002207 File Offset: 0x00000407
		public float GetStrengthModifier()
		{
			return (float)(this.ShouldBeActive() ? 1 : 0);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002218 File Offset: 0x00000418
		public bool ShouldBeActive()
		{
			return this._weatherService.IsHazardousWeather && this._hazardousWeatherWaterSourceSpec.ActiveInHazardousWeather.Contains(this._hazardousWeatherService.CurrentCycleHazardousWeather.Id);
		}

		// Token: 0x04000008 RID: 8
		public readonly WeatherService _weatherService;

		// Token: 0x04000009 RID: 9
		public readonly HazardousWeatherService _hazardousWeatherService;

		// Token: 0x0400000A RID: 10
		public HazardousWeatherWaterSourceSpec _hazardousWeatherWaterSourceSpec;

		// Token: 0x0400000B RID: 11
		public WaterSource _waterSource;

		// Token: 0x0400000C RID: 12
		public bool _activeInEditor;
	}
}
