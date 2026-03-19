using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.DropdownSystem;
using Timberborn.GraphicsQualitySystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x0200000F RID: 15
	public class GraphicsQualityDropdownProvider : IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002ACF File Offset: 0x00000CCF
		public GraphicsQualityDropdownProvider(ILoc loc, GraphicsQualitySettings graphicsQualitySettings)
		{
			this._loc = loc;
			this._graphicsQualitySettings = graphicsQualitySettings;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002AE5 File Offset: 0x00000CE5
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._valuesFormatted;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002AF2 File Offset: 0x00000CF2
		public string GetValue()
		{
			return this.GetFormattedValue(this._graphicsQualitySettings.OverallGraphicsQuality);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B05 File Offset: 0x00000D05
		public void SetValue(string value)
		{
			this._graphicsQualitySettings.OverallGraphicsQuality = GraphicsQualityDropdownProvider.Values[this._valuesFormatted.IndexOf(value)];
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B28 File Offset: 0x00000D28
		public void Load()
		{
			this._valuesFormatted = GraphicsQualityDropdownProvider.Values.Select(new Func<string, string>(this.GetFormattedValue)).ToImmutableArray<string>();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B4B File Offset: 0x00000D4B
		public string GetFormattedValue(string value)
		{
			return this._loc.T("Settings.Graphics.Quality." + value);
		}

		// Token: 0x04000033 RID: 51
		public static readonly ImmutableArray<string> Values = new string[]
		{
			"Low",
			"Medium",
			"High",
			"Ultra",
			"Custom"
		}.ToImmutableArray<string>();

		// Token: 0x04000034 RID: 52
		public readonly ILoc _loc;

		// Token: 0x04000035 RID: 53
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x04000036 RID: 54
		public ImmutableArray<string> _valuesFormatted;
	}
}
