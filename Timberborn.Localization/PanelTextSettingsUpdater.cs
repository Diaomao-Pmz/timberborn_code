using System;
using System.Collections.Generic;
using Timberborn.AssetSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace Timberborn.Localization
{
	// Token: 0x02000018 RID: 24
	public class PanelTextSettingsUpdater
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003248 File Offset: 0x00001448
		public PanelTextSettingsUpdater(IAssetLoader assetLoader)
		{
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003258 File Offset: 0x00001458
		public void Update(string languageCode)
		{
			List<FontAsset> fallbackFontAssets = this._assetLoader.Load<PanelTextSettings>(PanelTextSettingsUpdater.PanelTextSettingsPath).fallbackFontAssets;
			fallbackFontAssets.Clear();
			if (languageCode == LocalizationCodes.Japanese)
			{
				this.AddFontsInJapaneseOrder(fallbackFontAssets);
				return;
			}
			if (languageCode == LocalizationCodes.Korean)
			{
				this.AddFontsInKoreanOrder(fallbackFontAssets);
				return;
			}
			if (languageCode == LocalizationCodes.SimplifiedChinese)
			{
				this.AddFontsInSimplifiedChineseOrder(fallbackFontAssets);
				return;
			}
			if (languageCode == LocalizationCodes.TraditionalChinese)
			{
				this.AddFontsInTraditionalChineseOrder(fallbackFontAssets);
				return;
			}
			if (languageCode == LocalizationCodes.Thai)
			{
				this.AddFontsInThaiOrder(fallbackFontAssets);
				return;
			}
			this.AddFontsInDefaultOrder(fallbackFontAssets);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000032F1 File Offset: 0x000014F1
		public void AddFontsInDefaultOrder(List<FontAsset> fallbackFontAssets)
		{
			this.AddFontsInJapaneseOrder(fallbackFontAssets);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000032FA File Offset: 0x000014FA
		public void AddFontsInJapaneseOrder(List<FontAsset> fallbackFontAssets)
		{
			this.AddDefaultStaticFonts(fallbackFontAssets);
			this.AddFontsInJapaneseOrder(fallbackFontAssets, PanelTextSettingsUpdater.StaticKeyword);
			this.AddDefaultDynamicFonts(fallbackFontAssets);
			this.AddFontsInJapaneseOrder(fallbackFontAssets, PanelTextSettingsUpdater.DynamicKeyword);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003322 File Offset: 0x00001522
		public void AddFontsInKoreanOrder(List<FontAsset> fallbackFontAssets)
		{
			this.AddDefaultStaticFonts(fallbackFontAssets);
			this.AddFontsInKoreanOrder(fallbackFontAssets, PanelTextSettingsUpdater.StaticKeyword);
			this.AddDefaultDynamicFonts(fallbackFontAssets);
			this.AddFontsInKoreanOrder(fallbackFontAssets, PanelTextSettingsUpdater.DynamicKeyword);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000334A File Offset: 0x0000154A
		public void AddFontsInSimplifiedChineseOrder(List<FontAsset> fallbackFontAssets)
		{
			this.AddDefaultStaticFonts(fallbackFontAssets);
			this.AddFontsInSimplifiedChineseOrder(fallbackFontAssets, PanelTextSettingsUpdater.StaticKeyword);
			this.AddDefaultDynamicFonts(fallbackFontAssets);
			this.AddFontsInSimplifiedChineseOrder(fallbackFontAssets, PanelTextSettingsUpdater.DynamicKeyword);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003372 File Offset: 0x00001572
		public void AddFontsInTraditionalChineseOrder(List<FontAsset> fallbackFontAssets)
		{
			this.AddDefaultStaticFonts(fallbackFontAssets);
			this.AddFontsInTraditionalChineseOrder(fallbackFontAssets, PanelTextSettingsUpdater.StaticKeyword);
			this.AddDefaultDynamicFonts(fallbackFontAssets);
			this.AddFontsInTraditionalChineseOrder(fallbackFontAssets, PanelTextSettingsUpdater.DynamicKeyword);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000339A File Offset: 0x0000159A
		public void AddFontsInThaiOrder(List<FontAsset> fallbackFontAssets)
		{
			this.AddDefaultStaticFonts(fallbackFontAssets);
			this.AddFontsInThaiOrder(fallbackFontAssets, PanelTextSettingsUpdater.StaticKeyword);
			this.AddDefaultDynamicFonts(fallbackFontAssets);
			this.AddFontsInThaiOrder(fallbackFontAssets, PanelTextSettingsUpdater.DynamicKeyword);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000033C2 File Offset: 0x000015C2
		public void AddDefaultStaticFonts(List<FontAsset> fallbackFontAssets)
		{
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.RegularName, PanelTextSettingsUpdater.StaticKeyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.SymbolsName, PanelTextSettingsUpdater.StaticKeyword);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000033E6 File Offset: 0x000015E6
		public void AddDefaultDynamicFonts(List<FontAsset> fallbackFontAssets)
		{
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.MediumName, PanelTextSettingsUpdater.DynamicKeyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.RegularName, PanelTextSettingsUpdater.DynamicKeyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.SymbolsName, PanelTextSettingsUpdater.DynamicKeyword);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000341C File Offset: 0x0000161C
		public void AddFontsInJapaneseOrder(List<FontAsset> fallbackFontAssets, string keyword)
		{
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.JapaneseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.KoreanName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.SimplifiedChineseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.TraditionalChineseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.ThaiName, keyword);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000346C File Offset: 0x0000166C
		public void AddFontsInKoreanOrder(List<FontAsset> fallbackFontAssets, string keyword)
		{
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.KoreanName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.JapaneseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.SimplifiedChineseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.TraditionalChineseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.ThaiName, keyword);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000034BC File Offset: 0x000016BC
		public void AddFontsInSimplifiedChineseOrder(List<FontAsset> fallbackFontAssets, string keyword)
		{
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.SimplifiedChineseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.TraditionalChineseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.JapaneseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.KoreanName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.ThaiName, keyword);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000350C File Offset: 0x0000170C
		public void AddFontsInTraditionalChineseOrder(List<FontAsset> fallbackFontAssets, string keyword)
		{
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.TraditionalChineseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.SimplifiedChineseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.JapaneseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.KoreanName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.ThaiName, keyword);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000355C File Offset: 0x0000175C
		public void AddFontsInThaiOrder(List<FontAsset> fallbackFontAssets, string keyword)
		{
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.ThaiName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.JapaneseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.KoreanName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.SimplifiedChineseName, keyword);
			this.Add(fallbackFontAssets, PanelTextSettingsUpdater.TraditionalChineseName, keyword);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000035AA File Offset: 0x000017AA
		public void Add(List<FontAsset> fallbackFontAssets, string name, string type)
		{
			fallbackFontAssets.Add(this._assetLoader.Load<FontAsset>("UI/Fonts/" + name + type));
		}

		// Token: 0x04000051 RID: 81
		public static readonly string PanelTextSettingsPath = "UI/Fonts/PanelTextSettings";

		// Token: 0x04000052 RID: 82
		public static readonly string StaticKeyword = " - Static";

		// Token: 0x04000053 RID: 83
		public static readonly string DynamicKeyword = " - Dynamic";

		// Token: 0x04000054 RID: 84
		public static readonly string MediumName = "NotoSansDisplay-Medium SDF";

		// Token: 0x04000055 RID: 85
		public static readonly string RegularName = "NotoSans-Regular SDF";

		// Token: 0x04000056 RID: 86
		public static readonly string SymbolsName = "NotoSansSymbols2-Regular";

		// Token: 0x04000057 RID: 87
		public static readonly string JapaneseName = "NotoSansJP-Regular SDF";

		// Token: 0x04000058 RID: 88
		public static readonly string KoreanName = "NotoSansKR-Regular SDF";

		// Token: 0x04000059 RID: 89
		public static readonly string SimplifiedChineseName = "NotoSansSC-Regular SDF";

		// Token: 0x0400005A RID: 90
		public static readonly string TraditionalChineseName = "NotoSansTC-Regular SDF";

		// Token: 0x0400005B RID: 91
		public static readonly string ThaiName = "NotoSansTH-Medium SDF";

		// Token: 0x0400005C RID: 92
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400005D RID: 93
		public PanelTextSettings _panelTextSettings;
	}
}
