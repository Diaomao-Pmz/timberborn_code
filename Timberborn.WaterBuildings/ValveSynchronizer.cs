using System;
using System.Collections.Generic;
using Timberborn.Automation;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200002C RID: 44
	public class ValveSynchronizer
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000671C File Offset: 0x0000491C
		public ValveSynchronizer(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006741 File Offset: 0x00004941
		public void SynchronizeAllNeighbors(Valve valve)
		{
			this.SynchronizeNeighbors(valve, false);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000674B File Offset: 0x0000494B
		public void SynchronizeWithAllNeighbors(Valve valve)
		{
			if (valve.IsSynchronized)
			{
				this.SynchronizeWithNeighbors(valve, false);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000675D File Offset: 0x0000495D
		public void SynchronizeWithUnfinishedNeighbors(Valve valve)
		{
			if (valve.IsSynchronized)
			{
				this.SynchronizeWithNeighbors(valve, true);
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006770 File Offset: 0x00004970
		public void SynchronizeNeighbors(Valve startingValve, bool unfinishedOnly)
		{
			if (startingValve.IsSynchronized)
			{
				this.EnqueueValve(startingValve);
				while (!this._neighborsQueue.IsEmpty<Valve>())
				{
					Valve valve = this._neighborsQueue.Dequeue();
					BlockObject component = valve.GetComponent<BlockObject>();
					foreach (Vector3Int coordinates in ValveSynchronizer.Neighbors)
					{
						this.SynchronizeNeighbor(valve, component.TransformCoordinates(coordinates), component.Orientation, unfinishedOnly);
					}
				}
				this._visitedNeighbors.Clear();
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000067EC File Offset: 0x000049EC
		public void SynchronizeWithNeighbors(Valve valve, bool unfinishedOnly)
		{
			BlockObject component = valve.GetComponent<BlockObject>();
			foreach (Vector3Int coordinates in ValveSynchronizer.Neighbors)
			{
				Vector3Int coordinates2 = component.TransformCoordinates(coordinates);
				Valve valve2 = this.GetValve(coordinates2, component.Orientation);
				if (valve2 != null)
				{
					this.SynchronizeNeighbors(valve2, unfinishedOnly);
					return;
				}
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006844 File Offset: 0x00004A44
		public void SynchronizeNeighbor(Valve sourceValve, Vector3Int neighborCoords, Orientation orientation, bool unfinishedOnly)
		{
			Valve valve = this.GetValve(neighborCoords, orientation);
			if (valve && !this._visitedNeighbors.Contains(valve))
			{
				BlockObject component = valve.GetComponent<BlockObject>();
				if (!unfinishedOnly || !component.IsFinished)
				{
					valve.SetOutflowLimit(sourceValve.OutflowLimit);
					valve.SetOutflowLimitEnabled(sourceValve.OutflowLimitEnabled);
					valve.SetAutomationOutflowLimit(sourceValve.AutomationOutflowLimit);
					valve.SetAutomationOutflowLimitEnabled(sourceValve.AutomationOutflowLimitEnabled);
					valve.SetReactionSpeed(sourceValve.ReactionSpeed);
					Automatable component2 = sourceValve.GetComponent<Automatable>();
					valve.GetComponent<Automatable>().SetInput(component2.Input);
					this.EnqueueValve(valve);
				}
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000068DE File Offset: 0x00004ADE
		public void EnqueueValve(Valve valve)
		{
			this._neighborsQueue.Enqueue(valve);
			this._visitedNeighbors.Add(valve);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000068FC File Offset: 0x00004AFC
		public Valve GetValve(Vector3Int coordinates, Orientation orientation)
		{
			Valve bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<Valve>(coordinates);
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

		// Token: 0x040000C7 RID: 199
		public static readonly Vector3Int[] Neighbors = new Vector3Int[]
		{
			new Vector3Int(-1, 0, 0),
			new Vector3Int(1, 0, 0)
		};

		// Token: 0x040000C8 RID: 200
		public readonly IBlockService _blockService;

		// Token: 0x040000C9 RID: 201
		public readonly Queue<Valve> _neighborsQueue = new Queue<Valve>();

		// Token: 0x040000CA RID: 202
		public readonly HashSet<Valve> _visitedNeighbors = new HashSet<Valve>();
	}
}
