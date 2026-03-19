using System;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorHazardousWeatherUI
{
	// Token: 0x02000004 RID: 4
	public class HazardousWeatherToggleFactory
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public HazardousWeatherToggleFactory(SliderToggleFactory sliderToggleFactory, ILoc loc, MapEditorHazardousWeatherSetter mapEditorHazardousWeatherSetter)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
			this._mapEditorHazardousWeatherSetter = mapEditorHazardousWeatherSetter;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DC File Offset: 0x000002DC
		public SliderToggle Create(VisualElement parent)
		{
			SliderToggleItem sliderToggleItem = SliderToggleItem.Create(() => this._loc.T(HazardousWeatherToggleFactory.TemperateWeatherLocKey), HazardousWeatherToggleFactory.TemperateWeatherClass, delegate()
			{
				this._mapEditorHazardousWeatherSetter.SetTemperateWeather();
			}, () => this._mapEditorHazardousWeatherSetter.IsTemperateWeather);
			SliderToggleItem sliderToggleItem2 = SliderToggleItem.Create(() => this._loc.T(HazardousWeatherToggleFactory.DroughtWeatherLocKey), HazardousWeatherToggleFactory.DroughtWeatherClass, delegate()
			{
				this._mapEditorHazardousWeatherSetter.SetDroughtWeather();
			}, () => this._mapEditorHazardousWeatherSetter.IsDroughtWeather);
			SliderToggleItem sliderToggleItem3 = SliderToggleItem.Create(() => this._loc.T(HazardousWeatherToggleFactory.BadtideWeatherLocKey), HazardousWeatherToggleFactory.BadtideWeatherClass, delegate()
			{
				this._mapEditorHazardousWeatherSetter.SetBadtideWeather();
			}, () => this._mapEditorHazardousWeatherSetter.IsBadtideWeather);
			return this._sliderToggleFactory.Create(parent, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem2,
				sliderToggleItem3
			});
		}

		// Token: 0x04000006 RID: 6
		public static readonly string TemperateWeatherClass = "hazardous-weather-toggle__icon--temperate";

		// Token: 0x04000007 RID: 7
		public static readonly string DroughtWeatherClass = "hazardous-weather-toggle__icon--drought";

		// Token: 0x04000008 RID: 8
		public static readonly string BadtideWeatherClass = "hazardous-weather-toggle__icon--badtide";

		// Token: 0x04000009 RID: 9
		public static readonly string TemperateWeatherLocKey = "Weather.Temperate";

		// Token: 0x0400000A RID: 10
		public static readonly string DroughtWeatherLocKey = "Weather.Drought";

		// Token: 0x0400000B RID: 11
		public static readonly string BadtideWeatherLocKey = "Weather.Badtide";

		// Token: 0x0400000C RID: 12
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x0400000D RID: 13
		public readonly ILoc _loc;

		// Token: 0x0400000E RID: 14
		public readonly MapEditorHazardousWeatherSetter _mapEditorHazardousWeatherSetter;
	}
}
