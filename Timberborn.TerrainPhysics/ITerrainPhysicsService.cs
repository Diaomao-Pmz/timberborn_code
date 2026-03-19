using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x02000008 RID: 8
	public interface ITerrainPhysicsService
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7
		ReadOnlyList<Vector3Int> PhysicsSupportDeltas { get; }

		// Token: 0x06000008 RID: 8
		void GetTerrainAndBlockObjectStack(IEnumerable<BlockObject> inputBlockObjects, HashSet<Vector3Int> outputTerrain, HashSet<BlockObject> outputBlockObjects);

		// Token: 0x06000009 RID: 9
		void GetTerrainAndBlockObjectStack(IEnumerable<Vector3Int> inputTerrain, IEnumerable<BlockObject> inputBlockObjects, HashSet<Vector3Int> outputTerrain, HashSet<BlockObject> outputBlockObjects);

		// Token: 0x0600000A RID: 10
		void GetValidTerrainToAdd(ICollection<Vector3Int> inputTerrain, HashSet<Vector3Int> terrainToAdd);

		// Token: 0x0600000B RID: 11
		bool CanBeDestroyed(BlockObject blockObject);

		// Token: 0x0600000C RID: 12
		bool CanTerrainBeAdded(Vector3Int coordinates);

		// Token: 0x0600000D RID: 13
		bool ValidateBlockObjectPreview(BlockObject blockObject);
	}
}
