using System;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.MapStateSystem;
using UnityEngine.UIElements;

namespace Timberborn.WaterSourceSystemUI
{
	// Token: 0x02000005 RID: 5
	public class WaterSettingFactory
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000215C File Offset: 0x0000035C
		public WaterSettingFactory(VisualElementLoader visualElementLoader, DevModeManager devModeManager, MapEditorMode mapEditorMode)
		{
			this._visualElementLoader = visualElementLoader;
			this._devModeManager = devModeManager;
			this._mapEditorMode = mapEditorMode;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000217C File Offset: 0x0000037C
		public WaterSetting Create(string label, Action<float> setter, Func<float> getter, bool devModeOnly)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/WaterSetting");
			UQueryExtensions.Q<Label>(visualElement, "Text", null).text = label;
			FloatField inputField = UQueryExtensions.Q<FloatField>(visualElement, "Value", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(inputField, delegate(ChangeEvent<float> value)
			{
				setter(value.newValue);
				inputField.SetValueWithoutNotify(getter());
				inputField.Blur();
			});
			inputField.isDelayed = true;
			return new WaterSetting(this._devModeManager, this._mapEditorMode, visualElement, inputField, getter, devModeOnly);
		}

		// Token: 0x0400000C RID: 12
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000D RID: 13
		public readonly DevModeManager _devModeManager;

		// Token: 0x0400000E RID: 14
		public readonly MapEditorMode _mapEditorMode;
	}
}
