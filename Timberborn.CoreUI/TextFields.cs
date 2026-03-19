using System;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000051 RID: 81
	public static class TextFields
	{
		// Token: 0x0600014E RID: 334 RVA: 0x0000595C File Offset: 0x00003B5C
		public static void InitializeIntegerField(IntegerField integerField, int startingValue, int minValue = 0, int maxValue = 2147483647, Action<int> afterEditingCallback = null)
		{
			integerField.SetValueWithoutNotify(startingValue);
			integerField.RegisterCallback<FocusOutEvent>(delegate(FocusOutEvent _)
			{
				int valueWithoutNotify = Math.Clamp(integerField.value, minValue, maxValue);
				integerField.SetValueWithoutNotify(valueWithoutNotify);
				Action<int> afterEditingCallback2 = afterEditingCallback;
				if (afterEditingCallback2 == null)
				{
					return;
				}
				afterEditingCallback2(integerField.value);
			}, 0);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000059B0 File Offset: 0x00003BB0
		public static void InitializeFloatField(FloatField floatField, float startingValue, float minValue = -3.4028235E+38f, float maxValue = 3.4028235E+38f, Action<float> afterEditingCallback = null)
		{
			floatField.SetValueWithoutNotify(startingValue);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(floatField, delegate(ChangeEvent<float> evt)
			{
				float valueWithoutNotify = Math.Clamp(evt.newValue, minValue, maxValue);
				floatField.SetValueWithoutNotify(valueWithoutNotify);
				Action<float> afterEditingCallback2 = afterEditingCallback;
				if (afterEditingCallback2 == null)
				{
					return;
				}
				afterEditingCallback2(floatField.value);
			});
		}
	}
}
