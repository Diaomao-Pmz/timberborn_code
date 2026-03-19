using System;

namespace Timberborn.MapEditorHazardousWeatherUI
{
	// Token: 0x02000006 RID: 6
	public class MapEditorHazardousWeatherSetter
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022CA File Offset: 0x000004CA
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022D2 File Offset: 0x000004D2
		public bool IsDroughtWeather { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022DB File Offset: 0x000004DB
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000022E3 File Offset: 0x000004E3
		public bool IsBadtideWeather { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022EC File Offset: 0x000004EC
		public bool IsTemperateWeather
		{
			get
			{
				return !this.IsDroughtWeather && !this.IsBadtideWeather;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002301 File Offset: 0x00000501
		public void SetTemperateWeather()
		{
			this.IsDroughtWeather = false;
			this.IsBadtideWeather = false;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002311 File Offset: 0x00000511
		public void SetDroughtWeather()
		{
			this.IsDroughtWeather = true;
			this.IsBadtideWeather = false;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002321 File Offset: 0x00000521
		public void SetBadtideWeather()
		{
			this.IsDroughtWeather = false;
			this.IsBadtideWeather = true;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002331 File Offset: 0x00000531
		public string GetCurrentHazardousWeatherID()
		{
			if (this.IsBadtideWeather)
			{
				return MapEditorHazardousWeatherSetter.BadtideWeather;
			}
			if (!this.IsDroughtWeather)
			{
				return null;
			}
			return MapEditorHazardousWeatherSetter.DraughtWeather;
		}

		// Token: 0x04000013 RID: 19
		public static readonly string BadtideWeather = "BadtideWeather";

		// Token: 0x04000014 RID: 20
		public static readonly string DraughtWeather = "DraughtWeather";
	}
}
