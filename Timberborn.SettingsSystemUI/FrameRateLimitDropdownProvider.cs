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
	// Token: 0x0200000C RID: 12
	public class FrameRateLimitDropdownProvider : IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000028BB File Offset: 0x00000ABB
		public FrameRateLimitDropdownProvider(ScreenSettings screenSettings, ILoc loc)
		{
			this._screenSettings = screenSettings;
			this._loc = loc;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000028D1 File Offset: 0x00000AD1
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._valuesFormatted;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028DE File Offset: 0x00000ADE
		public void Load()
		{
			this._valuesFormatted = ScreenSettings.FrameRateLimitValues.Select(new Func<int?, string>(this.GetFormattedValue)).ToImmutableArray<string>();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002904 File Offset: 0x00000B04
		public string GetValue()
		{
			int? value = (this._screenSettings.VSyncCount == 0) ? this._screenSettings.FrameRateLimit : null;
			return this.GetFormattedValue(value);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000293C File Offset: 0x00000B3C
		public void SetValue(string value)
		{
			this._screenSettings.FrameRateLimit = ScreenSettings.FrameRateLimitValues[this._valuesFormatted.IndexOf(value)];
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000295F File Offset: 0x00000B5F
		public string GetFormattedValue(int? value)
		{
			if (value == null)
			{
				return this._loc.T(FrameRateLimitDropdownProvider.FrameRateLimitUnlimitedLocKey);
			}
			return this._loc.T<int?>(FrameRateLimitDropdownProvider.FrameRateLimitValueLocKey, value);
		}

		// Token: 0x04000029 RID: 41
		public static readonly string FrameRateLimitValueLocKey = "Settings.Screen.FrameRateLimit.Value";

		// Token: 0x0400002A RID: 42
		public static readonly string FrameRateLimitUnlimitedLocKey = "Settings.Screen.FrameRateLimit.Unlimited";

		// Token: 0x0400002B RID: 43
		public readonly ScreenSettings _screenSettings;

		// Token: 0x0400002C RID: 44
		public readonly ILoc _loc;

		// Token: 0x0400002D RID: 45
		public ImmutableArray<string> _valuesFormatted;
	}
}
