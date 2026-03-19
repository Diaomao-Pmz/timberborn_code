using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingRange;
using Timberborn.Buildings;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x0200000D RID: 13
	public class BuildingCachingFlowField : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002AF7 File Offset: 0x00000CF7
		public BuildingCachingFlowField(INavigationCachingService navigationCachingService)
		{
			this._navigationCachingService = navigationCachingService;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B06 File Offset: 0x00000D06
		public void Awake()
		{
			this._buildingAccessible = base.GetComponent<BuildingAccessible>();
			this._buildingWithTerrainRange = base.GetComponent<BuildingWithTerrainRange>();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B20 File Offset: 0x00000D20
		public void OnEnterFinishedState()
		{
			this.StartCaching();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B28 File Offset: 0x00000D28
		public void OnExitFinishedState()
		{
			this.StopCaching();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B30 File Offset: 0x00000D30
		public void StartCaching()
		{
			this._accessCoordinates = NavigationCoordinateSystem.WorldToGridInt(this._buildingAccessible.Accessible.Accesses.Single<Vector3>());
			this._navigationCachingService.StartCachingRoadFlowField(this._accessCoordinates);
			if (this._buildingWithTerrainRange)
			{
				this._navigationCachingService.StartCachingTerrainFlowField(this._accessCoordinates);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B91 File Offset: 0x00000D91
		public void StopCaching()
		{
			this._navigationCachingService.StopCachingRoadFlowField(this._accessCoordinates);
			if (this._buildingWithTerrainRange)
			{
				this._navigationCachingService.StopCachingTerrainFlowField(this._accessCoordinates);
			}
		}

		// Token: 0x04000022 RID: 34
		public readonly INavigationCachingService _navigationCachingService;

		// Token: 0x04000023 RID: 35
		public BuildingAccessible _buildingAccessible;

		// Token: 0x04000024 RID: 36
		public BuildingWithTerrainRange _buildingWithTerrainRange;

		// Token: 0x04000025 RID: 37
		public Vector3Int _accessCoordinates;
	}
}
