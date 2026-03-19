using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000008 RID: 8
	public interface ICutoutTilesProvider
	{
		// Token: 0x06000029 RID: 41
		IEnumerable<Vector3Int> GetPositionedCutoutTiles();
	}
}
