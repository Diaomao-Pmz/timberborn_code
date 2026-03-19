using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x02000009 RID: 9
	public class SupportsToBeDeleted
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002100 File Offset: 0x00000300
		public void Mark(Vector3Int coordinates)
		{
			this._supportsToBeDeleted.Add(coordinates);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000210F File Offset: 0x0000030F
		public bool IsMarked(Vector3Int coordinates)
		{
			return this._supportsToBeDeleted.Contains(coordinates);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000211D File Offset: 0x0000031D
		public void Clear()
		{
			this._supportsToBeDeleted.Clear();
		}

		// Token: 0x04000008 RID: 8
		public readonly HashSet<Vector3Int> _supportsToBeDeleted = new HashSet<Vector3Int>();
	}
}
