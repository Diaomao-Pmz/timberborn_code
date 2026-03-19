using System;
using System.Globalization;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using Timberborn.WaterBuildings;
using UnityEngine.UIElements;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x0200000C RID: 12
	public class SluiceFragment : IEntityPanelFragment
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002FD8 File Offset: 0x000011D8
		public SluiceFragment(VisualElementLoader visualElementLoader, SluiceToggleFactory sluiceToggleFactory, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._sluiceToggleFactory = sluiceToggleFactory;
			this._loc = loc;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002FF8 File Offset: 0x000011F8
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/SluiceFragment");
			this._modeToggle = this._sluiceToggleFactory.Create(UQueryExtensions.Q<VisualElement>(this._root, "ModeToggle", null));
			this._modeLabel = UQueryExtensions.Q<Label>(this._root, "Mode", null);
			this._waterLevelToggle = UQueryExtensions.Q<Toggle>(this._root, "WaterLevelToggle", null);
			this._waterLevelSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "WaterLevelSlider", null);
			this._aboveContaminationToggle = UQueryExtensions.Q<Toggle>(this._root, "AboveContaminationToggle", null);
			this._aboveContaminationSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "AboveContaminationSlider", null);
			this._belowContaminationToggle = UQueryExtensions.Q<Toggle>(this._root, "BelowContaminationToggle", null);
			this._belowContaminationSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "BelowContaminationSlider", null);
			this._synchronizeToggle = UQueryExtensions.Q<Toggle>(this._root, "Synchronize", null);
			this._depthLabel = UQueryExtensions.Q<Label>(this._root, "DepthLabel", null);
			this._contaminationLabel = UQueryExtensions.Q<Label>(this._root, "ContaminationLabel", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._waterLevelToggle, new EventCallback<ChangeEvent<bool>>(this.OnWaterLevelToggleChanged));
			this._waterLevelSlider.SetValueChangedCallback(new Action<float>(this.OnWaterLevelSliderChanged));
			this._waterLevelSlider.SetStepWithoutNotify(SluiceFragment.WaterLevelChangeStep);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._aboveContaminationToggle, new EventCallback<ChangeEvent<bool>>(this.OnAboveContaminationToggleChanged));
			this._aboveContaminationSlider.SetValueChangedCallback(new Action<float>(this.OnAboveContaminationSliderChanged));
			this._aboveContaminationSlider.SetStepWithoutNotify(SluiceFragment.ContaminationChangeStep);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._belowContaminationToggle, new EventCallback<ChangeEvent<bool>>(this.OnBelowContaminationToggleChanged));
			this._belowContaminationSlider.SetValueChangedCallback(new Action<float>(this.OnBelowContaminationSliderChanged));
			this._belowContaminationSlider.SetStepWithoutNotify(SluiceFragment.ContaminationChangeStep);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._synchronizeToggle, new EventCallback<ChangeEvent<bool>>(this.ToggleSynchronization));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000320C File Offset: 0x0000140C
		public void ShowFragment(BaseComponent entity)
		{
			this._sluice = entity.GetComponent<Sluice>();
			if (this._sluice)
			{
				this._sluiceState = this._sluice.GetComponent<SluiceState>();
				this._modeToggle.Show(this._sluiceState);
				this._sliderInitialization = true;
				this._waterLevelToggle.SetValueWithoutNotify(this._sluiceState.AutoCloseOnOutflow);
				this._waterLevelSlider.UpdateValuesWithoutNotify(this.WaterLevelSliderValue, (float)this.Range);
				this._aboveContaminationToggle.SetValueWithoutNotify(this._sluiceState.AutoCloseOnAbove);
				this._aboveContaminationSlider.UpdateValuesWithoutNotify(this._sluiceState.OnAboveLimit, 1f);
				this._belowContaminationToggle.SetValueWithoutNotify(this._sluiceState.AutoCloseOnBelow);
				this._belowContaminationSlider.UpdateValuesWithoutNotify(this._sluiceState.OnBelowLimit, 1f);
				this._sliderInitialization = false;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000032F5 File Offset: 0x000014F5
		public void ClearFragment()
		{
			this._sluice = null;
			this._sluiceState = null;
			this._modeToggle.Clear();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000331C File Offset: 0x0000151C
		public void UpdateFragment()
		{
			if (this._sluice)
			{
				this.UpdateModeToggle();
				this.UpdateAutomation();
				this._synchronizeToggle.SetValueWithoutNotify(this._sluiceState.IsSynchronized);
				this._depthLabel.text = this._loc.T<string>(SluiceFragment.DownstreamDepthLocKey, SluiceFragment.FormatValue(this._sluice.TargetDepth));
				this._contaminationLabel.text = this._loc.T<string>(SluiceFragment.ContaminationLocKey, NumberFormatter.FormatAsPercentRounded((double)this._sluice.Contamination));
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000033CB File Offset: 0x000015CB
		public int Range
		{
			get
			{
				return this._sluice.MaxHeight - this._sluice.MinHeight;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000033E4 File Offset: 0x000015E4
		public float WaterLevelSliderValue
		{
			get
			{
				return (float)this.Range + this._sluiceState.OutflowLimit;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000033F9 File Offset: 0x000015F9
		public void ToggleSynchronization(ChangeEvent<bool> evt)
		{
			this._sluiceState.ToggleSynchronization(evt.newValue);
			this._waterLevelSlider.SetValueWithoutNotify(this.WaterLevelSliderValue);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000341D File Offset: 0x0000161D
		public void OnWaterLevelToggleChanged(ChangeEvent<bool> evt)
		{
			if (evt.newValue)
			{
				this._sluiceState.EnableAutoCloseOnOutflow();
				return;
			}
			this._sluiceState.DisableAutoCloseOnOutflow();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000343E File Offset: 0x0000163E
		public void OnWaterLevelSliderChanged(float newValue)
		{
			if (!this._sliderInitialization)
			{
				this.ChangeFlow(newValue);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000344F File Offset: 0x0000164F
		public void OnAboveContaminationToggleChanged(ChangeEvent<bool> evt)
		{
			if (evt.newValue)
			{
				this._sluiceState.EnableAutoCloseOnAbove();
				return;
			}
			this._sluiceState.DisableAutoCloseOnAbove();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003470 File Offset: 0x00001670
		public void OnAboveContaminationSliderChanged(float newValue)
		{
			if (!this._sliderInitialization)
			{
				this.ChangeAboveContaminationLimit(newValue);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003481 File Offset: 0x00001681
		public void OnBelowContaminationToggleChanged(ChangeEvent<bool> evt)
		{
			if (evt.newValue)
			{
				this._sluiceState.EnableAutoCloseOnBelow();
				return;
			}
			this._sluiceState.DisableAutoCloseOnBelow();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000034A2 File Offset: 0x000016A2
		public void OnBelowContaminationSliderChanged(float newValue)
		{
			if (!this._sliderInitialization)
			{
				this.ChangeBelowContaminationLimit(newValue);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000034B4 File Offset: 0x000016B4
		public void UpdateModeToggle()
		{
			this._modeToggle.Update();
			if (this._sluiceState.AutoMode && this._sluice.IsOpen)
			{
				this._modeLabel.text = this._loc.T(SluiceFragment.AutoOpenKey);
				return;
			}
			if (this._sluiceState.AutoMode && !this._sluice.IsOpen)
			{
				this._modeLabel.text = this._loc.T(SluiceFragment.AutoClosedLocKey);
				return;
			}
			if (!this._sluiceState.AutoMode && this._sluiceState.IsOpen)
			{
				this._modeLabel.text = this._loc.T(SluiceFragment.OpenLocKey);
				return;
			}
			this._modeLabel.text = this._loc.T(SluiceFragment.ClosedLocKey);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000358C File Offset: 0x0000178C
		public void UpdateAutomation()
		{
			this._waterLevelSlider.UpdateValuesWithoutNotify(this.WaterLevelSliderValue, (float)this.Range);
			this._waterLevelToggle.SetValueWithoutNotify(this._sluiceState.AutoCloseOnOutflow);
			this._waterLevelToggle.text = this._loc.T<string>(SluiceFragment.MaximumDepthToggleLocKey, SluiceFragment.FormatValue(this._waterLevelSlider.Value));
			this._aboveContaminationSlider.SetValueWithoutNotify(this._sluiceState.OnAboveLimit);
			this._aboveContaminationToggle.SetValueWithoutNotify(this._sluiceState.AutoCloseOnAbove);
			this._aboveContaminationToggle.text = this._loc.T<string>(SluiceFragment.AboveContaminationToggleLocKey, NumberFormatter.FormatAsPercentRounded((double)this._aboveContaminationSlider.Value));
			this._belowContaminationSlider.SetValueWithoutNotify(this._sluiceState.OnBelowLimit);
			this._belowContaminationToggle.SetValueWithoutNotify(this._sluiceState.AutoCloseOnBelow);
			this._belowContaminationToggle.text = this._loc.T<string>(SluiceFragment.BelowContaminationToggleLocKey, NumberFormatter.FormatAsPercentRounded((double)this._belowContaminationSlider.Value));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000036A2 File Offset: 0x000018A2
		public void ChangeFlow(float newHeight)
		{
			if (this.WaterLevelSliderValue != newHeight)
			{
				this._sluiceState.SetOutflowLimit(newHeight - (float)this.Range);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000036C1 File Offset: 0x000018C1
		public void ChangeAboveContaminationLimit(float newValue)
		{
			if (this._sluiceState.OnAboveLimit != newValue)
			{
				this._sluiceState.SetAboveContaminationLimit(newValue);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000036DD File Offset: 0x000018DD
		public void ChangeBelowContaminationLimit(float newValue)
		{
			if (this._sluiceState.OnBelowLimit != newValue)
			{
				this._sluiceState.SetBelowContaminationLimit(newValue);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000036F9 File Offset: 0x000018F9
		public static string FormatValue(float value)
		{
			return value.ToString("F2", CultureInfo.InvariantCulture);
		}

		// Token: 0x04000040 RID: 64
		public static readonly float WaterLevelChangeStep = 0.05f;

		// Token: 0x04000041 RID: 65
		public static readonly float ContaminationChangeStep = 0.01f;

		// Token: 0x04000042 RID: 66
		public static readonly string AutoClosedLocKey = "Building.Sluice.Mode.AutoClosed";

		// Token: 0x04000043 RID: 67
		public static readonly string AutoOpenKey = "Building.Sluice.Mode.AutoOpen";

		// Token: 0x04000044 RID: 68
		public static readonly string ClosedLocKey = "Building.Sluice.Mode.Closed";

		// Token: 0x04000045 RID: 69
		public static readonly string OpenLocKey = "Building.Sluice.Mode.Open";

		// Token: 0x04000046 RID: 70
		public static readonly string MaximumDepthToggleLocKey = "Building.Sluice.MaximumDepthToggle";

		// Token: 0x04000047 RID: 71
		public static readonly string AboveContaminationToggleLocKey = "Building.Sluice.AboveContaminationToggle";

		// Token: 0x04000048 RID: 72
		public static readonly string BelowContaminationToggleLocKey = "Building.Sluice.BelowContaminationToggle";

		// Token: 0x04000049 RID: 73
		public static readonly string DownstreamDepthLocKey = "Building.Sluice.DownstreamDepth";

		// Token: 0x0400004A RID: 74
		public static readonly string ContaminationLocKey = "Building.Sluice.Contamination";

		// Token: 0x0400004B RID: 75
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400004C RID: 76
		public readonly SluiceToggleFactory _sluiceToggleFactory;

		// Token: 0x0400004D RID: 77
		public readonly ILoc _loc;

		// Token: 0x0400004E RID: 78
		public VisualElement _root;

		// Token: 0x0400004F RID: 79
		public SluiceToggle _modeToggle;

		// Token: 0x04000050 RID: 80
		public Label _modeLabel;

		// Token: 0x04000051 RID: 81
		public Toggle _waterLevelToggle;

		// Token: 0x04000052 RID: 82
		public PreciseSlider _waterLevelSlider;

		// Token: 0x04000053 RID: 83
		public Toggle _aboveContaminationToggle;

		// Token: 0x04000054 RID: 84
		public PreciseSlider _aboveContaminationSlider;

		// Token: 0x04000055 RID: 85
		public Toggle _belowContaminationToggle;

		// Token: 0x04000056 RID: 86
		public PreciseSlider _belowContaminationSlider;

		// Token: 0x04000057 RID: 87
		public Toggle _synchronizeToggle;

		// Token: 0x04000058 RID: 88
		public Label _depthLabel;

		// Token: 0x04000059 RID: 89
		public Label _contaminationLabel;

		// Token: 0x0400005A RID: 90
		public Sluice _sluice;

		// Token: 0x0400005B RID: 91
		public SluiceState _sluiceState;

		// Token: 0x0400005C RID: 92
		public bool _sliderInitialization;
	}
}
