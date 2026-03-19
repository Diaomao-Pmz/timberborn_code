using System;
using System.Runtime.CompilerServices;
using Timberborn.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200003E RID: 62
	[UxmlElement]
	public class PreciseSlider : VisualElement
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00004AEC File Offset: 0x00002CEC
		public PreciseSlider()
		{
			VisualTreeAsset visualTreeAsset = Resources.Load<VisualTreeAsset>("UI/Views/Core/PreciseSlider");
			VisualTreeAsset visualTreeAsset2 = Resources.Load<VisualTreeAsset>("UI/Views/Core/PreciseSliderMarker");
			visualTreeAsset.CloneTree(this);
			this._slider = UQueryExtensions.Q<Slider>(this, "Slider", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(this._slider, new EventCallback<ChangeEvent<float>>(this.OnSliderValueChanged));
			UQueryExtensions.Q<Button>(this, "DecreaseButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnDecreaseClicked), 0);
			UQueryExtensions.Q<Button>(this, "IncreaseButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnIncreaseClicked), 0);
			visualTreeAsset2.CloneTree(UQueryExtensions.Q(this._slider, "unity-drag-container", null));
			this._marker = UQueryExtensions.Q(this._slider, "PreciseSliderMarker", null);
			this._marker.PlaceBehind(UQueryExtensions.Q(this._slider, "unity-dragger", null));
			this._marker.ToggleDisplayStyle(false);
			this._slider.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChangedEvent), 0);
			this._marker.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChangedEvent), 0);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004C11 File Offset: 0x00002E11
		public float Value
		{
			get
			{
				return this._slider.value;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004C1E File Offset: 0x00002E1E
		public void SetValueChangedCallback(Action<float> valueChangedCallback)
		{
			this._valueChangedCallback = valueChangedCallback;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004C27 File Offset: 0x00002E27
		public void UpdateValuesWithoutNotify(float value, float maxValue)
		{
			this.UpdateValuesWithoutNotify(value, 0f, maxValue);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004C38 File Offset: 0x00002E38
		public void UpdateValuesWithoutNotify(float value, float minValue, float maxValue)
		{
			bool flag = false;
			this._suppressCallback = true;
			if (!this._slider.lowValue.Equals(minValue))
			{
				this._slider.lowValue = minValue;
				flag = true;
			}
			if (!this._slider.highValue.Equals(maxValue))
			{
				this._slider.highValue = maxValue;
				flag = true;
			}
			this._suppressCallback = false;
			this.SetValueWithoutNotify(value);
			if (flag)
			{
				this.UpdateMarker();
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004CAD File Offset: 0x00002EAD
		public void SetValueWithoutNotify(float value)
		{
			this.UpdateValue(value, this._slider.value, false);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004CC2 File Offset: 0x00002EC2
		public void SetStepWithoutNotify(float step)
		{
			this._step = step;
			this.UpdateValue(this.Value, this.Value, false);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004CDE File Offset: 0x00002EDE
		public void SetMarker(float value)
		{
			if (!this._markerValue.Equals(value))
			{
				this._markerValue = new float?(value);
				this.UpdateMarker();
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004D0B File Offset: 0x00002F0B
		public void ClearMarker()
		{
			if (this._markerValue != null)
			{
				this._markerValue = null;
				this.UpdateMarker();
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004D2C File Offset: 0x00002F2C
		public void OnDecreaseClicked(ClickEvent evt)
		{
			this._slider.value -= this._step;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004D46 File Offset: 0x00002F46
		public void OnIncreaseClicked(ClickEvent evt)
		{
			this._slider.value += this._step;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004D60 File Offset: 0x00002F60
		public void OnSliderValueChanged(ChangeEvent<float> evt)
		{
			if (!this._suppressCallback)
			{
				this.UpdateValue(evt.newValue, evt.previousValue, true);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004D7D File Offset: 0x00002F7D
		public void OnGeometryChangedEvent(GeometryChangedEvent evt)
		{
			this.UpdateMarker();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004D88 File Offset: 0x00002F88
		public void UpdateValue(float value, float previousValue, bool invokeCallback)
		{
			float num = Numbers.RoundToPrecision(value, this._step);
			if (Math.Abs(num - previousValue) > Mathf.Epsilon)
			{
				this._slider.SetValueWithoutNotify(value);
				if (invokeCallback)
				{
					Action<float> valueChangedCallback = this._valueChangedCallback;
					if (valueChangedCallback == null)
					{
						return;
					}
					valueChangedCallback(num);
				}
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004DD4 File Offset: 0x00002FD4
		public void UpdateMarker()
		{
			float num = this._slider.highValue - this._slider.lowValue;
			if (this._markerValue != null && num != 0f)
			{
				this._marker.style.translate = new Translate(Length.Pixels(Mathf.Clamp01((this._markerValue.Value - this._slider.lowValue) / num) * (this._slider.contentRect.width - this._marker.layout.width)), Length.Pixels(0f));
				this._marker.ToggleDisplayStyle(true);
				return;
			}
			this._marker.ToggleDisplayStyle(false);
		}

		// Token: 0x04000088 RID: 136
		public readonly Slider _slider;

		// Token: 0x04000089 RID: 137
		public readonly VisualElement _marker;

		// Token: 0x0400008A RID: 138
		public Action<float> _valueChangedCallback;

		// Token: 0x0400008B RID: 139
		public float _step = 1f;

		// Token: 0x0400008C RID: 140
		public float? _markerValue;

		// Token: 0x0400008D RID: 141
		public bool _suppressCallback;

		// Token: 0x0200003F RID: 63
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x0600010E RID: 270 RVA: 0x00004E99 File Offset: 0x00003099
			public override object CreateInstance()
			{
				return new PreciseSlider();
			}
		}
	}
}
