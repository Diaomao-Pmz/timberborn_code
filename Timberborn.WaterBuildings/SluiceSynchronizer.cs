using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000022 RID: 34
	public class SluiceSynchronizer
	{
		// Token: 0x06000158 RID: 344 RVA: 0x0000496B File Offset: 0x00002B6B
		public SluiceSynchronizer(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004990 File Offset: 0x00002B90
		public void SynchronizeNeighbors(SluiceState startingSluice)
		{
			this.EnqueueSluice(startingSluice);
			while (!this._neighborsQueue.IsEmpty<SluiceState>())
			{
				SluiceState sluiceState = this._neighborsQueue.Dequeue();
				BlockObject component = sluiceState.GetComponent<BlockObject>();
				foreach (Vector3Int coordinates in SluiceSynchronizer.Neighbors)
				{
					this.SynchronizeNeighbor(sluiceState, component.TransformCoordinates(coordinates), component.Orientation);
				}
			}
			this._visitedNeighbors.Clear();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004A04 File Offset: 0x00002C04
		public void SynchronizeWithNeighbors(SluiceState sluice)
		{
			BlockObject component = sluice.GetComponent<BlockObject>();
			foreach (Vector3Int coordinates in SluiceSynchronizer.Neighbors)
			{
				Vector3Int coordinates2 = component.TransformCoordinates(coordinates);
				SluiceState sluice2 = this.GetSluice(coordinates2, component.Orientation);
				if (sluice2 != null)
				{
					this.SynchronizeNeighbors(sluice2);
					return;
				}
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004A5C File Offset: 0x00002C5C
		public void SynchronizeNeighbor(SluiceState currentState, Vector3Int neighborCoords, Orientation orientation)
		{
			SluiceState sluice = this.GetSluice(neighborCoords, orientation);
			if (sluice && !this._visitedNeighbors.Contains(sluice))
			{
				Sluice component = sluice.GetComponent<Sluice>();
				sluice.SetState(currentState, component.MinHeight - component.MaxHeight);
				this.EnqueueSluice(sluice);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004AAA File Offset: 0x00002CAA
		public void EnqueueSluice(SluiceState sluice)
		{
			this._neighborsQueue.Enqueue(sluice);
			this._visitedNeighbors.Add(sluice);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public SluiceState GetSluice(Vector3Int coordinates, Orientation orientation)
		{
			SluiceState bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<SluiceState>(coordinates);
			if (bottomObjectComponentAt && bottomObjectComponentAt.IsSynchronized)
			{
				BlockObject component = bottomObjectComponentAt.GetComponent<BlockObject>();
				if (component.Orientation == orientation && component.Coordinates == coordinates)
				{
					return bottomObjectComponentAt;
				}
			}
			return null;
		}

		// Token: 0x0400007B RID: 123
		public static readonly Vector3Int[] Neighbors = new Vector3Int[]
		{
			new Vector3Int(-1, 0, 0),
			new Vector3Int(1, 0, 0)
		};

		// Token: 0x0400007C RID: 124
		public readonly IBlockService _blockService;

		// Token: 0x0400007D RID: 125
		public readonly Queue<SluiceState> _neighborsQueue = new Queue<SluiceState>();

		// Token: 0x0400007E RID: 126
		public readonly HashSet<SluiceState> _visitedNeighbors = new HashSet<SluiceState>();
	}
}
