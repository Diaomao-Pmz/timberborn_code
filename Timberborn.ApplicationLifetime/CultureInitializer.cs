using System;
using System.Globalization;
using UnityEngine;

namespace Timberborn.ApplicationLifetime
{
	// Token: 0x02000004 RID: 4
	public static class CultureInitializer
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		[RuntimeInitializeOnLoadMethod(1)]
		public static void Initialize()
		{
			CultureInitializer.SetDefaultCultureToInvariant();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C5 File Offset: 0x000002C5
		public static void SetDefaultCultureToInvariant()
		{
			CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
			CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
		}
	}
}
