using System;
using System.Collections.Generic;
using Timberborn.Automation;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000015 RID: 21
	public class FloodgateSynchronizer
	{
		// Token: 0x060000CE RID: 206 RVA: 0x0000393D File Offset: 0x00001B3D
		public FloodgateSynchronizer(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003962 File Offset: 0x00001B62
		public void SynchronizeAllNeighbors(Floodgate floodgate)
		{
			this.SynchronizeNeighbors(floodgate, false);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000396C File Offset: 0x00001B6C
		public void SynchronizeWithAllNeighbors(Floodgate floodgate)
		{
			if (floodgate.IsSynchronized)
			{
				this.SynchronizeWithNeighbors(floodgate, false);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000397E File Offset: 0x00001B7E
		public void SynchronizeWithUnfinishedNeighbors(Floodgate floodgate)
		{
			if (floodgate.IsSynchronized)
			{
				this.SynchronizeWithNeighbors(floodgate, true);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003990 File Offset: 0x00001B90
		public void SynchronizeNeighbors(Floodgate startingFloodgate, bool unfinishedOnly)
		{
			if (startingFloodgate.IsSynchronized)
			{
				this.EnqueueFloodgate(startingFloodgate);
				while (!this._neighborsQueue.IsEmpty<Floodgate>())
				{
					Floodgate floodgate = this._neighborsQueue.Dequeue();
					BlockObject component = floodgate.GetComponent<BlockObject>();
					int maxHeight = floodgate.MaxHeight;
					foreach (Vector3Int vector3Int in Deltas.Neighbors4Vector3Int)
					{
						Vector3Int vector3Int2 = component.Coordinates + vector3Int;
						for (int j = 0; j < maxHeight; j++)
						{
							Vector3Int neighborCoords = vector3Int2 + new Vector3Int(0, 0, j);
							this.SynchronizeNeighbor(startingFloodgate, neighborCoords, unfinishedOnly);
						}
					}
				}
				this._visitedNeighbors.Clear();
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003A3C File Offset: 0x00001C3C
		public void SynchronizeWithNeighbors(Floodgate floodgate, bool unfinishedOnly)
		{
			BlockObject component = floodgate.GetComponent<BlockObject>();
			foreach (Vector3Int vector3Int in Deltas.Neighbors4Vector3Int)
			{
				Vector3Int vector3Int2 = component.Coordinates + vector3Int;
				for (int j = 0; j < floodgate.MaxHeight; j++)
				{
					Vector3Int coordinates = vector3Int2 + new Vector3Int(0, 0, j);
					Floodgate bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<Floodgate>(coordinates);
					if (bottomObjectComponentAt != null && bottomObjectComponentAt.IsSynchronized)
					{
						this.SynchronizeNeighbors(bottomObjectComponentAt, unfinishedOnly);
						break;
					}
				}
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public void SynchronizeNeighbor(Floodgate sourceFloodgate, Vector3Int neighborCoords, bool unfinishedOnly)
		{
			Floodgate bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<Floodgate>(neighborCoords);
			if (bottomObjectComponentAt && bottomObjectComponentAt.IsSynchronized && !this._visitedNeighbors.Contains(bottomObjectComponentAt))
			{
				BlockObject component = bottomObjectComponentAt.GetComponent<BlockObject>();
				if (!unfinishedOnly || !component.IsFinished)
				{
					int z = component.Coordinates.z;
					bottomObjectComponentAt.SetHeight(sourceFloodgate.PositionedHeight - (float)z);
					bottomObjectComponentAt.SetAutomationHeight(sourceFloodgate.PositionedAutomationHeight - (float)z);
					Automatable component2 = sourceFloodgate.GetComponent<Automatable>();
					bottomObjectComponentAt.GetComponent<Automatable>().SetInput(component2.Input);
					this.EnqueueFloodgate(bottomObjectComponentAt);
				}
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003B5F File Offset: 0x00001D5F
		public void EnqueueFloodgate(Floodgate floodgate)
		{
			this._neighborsQueue.Enqueue(floodgate);
			this._visitedNeighbors.Add(floodgate);
		}

		// Token: 0x04000045 RID: 69
		public readonly IBlockService _blockService;

		// Token: 0x04000046 RID: 70
		public readonly Queue<Floodgate> _neighborsQueue = new Queue<Floodgate>();

		// Token: 0x04000047 RID: 71
		public readonly HashSet<Floodgate> _visitedNeighbors = new HashSet<Floodgate>();
	}
}
