using System;

namespace Timberborn.FeatureToggleSystem
{
	// Token: 0x02000005 RID: 5
	public static class FeatureToggles
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020C5 File Offset: 0x000002C5
		static FeatureToggles()
		{
			FeatureToggleService.InitializeToggles();
		}

		// Token: 0x04000006 RID: 6
		public static readonly bool SteamInEditor;
	}
}
