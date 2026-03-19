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
	// Token: 0x02000029 RID: 41
	public class ScienceCounterFragment : IEntityPanelFragment
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00005EEC File Offset: 0x000040EC
		public ScienceCounterFragment(VisualElementLoader visualElementLoader, DropdownItemsSetter dropdownItemsSetter, NumericComparisonModeDropdownFactory numericComparisonModeDropdownFactory, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._numericComparisonModeDropdownFactory = numericComparisonModeDropdownFactory;
			this._loc = loc;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005F50 File Offset: 0x00004150
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/ScienceCounterFragment");
			this._root.ToggleDisplayStyle(false);
			this._measurement = UQueryExtensions.Q<Label>(this._root, "Measurement", null);
			this._threshold = UQueryExtensions.Q<IntegerField>(this._root, "Threshold", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<int>(this._threshold, new EventCallback<ChangeEvent<int>>(this.ChangeThreshold));
			this._threshold.isDelayed = true;
			this._modeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "Mode", null);
			this._modeDropdownProvider = this._numericComparisonModeDropdownFactory.Create(() => this._scienceCounter.Mode, delegate(NumericComparisonMode mode)
			{
				this._scienceCounter.SetMode(mode);
			});
			return this._root;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006018 File Offset: 0x00004218
		public void ShowFragment(BaseComponent entity)
		{
			this._scienceCounter = entity.GetComponent<ScienceCounter>();
			if (this._scienceCounter)
			{
				this._threshold.SetValueWithoutNotify(this._scienceCounter.Threshold);
				this._root.ToggleDisplayStyle(true);
				this._dropdownItemsSetter.SetItems(this._modeDropdown, this._modeDropdownProvider);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006077 File Offset: 0x00004277
		public void ClearFragment()
		{
			this._scienceCounter = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000608C File Offset: 0x0000428C
		public void UpdateFragment()
		{
			if (this._scienceCounter)
			{
				this._measurement.text = this._loc.T<int>(this._measurementPhrase, this._scienceCounter.SampledSciencePoints);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000060C4 File Offset: 0x000042C4
		public void ChangeThreshold(ChangeEvent<int> evt)
		{
			int num = Math.Clamp(evt.newValue, 0, int.MaxValue);
			this._scienceCounter.SetThreshold(num);
			this._threshold.SetValueWithoutNotify(num);
		}

		// Token: 0x0400012A RID: 298
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400012B RID: 299
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400012C RID: 300
		public readonly NumericComparisonModeDropdownFactory _numericComparisonModeDropdownFactory;

		// Token: 0x0400012D RID: 301
		public readonly ILoc _loc;

		// Token: 0x0400012E RID: 302
		public VisualElement _root;

		// Token: 0x0400012F RID: 303
		public IntegerField _threshold;

		// Token: 0x04000130 RID: 304
		public ScienceCounter _scienceCounter;

		// Token: 0x04000131 RID: 305
		public Label _measurement;

		// Token: 0x04000132 RID: 306
		public Dropdown _modeDropdown;

		// Token: 0x04000133 RID: 307
		public EnumDropdownProvider<NumericComparisonMode> _modeDropdownProvider;

		// Token: 0x04000134 RID: 308
		public readonly Phrase _measurementPhrase = Phrase.New("Automation.Measurement").Format<int>((int value) => string.Format("{0}", value));
	}
}
