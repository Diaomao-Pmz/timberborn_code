using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Emptying;
using Timberborn.InventorySystem;
using Timberborn.Planting;
using Timberborn.SimpleOutputBuildings;
using Timberborn.WorkSystem;
using Timberborn.Yielding;

namespace Timberborn.Fields
{
	// Token: 0x0200000C RID: 12
	public class FarmHouseWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002498 File Offset: 0x00000698
		public void Awake()
		{
			this._farmHouse = base.GetComponent<FarmHouse>();
			this._plantablePrioritizer = base.GetComponent<PlantablePrioritizer>();
			this._planterBuildingStatusUpdater = base.GetComponent<PlanterBuildingStatusUpdater>();
			this._inventory = base.GetComponent<SimpleOutputInventory>().Inventory;
			this._emptyOutputWorkplaceBehavior = base.GetComponent<EmptyOutputWorkplaceBehavior>();
			this._inRangeYielders = base.GetComponent<InRangeYielders>();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000024F4 File Offset: 0x000006F4
		public override Decision Decide(BehaviorAgent agent)
		{
			if (this._plantablePrioritizer.PrioritizedPlantableSpec != null)
			{
				string templateName = this._plantablePrioritizer.PrioritizedPlantableSpec.TemplateName;
				Decision result = this.Decide(agent, templateName);
				if (!result.ShouldReleaseNow)
				{
					return result;
				}
			}
			return this.Decide(agent, null);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002544 File Offset: 0x00000744
		public Decision Decide(BehaviorAgent agent, string prioritizedName)
		{
			PlantBehavior plantBehavior = agent.GetComponent<PlantBehavior>();
			HarvestStarter harvestStarter = agent.GetComponent<HarvestStarter>();
			return this.Decide(plantBehavior, () => plantBehavior.StartPlanting(agent), () => harvestStarter.StartHarvesting(this._inventory, this._inRangeYielders, prioritizedName));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000025B4 File Offset: 0x000007B4
		public Decision Decide(Behavior plantBehavior, Func<Decision> plantDecisionGetter, Func<Decision> harvestDecisionGetter)
		{
			if (this._farmHouse.PlantingPrioritized)
			{
				Decision result;
				if (this.Decide(plantBehavior, plantDecisionGetter, out result))
				{
					return result;
				}
				Decision result2 = harvestDecisionGetter();
				if (!result2.ShouldReleaseNow)
				{
					this._planterBuildingStatusUpdater.DeactivateStatus();
					return result2;
				}
			}
			else
			{
				Decision result3 = harvestDecisionGetter();
				if (!result3.ShouldReleaseNow)
				{
					this._planterBuildingStatusUpdater.DeactivateStatus();
					return result3;
				}
				BehaviorAgent component = plantBehavior.GetComponent<BehaviorAgent>();
				Decision decision = this._emptyOutputWorkplaceBehavior.Decide(component);
				if (!decision.ShouldReleaseNow)
				{
					return Decision.TransferNow(this._emptyOutputWorkplaceBehavior, decision);
				}
				Decision result4;
				if (this.Decide(plantBehavior, plantDecisionGetter, out result4))
				{
					return result4;
				}
			}
			this._planterBuildingStatusUpdater.UpdateStatus();
			return Decision.ReleaseNow();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002664 File Offset: 0x00000864
		public bool Decide(Behavior behavior, Func<Decision> getDecision, out Decision decision)
		{
			Decision decision2 = getDecision();
			if (!decision2.ShouldReleaseNow)
			{
				this._planterBuildingStatusUpdater.DeactivateStatus();
				decision = Decision.TransferNow(behavior, decision2);
				return true;
			}
			decision = default(Decision);
			return false;
		}

		// Token: 0x04000011 RID: 17
		public FarmHouse _farmHouse;

		// Token: 0x04000012 RID: 18
		public PlantablePrioritizer _plantablePrioritizer;

		// Token: 0x04000013 RID: 19
		public PlanterBuildingStatusUpdater _planterBuildingStatusUpdater;

		// Token: 0x04000014 RID: 20
		public Inventory _inventory;

		// Token: 0x04000015 RID: 21
		public EmptyOutputWorkplaceBehavior _emptyOutputWorkplaceBehavior;

		// Token: 0x04000016 RID: 22
		public InRangeYielders _inRangeYielders;
	}
}
