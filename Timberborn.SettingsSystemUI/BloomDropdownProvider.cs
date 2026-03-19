using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.DropdownSystem;
using Timberborn.GraphicsQualitySystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000008 RID: 8
	public class BloomDropdownProvider : IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000235E File Offset: 0x0000055E
		public BloomDropdownProvider(ILoc loc, GraphicsQualitySettings graphicsQualitySettings)
		{
			this._loc = loc;
			this._graphicsQualitySettings = graphicsQualitySettings;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002374 File Offset: 0x00000574
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._valuesFormatted;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002381 File Offset: 0x00000581
		public string GetValue()
		{
			return this._valuesFormatted[this._graphicsQualitySettings.BloomEnabled ? 1 : 0];
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000239F File Offset: 0x0000059F
		public void SetValue(string value)
		{
			this._graphicsQualitySettings.ChangeToCustom();
			this._graphicsQualitySettings.BloomEnabled = (value == this._valuesFormatted[1]);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023C9 File Offset: 0x000005C9
		public void Load()
		{
			this._valuesFormatted = new string[]
			{
				this._loc.T(BloomDropdownProvider.OffLocKey),
				this._loc.T(BloomDropdownProvider.OnLocKey)
			}.ToImmutableArray<string>();
		}

		// Token: 0x04000013 RID: 19
		public static readonly string OffLocKey = "Settings.Graphics.Quality.Off";

		// Token: 0x04000014 RID: 20
		public static readonly string OnLocKey = "Settings.Graphics.Quality.On";

		// Token: 0x04000015 RID: 21
		public readonly ILoc _loc;

		// Token: 0x04000016 RID: 22
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x04000017 RID: 23
		public ImmutableArray<string> _valuesFormatted;
	}
}
