using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WaterSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200002F RID: 47
	public class WaterInput : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00006C43 File Offset: 0x00004E43
		public WaterInput(IWaterService waterService, IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._waterService = waterService;
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00006C59 File Offset: 0x00004E59
		public bool IsUnderwater
		{
			get
			{
				return this._inputCoordinates.Depth > 0 && this._threadSafeWaterMap.CellIsUnderwater(this._inputCoordinates.Coordinates);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00006C81 File Offset: 0x00004E81
		public float ContaminatedWaterAmount
		{
			get
			{
				return this.ContaminationPercentage * this.AvailableWater;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00006C90 File Offset: 0x00004E90
		public float CleanWaterAmount
		{
			get
			{
				return this.AvailableWater - this.ContaminatedWaterAmount;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00006C9F File Offset: 0x00004E9F
		public float ContaminationPercentage
		{
			get
			{
				return this._threadSafeWaterMap.ColumnContamination(this._inputCoordinates.Coordinates);
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00006CB7 File Offset: 0x00004EB7
		public void Awake()
		{
			this._inputCoordinates = base.GetComponent<WaterInputCoordinates>();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006CC5 File Offset: 0x00004EC5
		public void RemoveCleanWater(float waterAmount)
		{
			this._waterService.RemoveCleanWater(this._inputCoordinates.Coordinates, waterAmount);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00006CDE File Offset: 0x00004EDE
		public void RemoveContaminatedWater(float waterAmount)
		{
			this._waterService.RemoveContaminatedWater(this._inputCoordinates.Coordinates, waterAmount);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00006CF8 File Offset: 0x00004EF8
		public float AvailableWater
		{
			get
			{
				return this._threadSafeWaterMap.WaterHeightOrFloor(this._inputCoordinates.Coordinates) - (float)this._inputCoordinates.Coordinates.z;
			}
		}

		// Token: 0x040000CD RID: 205
		public readonly IWaterService _waterService;

		// Token: 0x040000CE RID: 206
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x040000CF RID: 207
		public WaterInputCoordinates _inputCoordinates;
	}
}
