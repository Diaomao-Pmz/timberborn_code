using System;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x0200000E RID: 14
	public class GameSavingSettingsController
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002A69 File Offset: 0x00000C69
		public GameSavingSettingsController(GameSavingSettings gameSavingSetting)
		{
			this._gameSavingSetting = gameSavingSetting;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A78 File Offset: 0x00000C78
		public void Initialize(VisualElement root)
		{
			this._autoSavingOnToggle = UQueryExtensions.Q<Toggle>(root, "AutoSavingOn", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._autoSavingOnToggle, delegate(ChangeEvent<bool> v)
			{
				this._gameSavingSetting.AutoSavingOn = v.newValue;
			});
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public void Update()
		{
			this._autoSavingOnToggle.SetValueWithoutNotify(this._gameSavingSetting.AutoSavingOn);
		}

		// Token: 0x04000031 RID: 49
		public readonly GameSavingSettings _gameSavingSetting;

		// Token: 0x04000032 RID: 50
		public Toggle _autoSavingOnToggle;
	}
}
