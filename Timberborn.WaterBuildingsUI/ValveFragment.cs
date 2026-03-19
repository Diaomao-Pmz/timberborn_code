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
	// Token: 0x02000014 RID: 20
	public class ValveFragment : IEntityPanelFragment
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00003EF4 File Offset: 0x000020F4
		public ValveFragment(VisualElementLoader visualElementLoader, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003F8C File Offset: 0x0000218C
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/ValveFragment");
			this._valveStateLabel = UQueryExtensions.Q<Label>(this._root, "ValveState", null);
			this._outflowLimitLabel = UQueryExtensions.Q<Label>(this._root, "OutflowLimitLabel", null);
			this._outflowLimitStateLabel = UQueryExtensions.Q<Label>(this._root, "OutflowLimitStateLabel", null);
			this._outflowLimitSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "OutflowLimitSlider", null);
			this._outflowLimitSlider.SetValueChangedCallback(new Action<float>(this.SetOutflowLimit));
			this._automationOutflowLimitWrapper = UQueryExtensions.Q<VisualElement>(this._root, "AutomationOutflowLimitWrapper", null);
			this._automationOutflowLimitLabel = UQueryExtensions.Q<Label>(this._root, "AutomationOutflowLimitLabel", null);
			this._automationOutflowLimitStateLabel = UQueryExtensions.Q<Label>(this._root, "AutomationOutflowLimitStateLabel", null);
			this._automationOutflowLimitSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "AutomationOutflowLimitSlider", null);
			this._automationOutflowLimitSlider.SetValueChangedCallback(new Action<float>(this.SetAutomationOutflowLimit));
			this._reactionSpeedWrapper = UQueryExtensions.Q<VisualElement>(this._root, "ReactionSpeedWrapper", null);
			this._reactionSpeedLabel = UQueryExtensions.Q<Label>(this._root, "ReactionSpeedLabel", null);
			this._reactionSpeedSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "ReactionSpeedSlider", null);
			this._reactionSpeedSlider.SetValueChangedCallback(new Action<float>(this.SetReactionSpeed));
			this._synchronizeToggle = UQueryExtensions.Q<Toggle>(this._root, "Synchronize", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._synchronizeToggle, new EventCallback<ChangeEvent<bool>>(this.ToggleSynchronization));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004134 File Offset: 0x00002334
		public void ShowFragment(BaseComponent entity)
		{
			this._valve = entity.GetComponent<Valve>();
			if (this._valve)
			{
				this._outflowLimitSlider.SetStepWithoutNotify(this._valve.OutflowLimitStep);
				this._automationOutflowLimitSlider.SetStepWithoutNotify(this._valve.OutflowLimitStep);
				this._reactionSpeedSlider.SetStepWithoutNotify(this._valve.ReactionSpeedStep);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000419C File Offset: 0x0000239C
		public void ClearFragment()
		{
			this._valve = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000041B4 File Offset: 0x000023B4
		public void UpdateFragment()
		{
			if (this._valve)
			{
				this.UpdateOutflowLimit();
				this.UpdateAutomationOutflowLimit();
				this.UpdateMarkers();
				this.UpdateReactionSpeed();
				this.UpdateValveState();
				this.UpdateSynchronizeToggle();
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000420B File Offset: 0x0000240B
		public float OutflowLimitSliderMaxValue
		{
			get
			{
				return this._valve.MaxOutflowLimit + this._valve.OutflowLimitStep;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004224 File Offset: 0x00002424
		public void UpdateOutflowLimit()
		{
			this._outflowLimitSlider.UpdateValuesWithoutNotify(this._valve.OutflowLimitEnabled ? Mathf.Clamp(this._valve.OutflowLimit, 0f, this._valve.MaxOutflowLimit) : this.OutflowLimitSliderMaxValue, this.OutflowLimitSliderMaxValue);
			this._outflowLimitLabel.text = (this._valve.OutflowLimitEnabled ? this._loc.T<float>(this._outflowLimitPhrase, this._valve.OutflowLimit) : this._loc.T(ValveFragment.OutflowUnlimitedLocKey));
			this._outflowLimitStateLabel.ToggleDisplayStyle(this._valve.IsAutomated);
			if (this._valve.IsAutomated)
			{
				this._outflowLimitStateLabel.EnableInClassList(ValveFragment.ActiveStateLabelClass, !this._valve.IsInputOn);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004300 File Offset: 0x00002500
		public void UpdateAutomationOutflowLimit()
		{
			this._automationOutflowLimitWrapper.ToggleDisplayStyle(this._valve.IsAutomated);
			if (this._valve.IsAutomated)
			{
				this._automationOutflowLimitSlider.UpdateValuesWithoutNotify(this._valve.AutomationOutflowLimitEnabled ? Mathf.Clamp(this._valve.AutomationOutflowLimit, 0f, this._valve.MaxOutflowLimit) : this.OutflowLimitSliderMaxValue, this.OutflowLimitSliderMaxValue);
				this._automationOutflowLimitLabel.text = (this._valve.AutomationOutflowLimitEnabled ? this._loc.T<float>(this._automationOutflowLimitPhrase, this._valve.AutomationOutflowLimit) : this._loc.T(ValveFragment.OutflowUnlimitedLocKey));
				this._automationOutflowLimitStateLabel.EnableInClassList(ValveFragment.ActiveStateLabelClass, this._valve.IsInputOn);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000043DC File Offset: 0x000025DC
		public void UpdateMarkers()
		{
			if (this._valve.IsAutomated)
			{
				float marker = this._valve.CurrentOutflowLimit ?? this.OutflowLimitSliderMaxValue;
				this._outflowLimitSlider.SetMarker(marker);
				this._automationOutflowLimitSlider.SetMarker(marker);
				return;
			}
			this._outflowLimitSlider.ClearMarker();
			this._automationOutflowLimitSlider.ClearMarker();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000444C File Offset: 0x0000264C
		public void UpdateReactionSpeed()
		{
			this._reactionSpeedWrapper.ToggleDisplayStyle(this._valve.IsAutomated);
			this._reactionSpeedSlider.UpdateValuesWithoutNotify(this._valve.ReactionSpeed, Valve.ReactionSpeedMin, Valve.ReactionSpeedMax);
			this._reactionSpeedLabel.text = this._loc.T<float>(this._reactionSpeedPhrase, this._valve.ReactionSpeed);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000044B8 File Offset: 0x000026B8
		public void UpdateValveState()
		{
			this._valveStateLabel.ToggleDisplayStyle(this._valve.State != null);
			if (this._valve.State != null)
			{
				Label valveStateLabel = this._valveStateLabel;
				ValveState? state = this._valve.State;
				if (state != null)
				{
					string text;
					switch (state.GetValueOrDefault())
					{
					case ValveState.Idle:
						text = this._loc.T(ValveFragment.IdleLocKey);
						break;
					case ValveState.Opening:
						text = this._loc.T(ValveFragment.OpeningLocKey);
						break;
					case ValveState.Closing:
						text = this._loc.T(ValveFragment.ClosingLocKey);
						break;
					default:
						goto IL_A4;
					}
					valveStateLabel.text = text;
					return;
				}
				IL_A4:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004576 File Offset: 0x00002776
		public void UpdateSynchronizeToggle()
		{
			this._synchronizeToggle.SetValueWithoutNotify(this._valve.IsSynchronized);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004590 File Offset: 0x00002790
		public void SetOutflowLimit(float value)
		{
			if (value > this._valve.MaxOutflowLimit)
			{
				this._valve.SetOutflowLimitEnabledAndSynchronize(false);
				this._valve.SetOutflowLimitAndSynchronize(this._valve.MaxOutflowLimit);
				return;
			}
			this._valve.SetOutflowLimitEnabledAndSynchronize(true);
			this._valve.SetOutflowLimitAndSynchronize(value);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000045E8 File Offset: 0x000027E8
		public void SetAutomationOutflowLimit(float value)
		{
			if (value > this._valve.MaxOutflowLimit)
			{
				this._valve.SetAutomationOutflowLimitEnabledAndSynchronize(false);
				this._valve.SetAutomationOutflowLimitAndSynchronize(this._valve.MaxOutflowLimit);
				return;
			}
			this._valve.SetAutomationOutflowLimitEnabledAndSynchronize(true);
			this._valve.SetAutomationOutflowLimitAndSynchronize(value);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000463E File Offset: 0x0000283E
		public void SetReactionSpeed(float value)
		{
			this._valve.SetReactionSpeedAndSynchronize(value);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000464C File Offset: 0x0000284C
		public void ToggleSynchronization(ChangeEvent<bool> changeEvent)
		{
			this._valve.ToggleSynchronization(changeEvent.newValue);
		}

		// Token: 0x0400008A RID: 138
		public static readonly string IdleLocKey = "Building.Valve.State.Idle";

		// Token: 0x0400008B RID: 139
		public static readonly string OpeningLocKey = "Building.Valve.State.Opening";

		// Token: 0x0400008C RID: 140
		public static readonly string ClosingLocKey = "Building.Valve.State.Closing";

		// Token: 0x0400008D RID: 141
		public static readonly string OutflowUnlimitedLocKey = "Building.Valve.OutflowUnlimited";

		// Token: 0x0400008E RID: 142
		public static readonly string ActiveStateLabelClass = "entity-panel__text--highlight-white";

		// Token: 0x0400008F RID: 143
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000090 RID: 144
		public readonly ILoc _loc;

		// Token: 0x04000091 RID: 145
		public readonly Phrase _outflowLimitPhrase = Phrase.New("Building.Valve.OutflowLimit").Format<float>(new Func<float, ILoc, string>(UnitFormatter.FormatFlow));

		// Token: 0x04000092 RID: 146
		public readonly Phrase _automationOutflowLimitPhrase = Phrase.New("Building.Valve.OutflowLimit").Format<float>(new Func<float, ILoc, string>(UnitFormatter.FormatFlow));

		// Token: 0x04000093 RID: 147
		public readonly Phrase _reactionSpeedPhrase = Phrase.New("Building.Valve.ReactionSpeed").Format<float>((float value) => NumberFormatter.FormatAsPercentRounded((double)Mathf.Max(value, 0.01f)));

		// Token: 0x04000094 RID: 148
		public Valve _valve;

		// Token: 0x04000095 RID: 149
		public VisualElement _root;

		// Token: 0x04000096 RID: 150
		public Label _valveStateLabel;

		// Token: 0x04000097 RID: 151
		public Label _outflowLimitLabel;

		// Token: 0x04000098 RID: 152
		public Label _outflowLimitStateLabel;

		// Token: 0x04000099 RID: 153
		public PreciseSlider _outflowLimitSlider;

		// Token: 0x0400009A RID: 154
		public Label _automationOutflowLimitLabel;

		// Token: 0x0400009B RID: 155
		public Label _automationOutflowLimitStateLabel;

		// Token: 0x0400009C RID: 156
		public VisualElement _automationOutflowLimitWrapper;

		// Token: 0x0400009D RID: 157
		public PreciseSlider _automationOutflowLimitSlider;

		// Token: 0x0400009E RID: 158
		public VisualElement _reactionSpeedWrapper;

		// Token: 0x0400009F RID: 159
		public Label _reactionSpeedLabel;

		// Token: 0x040000A0 RID: 160
		public PreciseSlider _reactionSpeedSlider;

		// Token: 0x040000A1 RID: 161
		public Toggle _synchronizeToggle;
	}
}
