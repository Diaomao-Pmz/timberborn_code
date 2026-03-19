using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200003E RID: 62
	public class WaterOutput : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060002CF RID: 719 RVA: 0x00008348 File Offset: 0x00006548
		// (remove) Token: 0x060002D0 RID: 720 RVA: 0x00008380 File Offset: 0x00006580
		public event EventHandler<WaterAddition> WaterAdded;

		// Token: 0x060002D1 RID: 721 RVA: 0x000083B5 File Offset: 0x000065B5
		public WaterOutput(IWaterService waterService, IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._waterService = waterService;
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x000083CB File Offset: 0x000065CB
		public bool HasSpaceForWater
		{
			get
			{
				return this.AvailableSpace > 0f;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x000083DA File Offset: 0x000065DA
		public float AvailableSpace
		{
			get
			{
				return (float)this._waterCoordinatesTransformed.z - WaterOutput.WaterUpperSafetySpace - this._threadSafeWaterMap.WaterHeightOrFloor(this._waterCoordinatesTransformed);
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00008400 File Offset: 0x00006600
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00008410 File Offset: 0x00006610
		public void InitializeEntity()
		{
			Vector3Int waterCoordinates = base.GetComponent<WaterOutputSpec>().WaterCoordinates;
			this._waterCoordinatesTransformed = this._blockObject.TransformCoordinates(waterCoordinates);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000843B File Offset: 0x0000663B
		public void AddCleanWater(float cleanWater)
		{
			this.AddWater(cleanWater, 0f);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00008449 File Offset: 0x00006649
		public void AddContaminatedWater(float contaminatedWater)
		{
			this.AddWater(0f, contaminatedWater);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008458 File Offset: 0x00006658
		public void AddWater(float cleanWater, float contaminatedWater)
		{
			if (cleanWater > 0f)
			{
				this._waterService.AddCleanWater(this._waterCoordinatesTransformed, cleanWater);
			}
			if (contaminatedWater > 0f)
			{
				this._waterService.AddContaminatedWater(this._waterCoordinatesTransformed, contaminatedWater);
			}
			if (cleanWater > 0f || contaminatedWater > 0f)
			{
				EventHandler<WaterAddition> waterAdded = this.WaterAdded;
				if (waterAdded == null)
				{
					return;
				}
				waterAdded(this, new WaterAddition(cleanWater, contaminatedWater));
			}
		}

		// Token: 0x04000117 RID: 279
		public static readonly float WaterUpperSafetySpace = 0.1f;

		// Token: 0x04000119 RID: 281
		public readonly IWaterService _waterService;

		// Token: 0x0400011A RID: 282
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x0400011B RID: 283
		public BlockObject _blockObject;

		// Token: 0x0400011C RID: 284
		public Vector3Int _waterCoordinatesTransformed;
	}
}
