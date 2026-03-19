using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000066 RID: 102
	public class NavMeshUpdater : ILoadableSingleton
	{
		// Token: 0x06000217 RID: 535 RVA: 0x00006F00 File Offset: 0x00005100
		public NavMeshUpdater(TerrainNavMeshSource terrainNavMeshSource, PreviewTerrainNavMeshSource previewTerrainNavMeshSource, InstantTerrainNavMeshSource instantTerrainNavMeshSource, RoadNavMeshSource roadNavMeshSource, PreviewRoadNavMeshSource previewRoadNavMeshSource, InstantRoadNavMeshSource instantRoadNavMeshSource, NavMeshChangeFactory navMeshChangeFactory, NavMeshUpdateNotifier navMeshUpdateNotifier, NavMeshUpdateBuilderFactory navMeshUpdateBuilderFactory)
		{
			this._terrainNavMeshSource = terrainNavMeshSource;
			this._previewTerrainNavMeshSource = previewTerrainNavMeshSource;
			this._instantTerrainNavMeshSource = instantTerrainNavMeshSource;
			this._roadNavMeshSource = roadNavMeshSource;
			this._previewRoadNavMeshSource = previewRoadNavMeshSource;
			this._instantRoadNavMeshSource = instantRoadNavMeshSource;
			this._navMeshChangeFactory = navMeshChangeFactory;
			this._navMeshUpdateNotifier = navMeshUpdateNotifier;
			this._navMeshUpdateBuilderFactory = navMeshUpdateBuilderFactory;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006F9A File Offset: 0x0000519A
		public void Load()
		{
			this._previewNavMeshUpdateBuilder = this._navMeshUpdateBuilderFactory.Create();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006FB0 File Offset: 0x000051B0
		public void EnqueueRegularChange(in NavMeshChangeSpecification specification)
		{
			NavMeshChange item = this._navMeshChangeFactory.Create(specification);
			this._enqueuedRegularTerrainChanges.Enqueue(item);
			this._enqueuedInstantTerrainChanges.Enqueue(item);
			if (!specification.NavMeshEdge.IsRoad)
			{
				NavMeshChangeType navMeshChangeType = specification.NavMeshChangeType;
				if (navMeshChangeType != NavMeshChangeType.BlockEdge && navMeshChangeType != NavMeshChangeType.UnblockEdge)
				{
					return;
				}
			}
			this._enqueuedRegularRoadChanges.Enqueue(item);
			this._enqueuedInstantRoadChanges.Enqueue(item);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000701C File Offset: 0x0000521C
		public void EnqueueRegularChanges(IReadOnlyList<NavMeshChangeSpecification> specifications)
		{
			for (int i = 0; i < specifications.Count; i++)
			{
				NavMeshChangeSpecification navMeshChangeSpecification = specifications[i];
				this.EnqueueRegularChange(navMeshChangeSpecification);
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000704C File Offset: 0x0000524C
		public void EnqueuePreviewChange(in NavMeshChangeSpecification specification)
		{
			NavMeshChange item = this._navMeshChangeFactory.Create(specification);
			this._enqueuedPreviewTerrainChanges.Enqueue(item);
			if (!specification.NavMeshEdge.IsRoad)
			{
				NavMeshChangeType navMeshChangeType = specification.NavMeshChangeType;
				if (navMeshChangeType != NavMeshChangeType.BlockEdge && navMeshChangeType != NavMeshChangeType.UnblockEdge)
				{
					return;
				}
			}
			this._enqueuedPreviewRoadChanges.Enqueue(item);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000070A0 File Offset: 0x000052A0
		public void EnqueuePreviewChanges(IReadOnlyList<NavMeshChangeSpecification> specifications)
		{
			for (int i = 0; i < specifications.Count; i++)
			{
				NavMeshChangeSpecification navMeshChangeSpecification = specifications[i];
				this.EnqueuePreviewChange(navMeshChangeSpecification);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000070D0 File Offset: 0x000052D0
		public void ApplyPreviewChanges(IReadOnlyList<NavMeshChangeSpecification> specifications)
		{
			this._previewNavMeshUpdateBuilder.Reset();
			int i = 0;
			while (i < specifications.Count)
			{
				NavMeshChangeSpecification navMeshChangeSpecification = specifications[i];
				NavMeshChange navMeshChange = this._navMeshChangeFactory.Create(navMeshChangeSpecification);
				navMeshChange.Apply(this._previewTerrainNavMeshSource, this._previewNavMeshUpdateBuilder);
				NavMeshChangeType navMeshChangeType = navMeshChangeSpecification.NavMeshChangeType;
				if (navMeshChangeType == NavMeshChangeType.BlockEdge || navMeshChangeType == NavMeshChangeType.UnblockEdge)
				{
					goto IL_6A;
				}
				if (navMeshChangeSpecification.NavMeshEdge.IsRoad)
				{
					navMeshChangeType = navMeshChangeSpecification.NavMeshChangeType;
					if (navMeshChangeType == NavMeshChangeType.AddEdge || navMeshChangeType == NavMeshChangeType.RemoveEdge)
					{
						goto IL_6A;
					}
				}
				IL_7D:
				i++;
				continue;
				IL_6A:
				navMeshChange.Apply(this._previewRoadNavMeshSource, this._previewNavMeshUpdateBuilder);
				goto IL_7D;
			}
			if (!this._previewNavMeshUpdateBuilder.IsEmpty)
			{
				this._navMeshUpdateNotifier.NotifyOfPreviewNavMeshUpdates(this._previewNavMeshUpdateBuilder.Build());
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000718C File Offset: 0x0000538C
		public void ProcessRegularChanges(NavMeshUpdate.Builder navMeshUpdateBuilder)
		{
			if (!this._enqueuedRegularTerrainChanges.IsEmpty<NavMeshChange>())
			{
				while (!this._enqueuedRegularTerrainChanges.IsEmpty<NavMeshChange>())
				{
					this._enqueuedRegularTerrainChanges.Dequeue().Apply(this._terrainNavMeshSource, navMeshUpdateBuilder);
				}
				while (!this._enqueuedRegularRoadChanges.IsEmpty<NavMeshChange>())
				{
					this._enqueuedRegularRoadChanges.Dequeue().Apply(this._roadNavMeshSource, navMeshUpdateBuilder);
				}
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000071F8 File Offset: 0x000053F8
		public void ProcessPreviewChanges(NavMeshUpdate.Builder navMeshUpdateBuilder)
		{
			if (!this._enqueuedPreviewTerrainChanges.IsEmpty<NavMeshChange>())
			{
				while (!this._enqueuedPreviewTerrainChanges.IsEmpty<NavMeshChange>())
				{
					this._enqueuedPreviewTerrainChanges.Dequeue().Apply(this._previewTerrainNavMeshSource, navMeshUpdateBuilder);
				}
				while (!this._enqueuedPreviewRoadChanges.IsEmpty<NavMeshChange>())
				{
					this._enqueuedPreviewRoadChanges.Dequeue().Apply(this._previewRoadNavMeshSource, navMeshUpdateBuilder);
				}
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007264 File Offset: 0x00005464
		public void ProcessInstantChanges(NavMeshUpdate.Builder navMeshUpdateBuilder)
		{
			if (!this._enqueuedInstantTerrainChanges.IsEmpty<NavMeshChange>())
			{
				while (!this._enqueuedInstantTerrainChanges.IsEmpty<NavMeshChange>())
				{
					NavMeshChange navMeshChange = this._enqueuedInstantTerrainChanges.Dequeue();
					navMeshChange.Apply(this._instantTerrainNavMeshSource, navMeshUpdateBuilder);
					navMeshChange.Apply(this._previewTerrainNavMeshSource, navMeshUpdateBuilder);
				}
				while (!this._enqueuedInstantRoadChanges.IsEmpty<NavMeshChange>())
				{
					NavMeshChange navMeshChange2 = this._enqueuedInstantRoadChanges.Dequeue();
					navMeshChange2.Apply(this._instantRoadNavMeshSource, navMeshUpdateBuilder);
					navMeshChange2.Apply(this._previewRoadNavMeshSource, navMeshUpdateBuilder);
				}
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000072EC File Offset: 0x000054EC
		public void TrimExcess()
		{
			this._enqueuedRegularTerrainChanges.TrimExcess();
			this._enqueuedRegularRoadChanges.TrimExcess();
			this._enqueuedInstantTerrainChanges.TrimExcess();
			this._enqueuedInstantRoadChanges.TrimExcess();
			this._enqueuedPreviewTerrainChanges.TrimExcess();
			this._enqueuedPreviewRoadChanges.TrimExcess();
		}

		// Token: 0x040000F2 RID: 242
		public readonly TerrainNavMeshSource _terrainNavMeshSource;

		// Token: 0x040000F3 RID: 243
		public readonly PreviewTerrainNavMeshSource _previewTerrainNavMeshSource;

		// Token: 0x040000F4 RID: 244
		public readonly InstantTerrainNavMeshSource _instantTerrainNavMeshSource;

		// Token: 0x040000F5 RID: 245
		public readonly RoadNavMeshSource _roadNavMeshSource;

		// Token: 0x040000F6 RID: 246
		public readonly PreviewRoadNavMeshSource _previewRoadNavMeshSource;

		// Token: 0x040000F7 RID: 247
		public readonly InstantRoadNavMeshSource _instantRoadNavMeshSource;

		// Token: 0x040000F8 RID: 248
		public readonly NavMeshChangeFactory _navMeshChangeFactory;

		// Token: 0x040000F9 RID: 249
		public readonly NavMeshUpdateNotifier _navMeshUpdateNotifier;

		// Token: 0x040000FA RID: 250
		public readonly NavMeshUpdateBuilderFactory _navMeshUpdateBuilderFactory;

		// Token: 0x040000FB RID: 251
		public readonly Queue<NavMeshChange> _enqueuedRegularTerrainChanges = new Queue<NavMeshChange>();

		// Token: 0x040000FC RID: 252
		public readonly Queue<NavMeshChange> _enqueuedRegularRoadChanges = new Queue<NavMeshChange>();

		// Token: 0x040000FD RID: 253
		public readonly Queue<NavMeshChange> _enqueuedInstantTerrainChanges = new Queue<NavMeshChange>();

		// Token: 0x040000FE RID: 254
		public readonly Queue<NavMeshChange> _enqueuedInstantRoadChanges = new Queue<NavMeshChange>();

		// Token: 0x040000FF RID: 255
		public readonly Queue<NavMeshChange> _enqueuedPreviewTerrainChanges = new Queue<NavMeshChange>();

		// Token: 0x04000100 RID: 256
		public readonly Queue<NavMeshChange> _enqueuedPreviewRoadChanges = new Queue<NavMeshChange>();

		// Token: 0x04000101 RID: 257
		public NavMeshUpdate.Builder _previewNavMeshUpdateBuilder;
	}
}
