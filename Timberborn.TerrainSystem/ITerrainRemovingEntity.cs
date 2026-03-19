using System;
using UnityEngine;

namespace Timberborn.TerrainSystem
{
	// Token: 0x0200000A RID: 10
	public interface ITerrainRemovingEntity
	{
		// Token: 0x0600002A RID: 42
		bool RemovesTerrainAt(Vector3Int coordinates);
	}
}
