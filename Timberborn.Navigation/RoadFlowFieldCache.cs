using System;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x02000077 RID: 119
	public class RoadFlowFieldCache : IPrioritizedSingletonNavMeshListener, IPrioritizedSingletonInstantNavMeshListener
	{
		// Token: 0x0600027C RID: 636 RVA: 0x000086F0 File Offset: 0x000068F0
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			ReadOnlyList<int> roadNodeIds = navMeshUpdate.RoadNodeIds;
			this._defaultFlowField.OnNodesChanged(roadNodeIds);
			this._flowFields.OnNodesChanged(roadNodeIds);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000871D File Offset: 0x0000691D
		public void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._instantFlowField.OnNodesChanged(navMeshUpdate.RoadNodeIds);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00008731 File Offset: 0x00006931
		public PathFlowField GetDefaultFlowField()
		{
			return this._defaultFlowField;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00008739 File Offset: 0x00006939
		public AccessFlowField GetInstantFlowField(int nodeId)
		{
			if (!this._instantFlowField.HasNode(nodeId))
			{
				this._instantFlowField.Clear();
			}
			return this._instantFlowField;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000875A File Offset: 0x0000695A
		public AccessFlowField GetFlowFieldAtNode(int nodeId)
		{
			return this._flowFields.GetFlowFieldAtNode(nodeId);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00008768 File Offset: 0x00006968
		public bool TryGetFlowFieldAtNode(int nodeId, out AccessFlowField flowField)
		{
			return this._flowFields.TryGetFlowFieldAtNode(nodeId, out flowField);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00008777 File Offset: 0x00006977
		public void StartCachingAtNode(int nodeId)
		{
			this._flowFields.StartCachingAtNode(nodeId);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00008785 File Offset: 0x00006985
		public void StopCachingAtNode(int nodeId)
		{
			this._flowFields.StopCachingAtNode(nodeId);
		}

		// Token: 0x0400012F RID: 303
		public readonly FlowFieldCache _flowFields = new FlowFieldCache();

		// Token: 0x04000130 RID: 304
		public readonly PathFlowField _defaultFlowField = new PathFlowField();

		// Token: 0x04000131 RID: 305
		public readonly AccessFlowField _instantFlowField = new AccessFlowField();
	}
}
