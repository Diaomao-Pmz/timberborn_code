using System;
using Timberborn.AutomationBuildings;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000030 RID: 48
	public class TimerIntervalElement
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00006A0C File Offset: 0x00004C0C
		public TimerIntervalElement(EnumDropdownProviderFactory enumDropdownProviderFactory, DropdownItemsSetter dropdownItemsSetter)
		{
			this._enumDropdownProviderFactory = enumDropdownProviderFactory;
			this._dropdownItemsSetter = dropdownItemsSetter;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006A24 File Offset: 0x00004C24
		public void Initialize(VisualElement root)
		{
			this._root = root;
			this._timeField = UQueryExtensions.Q<FloatField>(this._root, "TimeField", null);
			this._timeField.isDelayed = true;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(this._timeField, new EventCallback<ChangeEvent<float>>(this.SetTime));
			this._typeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "IntervalTypeDropdown", null);
			this._intervalTypeDropdownProvider = this._enumDropdownProviderFactory.CreateLocalized<IntervalType>(() => this._timerInterval.Type, new Action<IntervalType>(this.SetIntervalType), TimerIntervalElement.IntervalTypeLocKey);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006AB8 File Offset: 0x00004CB8
		public void Show(TimerInterval timerInterval)
		{
			this._timerInterval = timerInterval;
			this._dropdownItemsSetter.SetItems(this._typeDropdown, this._intervalTypeDropdownProvider);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006AD8 File Offset: 0x00004CD8
		public void Update()
		{
			if (!this._timeField.IsFocused())
			{
				this._timeField.SetValueWithoutNotify(this._timerInterval.GetTypeTime());
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006AFD File Offset: 0x00004CFD
		public void Clear()
		{
			this._timerInterval = null;
			this._typeDropdown.ClearItems();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006B11 File Offset: 0x00004D11
		public void SetDisplayStyle(bool visible)
		{
			this._root.ToggleDisplayStyle(visible);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006B1F File Offset: 0x00004D1F
		public void SetTime(ChangeEvent<float> time)
		{
			if (time.newValue >= 0f)
			{
				this.SetTimeInterval(time.newValue, this._timerInterval.Type);
				return;
			}
			this._timeField.SetValueWithoutNotify(this._timerInterval.GetTypeTime());
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006B5C File Offset: 0x00004D5C
		public void SetIntervalType(IntervalType intervalType)
		{
			this.SetTimeInterval(this._timeField.value, intervalType);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006B70 File Offset: 0x00004D70
		public void SetTimeInterval(float time, IntervalType intervalType)
		{
			switch (intervalType)
			{
			case IntervalType.Ticks:
				this._timerInterval.SetTicks((int)Math.Round((double)time));
				break;
			case IntervalType.Hours:
				this._timerInterval.SetHours(time);
				break;
			case IntervalType.Days:
				this._timerInterval.SetDays(time);
				break;
			default:
				throw new ArgumentOutOfRangeException("Type", this._timerInterval.Type, null);
			}
			this._timeField.SetValueWithoutNotify(this._timerInterval.GetTypeTime());
		}

		// Token: 0x04000166 RID: 358
		public static readonly string IntervalTypeLocKey = "Building.Timer.IntervalType.";

		// Token: 0x04000167 RID: 359
		public readonly EnumDropdownProviderFactory _enumDropdownProviderFactory;

		// Token: 0x04000168 RID: 360
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000169 RID: 361
		public VisualElement _root;

		// Token: 0x0400016A RID: 362
		public FloatField _timeField;

		// Token: 0x0400016B RID: 363
		public Dropdown _typeDropdown;

		// Token: 0x0400016C RID: 364
		public EnumDropdownProvider<IntervalType> _intervalTypeDropdownProvider;

		// Token: 0x0400016D RID: 365
		public TimerInterval _timerInterval;
	}
}
