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
	// Token: 0x02000022 RID: 34
	public class WaterQualityDropdownProvider : IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00004773 File Offset: 0x00002973
		public WaterQualityDropdownProvider(ILoc loc, GraphicsQualitySettings graphicsQualitySettings)
		{
			this._loc = loc;
			this._graphicsQualitySettings = graphicsQualitySettings;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004789 File Offset: 0x00002989
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._valuesFormatted;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004796 File Offset: 0x00002996
		public string GetValue()
		{
			return this.GetFormatedValue(this._graphicsQualitySettings.WaterQuality);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000047A9 File Offset: 0x000029A9
		public void SetValue(string value)
		{
			this._graphicsQualitySettings.ChangeToCustom();
			this._graphicsQualitySettings.WaterQuality = WaterQualityDropdownProvider.Values[this._valuesFormatted.IndexOf(value)];
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000047D7 File Offset: 0x000029D7
		public void Load()
		{
			this._valuesFormatted = WaterQualityDropdownProvider.Values.Select(new Func<int, string>(this.GetFormatedValue)).ToImmutableArray<string>();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000047FC File Offset: 0x000029FC
		public string GetFormatedValue(int value)
		{
			string result;
			if (value != 0)
			{
				if (value != 1)
				{
					throw new ArgumentOutOfRangeException("value", value, null);
				}
				result = this._loc.T(WaterQualityDropdownProvider.HighQualityLocKey);
			}
			else
			{
				result = this._loc.T(WaterQualityDropdownProvider.LowQualityLocKey);
			}
			return result;
		}

		// Token: 0x040000B6 RID: 182
		public static readonly string LowQualityLocKey = "Settings.Graphics.Quality.Low";

		// Token: 0x040000B7 RID: 183
		public static readonly string HighQualityLocKey = "Settings.Graphics.Quality.High";

		// Token: 0x040000B8 RID: 184
		public static readonly ImmutableArray<int> Values = new int[]
		{
			0,
			1
		}.ToImmutableArray<int>();

		// Token: 0x040000B9 RID: 185
		public readonly ILoc _loc;

		// Token: 0x040000BA RID: 186
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x040000BB RID: 187
		public ImmutableArray<string> _valuesFormatted;
	}
}
