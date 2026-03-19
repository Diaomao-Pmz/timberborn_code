using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000040 RID: 64
	public interface IBlockService
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001BB RID: 443
		Vector3Int Size { get; }

		// Token: 0x060001BC RID: 444
		bool AnyObjectAt(Vector3Int coordinates);

		// Token: 0x060001BD RID: 445
		bool AnyTopObjectAt(Vector3Int coordinates);

		// Token: 0x060001BE RID: 446
		bool BlockNeedsGroundBelow(Vector3Int coordinates);

		// Token: 0x060001BF RID: 447
		bool AnyNonOverridableObjectBelow(Vector3Int coordinates);

		// Token: 0x060001C0 RID: 448
		ReadOnlyList<BlockObject> GetObjectsAt(Vector3Int coordinates);

		// Token: 0x060001C1 RID: 449
		IEnumerable<BlockObject> GetStackedObjectsAt(Vector3Int coordinates);

		// Token: 0x060001C2 RID: 450
		IEnumerable<BlockObject> GetStackedObjectsWithUndergroundAt(Vector3Int coordinates);

		// Token: 0x060001C3 RID: 451
		IEnumerable<T> GetObjectsWithComponentAt<T>(Vector3Int coordinates);

		// Token: 0x060001C4 RID: 452
		T GetFirstObjectWithComponentAt<T>(Vector3Int coordinates);

		// Token: 0x060001C5 RID: 453
		void GetIntersectingObjectsAt(Vector3Int coordinates, BlockOccupations occupations, List<BlockObject> result);

		// Token: 0x060001C6 RID: 454
		bool AnyNonOverridableObjectsAt(Vector3Int coordinates, BlockOccupations occupations);

		// Token: 0x060001C7 RID: 455
		BlockObject GetBottomObjectAt(Vector3Int coordinates);

		// Token: 0x060001C8 RID: 456
		BlockObject GetUndergroundObjectAt(Vector3Int coordinates);

		// Token: 0x060001C9 RID: 457
		T GetBottomObjectComponentAt<T>(Vector3Int coordinates);

		// Token: 0x060001CA RID: 458
		T GetPathObjectComponentAt<T>(Vector3Int coordinates);

		// Token: 0x060001CB RID: 459
		T GetMiddleObjectComponentAt<T>(Vector3Int coordinates);

		// Token: 0x060001CC RID: 460
		T GetTopObjectComponentAt<T>(Vector3Int coordinates);

		// Token: 0x060001CD RID: 461
		BlockObject GetPathObjectAt(Vector3Int coordinates);

		// Token: 0x060001CE RID: 462
		Directions2D GetEntrancesAt(Vector3Int coordinates);

		// Token: 0x060001CF RID: 463
		bool Contains(Vector3Int coordinates);

		// Token: 0x060001D0 RID: 464
		bool Contains(Vector2Int coordinates);
	}
}
