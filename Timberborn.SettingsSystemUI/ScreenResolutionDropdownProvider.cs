using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.ScreenSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000017 RID: 23
	public class ScreenResolutionDropdownProvider : IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003614 File Offset: 0x00001814
		public ScreenResolutionDropdownProvider(ScreenSettings screenSettings)
		{
			this._screenSettings = screenSettings;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003623 File Offset: 0x00001823
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._resolutionsFormatted;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003630 File Offset: 0x00001830
		public void Load()
		{
			this._resolutions = ScreenResolutions.AvailableResolutions().Reverse<ScreenResolution>().ToImmutableArray<ScreenResolution>();
			this._resolutionsFormatted = this._resolutions.Select(new Func<ScreenResolution, string>(ScreenResolutionDropdownProvider.GetFormattedResolution)).ToImmutableArray<string>();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003669 File Offset: 0x00001869
		public string GetValue()
		{
			return ScreenResolutionDropdownProvider.GetFormattedResolution(this._screenSettings.ScreenResolution);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000367B File Offset: 0x0000187B
		public void SetValue(string value)
		{
			this._screenSettings.ScreenResolution = this._resolutions[this._resolutionsFormatted.IndexOf(value)];
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000369F File Offset: 0x0000189F
		public static string GetFormattedResolution(ScreenResolution screenResolution)
		{
			return string.Format("{0} {1} {2}", screenResolution.Width, SpecialStrings.SizeSeparator, screenResolution.Height);
		}

		// Token: 0x04000069 RID: 105
		public readonly ScreenSettings _screenSettings;

		// Token: 0x0400006A RID: 106
		public ImmutableArray<ScreenResolution> _resolutions;

		// Token: 0x0400006B RID: 107
		public ImmutableArray<string> _resolutionsFormatted;
	}
}
