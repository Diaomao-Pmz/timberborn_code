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
	// Token: 0x0200001B RID: 27
	public class ShadowQualityGraphicsDropdownProvider : IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00003E56 File Offset: 0x00002056
		public ShadowQualityGraphicsDropdownProvider(ILoc loc, GraphicsQualitySettings graphicsQualitySettings)
		{
			this._loc = loc;
			this._graphicsQualitySettings = graphicsQualitySettings;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003E6C File Offset: 0x0000206C
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._valuesFormatted;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003E79 File Offset: 0x00002079
		public string GetValue()
		{
			return this.GetFormattedValue(this._graphicsQualitySettings.ShadowQuality);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003E8C File Offset: 0x0000208C
		public void SetValue(string value)
		{
			this._graphicsQualitySettings.ChangeToCustom();
			this._graphicsQualitySettings.ShadowQuality = ShadowQualityGraphicsDropdownProvider.Values[this._valuesFormatted.IndexOf(value)];
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003EBA File Offset: 0x000020BA
		public void Load()
		{
			this._valuesFormatted = ShadowQualityGraphicsDropdownProvider.Values.Select(new Func<int, string>(this.GetFormattedValue)).ToImmutableArray<string>();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003EE0 File Offset: 0x000020E0
		public string GetFormattedValue(int value)
		{
			string result;
			switch (value)
			{
			case 0:
				result = this._loc.T("Settings.Graphics.Quality.Off");
				break;
			case 1:
				result = this._loc.T("Settings.Graphics.Quality.Low");
				break;
			case 2:
				result = this._loc.T("Settings.Graphics.Quality.Medium");
				break;
			case 3:
				result = this._loc.T("Settings.Graphics.Quality.High");
				break;
			case 4:
				result = this._loc.T("Settings.Graphics.Quality.Ultra");
				break;
			default:
				result = value.ToString();
				break;
			}
			return result;
		}

		// Token: 0x0400008E RID: 142
		public static readonly ImmutableArray<int> Values = new int[]
		{
			0,
			1,
			2,
			3,
			4
		}.ToImmutableArray<int>();

		// Token: 0x0400008F RID: 143
		public readonly ILoc _loc;

		// Token: 0x04000090 RID: 144
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x04000091 RID: 145
		public ImmutableArray<string> _valuesFormatted;
	}
}
