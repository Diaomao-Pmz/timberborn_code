using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using Timberborn.WaterBuildings;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000007 RID: 7
	public class FillValveFragment : IEntityPanelFragment
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public FillValveFragment(VisualElementLoader visualElementLoader, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002164 File Offset: 0x00000364
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/FillValveFragment");
			this._targetHeightLabel = UQueryExtensions.Q<Label>(this._root, "TargetHeightLabel", null);
			this._targetHeightStateLabel = UQueryExtensions.Q<Label>(this._root, "TargetHeightStateLabel", null);
			this._targetHeightSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "TargetHeightSlider", null);
			this._targetHeightSlider.SetValueChangedCallback(new Action<float>(this.SetTargetHeight));
			this._automationTargetHeightWrapper = UQueryExtensions.Q<VisualElement>(this._root, "AutomationTargetHeightWrapper", null);
			this._automationTargetHeightLabel = UQueryExtensions.Q<Label>(this._root, "AutomationTargetHeightLabel", null);
			this._automationTargetHeightStateLabel = UQueryExtensions.Q<Label>(this._root, "AutomationTargetHeightStateLabel", null);
			this._automationTargetHeightSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "AutomationTargetHeightSlider", null);
			this._automationTargetHeightSlider.SetValueChangedCallback(new Action<float>(this.SetAutomationTargetHeight));
			this._synchronizeToggle = UQueryExtensions.Q<Toggle>(this._root, "Synchronize", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._synchronizeToggle, new EventCallback<ChangeEvent<bool>>(this.ToggleSynchronization));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002298 File Offset: 0x00000498
		public void ShowFragment(BaseComponent entity)
		{
			this._fillValve = entity.GetComponent<FillValve>();
			if (this._fillValve)
			{
				this._root.ToggleDisplayStyle(true);
				this._targetHeightSlider.SetStepWithoutNotify(FillValveFragment.TargetHeightStep);
				this._automationTargetHeightSlider.SetStepWithoutNotify(FillValveFragment.TargetHeightStep);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022EA File Offset: 0x000004EA
		public void ClearFragment()
		{
			this._fillValve = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022FF File Offset: 0x000004FF
		public void UpdateFragment()
		{
			if (this._fillValve)
			{
				this.UpdateOutflowLimit();
				this.UpdateAutomationOutflowLimit();
				this.UpdateMarkers();
				this.UpdateSynchronizeToggle();
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002326 File Offset: 0x00000526
		public float TargetHeightSliderMinValue
		{
			get
			{
				return (float)this._fillValve.MinTargetHeight;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002334 File Offset: 0x00000534
		public float TargetHeightSliderMaxValue
		{
			get
			{
				return (float)this._fillValve.MaxTargetHeight + FillValveFragment.TargetHeightStep;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002348 File Offset: 0x00000548
		public void UpdateOutflowLimit()
		{
			this._targetHeightSlider.UpdateValuesWithoutNotify(this._fillValve.TargetHeightEnabled ? Mathf.Clamp(this._fillValve.ClampedTargetHeight, (float)this._fillValve.MinTargetHeight, (float)this._fillValve.MaxTargetHeight) : this.TargetHeightSliderMaxValue, this.TargetHeightSliderMinValue, this.TargetHeightSliderMaxValue);
			this._targetHeightLabel.text = (this._fillValve.TargetHeightEnabled ? this._loc.T<float>(this._targetHeightPhrase, this._fillValve.TargetDepth) : this._loc.T(FillValveFragment.TargetHeightUnlimitedLocKey));
			this._targetHeightStateLabel.ToggleDisplayStyle(this._fillValve.IsAutomated);
			if (this._fillValve.IsAutomated)
			{
				this._targetHeightStateLabel.EnableInClassList(FillValveFragment.ActiveStateLabelClass, !this._fillValve.IsInputOn);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002430 File Offset: 0x00000630
		public void UpdateAutomationOutflowLimit()
		{
			this._automationTargetHeightWrapper.ToggleDisplayStyle(this._fillValve.IsAutomated);
			if (this._fillValve.IsAutomated)
			{
				this._automationTargetHeightSlider.UpdateValuesWithoutNotify(this._fillValve.AutomationTargetHeightEnabled ? Mathf.Clamp(this._fillValve.ClampedAutomationTargetHeight, (float)this._fillValve.MinTargetHeight, (float)this._fillValve.MaxTargetHeight) : this.TargetHeightSliderMaxValue, this.TargetHeightSliderMinValue, this.TargetHeightSliderMaxValue);
				this._automationTargetHeightLabel.text = (this._fillValve.AutomationTargetHeightEnabled ? this._loc.T<float>(this._automationTargetHeightPhrase, this._fillValve.AutomationTargetDepth) : this._loc.T(FillValveFragment.TargetHeightUnlimitedLocKey));
				this._automationTargetHeightStateLabel.EnableInClassList(FillValveFragment.ActiveStateLabelClass, this._fillValve.IsInputOn);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002518 File Offset: 0x00000718
		public void UpdateMarkers()
		{
			float actualHeight = this._fillValve.ActualHeight;
			this._targetHeightSlider.SetMarker(actualHeight);
			if (this._fillValve.IsAutomated)
			{
				this._automationTargetHeightSlider.SetMarker(actualHeight);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002556 File Offset: 0x00000756
		public void UpdateSynchronizeToggle()
		{
			this._synchronizeToggle.SetValueWithoutNotify(this._fillValve.IsSynchronized);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002570 File Offset: 0x00000770
		public void SetTargetHeight(float value)
		{
			if (value > (float)this._fillValve.MaxTargetHeight)
			{
				this._fillValve.SetTargetHeightEnabledAndSynchronize(false);
				this._fillValve.SetTargetHeightAndSynchronize((float)this._fillValve.MaxTargetHeight);
				return;
			}
			this._fillValve.SetTargetHeightEnabledAndSynchronize(true);
			this._fillValve.SetTargetHeightAndSynchronize(value);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025C8 File Offset: 0x000007C8
		public void SetAutomationTargetHeight(float value)
		{
			if (value > (float)this._fillValve.MaxTargetHeight)
			{
				this._fillValve.SetAutomationTargetHeightEnabledAndSynchronize(false);
				this._fillValve.SetAutomationTargetHeightAndSynchronize((float)this._fillValve.MaxTargetHeight);
				return;
			}
			this._fillValve.SetAutomationTargetHeightEnabledAndSynchronize(true);
			this._fillValve.SetAutomationTargetHeightAndSynchronize(value);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002620 File Offset: 0x00000820
		public void ToggleSynchronization(ChangeEvent<bool> changeEvent)
		{
			this._fillValve.ToggleSynchronization(changeEvent.newValue);
		}

		// Token: 0x04000008 RID: 8
		public static readonly float TargetHeightStep = 0.05f;

		// Token: 0x04000009 RID: 9
		public static readonly string TargetHeightUnlimitedLocKey = "Building.FillValve.TargetHeightUnlimited";

		// Token: 0x0400000A RID: 10
		public static readonly string ActiveStateLabelClass = "entity-panel__text--highlight-white";

		// Token: 0x0400000B RID: 11
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000C RID: 12
		public readonly ILoc _loc;

		// Token: 0x0400000D RID: 13
		public readonly Phrase _targetHeightPhrase = Phrase.New("Building.FillValve.TargetHeight").Format<float>(new Func<float, ILoc, string>(UnitFormatter.FormatDistance));

		// Token: 0x0400000E RID: 14
		public readonly Phrase _automationTargetHeightPhrase = Phrase.New("Building.FillValve.TargetHeight").Format<float>(new Func<float, ILoc, string>(UnitFormatter.FormatDistance));

		// Token: 0x0400000F RID: 15
		public FillValve _fillValve;

		// Token: 0x04000010 RID: 16
		public VisualElement _root;

		// Token: 0x04000011 RID: 17
		public Label _targetHeightLabel;

		// Token: 0x04000012 RID: 18
		public Label _targetHeightStateLabel;

		// Token: 0x04000013 RID: 19
		public PreciseSlider _targetHeightSlider;

		// Token: 0x04000014 RID: 20
		public Label _automationTargetHeightLabel;

		// Token: 0x04000015 RID: 21
		public Label _automationTargetHeightStateLabel;

		// Token: 0x04000016 RID: 22
		public VisualElement _automationTargetHeightWrapper;

		// Token: 0x04000017 RID: 23
		public PreciseSlider _automationTargetHeightSlider;

		// Token: 0x04000018 RID: 24
		public Toggle _synchronizeToggle;
	}
}
