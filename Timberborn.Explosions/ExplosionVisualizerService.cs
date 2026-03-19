using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystemRendering;
using Timberborn.ToolSystem;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x02000012 RID: 18
	public class ExplosionVisualizerService : ILoadableSingleton
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000035C8 File Offset: 0x000017C8
		public ExplosionVisualizerService(TerrainHighlightingService terrainHighlightingService, ExplosionOutcomeGatherer explosionOutcomeGatherer, RollingHighlighter rollingHighlighter, ISpecService specService, EventBus eventBus)
		{
			this._terrainHighlightingService = terrainHighlightingService;
			this._explosionOutcomeGatherer = explosionOutcomeGatherer;
			this._rollingHighlighter = rollingHighlighter;
			this._specService = specService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003621 File Offset: 0x00001821
		public void Load()
		{
			this._eventBus.Register(this);
			this._spec = this._specService.GetSingleSpec<ExplosionVisualizerSpec>();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003640 File Offset: 0x00001840
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			if (this._unstableCore != null)
			{
				this.ClearSelected(null);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003654 File Offset: 0x00001854
		public void UpdateHighlight(UnstableCore unstableCore)
		{
			this.ClearSelected(this._unstableCore);
			this._unstableCore = unstableCore;
			this._blockObject = this._unstableCore.GetComponent<BlockObject>();
			this.Highlight();
			this._unstableCore.ExplosionRadiusChanged += this.OnExplosionRadiusChanged;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000036A4 File Offset: 0x000018A4
		public void ClearSelected(UnstableCore unstableCore)
		{
			if (this._unstableCore != null && (unstableCore == null || unstableCore == this._unstableCore))
			{
				this.ClearHighlight();
				this._unstableCore.ExplosionRadiusChanged -= this.OnExplosionRadiusChanged;
				this._unstableCore = null;
				this._blockObject = null;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000036F0 File Offset: 0x000018F0
		public void OnExplosionRadiusChanged(object sender, EventArgs e)
		{
			this.Highlight();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000036F8 File Offset: 0x000018F8
		public void Highlight()
		{
			this.ClearHighlight();
			this._explosionOutcomeGatherer.GetAllAffectedTerrainAndObjects(this._unstableCore, this._affectedTiles, this._affectedTerrain, this._affectedObjects);
			this._affectedObjects.Remove(this._blockObject);
			this._terrainHighlightingService.UpdateHighlight(this._affectedTerrain);
			this._rollingHighlighter.HighlightPrimary(this._affectedObjects, this._spec.ObjectHighlightColor);
			this._affectedTiles.Clear();
			this._affectedTerrain.Clear();
			this._affectedObjects.Clear();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000378E File Offset: 0x0000198E
		public void ClearHighlight()
		{
			this._rollingHighlighter.UnhighlightAllPrimary();
			this._terrainHighlightingService.ClearHighlight();
		}

		// Token: 0x0400004A RID: 74
		public readonly TerrainHighlightingService _terrainHighlightingService;

		// Token: 0x0400004B RID: 75
		public readonly ExplosionOutcomeGatherer _explosionOutcomeGatherer;

		// Token: 0x0400004C RID: 76
		public readonly RollingHighlighter _rollingHighlighter;

		// Token: 0x0400004D RID: 77
		public readonly ISpecService _specService;

		// Token: 0x0400004E RID: 78
		public readonly EventBus _eventBus;

		// Token: 0x0400004F RID: 79
		public readonly HashSet<Vector3Int> _affectedTiles = new HashSet<Vector3Int>();

		// Token: 0x04000050 RID: 80
		public readonly HashSet<Vector3Int> _affectedTerrain = new HashSet<Vector3Int>();

		// Token: 0x04000051 RID: 81
		public readonly HashSet<BlockObject> _affectedObjects = new HashSet<BlockObject>();

		// Token: 0x04000052 RID: 82
		public ExplosionVisualizerSpec _spec;

		// Token: 0x04000053 RID: 83
		public UnstableCore _unstableCore;

		// Token: 0x04000054 RID: 84
		public BlockObject _blockObject;
	}
}
