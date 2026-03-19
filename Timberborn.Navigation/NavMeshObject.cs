using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200005C RID: 92
	public class NavMeshObject
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000645B File Offset: 0x0000465B
		public NavMeshObject(NavMeshUpdater navMeshUpdater, RestrictedNodeUpdater restrictedNodeUpdater)
		{
			this._navMeshUpdater = navMeshUpdater;
			this._restrictedNodeUpdater = restrictedNodeUpdater;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006492 File Offset: 0x00004692
		public void Reset()
		{
			this._addingChanges.Clear();
			this._removingChanges.Clear();
			this._restrictedCoordinates.Clear();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000064B5 File Offset: 0x000046B5
		public void AddEdge(NavMeshEdge navMeshEdge)
		{
			this._addingChanges.Add(new NavMeshChangeSpecification(navMeshEdge, NavMeshChangeType.AddEdge));
			this._removingChanges.Add(new NavMeshChangeSpecification(navMeshEdge, NavMeshChangeType.RemoveEdge));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000064DB File Offset: 0x000046DB
		public void BlockEdge(NavMeshEdge navMeshEdge)
		{
			this._addingChanges.Add(new NavMeshChangeSpecification(navMeshEdge, NavMeshChangeType.BlockEdge));
			this._removingChanges.Add(new NavMeshChangeSpecification(navMeshEdge, NavMeshChangeType.UnblockEdge));
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00006501 File Offset: 0x00004701
		public void AddRestrictedCoordinates(Vector3Int coordinates)
		{
			this._restrictedCoordinates.Add(coordinates);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000650F File Offset: 0x0000470F
		public void EnqueueAddToRegularNavMesh()
		{
			this._navMeshUpdater.EnqueueRegularChanges(this._addingChanges);
			this._restrictedNodeUpdater.EnqueueAddingChange(this._restrictedCoordinates);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006533 File Offset: 0x00004733
		public void EnqueueRemoveFromRegularNavMesh()
		{
			this._navMeshUpdater.EnqueueRegularChanges(this._removingChanges);
			this._restrictedNodeUpdater.EnqueueRemovingChange(this._restrictedCoordinates);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006557 File Offset: 0x00004757
		public void EnqueueAddToPreviewNavMesh()
		{
			if (!this._addedToPreviewNavMesh)
			{
				this._navMeshUpdater.EnqueuePreviewChanges(this._addingChanges);
				this._addedToPreviewNavMesh = true;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006579 File Offset: 0x00004779
		public void EnqueueRemoveFromPreviewNavMesh()
		{
			if (this._addedToPreviewNavMesh)
			{
				this._navMeshUpdater.EnqueuePreviewChanges(this._removingChanges);
				this._addedToPreviewNavMesh = false;
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000659B File Offset: 0x0000479B
		public void AddToPreviewNavMesh()
		{
			if (!this._addedToPreviewNavMesh)
			{
				this._navMeshUpdater.ApplyPreviewChanges(this._addingChanges);
				this._addedToPreviewNavMesh = true;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000065BD File Offset: 0x000047BD
		public void RemoveFromPreviewNavMesh()
		{
			if (this._addedToPreviewNavMesh)
			{
				this._navMeshUpdater.ApplyPreviewChanges(this._removingChanges);
				this._addedToPreviewNavMesh = false;
			}
		}

		// Token: 0x040000C8 RID: 200
		public readonly NavMeshUpdater _navMeshUpdater;

		// Token: 0x040000C9 RID: 201
		public readonly RestrictedNodeUpdater _restrictedNodeUpdater;

		// Token: 0x040000CA RID: 202
		public readonly List<NavMeshChangeSpecification> _addingChanges = new List<NavMeshChangeSpecification>();

		// Token: 0x040000CB RID: 203
		public readonly List<NavMeshChangeSpecification> _removingChanges = new List<NavMeshChangeSpecification>();

		// Token: 0x040000CC RID: 204
		public readonly List<Vector3Int> _restrictedCoordinates = new List<Vector3Int>();

		// Token: 0x040000CD RID: 205
		public bool _addedToPreviewNavMesh;
	}
}
