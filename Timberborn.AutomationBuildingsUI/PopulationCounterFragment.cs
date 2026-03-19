using System;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000021 RID: 33
	public class PopulationCounterFragment : IEntityPanelFragment
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00004BDC File Offset: 0x00002DDC
		public PopulationCounterFragment(VisualElementLoader visualElementLoader, NumericComparisonModeDropdownFactory numericComparisonModeDropdownFactory, EnumDropdownProviderFactory enumDropdownProviderFactory, DropdownItemsSetter dropdownItemsSetter, RadioToggleFactory radioToggleFactory, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._numericComparisonModeDropdownFactory = numericComparisonModeDropdownFactory;
			this._enumDropdownProviderFactory = enumDropdownProviderFactory;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._radioToggleFactory = radioToggleFactory;
			this._loc = loc;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004C14 File Offset: 0x00002E14
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/PopulationCounterFragment");
			this._root.ToggleDisplayStyle(false);
			this._globalModeRadioToggle = this._radioToggleFactory.CreateLocalizable(new string[]
			{
				PopulationCounterFragment.DistrictModeLocKey,
				PopulationCounterFragment.GlobalModeLocKey
			}, UQueryExtensions.Q<VisualElement>(this._root, "GlobalModeRadioToggle", null));
			this._globalModeRadioToggle.RadioButtonSelected += this.OnGlobalModeChanged;
			this._threshold = UQueryExtensions.Q<IntegerField>(this._root, "Threshold", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<int>(this._threshold, new EventCallback<ChangeEvent<int>>(this.OnThresholdChanged));
			this._threshold.isDelayed = true;
			this._modeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "Mode", null);
			this._populationCounterModeDropdownProvider = this._enumDropdownProviderFactory.CreateLocalized<PopulationCounterMode>(() => this._populationCounter.Mode, new Action<PopulationCounterMode>(this.SetMode), PopulationCounterFragment.PopulationCounterModeLocKeyPrefix);
			this._workerTypeWrapper = UQueryExtensions.Q<VisualElement>(this._root, "WorkerTypeWrapper", null);
			this._beaversToggle = UQueryExtensions.Q<Toggle>(this._root, "BeaversToggle", null);
			this._botsToggle = UQueryExtensions.Q<Toggle>(this._root, "BotsToggle", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._beaversToggle, new EventCallback<ChangeEvent<bool>>(this.OnBeaversToggleChanged));
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._botsToggle, new EventCallback<ChangeEvent<bool>>(this.OnBotsToggleChanged));
			this._measurement = UQueryExtensions.Q<Label>(this._root, "Measurement", null);
			this._comparisonModeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "ComparisonMode", null);
			this._comparisonModeDropdownProvider = this._numericComparisonModeDropdownFactory.Create(() => this._populationCounter.ComparisonMode, delegate(NumericComparisonMode comparisonMode)
			{
				this._populationCounter.SetComparisonMode(comparisonMode);
			});
			return this._root;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public void ShowFragment(BaseComponent entity)
		{
			this._populationCounter = entity.GetComponent<PopulationCounter>();
			if (this._populationCounter)
			{
				this._threshold.SetValueWithoutNotify(this._populationCounter.Threshold);
				this._beaversToggle.SetValueWithoutNotify(this._populationCounter.CountBeavers);
				this._botsToggle.SetValueWithoutNotify(this._populationCounter.CountBots);
				this._root.ToggleDisplayStyle(true);
				this._dropdownItemsSetter.SetItems(this._modeDropdown, this._populationCounterModeDropdownProvider);
				this._dropdownItemsSetter.SetItems(this._comparisonModeDropdown, this._comparisonModeDropdownProvider);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004E86 File Offset: 0x00003086
		public void ClearFragment()
		{
			this._populationCounter = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004E9C File Offset: 0x0000309C
		public void UpdateFragment()
		{
			if (this._populationCounter)
			{
				this._globalModeRadioToggle.Update(this._populationCounter.GlobalMode ? 1 : 0);
				this._workerTypeWrapper.ToggleDisplayStyle(this._populationCounter.UsesWorkerType);
				this._measurement.text = this._loc.T<int>(PopulationCounterFragment.MeasurementLocKey, this._populationCounter.GetMeasurement());
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004F0E File Offset: 0x0000310E
		public void OnGlobalModeChanged(object sender, int modeIndex)
		{
			this._populationCounter.SetGlobalMode(modeIndex == 1);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004F1F File Offset: 0x0000311F
		public void SetMode(PopulationCounterMode mode)
		{
			this._populationCounter.SetMode(mode);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004F2D File Offset: 0x0000312D
		public void OnBeaversToggleChanged(ChangeEvent<bool> evt)
		{
			this._populationCounter.SetCountBeavers(evt.newValue);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004F40 File Offset: 0x00003140
		public void OnBotsToggleChanged(ChangeEvent<bool> evt)
		{
			this._populationCounter.SetCountBots(evt.newValue);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004F54 File Offset: 0x00003154
		public void OnThresholdChanged(ChangeEvent<int> evt)
		{
			int num = Math.Clamp(evt.newValue, 0, int.MaxValue);
			this._threshold.SetValueWithoutNotify(num);
			this._populationCounter.SetThreshold(num);
		}

		// Token: 0x040000CB RID: 203
		public static readonly string MeasurementLocKey = "Automation.Measurement";

		// Token: 0x040000CC RID: 204
		public static readonly string PopulationCounterModeLocKeyPrefix = "Building.PopulationCounter.Mode.";

		// Token: 0x040000CD RID: 205
		public static readonly string GlobalModeLocKey = "Building.PopulationCounter.GlobalMode";

		// Token: 0x040000CE RID: 206
		public static readonly string DistrictModeLocKey = "Building.PopulationCounter.DistrictMode";

		// Token: 0x040000CF RID: 207
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x040000D0 RID: 208
		public readonly NumericComparisonModeDropdownFactory _numericComparisonModeDropdownFactory;

		// Token: 0x040000D1 RID: 209
		public readonly EnumDropdownProviderFactory _enumDropdownProviderFactory;

		// Token: 0x040000D2 RID: 210
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x040000D3 RID: 211
		public readonly RadioToggleFactory _radioToggleFactory;

		// Token: 0x040000D4 RID: 212
		public readonly ILoc _loc;

		// Token: 0x040000D5 RID: 213
		public VisualElement _root;

		// Token: 0x040000D6 RID: 214
		public RadioToggle _globalModeRadioToggle;

		// Token: 0x040000D7 RID: 215
		public IntegerField _threshold;

		// Token: 0x040000D8 RID: 216
		public Label _measurement;

		// Token: 0x040000D9 RID: 217
		public PopulationCounter _populationCounter;

		// Token: 0x040000DA RID: 218
		public Dropdown _comparisonModeDropdown;

		// Token: 0x040000DB RID: 219
		public EnumDropdownProvider<NumericComparisonMode> _comparisonModeDropdownProvider;

		// Token: 0x040000DC RID: 220
		public Dropdown _modeDropdown;

		// Token: 0x040000DD RID: 221
		public EnumDropdownProvider<PopulationCounterMode> _populationCounterModeDropdownProvider;

		// Token: 0x040000DE RID: 222
		public VisualElement _workerTypeWrapper;

		// Token: 0x040000DF RID: 223
		public Toggle _beaversToggle;

		// Token: 0x040000E0 RID: 224
		public Toggle _botsToggle;
	}
}
