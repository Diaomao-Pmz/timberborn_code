using System;
using System.Collections.Generic;
using Timberborn.ActivatorSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.EntityPanelSystem;
using Timberborn.EntityUndoSystem;
using Timberborn.Localization;
using Timberborn.MapStateSystem;
using UnityEngine.UIElements;

namespace Timberborn.ActivatorSystemUI
{
	// Token: 0x0200000C RID: 12
	public class TimedComponentActivatorSettingsFragment : IEntityPanelFragment
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000258C File Offset: 0x0000078C
		public TimedComponentActivatorSettingsFragment(VisualElementLoader visualElementLoader, ILoc loc, TimedActivatorSettingFactory timedActivatorSettingFactory, MapEditorMode mapEditorMode, DevModeManager devModeManager, EntityChangeRecorderFactory entityChangeRecorderFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._timedActivatorSettingFactory = timedActivatorSettingFactory;
			this._mapEditorMode = mapEditorMode;
			this._devModeManager = devModeManager;
			this._entityChangeRecorderFactory = entityChangeRecorderFactory;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025CC File Offset: 0x000007CC
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/TimedComponentActivatorSettingsFragment");
			this._settingsRoot = UQueryExtensions.Q<VisualElement>(this._root, "TimedComponentActivatorSettings", null);
			this._isEnabledToggle = UQueryExtensions.Q<Toggle>(this._root, "IsEnabledToggle", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._isEnabledToggle, new EventCallback<ChangeEvent<bool>>(this.OnEnabledToggleStateChanged));
			this.AddSettings();
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002650 File Offset: 0x00000850
		public void ShowFragment(BaseComponent entity)
		{
			this._timedComponentActivator = entity.GetComponent<TimedComponentActivator>();
			if (this._timedComponentActivator && (this._mapEditorMode.IsMapEditor || this._devModeManager.Enabled))
			{
				this.UpdateSettings();
				this._root.ToggleDisplayStyle(true);
				this._isEnabledToggle.ToggleDisplayStyle(this._timedComponentActivator.IsOptional);
				this._isEnabledToggle.SetValueWithoutNotify(this._timedComponentActivator.IsEnabled);
				this._settingsRoot.ToggleDisplayStyle(this._timedComponentActivator.IsEnabled);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026E4 File Offset: 0x000008E4
		public void ClearFragment()
		{
			this._timedComponentActivator = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026FC File Offset: 0x000008FC
		public void UpdateFragment()
		{
			if (this._timedComponentActivator)
			{
				if (this._devModeManager.Enabled || this._mapEditorMode.IsMapEditor)
				{
					this._root.ToggleDisplayStyle(true);
					if (this._isEnabledToggle.value != this._timedComponentActivator.IsEnabled)
					{
						this._isEnabledToggle.SetValueWithoutNotify(this._timedComponentActivator.IsEnabled);
						this._settingsRoot.ToggleDisplayStyle(this._timedComponentActivator.IsEnabled);
					}
					this.UpdateSettings();
					return;
				}
				this._root.ToggleDisplayStyle(false);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002794 File Offset: 0x00000994
		public void OnEnabledToggleStateChanged(ChangeEvent<bool> evt)
		{
			if (this._timedComponentActivator && this._timedComponentActivator.IsOptional)
			{
				using (this._entityChangeRecorderFactory.CreateChangeRecorder(this._timedComponentActivator))
				{
					if (evt.newValue)
					{
						this._timedComponentActivator.EnableActivator();
					}
					else
					{
						this._timedComponentActivator.DisableActivator();
					}
				}
				this._settingsRoot.ToggleDisplayStyle(evt.newValue);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000281C File Offset: 0x00000A1C
		public void AddSettings()
		{
			this.AddSetting(this._loc.T(TimedComponentActivatorSettingsFragment.CyclesUntilCountdownActivationLocKey), new Action<float>(this.SetCyclesUntilCountdownActivation), () => (float)this._timedComponentActivator.CyclesUntilCountdownActivation, TimedComponentActivatorSettingsFragment.SettingsCyclesMinValue);
			this.AddSetting(this._loc.T(TimedComponentActivatorSettingsFragment.DaysUntilActivationLocKey), new Action<float>(this.SetDaysUntilActivation), () => this._timedComponentActivator.DaysUntilActivation, TimedComponentActivatorSettingsFragment.DaysCyclesMinValue);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002890 File Offset: 0x00000A90
		public void AddSetting(string label, Action<float> setter, Func<float> getter, float minValue)
		{
			TimedActivatorSetting timedActivatorSetting = this._timedActivatorSettingFactory.Create(label, setter, getter, minValue);
			this._timedActivatorSettings.Add(timedActivatorSetting);
			this._settingsRoot.Add(timedActivatorSetting.Root);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028CC File Offset: 0x00000ACC
		public void UpdateSettings()
		{
			foreach (TimedActivatorSetting timedActivatorSetting in this._timedActivatorSettings)
			{
				timedActivatorSetting.UpdateState();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000291C File Offset: 0x00000B1C
		public void SetCyclesUntilCountdownActivation(float value)
		{
			using (this._entityChangeRecorderFactory.CreateChangeRecorder(this._timedComponentActivator))
			{
				this._timedComponentActivator.SetCyclesUntilCountdownActivation((int)value);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002964 File Offset: 0x00000B64
		public void SetDaysUntilActivation(float value)
		{
			using (this._entityChangeRecorderFactory.CreateChangeRecorder(this._timedComponentActivator))
			{
				this._timedComponentActivator.SetDaysUntilActivation(value);
			}
		}

		// Token: 0x04000023 RID: 35
		public static readonly string CyclesUntilCountdownActivationLocKey = "TimedComponentActivator.CyclesUntilCountdownActivation";

		// Token: 0x04000024 RID: 36
		public static readonly string DaysUntilActivationLocKey = "TimedComponentActivator.DaysUntilActivationLoc";

		// Token: 0x04000025 RID: 37
		public static readonly float SettingsCyclesMinValue = 1f;

		// Token: 0x04000026 RID: 38
		public static readonly float DaysCyclesMinValue = 0f;

		// Token: 0x04000027 RID: 39
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000028 RID: 40
		public readonly ILoc _loc;

		// Token: 0x04000029 RID: 41
		public readonly TimedActivatorSettingFactory _timedActivatorSettingFactory;

		// Token: 0x0400002A RID: 42
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x0400002B RID: 43
		public readonly DevModeManager _devModeManager;

		// Token: 0x0400002C RID: 44
		public readonly EntityChangeRecorderFactory _entityChangeRecorderFactory;

		// Token: 0x0400002D RID: 45
		public VisualElement _root;

		// Token: 0x0400002E RID: 46
		public Toggle _isEnabledToggle;

		// Token: 0x0400002F RID: 47
		public VisualElement _settingsRoot;

		// Token: 0x04000030 RID: 48
		public TimedComponentActivator _timedComponentActivator;

		// Token: 0x04000031 RID: 49
		public readonly List<TimedActivatorSetting> _timedActivatorSettings = new List<TimedActivatorSetting>();
	}
}
