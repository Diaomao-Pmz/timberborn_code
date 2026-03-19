using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainPhysics;
using Timberborn.TerrainSystem;
using Timberborn.TerrainSystemRendering;
using UnityEngine;

namespace Timberborn.MapEditorBrushesUI
{
	// Token: 0x02000011 RID: 17
	public class TerrainIntegrityService : ILoadableSingleton
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600008F RID: 143 RVA: 0x00003ED0 File Offset: 0x000020D0
		// (remove) Token: 0x06000090 RID: 144 RVA: 0x00003F08 File Offset: 0x00002108
		public event EventHandler<bool> HighlightChanged;

		// Token: 0x06000091 RID: 145 RVA: 0x00003F40 File Offset: 0x00002140
		public TerrainIntegrityService(ITerrainPhysicsService terrainPhysicsService, TerrainHighlightingService terrainHighlightingService, RollingHighlighter rollingHighlighter, EntityService entityService, ITerrainService terrainService, IBlockService blockService, ISpecService specService)
		{
			this._terrainPhysicsService = terrainPhysicsService;
			this._terrainHighlightingService = terrainHighlightingService;
			this._rollingHighlighter = rollingHighlighter;
			this._entityService = entityService;
			this._terrainService = terrainService;
			this._blockService = blockService;
			this._specService = specService;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003FBF File Offset: 0x000021BF
		public void Load()
		{
			this._brushColorSpec = this._specService.GetSingleSpec<BrushColorSpec>();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003FD2 File Offset: 0x000021D2
		public void RemoveViolatingElements(IEnumerable<Vector3Int> blockObjectChanges, IEnumerable<Vector3Int> integrityChanges)
		{
			this.RemoveConflictingObjects(blockObjectChanges);
			this.CleanupCache();
			this._conflictingTerrain.AddRange(this._terrainCheckOutput);
			this._terrainCheckOutput.Clear();
			this.RemoveStackedTerrainAndObjects(integrityChanges);
			this.CleanupCache();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000400C File Offset: 0x0000220C
		public void HighlightViolatingElements(IEnumerable<Vector3Int> blockObjectChanges, IEnumerable<Vector3Int> integrityChanges)
		{
			this.ClearHighlight();
			this.CollectConflictingObjects(blockObjectChanges);
			this.CollectConflictingTerrainAndBlockObjectStack(integrityChanges);
			if (this._conflictingBlockObjects.Count > 0)
			{
				this._rollingHighlighter.HighlightPrimary(this._conflictingBlockObjects, this._brushColorSpec.Objects);
				this._isHighlighted = true;
			}
			if (this._conflictingTerrain.Count > 0)
			{
				this._terrainHighlightingService.UpdateHighlight(this._conflictingTerrain);
				this._isHighlighted = true;
			}
			EventHandler<bool> highlightChanged = this.HighlightChanged;
			if (highlightChanged != null)
			{
				highlightChanged(this, this._isHighlighted);
			}
			this.CleanupCache();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000040A2 File Offset: 0x000022A2
		public void ClearHighlight()
		{
			if (this._isHighlighted)
			{
				this._terrainHighlightingService.ClearHighlight();
				this._rollingHighlighter.UnhighlightAllPrimary();
				this._isHighlighted = false;
				EventHandler<bool> highlightChanged = this.HighlightChanged;
				if (highlightChanged == null)
				{
					return;
				}
				highlightChanged(this, false);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000040DC File Offset: 0x000022DC
		public void RemoveConflictingObjects(IEnumerable<Vector3Int> blocks)
		{
			this.CollectConflictingObjects(blocks);
			this._terrainPhysicsService.GetTerrainAndBlockObjectStack(this._conflictingBlockObjects, this._terrainCheckOutput, this._blockObjectCheckOutput);
			this._conflictingBlockObjects.AddRange(this._blockObjectCheckOutput);
			this._blockObjectCheckOutput.Clear();
			foreach (BlockObject entity in this._conflictingBlockObjects)
			{
				this._entityService.Delete(entity);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004174 File Offset: 0x00002374
		public void CollectConflictingObjects(IEnumerable<Vector3Int> blocks)
		{
			foreach (Vector3Int vector3Int in blocks)
			{
				if (this._blockService.AnyObjectAt(vector3Int))
				{
					this._conflictingBlockObjects.AddRange(this._blockService.GetObjectsAt(vector3Int));
				}
				if (this._blockService.BlockNeedsGroundBelow(vector3Int.Above()))
				{
					this._conflictingBlockObjects.AddRange(this._blockService.GetObjectsAt(vector3Int.Above()));
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004214 File Offset: 0x00002414
		public void RemoveStackedTerrainAndObjects(IEnumerable<Vector3Int> blocks)
		{
			this.CollectConflictingTerrainAndBlockObjectStack(blocks);
			foreach (BlockObject entity in this._conflictingBlockObjects)
			{
				this._entityService.Delete(entity);
			}
			foreach (Vector3Int coordinates in this._conflictingTerrain)
			{
				this._terrainService.UnsetTerrain(coordinates, 1);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000042BC File Offset: 0x000024BC
		public void CollectConflictingTerrainAndBlockObjectStack(IEnumerable<Vector3Int> blocks)
		{
			this._coordinatesCache.AddRange(blocks);
			this._terrainPhysicsService.GetTerrainAndBlockObjectStack(this._coordinatesCache, this._conflictingBlockObjects, this._conflictingTerrain, this._blockObjectCheckOutput);
			this._conflictingBlockObjects.AddRange(this._blockObjectCheckOutput);
			this._blockObjectCheckOutput.Clear();
			this._coordinatesCache.Clear();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000431F File Offset: 0x0000251F
		public void CleanupCache()
		{
			this._conflictingBlockObjects.Clear();
			this._conflictingTerrain.Clear();
		}

		// Token: 0x04000077 RID: 119
		public readonly ITerrainPhysicsService _terrainPhysicsService;

		// Token: 0x04000078 RID: 120
		public readonly TerrainHighlightingService _terrainHighlightingService;

		// Token: 0x04000079 RID: 121
		public readonly RollingHighlighter _rollingHighlighter;

		// Token: 0x0400007A RID: 122
		public readonly EntityService _entityService;

		// Token: 0x0400007B RID: 123
		public readonly ITerrainService _terrainService;

		// Token: 0x0400007C RID: 124
		public readonly IBlockService _blockService;

		// Token: 0x0400007D RID: 125
		public readonly ISpecService _specService;

		// Token: 0x0400007E RID: 126
		public BrushColorSpec _brushColorSpec;

		// Token: 0x0400007F RID: 127
		public readonly List<Vector3Int> _coordinatesCache = new List<Vector3Int>();

		// Token: 0x04000080 RID: 128
		public readonly HashSet<BlockObject> _conflictingBlockObjects = new HashSet<BlockObject>();

		// Token: 0x04000081 RID: 129
		public readonly HashSet<BlockObject> _blockObjectCheckOutput = new HashSet<BlockObject>();

		// Token: 0x04000082 RID: 130
		public readonly HashSet<Vector3Int> _conflictingTerrain = new HashSet<Vector3Int>();

		// Token: 0x04000083 RID: 131
		public readonly HashSet<Vector3Int> _terrainCheckOutput = new HashSet<Vector3Int>();

		// Token: 0x04000084 RID: 132
		public bool _isHighlighted;
	}
}
