using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200001D RID: 29
	public class IntegerSliderFactory
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00003263 File Offset: 0x00001463
		public IntegerSliderFactory(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003274 File Offset: 0x00001474
		public VisualElement Create(int current, int max, Action<int> callback)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/IntegerSlider");
			Slider slider = UQueryExtensions.Q<Slider>(visualElement, "Slider", null);
			slider.value = (float)current / (float)max;
			IntegerField integerField = UQueryExtensions.Q<IntegerField>(visualElement, "Value", null);
			UQueryExtensions.Q<Label>(visualElement, "MaxValue", null).text = max.ToString();
			int num = max.ToString().Length * IntegerSliderFactory.WidthPerDigit;
			UQueryExtensions.Q<TextElement>(integerField, null, null).style.width = (float)num;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(slider, delegate(ChangeEvent<float> changeEvent)
			{
				IntegerSliderFactory.ChangeValue(changeEvent, integerField, slider, max, callback);
			});
			TextFields.InitializeIntegerField(integerField, current, 0, max, delegate(int newValue)
			{
				IntegerSliderFactory.ChangeValue(newValue, integerField, slider, max, callback);
			});
			return visualElement;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003368 File Offset: 0x00001568
		public static void ChangeValue(ChangeEvent<float> changeEvent, IntegerField integerField, Slider slider, int max, Action<int> callback)
		{
			IntegerSliderFactory.ChangeValue(Mathf.RoundToInt(changeEvent.newValue * (float)max), integerField, slider, max, callback);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003382 File Offset: 0x00001582
		public static void ChangeValue(int newValue, IntegerField integerField, Slider slider, int max, Action<int> callback)
		{
			newValue = Mathf.Clamp(newValue, 0, max);
			IntegerSliderFactory.RefreshUI(integerField, slider, newValue, max);
			callback(newValue);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000339F File Offset: 0x0000159F
		public static void RefreshUI(IntegerField integerField, Slider slider, int current, int max)
		{
			integerField.SetValueWithoutNotify(current);
			slider.SetValueWithoutNotify((float)current / (float)max);
		}

		// Token: 0x04000046 RID: 70
		public static readonly int WidthPerDigit = 8;

		// Token: 0x04000047 RID: 71
		public readonly VisualElementLoader _visualElementLoader;
	}
}
