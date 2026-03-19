using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200002E RID: 46
	public interface INavigationCachingService
	{
		// Token: 0x06000123 RID: 291
		void StartCachingRoadFlowField(Vector3Int coordinates);

		// Token: 0x06000124 RID: 292
		void StopCachingRoadFlowField(Vector3Int coordinates);

		// Token: 0x06000125 RID: 293
		void StartCachingTerrainFlowField(Vector3Int coordinates);

		// Token: 0x06000126 RID: 294
		void StopCachingTerrainFlowField(Vector3Int coordinates);
	}
}
