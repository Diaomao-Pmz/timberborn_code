using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.DropdownSystem;
using Timberborn.GraphicsQualitySystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000006 RID: 6
	public class AnisotropicFilteringDropdownProvider : IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021A3 File Offset: 0x000003A3
		public AnisotropicFilteringDropdownProvider(ILoc loc, GraphicsQualitySettings graphicsQualitySettings)
		{
			this._loc = loc;
			this._graphicsQualitySettings = graphicsQualitySettings;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021B9 File Offset: 0x000003B9
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._valuesFormatted;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021C6 File Offset: 0x000003C6
		public string GetValue()
		{
			return this._valuesFormatted[this._graphicsQualitySettings.AnisotropicFilteringEnabled ? 1 : 0];
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021E4 File Offset: 0x000003E4
		public void SetValue(string value)
		{
			this._graphicsQualitySettings.ChangeToCustom();
			this._graphicsQualitySettings.AnisotropicFilteringEnabled = (value == this._valuesFormatted[1]);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000220E File Offset: 0x0000040E
		public void Load()
		{
			this._valuesFormatted = new string[]
			{
				this._loc.T(AnisotropicFilteringDropdownProvider.OffLocKey),
				this._loc.T(AnisotropicFilteringDropdownProvider.OnLocKey)
			}.ToImmutableArray<string>();
		}

		// Token: 0x0400000A RID: 10
		public static readonly string OffLocKey = "Settings.Graphics.Quality.Off";

		// Token: 0x0400000B RID: 11
		public static readonly string OnLocKey = "Settings.Graphics.Quality.On";

		// Token: 0x0400000C RID: 12
		public readonly ILoc _loc;

		// Token: 0x0400000D RID: 13
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x0400000E RID: 14
		public ImmutableArray<string> _valuesFormatted;
	}
}
