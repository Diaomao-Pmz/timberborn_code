using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200006C RID: 108
	public readonly struct PathRequest
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000824B File Offset: 0x0000644B
		public Vector3 Start { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00008253 File Offset: 0x00006453
		public Vector3 Destination { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000825B File Offset: 0x0000645B
		public bool Reversed { get; }

		// Token: 0x0600025D RID: 605 RVA: 0x00008263 File Offset: 0x00006463
		public PathRequest(Vector3 start, Vector3 destination, bool reversed)
		{
			this.Start = start;
			this.Destination = destination;
			this.Reversed = reversed;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000827A File Offset: 0x0000647A
		public static PathRequest Create(Vector3 start, Vector3 destination)
		{
			return new PathRequest(start, destination, false);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00008284 File Offset: 0x00006484
		public static PathRequest CreateReversed(Vector3 start, Vector3 destination)
		{
			return new PathRequest(start, destination, true);
		}
	}
}
