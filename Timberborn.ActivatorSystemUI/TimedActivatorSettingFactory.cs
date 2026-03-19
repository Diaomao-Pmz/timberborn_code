using System;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.MapStateSystem;
using UnityEngine.UIElements;

namespace Timberborn.ActivatorSystemUI
{
	// Token: 0x02000009 RID: 9
	public class TimedActivatorSettingFactory
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000230B File Offset: 0x0000050B
		public TimedActivatorSettingFactory(VisualElementLoader visualElementLoader, DevModeManager devModeManager, MapEditorMode mapEditorMode)
		{
			this._visualElementLoader = visualElementLoader;
			this._devModeManager = devModeManager;
			this._mapEditorMode = mapEditorMode;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002328 File Offset: 0x00000528
		public TimedActivatorSetting Create(string label, Action<float> setter, Func<float> getter, float minValue)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/TimedActivatorSetting");
			UQueryExtensions.Q<Label>(visualElement, "Text", null).text = label;
			FloatField inputField = UQueryExtensions.Q<FloatField>(visualElement, "Value", null);
			TextFields.InitializeFloatField(inputField, minValue, minValue, float.MaxValue, delegate(float value)
			{
				TimedActivatorSettingFactory.OnInputValueChanged(setter, value, inputField);
			});
			inputField.isDelayed = true;
			return new TimedActivatorSetting(this._devModeManager, this._mapEditorMode, visualElement, inputField, getter);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023BC File Offset: 0x000005BC
		public static void OnInputValueChanged(Action<float> setter, float value, FloatField inputField)
		{
			setter(value);
			inputField.Blur();
		}

		// Token: 0x04000017 RID: 23
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000018 RID: 24
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000019 RID: 25
		public readonly MapEditorMode _mapEditorMode;
	}
}
