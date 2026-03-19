using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.GameCycleSystem;
using Timberborn.HazardousWeatherSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.WeatherSystem;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000007 RID: 7
	public class BadtideWaterSourceContaminationController : TickableComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public BadtideWaterSourceContaminationController(EventBus eventBus, WeatherService weatherService, GameCycleService gameCycleService)
		{
			this._eventBus = eventBus;
			this._weatherService = weatherService;
			this._gameCycleService = gameCycleService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211D File Offset: 0x0000031D
		public void Awake()
		{
			this._waterSourceContamination = base.GetComponent<WaterSourceContamination>();
			this._hazardousWeatherObserver = base.GetComponent<HazardousWeatherObserver>();
			this._eventBus.Register(this);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002143 File Offset: 0x00000343
		public void InitializeEntity()
		{
			if (this._hazardousWeatherObserver.IsBadtideWeather)
			{
				base.EnableComponent();
				this.UpdateBadtideContamination();
				return;
			}
			base.DisableComponent();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002165 File Offset: 0x00000365
		public override void Tick()
		{
			this.UpdateBadtideContamination();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000216D File Offset: 0x0000036D
		[OnEvent]
		public void OnHazardousWeatherStarted(HazardousWeatherStartedEvent hazardousWeatherStartedEvent)
		{
			if (hazardousWeatherStartedEvent.HazardousWeather is BadtideWeather)
			{
				base.EnableComponent();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002182 File Offset: 0x00000382
		[OnEvent]
		public void OnHazardousWeatherEnded(HazardousWeatherEndedEvent hazardousWeatherEndedEvent)
		{
			if (hazardousWeatherEndedEvent.HazardousWeather is BadtideWeather)
			{
				this._waterSourceContamination.ResetContamination();
				base.DisableComponent();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A2 File Offset: 0x000003A2
		public void UpdateBadtideContamination()
		{
			this._waterSourceContamination.SetContamination(this.GetCurrentContamination());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B8 File Offset: 0x000003B8
		public float GetCurrentContamination()
		{
			float num = this._gameCycleService.PartialCycleDay - (float)this._weatherService.HazardousWeatherStartCycleDay;
			if (num < BadtideWaterSourceContaminationController.HalfDay)
			{
				return BadtideWaterSourceContaminationController.EvaluateContamination(num);
			}
			float num2 = (float)(this._weatherService.CycleLengthInDays + 1) - this._gameCycleService.PartialCycleDay;
			if (num2 < BadtideWaterSourceContaminationController.HalfDay)
			{
				return BadtideWaterSourceContaminationController.EvaluateContamination(num2);
			}
			return 1f;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000221C File Offset: 0x0000041C
		public static float EvaluateContamination(float time)
		{
			return BadtideWaterSourceContaminationController.HyperbolicSecant(17f * (time - BadtideWaterSourceContaminationController.HalfDay)) * 0.5f + 0.5f;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000223C File Offset: 0x0000043C
		public static float HyperbolicSecant(float x)
		{
			return 2f / (MathF.Exp(x) + MathF.Exp(-x));
		}

		// Token: 0x04000008 RID: 8
		public static readonly float HalfDay = 0.5f;

		// Token: 0x04000009 RID: 9
		public readonly EventBus _eventBus;

		// Token: 0x0400000A RID: 10
		public readonly WeatherService _weatherService;

		// Token: 0x0400000B RID: 11
		public readonly GameCycleService _gameCycleService;

		// Token: 0x0400000C RID: 12
		public WaterSourceContamination _waterSourceContamination;

		// Token: 0x0400000D RID: 13
		public HazardousWeatherObserver _hazardousWeatherObserver;
	}
}
