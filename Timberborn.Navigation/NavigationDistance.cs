using System;

namespace Timberborn.Navigation
{
	// Token: 0x0200004E RID: 78
	public class NavigationDistance
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005497 File Offset: 0x00003697
		public float ResourceBuildings { get; } = 20f;

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000549F File Offset: 0x0000369F
		public int DistrictTerrain { get; } = 10;

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000054A7 File Offset: 0x000036A7
		public float LargeDistrictThreshold { get; } = 70f;
	}
}
