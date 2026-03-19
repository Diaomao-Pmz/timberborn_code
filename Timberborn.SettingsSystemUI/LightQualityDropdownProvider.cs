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
	// Token: 0x02000015 RID: 21
	public class LightQualityDropdownProvider : IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003456 File Offset: 0x00001656
		public LightQualityDropdownProvider(ILoc loc, GraphicsQualitySettings graphicsQualitySettings)
		{
			this._loc = loc;
			this._graphicsQualitySettings = graphicsQualitySettings;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000346C File Offset: 0x0000166C
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._valuesFormatted;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003479 File Offset: 0x00001679
		public string GetValue()
		{
			return this.GetFormatedValue(this._graphicsQualitySettings.LightQuality);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000348C File Offset: 0x0000168C
		public void SetValue(string value)
		{
			this._graphicsQualitySettings.ChangeToCustom();
			this._graphicsQualitySettings.LightQuality = LightQualityDropdownProvider.Values[this._valuesFormatted.IndexOf(value)];
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000034BA File Offset: 0x000016BA
		public void Load()
		{
			this._valuesFormatted = LightQualityDropdownProvider.Values.Select(new Func<int, string>(this.GetFormatedValue)).ToImmutableArray<string>();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000034E0 File Offset: 0x000016E0
		public string GetFormatedValue(int value)
		{
			string result;
			if (value != 0)
			{
				switch (value)
				{
				case 4:
					return this._loc.T("Settings.Graphics.Quality.Low");
				case 6:
					return this._loc.T("Settings.Graphics.Quality.Medium");
				case 8:
					return this._loc.T("Settings.Graphics.Quality.High");
				}
				result = value.ToString();
			}
			else
			{
				result = this._loc.T("Settings.Graphics.Quality.Off");
			}
			return result;
		}

		// Token: 0x04000062 RID: 98
		public static readonly ImmutableArray<int> Values = new int[]
		{
			0,
			4,
			6,
			8
		}.ToImmutableArray<int>();

		// Token: 0x04000063 RID: 99
		public readonly ILoc _loc;

		// Token: 0x04000064 RID: 100
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x04000065 RID: 101
		public ImmutableArray<string> _valuesFormatted;
	}
}
