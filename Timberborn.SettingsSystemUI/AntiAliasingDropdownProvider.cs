using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Timberborn.DropdownSystem;
using Timberborn.GraphicsQualitySystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000007 RID: 7
	public class AntiAliasingDropdownProvider : IDropdownProvider, ILoadableSingleton
	{
		// Token: 0x06000012 RID: 18 RVA: 0x0000225D File Offset: 0x0000045D
		public AntiAliasingDropdownProvider(ILoc loc, GraphicsQualitySettings graphicsQualitySettings)
		{
			this._loc = loc;
			this._graphicsQualitySettings = graphicsQualitySettings;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002273 File Offset: 0x00000473
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._valuesFormatted;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002280 File Offset: 0x00000480
		public string GetValue()
		{
			return this.GetFormattedValue(this._graphicsQualitySettings.AntiAliasingType);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002293 File Offset: 0x00000493
		public void SetValue(string value)
		{
			this._graphicsQualitySettings.ChangeToCustom();
			this._graphicsQualitySettings.AntiAliasingType = AntiAliasingDropdownProvider.Values[this._valuesFormatted.IndexOf(value)];
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022C1 File Offset: 0x000004C1
		public void Load()
		{
			this._valuesFormatted = AntiAliasingDropdownProvider.Values.Select(new Func<AntialiasingType, string>(this.GetFormattedValue)).ToImmutableArray<string>();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022E4 File Offset: 0x000004E4
		public string GetFormattedValue(AntialiasingType antialiasingType)
		{
			if (antialiasingType == AntialiasingType.Off)
			{
				return this._loc.T("Settings.Graphics.Quality.Off");
			}
			if (antialiasingType - AntialiasingType.FXAA > 4)
			{
				throw new ArgumentOutOfRangeException("antialiasingType", antialiasingType, null);
			}
			return this._loc.T("Settings.Graphics.AntiAliasing." + antialiasingType.ToString());
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002341 File Offset: 0x00000541
		// Note: this type is marked as 'beforefieldinit'.
		static AntiAliasingDropdownProvider()
		{
			AntialiasingType[] array = new AntialiasingType[6];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.CD9A54ED1F18BF97DB08914E280EA7349E11CA2C4885A4D8052552CEBA84208D).FieldHandle);
			AntiAliasingDropdownProvider.Values = ImmutableArray.Create<AntialiasingType>(array);
		}

		// Token: 0x0400000F RID: 15
		public static readonly ImmutableArray<AntialiasingType> Values;

		// Token: 0x04000010 RID: 16
		public readonly ILoc _loc;

		// Token: 0x04000011 RID: 17
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x04000012 RID: 18
		public ImmutableArray<string> _valuesFormatted;
	}
}
