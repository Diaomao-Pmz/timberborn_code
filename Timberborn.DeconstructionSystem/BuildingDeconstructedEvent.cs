using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.DeconstructionSystem
{
	// Token: 0x02000007 RID: 7
	public class BuildingDeconstructedEvent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public Deconstructible Deconstructible { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public ReadOnlyList<Vector3Int> Coordinates { get; }

		// Token: 0x06000009 RID: 9 RVA: 0x0000210E File Offset: 0x0000030E
		public BuildingDeconstructedEvent(Deconstructible deconstructible, ReadOnlyList<Vector3Int> coordinates)
		{
			this.Deconstructible = deconstructible;
			this.Coordinates = coordinates;
		}
	}
}
