using System;
using Timberborn.CoreUI;
using Timberborn.GameCycleSystem;
using Timberborn.HazardousWeatherSystemUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using Timberborn.UILayoutSystem;
using Timberborn.WeatherSystem;
using UnityEngine.UIElements;

namespace Timberborn.WeatherSystemUI
{
	// Token: 0x02000007 RID: 7
	public class DatePanel : ILoadableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public DatePanel(UILayout uiLayout, VisualElementLoader visualElementLoader, WeatherService weatherService, TimestampFormatter timestampFormatter, ILoc loc, ITooltipRegistrar tooltipRegistrar, EventBus eventBus, HazardousWeatherUIHelper hazardousWeatherUIHelper, GameCycleService gameCycleService)
		{
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._weatherService = weatherService;
			this._timestampFormatter = timestampFormatter;
			this._loc = loc;
			this._tooltipRegistrar = tooltipRegistrar;
			this._eventBus = eventBus;
			this._hazardousWeatherUIHelper = hazardousWeatherUIHelper;
			this._gameCycleService = gameCycleService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002158 File Offset: 0x00000358
		public void Load()
		{
			this._eventBus.Register(this);
			this._root = this._visualElementLoader.LoadVisualElement("Game/DatePanel");
			this._tooltipRegistrar.Register(this._root, () => this._tooltipText);
			this._text = UQueryExtensions.Q<Label>(this._root, "Text", null);
			this.UpdatePanel();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021C1 File Offset: 0x000003C1
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddTopRight(this._root, 5);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021D5 File Offset: 0x000003D5
		[OnEvent]
		public void OnDaytimeStart(DaytimeStartEvent daytimeStartEvent)
		{
			this.UpdatePanel();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021DD File Offset: 0x000003DD
		public void UpdatePanel()
		{
			this.UpdateIcon();
			this.UpdateText();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021EC File Offset: 0x000003EC
		public void UpdateIcon()
		{
			if (!string.IsNullOrEmpty(this._currentIconClass))
			{
				this._root.RemoveFromClassList(this._currentIconClass);
				this._currentIconClass = null;
			}
			if (this._weatherService.IsHazardousWeather)
			{
				this._currentIconClass = this._hazardousWeatherUIHelper.IconClass;
				this._root.AddToClassList(this._currentIconClass);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002250 File Offset: 0x00000450
		public void UpdateText()
		{
			this._text.text = this._timestampFormatter.FormatLongLocalized(this._gameCycleService.Cycle, this._gameCycleService.CycleDay);
			this._tooltipText = this._loc.T(this._weatherService.IsHazardousWeather ? this._hazardousWeatherUIHelper.NameLocKey : DatePanel.WeatherTemperateLocKey);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string WeatherTemperateLocKey = "Weather.Temperate";

		// Token: 0x04000009 RID: 9
		public readonly UILayout _uiLayout;

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000B RID: 11
		public readonly WeatherService _weatherService;

		// Token: 0x0400000C RID: 12
		public readonly TimestampFormatter _timestampFormatter;

		// Token: 0x0400000D RID: 13
		public readonly ILoc _loc;

		// Token: 0x0400000E RID: 14
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000F RID: 15
		public readonly EventBus _eventBus;

		// Token: 0x04000010 RID: 16
		public readonly HazardousWeatherUIHelper _hazardousWeatherUIHelper;

		// Token: 0x04000011 RID: 17
		public readonly GameCycleService _gameCycleService;

		// Token: 0x04000012 RID: 18
		public VisualElement _root;

		// Token: 0x04000013 RID: 19
		public Label _text;

		// Token: 0x04000014 RID: 20
		public string _tooltipText;

		// Token: 0x04000015 RID: 21
		public string _currentIconClass;
	}
}
