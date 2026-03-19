using System;

namespace Timberborn.Forestry
{
	// Token: 0x02000017 RID: 23
	public class TreeCuttingAreaChangedEvent
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000300D File Offset: 0x0000120D
		public bool CoordinatesAdded { get; }

		// Token: 0x06000082 RID: 130 RVA: 0x00003015 File Offset: 0x00001215
		public TreeCuttingAreaChangedEvent(bool coordinatesAdded = false)
		{
			this.CoordinatesAdded = coordinatesAdded;
		}
	}
}
