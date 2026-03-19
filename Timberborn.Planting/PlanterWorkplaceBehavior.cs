using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BlockSystem;
using Timberborn.Demolishing;
using Timberborn.WorkSystem;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x02000017 RID: 23
	public class PlanterWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002E57 File Offset: 0x00001057
		public PlanterWorkplaceBehavior(PlantingService plantingService)
		{
			this._plantingService = plantingService;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002E66 File Offset: 0x00001066
		public void Awake()
		{
			this._planterBuildingStatusUpdater = base.GetComponent<PlanterBuildingStatusUpdater>();
			this._plantingSpotFinder = base.GetComponent<PlantingSpotFinder>();
			this._workplace = base.GetComponent<Workplace>();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002E8C File Offset: 0x0000108C
		public override Decision Decide(BehaviorAgent agent)
		{
			if (agent.GetComponent<Worker>().Workplace != this._workplace)
			{
				return Decision.ReleaseNow();
			}
			Demolisher component = agent.GetComponent<Demolisher>();
			if (component.HasReservedDemolishable)
			{
				return PlanterWorkplaceBehavior.StartDemolition(agent);
			}
			Planter component2 = agent.GetComponent<Planter>();
			PlantingSpot? plantingSpot = null;
			if (component2.PlantingCoordinates == null)
			{
				plantingSpot = this._plantingSpotFinder.FindClosest(agent.Transform.position);
				if (plantingSpot == null)
				{
					return Decision.ReleaseNow();
				}
			}
			Vector3Int coordinates = component2.PlantingCoordinates ?? plantingSpot.Value.Coordinates;
			BlockObject blockObject;
			if (this._plantingService.TryGetPlantingBlocker(coordinates, out blockObject))
			{
				Demolishable component3 = blockObject.GetComponent<Demolishable>();
				component.ReserveWithForcedDemolition(component3);
				Vector3Int coordinates2 = component3.GetComponent<BlockObject>().Coordinates;
				component2.Reserve(coordinates2);
				return PlanterWorkplaceBehavior.StartDemolition(agent);
			}
			PlantBehavior component4 = agent.GetComponent<PlantBehavior>();
			Decision decision = component4.StartPlanting(agent);
			if (!decision.ShouldReleaseNow)
			{
				this._planterBuildingStatusUpdater.DeactivateStatus();
				return Decision.TransferNow(component4, decision);
			}
			this._planterBuildingStatusUpdater.UpdateStatus();
			return Decision.ReleaseNow();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002FB7 File Offset: 0x000011B7
		public static Decision StartDemolition(BehaviorAgent agent)
		{
			return agent.GetComponent<DemolishBehavior>().Decide(agent);
		}

		// Token: 0x04000033 RID: 51
		public readonly PlantingService _plantingService;

		// Token: 0x04000034 RID: 52
		public PlanterBuildingStatusUpdater _planterBuildingStatusUpdater;

		// Token: 0x04000035 RID: 53
		public PlantingSpotFinder _plantingSpotFinder;

		// Token: 0x04000036 RID: 54
		public Workplace _workplace;
	}
}
