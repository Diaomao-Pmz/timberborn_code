using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.NaturalResources;
using Timberborn.NaturalResourcesMoisture;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x02000027 RID: 39
	public class PlantingSpotFinder : BaseComponent, IAwakableComponent
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x000042AD File Offset: 0x000024AD
		public PlantingSpotFinder(PlantingService plantingService, FloodableNaturalResourceService floodableNaturalResourceService, SpawnValidationService spawnValidationService, PlantingSoilValidator plantingSoilValidator)
		{
			this._plantingService = plantingService;
			this._floodableNaturalResourceService = floodableNaturalResourceService;
			this._spawnValidationService = spawnValidationService;
			this._plantingSoilValidator = plantingSoilValidator;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000042D2 File Offset: 0x000024D2
		public void Awake()
		{
			this._plantablePrioritizer = base.GetComponent<PlantablePrioritizer>();
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
			this._plantingSpotValidator = base.GetComponent<IPlantingSpotValidator>();
			this._inRangePlantingCoordinates = base.GetComponent<InRangePlantingCoordinates>();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004304 File Offset: 0x00002504
		public PlantingSpot? FindClosest(Vector3 agentPosition)
		{
			PlantableSpec prioritizedPlantableSpec = this._plantablePrioritizer.PrioritizedPlantableSpec;
			if (prioritizedPlantableSpec != null)
			{
				PlantingSpot? plantingSpot = this.FindClosest(agentPosition, prioritizedPlantableSpec);
				if (plantingSpot != null)
				{
					PlantingSpot valueOrDefault = plantingSpot.GetValueOrDefault();
					return new PlantingSpot?(valueOrDefault);
				}
			}
			return this.FindClosest(agentPosition, null);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004354 File Offset: 0x00002554
		public PlantingSpot? FindClosest(Vector3 agentPosition, PlantableSpec prioritizedPlantableSpec)
		{
			PlantingSpot? closestOrDefault = this.GetClosestOrDefault(this.GetNeighboring(agentPosition), prioritizedPlantableSpec);
			if (closestOrDefault == null)
			{
				return this.GetClosestOrDefault(this.GetReachable(), prioritizedPlantableSpec);
			}
			return closestOrDefault;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004388 File Offset: 0x00002588
		public PlantingSpot? GetClosestOrDefault(IEnumerable<PlantingSpot> plantingCoordinates, PlantableSpec prioritizedPlantableSpec)
		{
			float num = float.PositiveInfinity;
			PlantingSpot? result = null;
			foreach (PlantingSpot plantingSpot in plantingCoordinates)
			{
				float num2 = Vector3.Distance(this._blockObjectCenter.WorldCenterGrounded, CoordinateSystem.GridToWorldCentered(plantingSpot.Coordinates));
				if (num2 < num && this.CanPlantAt(plantingSpot, prioritizedPlantableSpec))
				{
					result = new PlantingSpot?(plantingSpot);
					num = num2;
				}
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004410 File Offset: 0x00002610
		public IEnumerable<PlantingSpot> GetNeighboring(Vector3 agentPosition)
		{
			Vector3Int agentCoordinates = CoordinateSystem.WorldToGridInt(agentPosition);
			foreach (Vector3Int vector3Int in Deltas.Neighbors8Vector3IntOrdered)
			{
				Vector3Int coordinates = agentCoordinates + vector3Int;
				if (this._inRangePlantingCoordinates.AreCoordinatesInRange(coordinates))
				{
					PlantingSpot? spotAt = this._plantingService.GetSpotAt(coordinates);
					if (spotAt != null)
					{
						yield return spotAt.Value;
					}
				}
			}
			Vector3Int[] array = null;
			yield break;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004427 File Offset: 0x00002627
		public IEnumerable<PlantingSpot> GetReachable()
		{
			foreach (Vector3Int coordinates in this._inRangePlantingCoordinates.GetCoordinates())
			{
				PlantingSpot? spotAt = this._plantingService.GetSpotAt(coordinates);
				if (spotAt != null)
				{
					yield return spotAt.Value;
				}
			}
			HashSet<Vector3Int>.Enumerator enumerator = default(HashSet<Vector3Int>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004438 File Offset: 0x00002638
		public bool CanPlantAt(PlantingSpot plantingSpot, PlantableSpec prioritizedPlantableSpec)
		{
			string resourceToPlant = plantingSpot.ResourceToPlant;
			if (prioritizedPlantableSpec != null && resourceToPlant != prioritizedPlantableSpec.TemplateName)
			{
				return false;
			}
			if (!this._plantingSoilValidator.Validate(plantingSpot))
			{
				return false;
			}
			if (!this._plantingSpotValidator.Validate(plantingSpot))
			{
				return false;
			}
			Vector3Int coordinates = plantingSpot.Coordinates;
			return this._floodableNaturalResourceService.ConditionsAreMet(resourceToPlant, coordinates) && (plantingSpot.PlantingBlocker || this._spawnValidationService.IsUnobstructed(coordinates, resourceToPlant));
		}

		// Token: 0x0400007A RID: 122
		public readonly PlantingService _plantingService;

		// Token: 0x0400007B RID: 123
		public readonly FloodableNaturalResourceService _floodableNaturalResourceService;

		// Token: 0x0400007C RID: 124
		public readonly SpawnValidationService _spawnValidationService;

		// Token: 0x0400007D RID: 125
		public readonly PlantingSoilValidator _plantingSoilValidator;

		// Token: 0x0400007E RID: 126
		public PlantablePrioritizer _plantablePrioritizer;

		// Token: 0x0400007F RID: 127
		public BlockObjectCenter _blockObjectCenter;

		// Token: 0x04000080 RID: 128
		public IPlantingSpotValidator _plantingSpotValidator;

		// Token: 0x04000081 RID: 129
		public InRangePlantingCoordinates _inRangePlantingCoordinates;
	}
}
