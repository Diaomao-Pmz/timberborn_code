using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200001F RID: 31
	public class DistrictObstacle : BaseComponent, IAwakableComponent, IUnfinishedStateListener, IFinishedStateListener
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x0000416B File Offset: 0x0000236B
		public DistrictObstacle(IDistrictService districtService)
		{
			this._districtService = districtService;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000417A File Offset: 0x0000237A
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._districtObstacleSpec = base.GetComponent<DistrictObstacleSpec>();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004194 File Offset: 0x00002394
		public void OnEnterUnfinishedState()
		{
			this.AddToPreviewDistricts();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000419C File Offset: 0x0000239C
		public void OnExitUnfinishedState()
		{
			this.RemoveFromPreviewDistricts();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000041A4 File Offset: 0x000023A4
		public void OnEnterFinishedState()
		{
			this._districtService.SetObstacle(this.ObstacleCoordinates);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000041B7 File Offset: 0x000023B7
		public void OnExitFinishedState()
		{
			this._districtService.UnsetObstacle(this.ObstacleCoordinates);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000041CA File Offset: 0x000023CA
		public void AddToPreviewDistricts()
		{
			this._districtService.SetPreviewObstacle(this.ObstacleCoordinates);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000041DD File Offset: 0x000023DD
		public void RemoveFromPreviewDistricts()
		{
			this._districtService.UnsetPreviewObstacle(this.ObstacleCoordinates);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000041F0 File Offset: 0x000023F0
		public Vector3Int ObstacleCoordinates
		{
			get
			{
				return this._blockObject.TransformCoordinates(this._districtObstacleSpec.CoordinateOffset);
			}
		}

		// Token: 0x04000058 RID: 88
		public readonly IDistrictService _districtService;

		// Token: 0x04000059 RID: 89
		public BlockObject _blockObject;

		// Token: 0x0400005A RID: 90
		public DistrictObstacleSpec _districtObstacleSpec;
	}
}
