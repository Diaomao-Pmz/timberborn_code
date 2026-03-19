using System;
using Timberborn.Coordinates;

namespace Timberborn.GameStartup
{
	// Token: 0x0200000F RID: 15
	public class StartingBuildingPlacedEvent
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000265A File Offset: 0x0000085A
		public Placement Placement { get; }

		// Token: 0x0600002C RID: 44 RVA: 0x00002662 File Offset: 0x00000862
		public StartingBuildingPlacedEvent(Placement placement)
		{
			this.Placement = placement;
		}
	}
}
