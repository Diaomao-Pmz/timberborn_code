using System;
using System.Collections.Generic;
using Timberborn.Automation;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200000A RID: 10
	public class FillValveSynchronizer
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public FillValveSynchronizer(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002CE9 File Offset: 0x00000EE9
		public void SynchronizeAllNeighbors(FillValve fillValve)
		{
			this.SynchronizeNeighbors(fillValve, false);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002CF3 File Offset: 0x00000EF3
		public void SynchronizeWithAllNeighbors(FillValve fillValve)
		{
			if (fillValve.IsSynchronized)
			{
				this.SynchronizeWithNeighbors(fillValve, false);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D05 File Offset: 0x00000F05
		public void SynchronizeWithUnfinishedNeighbors(FillValve fillValve)
		{
			if (fillValve.IsSynchronized)
			{
				this.SynchronizeWithNeighbors(fillValve, true);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D18 File Offset: 0x00000F18
		public void SynchronizeNeighbors(FillValve startingFillValve, bool unfinishedOnly)
		{
			if (startingFillValve.IsSynchronized)
			{
				this.EnqueueValve(startingFillValve);
				while (!this._neighborsQueue.IsEmpty<FillValve>())
				{
					FillValve fillValve = this._neighborsQueue.Dequeue();
					BlockObject component = fillValve.GetComponent<BlockObject>();
					foreach (Vector3Int coordinates in FillValveSynchronizer.Neighbors)
					{
						this.SynchronizeNeighbor(fillValve, component.TransformCoordinates(coordinates), component.Orientation, unfinishedOnly);
					}
				}
				this._visitedNeighbors.Clear();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D94 File Offset: 0x00000F94
		public void SynchronizeWithNeighbors(FillValve fillValve, bool unfinishedOnly)
		{
			BlockObject component = fillValve.GetComponent<BlockObject>();
			foreach (Vector3Int coordinates in FillValveSynchronizer.Neighbors)
			{
				Vector3Int coordinates2 = component.TransformCoordinates(coordinates);
				FillValve valve = this.GetValve(coordinates2, component.Orientation);
				if (valve != null)
				{
					this.SynchronizeNeighbors(valve, unfinishedOnly);
					return;
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002DEC File Offset: 0x00000FEC
		public void SynchronizeNeighbor(FillValve sourceFillValve, Vector3Int neighborCoords, Orientation orientation, bool unfinishedOnly)
		{
			FillValve valve = this.GetValve(neighborCoords, orientation);
			if (valve && !this._visitedNeighbors.Contains(valve))
			{
				BlockObject component = valve.GetComponent<BlockObject>();
				if (!unfinishedOnly || !component.IsFinished)
				{
					valve.SetTargetHeightEnabled(sourceFillValve.TargetHeightEnabled);
					valve.SetTargetHeight(sourceFillValve.TargetHeight);
					valve.SetAutomationTargetHeightEnabled(sourceFillValve.AutomationTargetHeightEnabled);
					valve.SetAutomationTargetHeight(sourceFillValve.AutomationTargetHeight);
					Automatable component2 = sourceFillValve.GetComponent<Automatable>();
					valve.GetComponent<Automatable>().SetInput(component2.Input);
					this.EnqueueValve(valve);
				}
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E7A File Offset: 0x0000107A
		public void EnqueueValve(FillValve fillValve)
		{
			this._neighborsQueue.Enqueue(fillValve);
			this._visitedNeighbors.Add(fillValve);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E98 File Offset: 0x00001098
		public FillValve GetValve(Vector3Int coordinates, Orientation orientation)
		{
			FillValve bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<FillValve>(coordinates);
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

		// Token: 0x04000027 RID: 39
		public static readonly Vector3Int[] Neighbors = new Vector3Int[]
		{
			new Vector3Int(-1, 0, 0),
			new Vector3Int(1, 0, 0)
		};

		// Token: 0x04000028 RID: 40
		public readonly IBlockService _blockService;

		// Token: 0x04000029 RID: 41
		public readonly Queue<FillValve> _neighborsQueue = new Queue<FillValve>();

		// Token: 0x0400002A RID: 42
		public readonly HashSet<FillValve> _visitedNeighbors = new HashSet<FillValve>();
	}
}
