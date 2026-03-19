using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.CharacterModelSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200000B RID: 11
	public class CitizenUnstucker
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002469 File Offset: 0x00000669
		public CitizenUnstucker(IBlockService blockService, IDistrictService districtService, DistrictCenterRegistry districtCenterRegistry)
		{
			this._blockService = blockService;
			this._districtService = districtService;
			this._districtCenterRegistry = districtCenterRegistry;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002494 File Offset: 0x00000694
		public bool TryUnstuckAndKeepDistrict(Citizen citizen, DistrictCenter preferredDistrict)
		{
			Asserts.IsTrue<CitizenUnstucker>(this, preferredDistrict, "preferredDistrict");
			if (this.IsStuckInsideFinishedBuilding(citizen))
			{
				this.CollectDistricts(preferredDistrict);
				if (this._districts.Any<DistrictCenter>())
				{
					Vector3 position;
					bool flag = this.TryFindUnstuckPosition(citizen, out position);
					if (flag)
					{
						CitizenUnstucker.MoveCitizen(citizen, position);
					}
					this._districts.Clear();
					return flag;
				}
			}
			return false;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024F0 File Offset: 0x000006F0
		public bool IsStuckInsideFinishedBuilding(Citizen citizen)
		{
			Vector3Int coordinates = NavigationCoordinateSystem.WorldToGridInt(citizen.Transform.position);
			Building middleObjectComponentAt = this._blockService.GetMiddleObjectComponentAt<Building>(coordinates);
			return middleObjectComponentAt != null && middleObjectComponentAt.GetComponent<BlockObject>().IsFinished;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000252C File Offset: 0x0000072C
		public void CollectDistricts(DistrictCenter preferredDistrict)
		{
			this._districts.Add(preferredDistrict);
			foreach (DistrictCenter districtCenter in this._districtCenterRegistry.FinishedDistrictCenters)
			{
				if (districtCenter != preferredDistrict)
				{
					this._districts.Add(districtCenter);
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000259C File Offset: 0x0000079C
		public bool TryFindUnstuckPosition(Citizen citizen, out Vector3 unstuckPosition)
		{
			if (this.TryFindReachablePosition(citizen, out unstuckPosition))
			{
				return true;
			}
			unstuckPosition = Vector3.zero;
			return false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025B8 File Offset: 0x000007B8
		public bool TryFindReachablePosition(Citizen citizen, out Vector3 reachablePosition)
		{
			Vector3 position = citizen.Transform.position;
			foreach (DistrictCenter districtCenter in this._districts)
			{
				foreach (Vector3Int vector3Int in Deltas.Neighbors26Vector3Int)
				{
					Vector3 vector = position + vector3Int;
					if (this._districtService.DistrictIsGloballyReachable(districtCenter.District, vector))
					{
						reachablePosition = vector;
						return true;
					}
				}
			}
			reachablePosition = Vector3.zero;
			return false;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002674 File Offset: 0x00000874
		public static void MoveCitizen(Citizen citizen, Vector3 position)
		{
			citizen.Transform.position = position;
			citizen.GetComponent<CharacterModel>().Position = position;
		}

		// Token: 0x04000014 RID: 20
		public readonly IBlockService _blockService;

		// Token: 0x04000015 RID: 21
		public readonly IDistrictService _districtService;

		// Token: 0x04000016 RID: 22
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000017 RID: 23
		public readonly List<DistrictCenter> _districts = new List<DistrictCenter>();
	}
}
