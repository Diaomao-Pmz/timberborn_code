using System;
using UnityEngine;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x0200001F RID: 31
	public class HttpApiUrlGenerator
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00004643 File Offset: 0x00002843
		public string SwitchOnLeverUrlPath(string name)
		{
			return "/api/switch-on/" + Uri.EscapeDataString(name);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004655 File Offset: 0x00002855
		public string SwitchOffLeverUrlPath(string name)
		{
			return "/api/switch-off/" + Uri.EscapeDataString(name);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004667 File Offset: 0x00002867
		public string ColorLeverUrlPath(string name, Color color)
		{
			return "/api/color/" + Uri.EscapeDataString(name) + "/" + HttpApiUrlGenerator.FormatColor(color);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004684 File Offset: 0x00002884
		public static string FormatColor(Color color)
		{
			return ColorUtility.ToHtmlStringRGB(color).ToLowerInvariant();
		}
	}
}
