using System;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Buildings;
using Timberborn.Coordinates;
using Timberborn.GameFactionSystem;
using Timberborn.PrefabOptimization;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x0200000B RID: 11
	public class DrivewayModelInstantiator : ILoadableSingleton
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002599 File Offset: 0x00000799
		public DrivewayModelInstantiator(FactionService factionService, OptimizedPrefabInstantiator optimizedPrefabInstantiator, ISpecService specService)
		{
			this._factionService = factionService;
			this._optimizedPrefabInstantiator = optimizedPrefabInstantiator;
			this._specService = specService;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025B8 File Offset: 0x000007B8
		public void Load()
		{
			DrivewayModelInstantiatorSpec singleSpec = this._specService.GetSingleSpec<DrivewayModelInstantiatorSpec>();
			this._narrowLeftDrivewayPrefab = singleSpec.NarrowLeftDrivewayPrefab.Asset;
			this._narrowCenterDrivewayPrefab = singleSpec.NarrowCenterDrivewayPrefab.Asset;
			this._narrowRightDrivewayPrefab = singleSpec.NarrowRightDrivewayPrefab.Asset;
			this._wideCenterDrivewayPrefab = singleSpec.WideCenterDrivewayPrefab.Asset;
			this._longCenterDrivewayPrefab = singleSpec.LongCenterDrivewayPrefab.Asset;
			this._straightPathDrivewayPrefab = singleSpec.StraightPathDrivewayPrefab.Asset;
			this._pathMaterial = this._factionService.Current.PathMaterial.Asset;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002654 File Offset: 0x00000854
		public GameObject InstantiateModel(DrivewayModel drivewayModel, Vector3Int coordinates, Direction2D direction)
		{
			GameObject modelPrefab = this.GetModelPrefab(drivewayModel.Driveway);
			Transform transform = drivewayModel.GetComponent<BuildingModel>().FinishedModel.transform;
			BlockObject component = drivewayModel.GetComponent<BlockObject>();
			GameObject gameObject = this._optimizedPrefabInstantiator.Instantiate(modelPrefab, transform);
			Vector3 vector = CoordinateSystem.GridToWorld(component.Blocks.Pivot(coordinates, direction.ToOrientation()));
			Quaternion quaternion = direction.ToWorldSpaceRotation();
			gameObject.transform.SetLocalPositionAndRotation(vector, quaternion);
			gameObject.GetComponentInChildren<Renderer>().sharedMaterial = this._pathMaterial;
			return gameObject;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026D4 File Offset: 0x000008D4
		public GameObject GetModelPrefab(Driveway driveway)
		{
			switch (driveway)
			{
			case Driveway.NarrowLeft:
				return this._narrowLeftDrivewayPrefab;
			case Driveway.NarrowCenter:
				return this._narrowCenterDrivewayPrefab;
			case Driveway.NarrowRight:
				return this._narrowRightDrivewayPrefab;
			case Driveway.WideCenter:
				return this._wideCenterDrivewayPrefab;
			case Driveway.LongCenter:
				return this._longCenterDrivewayPrefab;
			case Driveway.StraightPath:
				return this._straightPathDrivewayPrefab;
			default:
				throw new ArgumentOutOfRangeException("driveway", driveway, null);
			}
		}

		// Token: 0x0400001F RID: 31
		public readonly FactionService _factionService;

		// Token: 0x04000020 RID: 32
		public readonly OptimizedPrefabInstantiator _optimizedPrefabInstantiator;

		// Token: 0x04000021 RID: 33
		public readonly ISpecService _specService;

		// Token: 0x04000022 RID: 34
		public GameObject _narrowLeftDrivewayPrefab;

		// Token: 0x04000023 RID: 35
		public GameObject _narrowCenterDrivewayPrefab;

		// Token: 0x04000024 RID: 36
		public GameObject _narrowRightDrivewayPrefab;

		// Token: 0x04000025 RID: 37
		public GameObject _wideCenterDrivewayPrefab;

		// Token: 0x04000026 RID: 38
		public GameObject _longCenterDrivewayPrefab;

		// Token: 0x04000027 RID: 39
		public GameObject _straightPathDrivewayPrefab;

		// Token: 0x04000028 RID: 40
		public Material _pathMaterial;
	}
}
