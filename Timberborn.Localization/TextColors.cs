using System;
using UnityEngine;

namespace Timberborn.Localization
{
	// Token: 0x0200001C RID: 28
	public static class TextColors
	{
		// Token: 0x06000096 RID: 150 RVA: 0x0000384C File Offset: 0x00001A4C
		public static string ColorizeText(string text)
		{
			return text.Replace("<GreenHighlight>", "<color=#" + ColorUtility.ToHtmlStringRGB(TextColors.GreenHighlight) + ">").Replace("</GreenHighlight>", "</color>").Replace("<RedHighlight>", "<color=#" + ColorUtility.ToHtmlStringRGB(TextColors.RedHighlight) + ">").Replace("</RedHighlight>", "</color>").Replace("<YellowHighlight>", "<color=#" + ColorUtility.ToHtmlStringRGB(TextColors.YellowHighlight) + ">").Replace("</YellowHighlight>", "</color>");
		}

		// Token: 0x04000066 RID: 102
		public static readonly Color GreenHighlight = new Color(0.35f, 1f, 0.38f);

		// Token: 0x04000067 RID: 103
		public static readonly Color RedHighlight = new Color(1f, 0.3f, 0.3f);

		// Token: 0x04000068 RID: 104
		public static readonly Color YellowHighlight = new Color(1f, 1f, 0.1f);
	}
}
