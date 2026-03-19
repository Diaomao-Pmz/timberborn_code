using System;
using UnityEngine;

namespace Timberborn.CoreUI
{
	// Token: 0x0200000A RID: 10
	public static class ColorParser
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000228C File Offset: 0x0000048C
		public static bool TryGetColorFromText(string text, out Color color)
		{
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.IndexOf('#');
				if (num != -1 && num + 7 <= text.Length)
				{
					return ColorUtility.TryParseHtmlString(text.Substring(num, 7), ref color);
				}
			}
			color = default(Color);
			return false;
		}
	}
}
