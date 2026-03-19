using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000062 RID: 98
	public readonly struct NavMeshUpdate
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00006CAA File Offset: 0x00004EAA
		public BoundingBox Bounds { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00006CB2 File Offset: 0x00004EB2
		public ReadOnlyList<Vector3Int> TerrainCoordinates { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00006CBA File Offset: 0x00004EBA
		internal ReadOnlyList<int> TerrainNodeIds { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00006CC2 File Offset: 0x00004EC2
		internal ReadOnlyList<int> RoadNodeIds { get; }

		// Token: 0x06000209 RID: 521 RVA: 0x00006CCA File Offset: 0x00004ECA
		public NavMeshUpdate(BoundingBox bounds, ReadOnlyList<Vector3Int> terrainCoordinates, ReadOnlyList<int> terrainNodeIds, ReadOnlyList<int> roadNodeIds)
		{
			this.Bounds = bounds;
			this.TerrainCoordinates = terrainCoordinates;
			this.TerrainNodeIds = terrainNodeIds;
			this.RoadNodeIds = roadNodeIds;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00006CEC File Offset: 0x00004EEC
		public bool UpdatedRoads
		{
			get
			{
				return !this.RoadNodeIds.IsEmpty();
			}
		}

		// Token: 0x02000063 RID: 99
		public class Builder
		{
			// Token: 0x0600020B RID: 523 RVA: 0x00006D0C File Offset: 0x00004F0C
			public Builder(NodeIdService nodeIdService)
			{
				this._nodeIdService = nodeIdService;
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x0600020C RID: 524 RVA: 0x00006D5D File Offset: 0x00004F5D
			public bool IsEmpty
			{
				get
				{
					return this._uniqueTerrainNodeIds.IsEmpty<int>() && this._uniqueRoadNodeIds.IsEmpty<int>();
				}
			}

			// Token: 0x0600020D RID: 525 RVA: 0x00006D7C File Offset: 0x00004F7C
			public void Reset()
			{
				this._boundingBoxBuilder = default(BoundingBox.Builder);
				this._uniqueTerrainNodeIds.Clear();
				this._terrainNodeIds.Clear();
				this._terrainCoordinates.Clear();
				this._uniqueRoadNodeIds.Clear();
				this._roadNodeIds.Clear();
			}

			// Token: 0x0600020E RID: 526 RVA: 0x00006DCC File Offset: 0x00004FCC
			public void AddTerrainNode(int nodeId)
			{
				if (this._uniqueTerrainNodeIds.Add(nodeId))
				{
					this._terrainNodeIds.Add(nodeId);
					Vector3Int vector3Int = this._nodeIdService.IdToGrid(nodeId);
					this._terrainCoordinates.Add(vector3Int);
					this._boundingBoxBuilder.Expand(vector3Int);
				}
			}

			// Token: 0x0600020F RID: 527 RVA: 0x00006E18 File Offset: 0x00005018
			public void AddRoadNode(int nodeId)
			{
				if (this._uniqueRoadNodeIds.Add(nodeId))
				{
					this._roadNodeIds.Add(nodeId);
					Vector3Int point = this._nodeIdService.IdToGrid(nodeId);
					this._boundingBoxBuilder.Expand(point);
				}
			}

			// Token: 0x06000210 RID: 528 RVA: 0x00006E58 File Offset: 0x00005058
			public NavMeshUpdate Build()
			{
				return new NavMeshUpdate(this._boundingBoxBuilder.Build(), this._terrainCoordinates.AsReadOnlyList<Vector3Int>(), this._terrainNodeIds.AsReadOnlyList<int>(), this._roadNodeIds.AsReadOnlyList<int>());
			}

			// Token: 0x040000E8 RID: 232
			public readonly NodeIdService _nodeIdService;

			// Token: 0x040000E9 RID: 233
			public readonly HashSet<int> _uniqueTerrainNodeIds = new HashSet<int>();

			// Token: 0x040000EA RID: 234
			public readonly List<int> _terrainNodeIds = new List<int>();

			// Token: 0x040000EB RID: 235
			public readonly List<Vector3Int> _terrainCoordinates = new List<Vector3Int>();

			// Token: 0x040000EC RID: 236
			public readonly HashSet<int> _uniqueRoadNodeIds = new HashSet<int>();

			// Token: 0x040000ED RID: 237
			public readonly List<int> _roadNodeIds = new List<int>();

			// Token: 0x040000EE RID: 238
			public BoundingBox.Builder _boundingBoxBuilder;
		}
	}
}
