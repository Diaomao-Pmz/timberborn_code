using System;
using System.Text;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200004D RID: 77
	public class NavigationDebuggingService : INavigationDebuggingService
	{
		// Token: 0x0600016D RID: 365 RVA: 0x0000537F File Offset: 0x0000357F
		public NavigationDebuggingService(TerrainFlowFieldCache terrainFlowFieldCache, RoadFlowFieldCache roadFlowFieldCache, NodeIdService nodeIdService)
		{
			this._terrainFlowFieldCache = terrainFlowFieldCache;
			this._roadFlowFieldCache = roadFlowFieldCache;
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000539C File Offset: 0x0000359C
		public string InfoAt(Vector3 position)
		{
			if (!this._nodeIdService.Contains(position))
			{
				return "Out of map";
			}
			StringBuilder stringBuilder = new StringBuilder();
			int nodeId = this._nodeIdService.WorldToId(position);
			NavigationDebuggingService.AddGeneralInfo(stringBuilder, nodeId);
			this.AddCachedFlowFieldInfo(stringBuilder, nodeId);
			return stringBuilder.ToString();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000053E5 File Offset: 0x000035E5
		public static void AddGeneralInfo(StringBuilder nodeInfo, int nodeId)
		{
			nodeInfo.Append(string.Format("Node id: {0}", nodeId));
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005400 File Offset: 0x00003600
		public void AddCachedFlowFieldInfo(StringBuilder nodeInfo, int nodeId)
		{
			AccessFlowField accessFlowField;
			bool flag = this._terrainFlowFieldCache.TryGetFlowFieldAtNode(nodeId, out accessFlowField);
			nodeInfo.Append(string.Format("\nCached terrain flow field at position: {0}", flag));
			if (flag)
			{
				nodeInfo.Append(string.Format(", number of nodes: {0}", accessFlowField.NumberOfNodes));
			}
			AccessFlowField accessFlowField2;
			bool flag2 = this._roadFlowFieldCache.TryGetFlowFieldAtNode(nodeId, out accessFlowField2);
			nodeInfo.Append(string.Format("\nCached road flow field at position: {0}", flag2));
			if (flag2)
			{
				nodeInfo.Append(string.Format(", number of nodes: {0}", accessFlowField2.NumberOfNodes));
			}
		}

		// Token: 0x04000087 RID: 135
		public readonly TerrainFlowFieldCache _terrainFlowFieldCache;

		// Token: 0x04000088 RID: 136
		public readonly RoadFlowFieldCache _roadFlowFieldCache;

		// Token: 0x04000089 RID: 137
		public readonly NodeIdService _nodeIdService;
	}
}
