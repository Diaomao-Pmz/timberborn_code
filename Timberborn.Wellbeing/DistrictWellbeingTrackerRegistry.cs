using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.Wellbeing
{
	// Token: 0x02000007 RID: 7
	public class DistrictWellbeingTrackerRegistry : BaseComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public WellbeingTrackerRegistry Registry { get; } = new WellbeingTrackerRegistry();
	}
}
