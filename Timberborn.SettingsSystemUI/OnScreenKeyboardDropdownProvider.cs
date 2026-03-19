using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.DropdownSystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000016 RID: 22
	public class OnScreenKeyboardDropdownProvider : IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003580 File Offset: 0x00001780
		public OnScreenKeyboardDropdownProvider(InputSettings inputSettings, ILoc loc)
		{
			this._inputSettings = inputSettings;
			this._loc = loc;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003596 File Offset: 0x00001796
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._valuesFormatted;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000035A3 File Offset: 0x000017A3
		public string GetValue()
		{
			return this.GetFormattedValue(this._inputSettings.OnScreenKeyboard);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000035B6 File Offset: 0x000017B6
		public void SetValue(string value)
		{
			this._inputSettings.OnScreenKeyboard = InputSettings.OnScreenKeyboardValues[this._valuesFormatted.IndexOf(value)];
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000035D9 File Offset: 0x000017D9
		public void Load()
		{
			this._valuesFormatted = InputSettings.OnScreenKeyboardValues.Select(new Func<string, string>(this.GetFormattedValue)).ToImmutableArray<string>();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000035FC File Offset: 0x000017FC
		public string GetFormattedValue(string value)
		{
			return this._loc.T("Settings.Input.OnScreenKeyboard." + value);
		}

		// Token: 0x04000066 RID: 102
		public readonly InputSettings _inputSettings;

		// Token: 0x04000067 RID: 103
		public readonly ILoc _loc;

		// Token: 0x04000068 RID: 104
		public ImmutableArray<string> _valuesFormatted;
	}
}
