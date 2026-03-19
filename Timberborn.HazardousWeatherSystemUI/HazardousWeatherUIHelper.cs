using System;
using Timberborn.HazardousWeatherSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.HazardousWeatherSystemUI
{
	// Token: 0x0200000E RID: 14
	public class HazardousWeatherUIHelper : ILoadableSingleton
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002741 File Offset: 0x00000941
		public HazardousWeatherUIHelper(HazardousWeatherService hazardousWeatherService, EventBus eventBus, DroughtWeatherUISpecification droughtWeatherUISpecification, BadtideWeatherUISpecification badtideWeatherUISpecification)
		{
			this._hazardousWeatherService = hazardousWeatherService;
			this._eventBus = eventBus;
			this._droughtWeatherUISpecification = droughtWeatherUISpecification;
			this._badtideWeatherUISpecification = badtideWeatherUISpecification;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002766 File Offset: 0x00000966
		public string NameLocKey
		{
			get
			{
				return this._currentUISpecification.NameLocKey;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002773 File Offset: 0x00000973
		public string ApproachingLocKey
		{
			get
			{
				return this._currentUISpecification.ApproachingLocKey;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002780 File Offset: 0x00000980
		public string InProgressLocKey
		{
			get
			{
				return this._currentUISpecification.InProgressLocKey;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000278D File Offset: 0x0000098D
		public string StartedNotificationLocKey
		{
			get
			{
				return this._currentUISpecification.StartedNotificationLocKey;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000279A File Offset: 0x0000099A
		public string EndedNotificationLocKey
		{
			get
			{
				return this._currentUISpecification.EndedNotificationLocKey;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000027A7 File Offset: 0x000009A7
		public string InProgressClass
		{
			get
			{
				return this._currentUISpecification.InProgressClass;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000027B4 File Offset: 0x000009B4
		public string IconClass
		{
			get
			{
				return this._currentUISpecification.IconClass;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000027C1 File Offset: 0x000009C1
		public string NotificationBackgroundClass
		{
			get
			{
				return this._currentUISpecification.NotificationBackgroundClass;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027CE File Offset: 0x000009CE
		public void Load()
		{
			this._eventBus.Register(this);
			this.UpdateCurrentUISpecification();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000027E2 File Offset: 0x000009E2
		[OnEvent]
		public void OnHazardousWeatherSelected(HazardousWeatherSelectedEvent hazardousWeatherSelectedEvent)
		{
			this.UpdateCurrentUISpecification();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000027EC File Offset: 0x000009EC
		public void UpdateCurrentUISpecification()
		{
			IHazardousWeather currentCycleHazardousWeather = this._hazardousWeatherService.CurrentCycleHazardousWeather;
			IHazardousWeatherUISpecification currentUISpecification;
			if (!(currentCycleHazardousWeather is DroughtWeather))
			{
				if (!(currentCycleHazardousWeather is BadtideWeather))
				{
					string str = "No UI for weather: ";
					IHazardousWeather currentCycleHazardousWeather2 = this._hazardousWeatherService.CurrentCycleHazardousWeather;
					throw new InvalidOperationException(str + ((currentCycleHazardousWeather2 != null) ? currentCycleHazardousWeather2.ToString() : null));
				}
				currentUISpecification = this._badtideWeatherUISpecification;
			}
			else
			{
				currentUISpecification = this._droughtWeatherUISpecification;
			}
			this._currentUISpecification = currentUISpecification;
		}

		// Token: 0x04000024 RID: 36
		public readonly HazardousWeatherService _hazardousWeatherService;

		// Token: 0x04000025 RID: 37
		public readonly EventBus _eventBus;

		// Token: 0x04000026 RID: 38
		public readonly DroughtWeatherUISpecification _droughtWeatherUISpecification;

		// Token: 0x04000027 RID: 39
		public readonly BadtideWeatherUISpecification _badtideWeatherUISpecification;

		// Token: 0x04000028 RID: 40
		public IHazardousWeatherUISpecification _currentUISpecification;
	}
}
