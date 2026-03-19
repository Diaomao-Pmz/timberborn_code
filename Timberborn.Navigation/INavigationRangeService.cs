using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000031 RID: 49
	public interface INavigationRangeService
	{
		// Token: 0x06000128 RID: 296
		IEnumerable<WeightedCoordinates> GetRoadNodesInRange(Vector3 position);

		// Token: 0x06000129 RID: 297
		IEnumerable<WeightedCoordinates> GetRoadPreviewNodesInRange(Vector3 position);

		// Token: 0x0600012A RID: 298
		IEnumerable<Vector3Int> GetTerrainNodesInRange(Vector3 position);

		// Token: 0x0600012B RID: 299
		IEnumerable<Vector3Int> GetTerrainPreviewNodesInRange(Vector3 position);

		// Token: 0x0600012C RID: 300
		IEnumerable<Vector3Int> GetRoadSpillNodesInRange(Vector3 position);

		// Token: 0x0600012D RID: 301
		IEnumerable<Vector3Int> GetRoadSpillPreviewNodesInRange(Vector3 position);
	}
}
