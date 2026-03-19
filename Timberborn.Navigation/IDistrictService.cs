using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200002B RID: 43
	public interface IDistrictService
	{
		// Token: 0x06000111 RID: 273
		District AddDistrict(Vector3Int centerCoordinates);

		// Token: 0x06000112 RID: 274
		void RemoveDistrict(District district);

		// Token: 0x06000113 RID: 275
		District AddPreviewDistrict(Vector3Int centerCoordinates);

		// Token: 0x06000114 RID: 276
		void RemovePreviewDistrict(District district);

		// Token: 0x06000115 RID: 277
		void SetObstacle(Vector3Int coordinates);

		// Token: 0x06000116 RID: 278
		void UnsetObstacle(Vector3Int coordinates);

		// Token: 0x06000117 RID: 279
		void SetPreviewObstacle(Vector3Int coordinates);

		// Token: 0x06000118 RID: 280
		void UnsetPreviewObstacle(Vector3Int coordinates);

		// Token: 0x06000119 RID: 281
		bool IsPreviewDistrictInConflict(Vector3Int? previewDistrictCenter);

		// Token: 0x0600011A RID: 282
		bool DistrictIsGloballyReachable(District district, Vector3 start);

		// Token: 0x0600011B RID: 283
		bool IsOnDistrictRoad(District district, Vector3 road);

		// Token: 0x0600011C RID: 284
		bool IsOnInstantDistrictRoad(District district, Vector3 road);

		// Token: 0x0600011D RID: 285
		bool IsOnPreviewDistrictRoad(District district, Vector3 road);

		// Token: 0x0600011E RID: 286
		bool IsOnInstantDistrictRoadSpill(Accessible accessible);

		// Token: 0x0600011F RID: 287
		bool IsOnInstantDistrictRoadSpill(Vector3 position);

		// Token: 0x06000120 RID: 288
		Vector3 GetRandomDestinationInDistrict(District district, Vector3 coordinates);
	}
}
