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
	// Token: 0x0200001E RID: 30
	public class TextureQualityDropdownProvider : IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x060000AB RID: 171 RVA: 0x000042CD File Offset: 0x000024CD
		public TextureQualityDropdownProvider(ILoc loc, GraphicsQualitySettings graphicsQualitySettings)
		{
			this._loc = loc;
			this._graphicsQualitySettings = graphicsQualitySettings;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000042E3 File Offset: 0x000024E3
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._valuesFormatted;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000042F0 File Offset: 0x000024F0
		public string GetValue()
		{
			return this.GetFormattedValue(this._graphicsQualitySettings.TextureQuality);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004303 File Offset: 0x00002503
		public void SetValue(string value)
		{
			this._graphicsQualitySettings.ChangeToCustom();
			this._graphicsQualitySettings.TextureQuality = TextureQualityDropdownProvider.Values[this._valuesFormatted.IndexOf(value)];
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004331 File Offset: 0x00002531
		public void Load()
		{
			this._valuesFormatted = TextureQualityDropdownProvider.Values.Select(new Func<int, string>(this.GetFormattedValue)).ToImmutableArray<string>();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004354 File Offset: 0x00002554
		public string GetFormattedValue(int value)
		{
			string result;
			switch (value)
			{
			case 0:
				result = this._loc.T("Settings.Graphics.Quality.High");
				break;
			case 1:
				result = this._loc.T("Settings.Graphics.Quality.Medium");
				break;
			case 2:
				result = this._loc.T("Settings.Graphics.Quality.Low");
				break;
			default:
				result = value.ToString();
				break;
			}
			return result;
		}

		// Token: 0x040000A0 RID: 160
		public static readonly ImmutableArray<int> Values = ImmutableArray.Create<int>(2, 1, 0);

		// Token: 0x040000A1 RID: 161
		public readonly ILoc _loc;

		// Token: 0x040000A2 RID: 162
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x040000A3 RID: 163
		public ImmutableArray<string> _valuesFormatted;
	}
}
