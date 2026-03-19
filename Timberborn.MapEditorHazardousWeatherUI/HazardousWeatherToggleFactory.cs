using System;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorHazardousWeatherUI
{
	// Token: 0x02000003 RID: 3
	internal class HazardousWeatherToggleFactory
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

		// Token: 0x04000001 RID: 1
		private static readonly string TemperateWeatherClass = "hazardous-weather-toggle__icon--temperate";

		// Token: 0x04000002 RID: 2
		private static readonly string DroughtWeatherClass = "hazardous-weather-toggle__icon--drought";

		// Token: 0x04000003 RID: 3
		private static readonly string BadtideWeatherClass = "hazardous-weather-toggle__icon--badtide";

		// Token: 0x04000004 RID: 4
		private static readonly string TemperateWeatherLocKey = "Weather.Temperate";

		// Token: 0x04000005 RID: 5
		private static readonly string DroughtWeatherLocKey = "Weather.Drought";

		// Token: 0x04000006 RID: 6
		private static readonly string BadtideWeatherLocKey = "Weather.Badtide";

		// Token: 0x04000007 RID: 7
		private readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x04000008 RID: 8
		private readonly ILoc _loc;

		// Token: 0x04000009 RID: 9
		private readonly MapEditorHazardousWeatherSetter _mapEditorHazardousWeatherSetter;
	}
}
