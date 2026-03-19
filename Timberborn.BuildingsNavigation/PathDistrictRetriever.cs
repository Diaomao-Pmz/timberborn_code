using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.GameDistricts;
using Timberborn.Navigation;
using Timberborn.PathSystem;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x0200001A RID: 26
	public class PathDistrictRetriever : BaseComponent, IAwakableComponent
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public PathDistrictRetriever(DistrictCenterRegistry districtCenterRegistry)
		{
			this._districtCenterRegistry = districtCenterRegistry;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003BC3 File Offset: 0x00001DC3
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._pathSpec = base.GetComponent<PathSpec>();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public DistrictCenter GetFinishedDistrictCenter()
		{
			foreach (DistrictCenter districtCenter in this._districtCenterRegistry.FinishedDistrictCenters)
			{
				if (districtCenter.IsOnInstantDistrictRoad(this.PathCoordinates))
				{
					return districtCenter;
				}
			}
			return null;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003C4C File Offset: 0x00001E4C
		public DistrictCenter GetAnyDistrictCenter()
		{
			foreach (DistrictCenter districtCenter in this._districtCenterRegistry.AllDistrictCenters)
			{
				if (districtCenter.IsOnPreviewDistrictRoad(this.PathCoordinates) || districtCenter.IsOnInstantDistrictRoad(this.PathCoordinates))
				{
					return districtCenter;
				}
			}
			return null;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public bool TryGetDistanceToDistrictCenter(out DistrictCenter districtCenter, out float distance)
		{
			DistrictCenter finishedDistrictCenter = this.GetFinishedDistrictCenter();
			if (finishedDistrictCenter != null)
			{
				districtCenter = finishedDistrictCenter;
				Accessible enabledComponent = districtCenter.GetEnabledComponent<Accessible>();
				return enabledComponent.FindRoadPath(this.PathCoordinates, out distance) || enabledComponent.FindInstantRoadPath(this.PathCoordinates, out distance);
			}
			districtCenter = null;
			distance = 0f;
			return false;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003D10 File Offset: 0x00001F10
		public Vector3 PathCoordinates
		{
			get
			{
				return CoordinateSystem.GridToWorld(this._blockObject.TransformCoordinates(this._pathSpec.MainPathCoordinates));
			}
		}

		// Token: 0x0400005D RID: 93
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x0400005E RID: 94
		public PathSpec _pathSpec;

		// Token: 0x0400005F RID: 95
		public BlockObject _blockObject;
	}
}
