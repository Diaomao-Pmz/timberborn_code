using System;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.DwellingSystem;
using Timberborn.EnterableSystem;
using Timberborn.GameDistricts;
using Timberborn.Navigation;
using Timberborn.TerrainSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000012 RID: 18
	public class RandomDestinationPicker
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000028E0 File Offset: 0x00000AE0
		public RandomDestinationPicker(IDistrictService districtService, IRandomNumberGenerator randomNumberGenerator, IThreadSafeWaterMap threadSafeWaterMap, ITerrainService terrainService)
		{
			this._districtService = districtService;
			this._randomNumberGenerator = randomNumberGenerator;
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._terrainService = terrainService;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002908 File Offset: 0x00000B08
		public Vector3 RandomDestination(Citizen citizen)
		{
			DistrictCenter assignedDistrict = citizen.AssignedDistrict;
			Vector3 randomDestination = (assignedDistrict && assignedDistrict.District != null) ? this.RandomDestination(citizen, assignedDistrict.District) : citizen.Transform.position;
			return this.OffsetDestination(randomDestination);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002950 File Offset: 0x00000B50
		public bool TryGetSafeRandomDestination(Citizen citizen, out Vector3 destination)
		{
			destination = this.RandomDestination(citizen);
			Vector3Int coordinates = NavigationCoordinateSystem.WorldToGridInt(destination);
			if (this._threadSafeWaterMap.ColumnContamination(coordinates) == 0f)
			{
				return true;
			}
			Enterer component = citizen.GetComponent<Enterer>();
			if (component.IsInside)
			{
				Vector3Int coordinates2 = component.CurrentBuilding.GetComponent<BlockObject>().PositionedEntrance.Coordinates;
				destination = this.OffsetDestination(CoordinateSystem.GridToWorldCentered(coordinates2));
				return true;
			}
			return false;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000029C5 File Offset: 0x00000BC5
		public Vector3 RandomDestination(Citizen citizen, District district)
		{
			return this._districtService.GetRandomDestinationInDistrict(district, RandomDestinationPicker.GetCoordinates(citizen));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000029DC File Offset: 0x00000BDC
		public static Vector3 GetCoordinates(Citizen citizen)
		{
			Dweller component = citizen.GetComponent<Dweller>();
			if (component && component.HasHome)
			{
				Vector3? homeAccess = component.HomeAccess;
				if (homeAccess != null)
				{
					Vector3 valueOrDefault = homeAccess.GetValueOrDefault();
					if (component.Home.GetComponent<DistrictBuilding>().District != null)
					{
						return valueOrDefault;
					}
				}
			}
			return CoordinateSystem.GridToWorldCentered(citizen.AssignedDistrict.CenterCoordinates);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A3C File Offset: 0x00000C3C
		public Vector3 OffsetDestination(Vector3 randomDestination)
		{
			Vector3Int coords = NavigationCoordinateSystem.WorldToGridInt(randomDestination);
			float num = this._terrainService.OnGround(coords) ? RandomDestinationPicker.TerrainDestinationOffset : RandomDestinationPicker.NonTerrainDestinationOffset;
			Vector3 vector = CoordinateSystem.GridToWorld(this._randomNumberGenerator.InsideUnitCircle() * num);
			return randomDestination + vector;
		}

		// Token: 0x0400001E RID: 30
		public static readonly float TerrainDestinationOffset = 0.4f;

		// Token: 0x0400001F RID: 31
		public static readonly float NonTerrainDestinationOffset = 0.1f;

		// Token: 0x04000020 RID: 32
		public readonly IDistrictService _districtService;

		// Token: 0x04000021 RID: 33
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000022 RID: 34
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000023 RID: 35
		public readonly ITerrainService _terrainService;
	}
}
