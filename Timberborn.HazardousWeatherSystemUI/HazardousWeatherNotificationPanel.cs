using System;
using Timberborn.BlueprintSystem;
using Timberborn.CameraSystem;
using Timberborn.CoreUI;
using Timberborn.GameCycleSystem;
using Timberborn.HazardousWeatherSystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using Timberborn.WeatherSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.HazardousWeatherSystemUI
{
	// Token: 0x0200000B RID: 11
	public class HazardousWeatherNotificationPanel : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002280 File Offset: 0x00000480
		public HazardousWeatherNotificationPanel(ILoc loc, EventBus eventBus, HazardousWeatherUIHelper hazardousWeatherUIHelper, UILayout uiLayout, PanelStack panelStack, VisualElementLoader visualElementLoader, WeatherService weatherService, CameraHorizontalShifter cameraHorizontalShifter, ISpecService specService)
		{
			this._loc = loc;
			this._eventBus = eventBus;
			this._hazardousWeatherUIHelper = hazardousWeatherUIHelper;
			this._uiLayout = uiLayout;
			this._panelStack = panelStack;
			this._visualElementLoader = visualElementLoader;
			this._weatherService = weatherService;
			this._cameraHorizontalShifter = cameraHorizontalShifter;
			this._specService = specService;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022D8 File Offset: 0x000004D8
		public void Load()
		{
			this._spec = this._specService.GetSingleSpec<HazardousWeatherUISpec>();
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/HazardousWeatherNotificationPanel");
			this._panel = UQueryExtensions.Q<VisualElement>(visualElement, "HazardousWeatherNotificationPanel", null);
			this._panel.ToggleDisplayStyle(false);
			this._panel.RegisterCallback<TransitionEndEvent>(new EventCallback<TransitionEndEvent>(this.OnTransitionEnd), 0);
			this._panel.RegisterCallback<TransitionCancelEvent>(new EventCallback<TransitionCancelEvent>(this.OnTransitionCancel), 0);
			this._header = UQueryExtensions.Q<Label>(visualElement, "Header", null);
			this._description = UQueryExtensions.Q<Label>(visualElement, "Description", null);
			this._background = UQueryExtensions.Q<Image>(visualElement, "Background", null);
			this._uiLayout.AddAbsoluteItem(visualElement);
			this._eventBus.Register(this);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023A4 File Offset: 0x000005A4
		public void UpdateSingleton()
		{
			if (this._panel.IsDisplayed())
			{
				this.UpdateTimer();
				this.UpdatePanelPosition();
			}
			if (this._showApproachingNotificationIfUnpaused && Time.timeScale != 0f)
			{
				this._showApproachingNotificationIfUnpaused = false;
				this.ShowHazardousSeasonNotification(this._loc.T(this._hazardousWeatherUIHelper.ApproachingLocKey));
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002401 File Offset: 0x00000601
		[OnEvent]
		public void OnHazardousWeatherStarted(HazardousWeatherStartedEvent hazardousWeatherStartedEvent)
		{
			this.ShowHazardousSeasonNotification(this._loc.T(this._hazardousWeatherUIHelper.StartedNotificationLocKey));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000241F File Offset: 0x0000061F
		[OnEvent]
		public void OnCycleEndedEvent(CycleEndedEvent cycleEndedEvent)
		{
			this.ShowTemperateSeasonNotification(cycleEndedEvent.Cycle + 1);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000242F File Offset: 0x0000062F
		[OnEvent]
		public void OnHazardousWeatherApproaching(HazardousWeatherApproachingEvent hazardousWeatherApproachingEvent)
		{
			this._showApproachingNotificationIfUnpaused = true;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002438 File Offset: 0x00000638
		[OnEvent]
		public void OnPanelShown(PanelShownEvent panelShownEvent)
		{
			this.UpdateTimerBlocker();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002438 File Offset: 0x00000638
		[OnEvent]
		public void OnPanelHidden(PanelHiddenEvent panelHiddenEvent)
		{
			this.UpdateTimerBlocker();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002440 File Offset: 0x00000640
		public void OnTransitionEnd(TransitionEndEvent evt)
		{
			if (this._panel.style.opacity == 0f)
			{
				this._panel.ToggleDisplayStyle(false);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000246F File Offset: 0x0000066F
		public void OnTransitionCancel(TransitionCancelEvent evt)
		{
			this._panel.ToggleDisplayStyle(false);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000247D File Offset: 0x0000067D
		public void UpdateTimerBlocker()
		{
			this._isTimerBlockerActive = this._panelStack.ContainsPanelBlocker();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002490 File Offset: 0x00000690
		public void UpdateTimer()
		{
			if (!this._isTimerBlockerActive)
			{
				this._notificationTimer += Time.unscaledDeltaTime;
				if (this._notificationTimer > this._spec.NotificationDuration)
				{
					this.SetPanelFade(false);
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024C6 File Offset: 0x000006C6
		public void UpdatePanelPosition()
		{
			this._panel.style.translate = new Translate(this._cameraHorizontalShifter.CurrentOffset * HazardousWeatherNotificationPanel.CameraShiftScale, 0f);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002502 File Offset: 0x00000702
		public void ShowHazardousSeasonNotification(string seasonText)
		{
			this.ShowNotification(seasonText, null, true);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002510 File Offset: 0x00000710
		public void ShowTemperateSeasonNotification(int beginningCycle)
		{
			string text = this._loc.T<int>(HazardousWeatherNotificationPanel.CycleBeginsKey, beginningCycle);
			if (this._weatherService.HazardousWeatherDuration > 0)
			{
				this.ShowNotification(this._loc.T(this._hazardousWeatherUIHelper.EndedNotificationLocKey), text, false);
				return;
			}
			this.ShowNotification(text, null, false);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002568 File Offset: 0x00000768
		public void ShowNotification(string headerText, string descriptionText, bool isHazardous)
		{
			this._header.text = headerText;
			this._description.text = descriptionText;
			this._description.ToggleDisplayStyle(!string.IsNullOrEmpty(descriptionText));
			if (isHazardous)
			{
				this._background.AddToClassList(this._hazardousWeatherUIHelper.NotificationBackgroundClass);
				this._background.RemoveFromClassList(HazardousWeatherNotificationPanel.WetWeatherClass);
			}
			else
			{
				this._background.RemoveFromClassList(this._hazardousWeatherUIHelper.NotificationBackgroundClass);
				this._background.AddToClassList(HazardousWeatherNotificationPanel.WetWeatherClass);
			}
			this._panel.ToggleDisplayStyle(true);
			this._notificationTimer = 0f;
			this.SetPanelFade(true);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002610 File Offset: 0x00000810
		public void SetPanelFade(bool fadeEnabled)
		{
			this._panel.EnableInClassList(HazardousWeatherNotificationPanel.FadeClass, fadeEnabled);
		}

		// Token: 0x0400000D RID: 13
		public static readonly float CameraShiftScale = -650f;

		// Token: 0x0400000E RID: 14
		public static readonly string FadeClass = "hazardous-weather-notification__fade--enabled";

		// Token: 0x0400000F RID: 15
		public static readonly string WetWeatherClass = "hazardous-weather-notification__background--wet";

		// Token: 0x04000010 RID: 16
		public static readonly string CycleBeginsKey = "Weather.CycleBegins";

		// Token: 0x04000011 RID: 17
		public readonly ILoc _loc;

		// Token: 0x04000012 RID: 18
		public readonly EventBus _eventBus;

		// Token: 0x04000013 RID: 19
		public readonly HazardousWeatherUIHelper _hazardousWeatherUIHelper;

		// Token: 0x04000014 RID: 20
		public readonly UILayout _uiLayout;

		// Token: 0x04000015 RID: 21
		public readonly PanelStack _panelStack;

		// Token: 0x04000016 RID: 22
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000017 RID: 23
		public readonly WeatherService _weatherService;

		// Token: 0x04000018 RID: 24
		public readonly CameraHorizontalShifter _cameraHorizontalShifter;

		// Token: 0x04000019 RID: 25
		public readonly ISpecService _specService;

		// Token: 0x0400001A RID: 26
		public HazardousWeatherUISpec _spec;

		// Token: 0x0400001B RID: 27
		public VisualElement _panel;

		// Token: 0x0400001C RID: 28
		public Image _background;

		// Token: 0x0400001D RID: 29
		public Label _header;

		// Token: 0x0400001E RID: 30
		public Label _description;

		// Token: 0x0400001F RID: 31
		public float _notificationTimer;

		// Token: 0x04000020 RID: 32
		public bool _showApproachingNotificationIfUnpaused;

		// Token: 0x04000021 RID: 33
		public bool _isTimerBlockerActive;
	}
}
