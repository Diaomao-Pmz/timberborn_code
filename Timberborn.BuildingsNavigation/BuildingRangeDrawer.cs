using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingRange;
using Timberborn.Buildings;
using Timberborn.ConstructionMode;
using Timberborn.GameDistricts;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x0200000F RID: 15
	public class BuildingRangeDrawer : BaseComponent, IAwakableComponent, IUpdatableComponent, ISelectionListener, IPreviewSelectionListener
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002CA5 File Offset: 0x00000EA5
		public BuildingRangeDrawer(BoundsNavRangeDrawingService boundsNavRangeDrawingService, DistrictCenterRegistry districtCenterRegistry, ConstructionModeService constructionModeService)
		{
			this._boundsNavRangeDrawingService = boundsNavRangeDrawingService;
			this._districtCenterRegistry = districtCenterRegistry;
			this._constructionModeService = constructionModeService;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._buildingAccessible = base.GetComponent<BuildingAccessible>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._districtCenter = base.GetComponent<DistrictCenter>();
			this._preview = base.GetComponent<Preview>();
			this._drawTerrainRange = base.GetComponent<BuildingWithTerrainRange>();
			this._drawRoadSpilledRange = base.GetComponent<BuildingWithRoadSpillRange>();
			base.DisableComponent();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D35 File Offset: 0x00000F35
		public void Update()
		{
			this.DrawRange();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D3D File Offset: 0x00000F3D
		public void OnSelect()
		{
			this.DrawRange();
			base.EnableComponent();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D4B File Offset: 0x00000F4B
		public void OnUnselect()
		{
			base.DisableComponent();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D53 File Offset: 0x00000F53
		public void OnPreviewSelect()
		{
			base.EnableComponent();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D4B File Offset: 0x00000F4B
		public void OnPreviewUnselect()
		{
			base.DisableComponent();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D5C File Offset: 0x00000F5C
		public void DrawRange()
		{
			bool isPreview = this._blockObject.IsPreview;
			bool isFinished = this._blockObject.IsFinished;
			bool flag = isPreview || !isFinished;
			Vector3? unblockedSingleAccess = BuildingRangeDrawer.GetUnblockedSingleAccess(this._buildingAccessible, flag);
			if (unblockedSingleAccess != null)
			{
				Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
				if (this._drawTerrainRange || this._drawRoadSpilledRange)
				{
					bool inConstructionMode = this._constructionModeService.InConstructionMode;
					this._boundsNavRangeDrawingService.DrawRange(valueOrDefault, flag || inConstructionMode, this._drawTerrainRange, this._drawRoadSpilledRange);
				}
				DistrictCenter districtCenter = this.GetDistrictCenter(isPreview, isFinished, valueOrDefault);
				if (districtCenter)
				{
					DistrictPathNavRangeDrawer component = districtCenter.GetComponent<DistrictPathNavRangeDrawer>();
					DrawingParameters drawingParameters = new DrawingParameters(flag, valueOrDefault, this._blockObject.Orientation, true);
					component.DrawRange(drawingParameters);
				}
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E1C File Offset: 0x0000101C
		public DistrictCenter GetDistrictCenter(bool isPreview, bool isFinished, Vector3 buildingAccess)
		{
			if (isFinished)
			{
				return this._districtBuilding.InstantDistrict;
			}
			if (isPreview && !this._preview.PreviewState.IsBuildable)
			{
				return null;
			}
			foreach (DistrictCenter districtCenter in this._districtCenterRegistry.AllDistrictCenters)
			{
				if (districtCenter.IsOnPreviewDistrictRoad(buildingAccess))
				{
					return districtCenter;
				}
			}
			return this._districtCenter;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002EB0 File Offset: 0x000010B0
		public static Vector3? GetUnblockedSingleAccess(BuildingAccessible buildingAccessible, bool isPreview)
		{
			if (!isPreview)
			{
				return buildingAccessible.Accessible.UnblockedSingleAccessInstant;
			}
			return new Vector3?(buildingAccessible.CalculateAccess());
		}

		// Token: 0x0400002A RID: 42
		public readonly BoundsNavRangeDrawingService _boundsNavRangeDrawingService;

		// Token: 0x0400002B RID: 43
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x0400002C RID: 44
		public readonly ConstructionModeService _constructionModeService;

		// Token: 0x0400002D RID: 45
		public BlockObject _blockObject;

		// Token: 0x0400002E RID: 46
		public BuildingAccessible _buildingAccessible;

		// Token: 0x0400002F RID: 47
		public DistrictBuilding _districtBuilding;

		// Token: 0x04000030 RID: 48
		public DistrictCenter _districtCenter;

		// Token: 0x04000031 RID: 49
		public Preview _preview;

		// Token: 0x04000032 RID: 50
		public bool _drawTerrainRange;

		// Token: 0x04000033 RID: 51
		public bool _drawRoadSpilledRange;
	}
}
