using System;
using Timberborn.ActivatorSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.HazardousWeatherSystem;
using Timberborn.WaterSourceSystem;
using Timberborn.WeatherSystem;

namespace Timberborn.WaterSourceRendering
{
	// Token: 0x02000009 RID: 9
	public class WaterSourceRenderer : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000021ED File Offset: 0x000003ED
		public WaterSourceRenderer(WaterSourceRenderingService waterSourceRenderingService, WeatherService weatherService, HazardousWeatherService hazardousWeatherService)
		{
			this._waterSourceRenderingService = waterSourceRenderingService;
			this._weatherService = weatherService;
			this._hazardousWeatherService = hazardousWeatherService;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000220A File Offset: 0x0000040A
		public bool CanBeRendered
		{
			get
			{
				return this.CanEmitWater() || this._waterSource.CurrentStrength > 0f;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002228 File Offset: 0x00000428
		public void Awake()
		{
			this._waterSource = base.GetComponent<WaterSource>();
			this._timedComponentActivator = base.GetComponent<TimedComponentActivator>();
			this._isDepthLimitedWaterSource = base.HasComponent<WaterDepthStrengthModifierSpec>();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000224E File Offset: 0x0000044E
		public void InitializeEntity()
		{
			this._waterSourceRenderingService.AddRenderer(this);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000225C File Offset: 0x0000045C
		public void DeleteEntity()
		{
			this._waterSourceRenderingService.RemoveRenderer(this);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000226C File Offset: 0x0000046C
		public bool CanEmitWater()
		{
			return (!this._weatherService.IsHazardousWeather || !(this._hazardousWeatherService.CurrentCycleHazardousWeather is DroughtWeather)) && (!this._timedComponentActivator.IsEnabled || this._timedComponentActivator.IsPastActivationTime) && this._isDepthLimitedWaterSource;
		}

		// Token: 0x0400000E RID: 14
		public readonly WaterSourceRenderingService _waterSourceRenderingService;

		// Token: 0x0400000F RID: 15
		public readonly WeatherService _weatherService;

		// Token: 0x04000010 RID: 16
		public readonly HazardousWeatherService _hazardousWeatherService;

		// Token: 0x04000011 RID: 17
		public WaterSource _waterSource;

		// Token: 0x04000012 RID: 18
		public TimedComponentActivator _timedComponentActivator;

		// Token: 0x04000013 RID: 19
		public bool _isDepthLimitedWaterSource;
	}
}
