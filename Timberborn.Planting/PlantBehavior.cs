using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Coordinates;
using Timberborn.WalkingSystem;
using Timberborn.WorkSystem;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x02000010 RID: 16
	public class PlantBehavior : Behavior, IAwakableComponent, IStartableComponent, IJobBehavior
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002779 File Offset: 0x00000979
		public PlantBehavior(PlantingService plantingService)
		{
			this._plantingService = plantingService;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002788 File Offset: 0x00000988
		public void Awake()
		{
			this._planter = base.GetComponent<Planter>();
			this._worker = base.GetComponent<Worker>();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027A2 File Offset: 0x000009A2
		public void Start()
		{
			this._walkToPositionExecutor = base.GetComponent<WalkToPositionExecutor>();
			this._plantExecutor = base.GetComponent<PlantExecutor>();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027BC File Offset: 0x000009BC
		public Decision StartPlanting(BehaviorAgent agent)
		{
			this.ReserveCoordinates(agent);
			return this.Decide(agent);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000027CC File Offset: 0x000009CC
		public override Decision Decide(BehaviorAgent agent)
		{
			if (this._planter.PlantingCoordinates == null)
			{
				return Decision.ReleaseNow();
			}
			Vector3 position = CoordinateSystem.GridToWorldCentered(this._planter.PlantingCoordinates.Value);
			switch (this._walkToPositionExecutor.Launch(position))
			{
			case ExecutorStatus.Success:
			{
				Vector3Int? plantingCoordinates = this._planter.PlantingCoordinates;
				this._planter.Unreserve();
				return this.Plant(plantingCoordinates.Value);
			}
			case ExecutorStatus.Failure:
				this._planter.Unreserve();
				return Decision.ReleaseNextTick();
			case ExecutorStatus.Running:
				return Decision.ReturnWhenFinished(this._walkToPositionExecutor);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002878 File Offset: 0x00000A78
		public void ReserveCoordinates(BehaviorAgent agent)
		{
			if (this._planter.PlantingCoordinates == null)
			{
				Vector3 position = agent.Transform.position;
				PlantingSpot? plantingSpot = this._worker.Workplace.GetComponent<PlantingSpotFinder>().FindClosest(position);
				if (plantingSpot != null)
				{
					this._planter.Reserve(plantingSpot.Value.Coordinates);
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000028E0 File Offset: 0x00000AE0
		public Decision Plant(Vector3Int coordinates)
		{
			if (!this._plantExecutor.Launch(coordinates, this._plantingService.GetResourceAt(coordinates)))
			{
				return Decision.ReleaseNextTick();
			}
			return Decision.ReleaseWhenFinished(this._plantExecutor);
		}

		// Token: 0x0400001A RID: 26
		public readonly PlantingService _plantingService;

		// Token: 0x0400001B RID: 27
		public Planter _planter;

		// Token: 0x0400001C RID: 28
		public Worker _worker;

		// Token: 0x0400001D RID: 29
		public WalkToPositionExecutor _walkToPositionExecutor;

		// Token: 0x0400001E RID: 30
		public PlantExecutor _plantExecutor;
	}
}
