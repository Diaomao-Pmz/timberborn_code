using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x0200000C RID: 12
	public class BoundsNavRangeDrawingService : ILoadableSingleton, ISingletonPreviewNavMeshListener
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002931 File Offset: 0x00000B31
		public BoundsNavRangeDrawingService(INavigationRangeService navigationRangeService, BoundsNavRangeDrawer boundsNavRangeDrawer, EventBus eventBus)
		{
			this._navigationRangeService = navigationRangeService;
			this._boundsNavRangeDrawer = boundsNavRangeDrawer;
			this._eventBus = eventBus;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002959 File Offset: 0x00000B59
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002967 File Offset: 0x00000B67
		[OnEvent]
		public void OnMaxVisibleLevelChanged(MaxVisibleLevelChangedEvent maxVisibleLevelChangedEvent)
		{
			this._fresh = false;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002967 File Offset: 0x00000B67
		public void OnPreviewNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._fresh = false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002970 File Offset: 0x00000B70
		public void DrawRange(Vector3 rangeAreaCenter, bool isPreview, bool drawTerrain, bool drawRoadSpill)
		{
			this.UpdateArea(rangeAreaCenter, isPreview, drawTerrain, drawRoadSpill);
			this._boundsNavRangeDrawer.Draw();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002988 File Offset: 0x00000B88
		public void UpdateArea(Vector3 rangeAreaCenter, bool isPreview, bool drawTerrain, bool drawRoadSpill)
		{
			if (!this._fresh || this._rangeAreaCenter != rangeAreaCenter || this._isPreview != isPreview || this._drawTerrain != drawTerrain || this._drawRoadSpill != drawRoadSpill)
			{
				this._fresh = true;
				this._rangeAreaCenter = rangeAreaCenter;
				this._isPreview = isPreview;
				this._drawTerrain = drawTerrain;
				this._drawRoadSpill = drawRoadSpill;
				this.UpdateAllNodes();
				this.UpdateNavRangeDrawer();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029F7 File Offset: 0x00000BF7
		public void UpdateAllNodes()
		{
			this._terrainNodes.Clear();
			if (this._isPreview)
			{
				this.UpdatePreviewNodes();
				return;
			}
			this.UpdateNodes();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A1C File Offset: 0x00000C1C
		public void UpdatePreviewNodes()
		{
			if (this._drawTerrain)
			{
				this._terrainNodes.AddRange(this._navigationRangeService.GetTerrainPreviewNodesInRange(this._rangeAreaCenter));
				return;
			}
			if (this._drawRoadSpill)
			{
				this._terrainNodes.AddRange(this._navigationRangeService.GetRoadSpillPreviewNodesInRange(this._rangeAreaCenter));
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A74 File Offset: 0x00000C74
		public void UpdateNodes()
		{
			if (this._drawTerrain)
			{
				this._terrainNodes.AddRange(this._navigationRangeService.GetTerrainNodesInRange(this._rangeAreaCenter));
				return;
			}
			if (this._drawRoadSpill)
			{
				this._terrainNodes.AddRange(this._navigationRangeService.GetRoadSpillNodesInRange(this._rangeAreaCenter));
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002ACA File Offset: 0x00000CCA
		public void UpdateNavRangeDrawer()
		{
			if (this._isPreview)
			{
				this._boundsNavRangeDrawer.UpdateAreaPreview(this._terrainNodes);
				return;
			}
			this._boundsNavRangeDrawer.UpdateArea(this._terrainNodes);
		}

		// Token: 0x04000019 RID: 25
		public readonly INavigationRangeService _navigationRangeService;

		// Token: 0x0400001A RID: 26
		public readonly BoundsNavRangeDrawer _boundsNavRangeDrawer;

		// Token: 0x0400001B RID: 27
		public readonly EventBus _eventBus;

		// Token: 0x0400001C RID: 28
		public readonly HashSet<Vector3Int> _terrainNodes = new HashSet<Vector3Int>();

		// Token: 0x0400001D RID: 29
		public bool _fresh;

		// Token: 0x0400001E RID: 30
		public Vector3 _rangeAreaCenter;

		// Token: 0x0400001F RID: 31
		public bool _isPreview;

		// Token: 0x04000020 RID: 32
		public bool _drawTerrain;

		// Token: 0x04000021 RID: 33
		public bool _drawRoadSpill;
	}
}
