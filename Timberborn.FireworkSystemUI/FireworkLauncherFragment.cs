using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.FireworkSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.FireworkSystemUI
{
	// Token: 0x02000005 RID: 5
	public class FireworkLauncherFragment : IEntityPanelFragment
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002138 File Offset: 0x00000338
		public FireworkLauncherFragment(VisualElementLoader visualElementLoader, DropdownItemsSetter dropdownItemsSetter, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._loc = loc;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021C4 File Offset: 0x000003C4
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/FireworkLauncherFragment");
			this._idDropdown = UQueryExtensions.Q<Dropdown>(this._root, "FireworkId", null);
			this._headingSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "HeadingSlider", null);
			this._headingSlider.SetValueChangedCallback(new Action<float>(this.OnHeadingChanged));
			this._pitchSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "PitchSlider", null);
			this._pitchSlider.SetValueChangedCallback(new Action<float>(this.OnPitchChanged));
			this._flightDistanceSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "FlightDistanceSlider", null);
			this._flightDistanceSlider.SetValueChangedCallback(new Action<float>(this.OnFlightDistanceChanged));
			this._headingLabel = UQueryExtensions.Q<Label>(this._root, "HeadingLabel", null);
			this._pitchLabel = UQueryExtensions.Q<Label>(this._root, "PitchLabel", null);
			this._flightDistanceLabel = UQueryExtensions.Q<Label>(this._root, "FlightDistanceLabel", null);
			this._isContinuousToggle = UQueryExtensions.Q<Toggle>(this._root, "IsContinuous", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._isContinuousToggle, new EventCallback<ChangeEvent<bool>>(this.OnContinuousToggleChanged));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002310 File Offset: 0x00000510
		public void ShowFragment(BaseComponent entity)
		{
			this._fireworkLauncher = entity.GetComponent<FireworkLauncher>();
			if (this._fireworkLauncher)
			{
				this._root.ToggleDisplayStyle(true);
				this._headingSlider.UpdateValuesWithoutNotify((float)this._fireworkLauncher.Heading, (float)FireworkLimits.MinHeading, (float)FireworkLimits.MaxHeading);
				this._pitchSlider.UpdateValuesWithoutNotify((float)this._fireworkLauncher.Pitch, (float)FireworkLimits.MinPitch, (float)FireworkLimits.MaxPitch);
				this._flightDistanceSlider.UpdateValuesWithoutNotify((float)this._fireworkLauncher.FlightDistance, (float)FireworkLimits.MinFlightDistance, (float)FireworkLimits.MaxFlightDistance);
				this._fireworkIdDropdownProvider = this._fireworkLauncher.GetComponent<FireworkIdDropdownProvider>();
				this._dropdownItemsSetter.SetItems(this._idDropdown, this._fireworkIdDropdownProvider);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023D6 File Offset: 0x000005D6
		public void UpdateFragment()
		{
			if (this._fireworkLauncher)
			{
				this._isContinuousToggle.SetValueWithoutNotify(this._fireworkLauncher.IsContinuous);
				this.UpdateLabels();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002401 File Offset: 0x00000601
		public void ClearFragment()
		{
			this._idDropdown.ClearItems();
			this._fireworkLauncher = null;
			this._fireworkIdDropdownProvider = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002428 File Offset: 0x00000628
		public void UpdateLabels()
		{
			this._headingLabel.text = this._loc.T<int>(this._headingPhrase, this._fireworkLauncher.Heading);
			this._pitchLabel.text = this._loc.T<int>(this._pitchPhrase, this._fireworkLauncher.Pitch);
			this._flightDistanceLabel.text = this._loc.T<int>(this._flightDistancePhrase, this._fireworkLauncher.FlightDistance);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024AC File Offset: 0x000006AC
		public void OnHeadingChanged(float newValue)
		{
			float num = Mathf.Clamp(Mathf.Round(newValue), (float)FireworkLimits.MinHeading, (float)FireworkLimits.MaxHeading);
			this._fireworkLauncher.SetHeading((int)num);
			this.UpdateLabels();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024E4 File Offset: 0x000006E4
		public void OnPitchChanged(float newValue)
		{
			float num = Mathf.Clamp(Mathf.Round(newValue), (float)FireworkLimits.MinPitch, (float)FireworkLimits.MaxPitch);
			this._fireworkLauncher.SetPitch((int)num);
			this.UpdateLabels();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000251C File Offset: 0x0000071C
		public void OnFlightDistanceChanged(float newValue)
		{
			float num = Mathf.Clamp(Mathf.Round(newValue), (float)FireworkLimits.MinFlightDistance, (float)FireworkLimits.MaxFlightDistance);
			this._fireworkLauncher.SetFlightDistance((int)num);
			this.UpdateLabels();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002554 File Offset: 0x00000754
		public void OnContinuousToggleChanged(ChangeEvent<bool> evt)
		{
			this._fireworkLauncher.SetContinuous(evt.newValue);
		}

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400000B RID: 11
		public readonly ILoc _loc;

		// Token: 0x0400000C RID: 12
		public VisualElement _root;

		// Token: 0x0400000D RID: 13
		public Dropdown _idDropdown;

		// Token: 0x0400000E RID: 14
		public PreciseSlider _headingSlider;

		// Token: 0x0400000F RID: 15
		public PreciseSlider _pitchSlider;

		// Token: 0x04000010 RID: 16
		public PreciseSlider _flightDistanceSlider;

		// Token: 0x04000011 RID: 17
		public Label _headingLabel;

		// Token: 0x04000012 RID: 18
		public Label _pitchLabel;

		// Token: 0x04000013 RID: 19
		public Label _flightDistanceLabel;

		// Token: 0x04000014 RID: 20
		public Toggle _isContinuousToggle;

		// Token: 0x04000015 RID: 21
		public FireworkLauncher _fireworkLauncher;

		// Token: 0x04000016 RID: 22
		public FireworkIdDropdownProvider _fireworkIdDropdownProvider;

		// Token: 0x04000017 RID: 23
		public readonly Phrase _headingPhrase = Phrase.New("Building.FireworkLauncher.Heading").Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatAngle));

		// Token: 0x04000018 RID: 24
		public readonly Phrase _pitchPhrase = Phrase.New("Building.FireworkLauncher.Pitch").Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatAngle));

		// Token: 0x04000019 RID: 25
		public readonly Phrase _flightDistancePhrase = Phrase.New("Building.FireworkLauncher.FlightDistance").Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatDistance));
	}
}
