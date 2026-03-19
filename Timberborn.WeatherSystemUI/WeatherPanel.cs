using System;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.GameCycleSystem;
using Timberborn.GameSound;
using Timberborn.HazardousWeatherSystemUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using Timberborn.UILayoutSystem;
using Timberborn.WeatherSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.WeatherSystemUI
{
	// Token: 0x0200000A RID: 10
	public class WeatherPanel : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000023E8 File Offset: 0x000005E8
		public WeatherPanel(UILayout uiLayout, EventBus eventBus, VisualElementLoader visualElementLoader, WeatherService weatherService, ILoc loc, GameUISoundController gameUISoundController, ITooltipRegistrar tooltipRegistrar, HazardousWeatherUIHelper hazardousWeatherUIHelper, HazardousWeatherApproachingTimer hazardousWeatherApproachingTimer, GameCycleService gameCycleService, ISpecService specService)
		{
			this._uiLayout = uiLayout;
			this._eventBus = eventBus;
			this._visualElementLoader = visualElementLoader;
			this._weatherService = weatherService;
			this._loc = loc;
			this._gameUISoundController = gameUISoundController;
			this._tooltipRegistrar = tooltipRegistrar;
			this._hazardousWeatherUIHelper = hazardousWeatherUIHelper;
			this._hazardousWeatherApproachingTimer = hazardousWeatherApproachingTimer;
			this._gameCycleService = gameCycleService;
			this._specService = specService;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000246C File Offset: 0x0000066C
		public void Load()
		{
			this._weatherPanelSpec = this._specService.GetSingleSpec<WeatherPanelSpec>();
			this._root = this._visualElementLoader.LoadVisualElement("Game/WeatherPanel");
			this._tooltipRegistrar.Register(this._root, () => this._tooltipText);
			this._simpleProgressBar = UQueryExtensions.Q<SimpleProgressBar>(this._root, "Progress", null);
			this._forecastCounter = UQueryExtensions.Q<Label>(this._root, "ForecastCounter", null);
			this._eventBus.Register(this);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024F7 File Offset: 0x000006F7
		public void UpdateSingleton()
		{
			if (this._pausedUntilTimeUnpaused && Time.deltaTime > 0f)
			{
				this._pausedUntilTimeUnpaused = false;
			}
			if (!this._pausedUntilTimeUnpaused)
			{
				this.UpdatePanel();
				if (this._startBlinkingIfUnpaused)
				{
					this._startBlinkingIfUnpaused = false;
					this.StartBlinking();
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002537 File Offset: 0x00000737
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddTopRight(this._root, 6);
			this.UpdatePanel();
			this._pausedUntilTimeUnpaused = true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002558 File Offset: 0x00000758
		[OnEvent]
		public void OnHazardousWeatherApproaching(HazardousWeatherApproachingEvent hazardousWeatherApproachingEvent)
		{
			this._startBlinkingIfUnpaused = true;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002564 File Offset: 0x00000764
		public void UpdatePanel()
		{
			int hazardousWeatherStartCycleDay = this._weatherService.HazardousWeatherStartCycleDay;
			float partialCycleDay = this._gameCycleService.PartialCycleDay;
			this.UpdateHazardousWeatherClasses();
			if (this._weatherService.IsHazardousWeather)
			{
				this.SetHazardousWeatherUI(partialCycleDay, hazardousWeatherStartCycleDay);
				return;
			}
			if (this._hazardousWeatherApproachingTimer.GetProgress() > 0f)
			{
				float approachingHazardUI = (float)hazardousWeatherStartCycleDay - partialCycleDay;
				this.SetApproachingHazardUI(approachingHazardUI);
				return;
			}
			this.SetPanelContent(this._loc.T(WeatherPanel.TemperateWeatherLocKey), 0f, 0f, false);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025E8 File Offset: 0x000007E8
		public void UpdateHazardousWeatherClasses()
		{
			this._root.RemoveFromClassList(WeatherPanel.ApproachingClass);
			this._root.RemoveFromClassList(WeatherPanel.InProgressClass);
			if (!string.IsNullOrEmpty(this._hazardousWeatherClass))
			{
				this._root.RemoveFromClassList(this._hazardousWeatherClass);
			}
			this._hazardousWeatherClass = this._hazardousWeatherUIHelper.InProgressClass;
			this._root.AddToClassList(this._hazardousWeatherClass);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002658 File Offset: 0x00000858
		public void SetHazardousWeatherUI(float partialCycleDay, int hazardousWeatherStartCycleDay)
		{
			float num = partialCycleDay - (float)hazardousWeatherStartCycleDay;
			float forecastCount = (float)this._weatherService.HazardousWeatherDuration - num;
			float progressBarValue = num / (float)this._weatherService.HazardousWeatherDuration;
			string inProgressLocKey = this._hazardousWeatherUIHelper.InProgressLocKey;
			this.SetPanelContent(this._loc.T(inProgressLocKey), progressBarValue, forecastCount, false);
			this._root.AddToClassList(WeatherPanel.InProgressClass);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026BC File Offset: 0x000008BC
		public void SetApproachingHazardUI(float daysToHazardousWeather)
		{
			this._root.AddToClassList(WeatherPanel.ApproachingClass);
			bool blink = this._remainingBlinks > 0 && this.NextBlinkingBarState();
			float progress = this._hazardousWeatherApproachingTimer.GetProgress();
			this.SetPanelContent(this._loc.T(this._hazardousWeatherUIHelper.ApproachingLocKey), progress, daysToHazardousWeather, blink);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002718 File Offset: 0x00000918
		public void SetPanelContent(string forecast, float progressBarValue, float forecastCount, bool blink = false)
		{
			this._simpleProgressBar.SetProgress(Math.Max(progressBarValue, 0f));
			this._root.EnableInClassList(WeatherPanel.BlinkingClass, blink);
			this._forecastCounter.ToggleDisplayStyle(forecastCount > 0f);
			this._forecastCounter.text = this._loc.T<float>(this._forecastCounterPhrase, forecastCount);
			this._tooltipText = forecast;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002784 File Offset: 0x00000984
		public void StartBlinking()
		{
			this._remainingBlinks = this._weatherPanelSpec.NumberOfBlinks * 2 - 1;
			this._midBlink = true;
			this._secondsToNextBlink = this._weatherPanelSpec.SecondsBetweenBlinks + Time.unscaledDeltaTime;
			this._gameUISoundController.PlayBlinkingSound();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027C4 File Offset: 0x000009C4
		public bool NextBlinkingBarState()
		{
			this._secondsToNextBlink -= Time.unscaledDeltaTime;
			if (this._secondsToNextBlink <= 0f)
			{
				this._secondsToNextBlink = this._weatherPanelSpec.SecondsBetweenBlinks;
				this._remainingBlinks--;
				this._midBlink = !this._midBlink;
			}
			return this._midBlink;
		}

		// Token: 0x0400001C RID: 28
		public static readonly string TemperateWeatherLocKey = "Weather.Temperate";

		// Token: 0x0400001D RID: 29
		public static readonly string BlinkingClass = "weather-panel--blink";

		// Token: 0x0400001E RID: 30
		public static readonly string ApproachingClass = "weather-approaching";

		// Token: 0x0400001F RID: 31
		public static readonly string InProgressClass = "weather-in-progress";

		// Token: 0x04000020 RID: 32
		public readonly UILayout _uiLayout;

		// Token: 0x04000021 RID: 33
		public readonly EventBus _eventBus;

		// Token: 0x04000022 RID: 34
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000023 RID: 35
		public readonly WeatherService _weatherService;

		// Token: 0x04000024 RID: 36
		public readonly ILoc _loc;

		// Token: 0x04000025 RID: 37
		public readonly GameUISoundController _gameUISoundController;

		// Token: 0x04000026 RID: 38
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000027 RID: 39
		public readonly HazardousWeatherUIHelper _hazardousWeatherUIHelper;

		// Token: 0x04000028 RID: 40
		public readonly HazardousWeatherApproachingTimer _hazardousWeatherApproachingTimer;

		// Token: 0x04000029 RID: 41
		public readonly GameCycleService _gameCycleService;

		// Token: 0x0400002A RID: 42
		public readonly ISpecService _specService;

		// Token: 0x0400002B RID: 43
		public WeatherPanelSpec _weatherPanelSpec;

		// Token: 0x0400002C RID: 44
		public VisualElement _root;

		// Token: 0x0400002D RID: 45
		public Label _forecastCounter;

		// Token: 0x0400002E RID: 46
		public SimpleProgressBar _simpleProgressBar;

		// Token: 0x0400002F RID: 47
		public float _secondsToNextBlink;

		// Token: 0x04000030 RID: 48
		public int _remainingBlinks;

		// Token: 0x04000031 RID: 49
		public bool _midBlink;

		// Token: 0x04000032 RID: 50
		public bool _pausedUntilTimeUnpaused;

		// Token: 0x04000033 RID: 51
		public bool _startBlinkingIfUnpaused;

		// Token: 0x04000034 RID: 52
		public string _tooltipText;

		// Token: 0x04000035 RID: 53
		public string _hazardousWeatherClass;

		// Token: 0x04000036 RID: 54
		public readonly Phrase _forecastCounterPhrase = Phrase.New().Format<float>(new Func<float, ILoc, string>(UnitFormatter.FormatDays));
	}
}
