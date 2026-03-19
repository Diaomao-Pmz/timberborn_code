using System;
using UnityEngine;

namespace Timberborn.PlatformUtilities
{
	// Token: 0x02000004 RID: 4
	public static class ApplicationPlatform
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public static bool IsMacOS()
		{
			RuntimePlatform platform = Application.platform;
			return platform == null || platform == 1;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E4 File Offset: 0x000002E4
		public static bool IsWindows()
		{
			RuntimePlatform platform = Application.platform;
			return platform == 7 || platform == 2;
		}
	}
}
