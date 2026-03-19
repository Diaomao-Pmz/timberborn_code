using System;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x02000085 RID: 133
	public class TerrainFlowFieldCache : IPrioritizedSingletonNavMeshListener
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00009418 File Offset: 0x00007618
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			ReadOnlyList<int> terrainNodeIds = navMeshUpdate.TerrainNodeIds;
			this._defaultFlowField.OnNodesChanged(terrainNodeIds);
			this._flowFields.OnNodesChanged(terrainNodeIds);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00009445 File Offset: 0x00007645
		public PathFlowField GetDefaultFlowField()
		{
			return this._defaultFlowField;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000944D File Offset: 0x0000764D
		public AccessFlowField GetFlowFieldAtNode(int nodeId)
		{
			return this._flowFields.GetFlowFieldAtNode(nodeId);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000945B File Offset: 0x0000765B
		public bool TryGetFlowFieldAtNode(int nodeId, out AccessFlowField flowField)
		{
			return this._flowFields.TryGetFlowFieldAtNode(nodeId, out flowField);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000946A File Offset: 0x0000766A
		public void StartCachingAtNode(int nodeId)
		{
			this._flowFields.StartCachingAtNode(nodeId);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00009478 File Offset: 0x00007678
		public void StopCachingAtNode(int nodeId)
		{
			this._flowFields.StopCachingAtNode(nodeId);
		}

		// Token: 0x04000161 RID: 353
		public readonly FlowFieldCache _flowFields = new FlowFieldCache();

		// Token: 0x04000162 RID: 354
		public readonly PathFlowField _defaultFlowField = new PathFlowField();
	}
}
