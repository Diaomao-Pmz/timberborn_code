using System;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000009 RID: 9
	public class ChronometerFragment : IEntityPanelFragment
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000242C File Offset: 0x0000062C
		public ChronometerFragment(VisualElementLoader visualElementLoader, ILoc loc, RadioToggleFactory radioToggleFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc2;
			this._radioToggleFactory = radioToggleFactory;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002484 File Offset: 0x00000684
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/ChronometerFragment");
			this._modeRadioToggle = this._radioToggleFactory.CreateLocalizable<ChronometerMode>(ChronometerFragment.ModeLocKeyPrefix, UQueryExtensions.Q<VisualElement>(this._root, "Mode", null));
			this._modeRadioToggle.RadioButtonSelected += this.OnModeChanged;
			this._timeRangeControls = UQueryExtensions.Q<VisualElement>(this._root, "TimeRangeControls", null);
			this._startLabel = UQueryExtensions.Q<Label>(this._root, "StartLabel", null);
			this._endLabel = UQueryExtensions.Q<Label>(this._root, "EndLabel", null);
			this._startSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "StartSlider", null);
			this._startSlider.SetValueChangedCallback(new Action<float>(this.SetStartTime));
			this._startSlider.SetStepWithoutNotify(ChronometerFragment.TimeChangeStep);
			this._endSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "EndSlider", null);
			this._endSlider.SetValueChangedCallback(new Action<float>(this.SetEndTime));
			this._endSlider.SetStepWithoutNotify(ChronometerFragment.TimeChangeStep);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000025B8 File Offset: 0x000007B8
		public void ShowFragment(BaseComponent entity)
		{
			if (entity.TryGetComponent<Chronometer>(out this._chronometer))
			{
				this._root.ToggleDisplayStyle(true);
				this._startSlider.UpdateValuesWithoutNotify(this._chronometer.StartTime, 24f);
				this.UpdateStartLabel();
				this._endSlider.UpdateValuesWithoutNotify(this._chronometer.EndTime, 24f);
				this.UpdateEndLabel();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002621 File Offset: 0x00000821
		public void ClearFragment()
		{
			this._chronometer = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002638 File Offset: 0x00000838
		public void UpdateFragment()
		{
			if (this._chronometer != null)
			{
				this._modeRadioToggle.Update((int)this._chronometer.Mode);
				this._timeRangeControls.ToggleDisplayStyle(this._chronometer.Mode == ChronometerMode.TimeRange);
				this._startSlider.SetMarker(this._chronometer.SampledTime);
				this._endSlider.SetMarker(this._chronometer.SampledTime);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026A8 File Offset: 0x000008A8
		public void OnModeChanged(object sender, int index)
		{
			this._chronometer.SetMode((ChronometerMode)index);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000026B8 File Offset: 0x000008B8
		public void SetStartTime(float value)
		{
			float startTime = ChronometerFragment.ClampTime(value);
			this._chronometer.SetStartTime(startTime);
			this.UpdateStartLabel();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026DE File Offset: 0x000008DE
		public void UpdateStartLabel()
		{
			this._startLabel.text = this._loc.T<string>(ChronometerFragment.StartTimeLocKey, this.GetHoursText(this._chronometer.StartTime));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000270C File Offset: 0x0000090C
		public void SetEndTime(float value)
		{
			float endTime = ChronometerFragment.ClampTime(value);
			this._chronometer.SetEndTime(endTime);
			this.UpdateEndLabel();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002732 File Offset: 0x00000932
		public void UpdateEndLabel()
		{
			this._endLabel.text = this._loc.T<string>(ChronometerFragment.EndTimeLocKey, this.GetHoursText(this._chronometer.EndTime));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002760 File Offset: 0x00000960
		public string GetHoursText(float value)
		{
			return this._loc.T<float>(this._hoursShortPhrase, value);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002774 File Offset: 0x00000974
		public static float ClampTime(float time)
		{
			return Math.Clamp(time, 0f, 24f);
		}

		// Token: 0x04000018 RID: 24
		public static readonly string StartTimeLocKey = "Building.Chronometer.StartTime";

		// Token: 0x04000019 RID: 25
		public static readonly string EndTimeLocKey = "Building.Chronometer.EndTime";

		// Token: 0x0400001A RID: 26
		public static readonly string ModeLocKeyPrefix = "Building.Chronometer.Mode.";

		// Token: 0x0400001B RID: 27
		public static readonly float TimeChangeStep = 0.25f;

		// Token: 0x0400001C RID: 28
		public readonly Phrase _hoursShortPhrase = Phrase.New().Format<float>((float value, ILoc loc) => UnitFormatter.FormatHours(string.Format("{0:F2}", value), loc));

		// Token: 0x0400001D RID: 29
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001E RID: 30
		public readonly ILoc _loc;

		// Token: 0x0400001F RID: 31
		public readonly RadioToggleFactory _radioToggleFactory;

		// Token: 0x04000020 RID: 32
		public VisualElement _root;

		// Token: 0x04000021 RID: 33
		public RadioToggle _modeRadioToggle;

		// Token: 0x04000022 RID: 34
		public VisualElement _timeRangeControls;

		// Token: 0x04000023 RID: 35
		public Label _startLabel;

		// Token: 0x04000024 RID: 36
		public Label _endLabel;

		// Token: 0x04000025 RID: 37
		public PreciseSlider _startSlider;

		// Token: 0x04000026 RID: 38
		public PreciseSlider _endSlider;

		// Token: 0x04000027 RID: 39
		public Chronometer _chronometer;
	}
}
