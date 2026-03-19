using System;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.NeedSpecs
{
	// Token: 0x02000012 RID: 18
	public class NeedSpecFormatter : ILoadableSingleton
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000036A0 File Offset: 0x000018A0
		public NeedSpecFormatter(ILoc loc, NeedGroupSpecService needGroupSpecService, ISpecService specService)
		{
			this._loc = loc;
			this._needGroupSpecService = needGroupSpecService;
			this._specService = specService;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000036C0 File Offset: 0x000018C0
		public void Load()
		{
			NeedSpecFormatterSpec singleSpec = this._specService.GetSingleSpec<NeedSpecFormatterSpec>();
			this._positiveHighlightColor = singleSpec.PositiveHighlightColor;
			this._negativeHighlightColor = singleSpec.NegativeHighlightColor;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000036F1 File Offset: 0x000018F1
		public string ColorizeNeedByEffect(NeedSpec needSpec)
		{
			return this.ColorizeByEffect(needSpec.DisplayName.Value ?? "", needSpec);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000370E File Offset: 0x0000190E
		public string FormatNeed(NeedSpec needSpec)
		{
			return this.Colorize(this.NeedDisplayNameWithGroup(needSpec) + SpecialStrings.ArrowUp);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003727 File Offset: 0x00001927
		public string FormatRangedNeed(NeedSpec needSpec, int range)
		{
			return this.FormatNeed(needSpec) + " " + this._loc.T<int>(NeedSpecFormatter.InRangeLocKey, range);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000374B File Offset: 0x0000194B
		public string NeedDisplayNameWithGroup(NeedSpec needSpec)
		{
			return this._needGroupSpecService.GetNeedGroup(needSpec.NeedGroupId).DisplayName.Value + ": " + needSpec.DisplayName.Value;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003780 File Offset: 0x00001980
		public string ColorizeByEffect(string text, NeedSpec needSpec)
		{
			string text2 = ColorUtility.ToHtmlStringRGB(needSpec.IsNeverPositive ? this._negativeHighlightColor : this._positiveHighlightColor);
			return string.Concat(new string[]
			{
				"<color=#",
				text2,
				">",
				text,
				"</color>"
			});
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000037D4 File Offset: 0x000019D4
		public string Colorize(string text)
		{
			string text2 = ColorUtility.ToHtmlStringRGB(this._positiveHighlightColor);
			return string.Concat(new string[]
			{
				"<color=#",
				text2,
				">",
				text,
				"</color>"
			});
		}

		// Token: 0x04000035 RID: 53
		public static readonly string InRangeLocKey = "Needs.InRange";

		// Token: 0x04000036 RID: 54
		public readonly ILoc _loc;

		// Token: 0x04000037 RID: 55
		public readonly NeedGroupSpecService _needGroupSpecService;

		// Token: 0x04000038 RID: 56
		public readonly ISpecService _specService;

		// Token: 0x04000039 RID: 57
		public Color _positiveHighlightColor;

		// Token: 0x0400003A RID: 58
		public Color _negativeHighlightColor;
	}
}
