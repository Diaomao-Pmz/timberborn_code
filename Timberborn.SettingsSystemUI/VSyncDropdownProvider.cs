using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.DropdownSystem;
using Timberborn.Localization;
using Timberborn.ScreenSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000021 RID: 33
	public class VSyncDropdownProvider : IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004671 File Offset: 0x00002871
		public VSyncDropdownProvider(ScreenSettings screenSettings, ILoc loc)
		{
			this._screenSettings = screenSettings;
			this._loc = loc;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004687 File Offset: 0x00002887
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._valuesFormatted;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004694 File Offset: 0x00002894
		public void Load()
		{
			this._valuesFormatted = ScreenSettings.VSyncValues.Select(new Func<int, string>(this.GetFormattedValue)).ToImmutableArray<string>();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000046B7 File Offset: 0x000028B7
		public string GetValue()
		{
			return this.GetFormattedValue(this._screenSettings.VSyncCount);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000046CA File Offset: 0x000028CA
		public void SetValue(string value)
		{
			this._screenSettings.VSyncCount = ScreenSettings.VSyncValues[this._valuesFormatted.IndexOf(value)];
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000046F0 File Offset: 0x000028F0
		public string GetFormattedValue(int value)
		{
			string result;
			switch (value)
			{
			case 0:
				result = this._loc.T(VSyncDropdownProvider.VSync0LocKey);
				break;
			case 1:
				result = this._loc.T(VSyncDropdownProvider.VSync1LocKey);
				break;
			case 2:
				result = this._loc.T(VSyncDropdownProvider.VSync2LocKey);
				break;
			default:
				result = value.ToString();
				break;
			}
			return result;
		}

		// Token: 0x040000B0 RID: 176
		public static readonly string VSync0LocKey = "Settings.Screen.VSync.0";

		// Token: 0x040000B1 RID: 177
		public static readonly string VSync1LocKey = "Settings.Screen.VSync.1";

		// Token: 0x040000B2 RID: 178
		public static readonly string VSync2LocKey = "Settings.Screen.VSync.2";

		// Token: 0x040000B3 RID: 179
		public readonly ScreenSettings _screenSettings;

		// Token: 0x040000B4 RID: 180
		public readonly ILoc _loc;

		// Token: 0x040000B5 RID: 181
		public ImmutableArray<string> _valuesFormatted;
	}
}
