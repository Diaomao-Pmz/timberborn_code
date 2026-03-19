using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000027 RID: 39
	public class TickableWaterBuilding : TickableComponent, IAwakableComponent, IFinishedStateListener, IWaterNeedingBuilding
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000197 RID: 407 RVA: 0x00005140 File Offset: 0x00003340
		// (remove) Token: 0x06000198 RID: 408 RVA: 0x00005178 File Offset: 0x00003378
		public event EventHandler StartedNeedingWater;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000199 RID: 409 RVA: 0x000051B0 File Offset: 0x000033B0
		// (remove) Token: 0x0600019A RID: 410 RVA: 0x000051E8 File Offset: 0x000033E8
		public event EventHandler StoppedNeedingWater;

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000521D File Offset: 0x0000341D
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00005225 File Offset: 0x00003425
		public Vector3Int WaterCoordinatesTransformed { get; private set; }

		// Token: 0x0600019D RID: 413 RVA: 0x0000522E File Offset: 0x0000342E
		public TickableWaterBuilding(IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005244 File Offset: 0x00003444
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._tickableWaterBuildingSpec = base.GetComponent<TickableWaterBuildingSpec>();
			base.DisableComponent();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005270 File Offset: 0x00003470
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00004BFE File Offset: 0x00002DFE
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005278 File Offset: 0x00003478
		public override void StartTickable()
		{
			this.WaterCoordinatesTransformed = this._blockObject.TransformCoordinates(this._tickableWaterBuildingSpec.WaterCoordinates);
			this.CheckWaterHeight();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000529C File Offset: 0x0000349C
		public override void Tick()
		{
			this.CheckWaterHeight();
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000052A4 File Offset: 0x000034A4
		public float WaterHeight
		{
			get
			{
				return this._threadSafeWaterMap.WaterHeightOrFloor(this.WaterCoordinatesTransformed) - (float)this.WaterCoordinatesTransformed.z;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000052D4 File Offset: 0x000034D4
		public void CheckWaterHeight()
		{
			float minWaterHeight = this._tickableWaterBuildingSpec.MinWaterHeight;
			float changeRange = this._tickableWaterBuildingSpec.ChangeRange;
			if (this.WaterHeight > minWaterHeight - changeRange || !this._hasWater)
			{
				if (this.WaterHeight > minWaterHeight + changeRange && !this._hasWater)
				{
					this._hasWater = true;
					this._blockableObject.Unblock(this);
					EventHandler stoppedNeedingWater = this.StoppedNeedingWater;
					if (stoppedNeedingWater == null)
					{
						return;
					}
					stoppedNeedingWater(this, EventArgs.Empty);
				}
				return;
			}
			this._hasWater = false;
			this._blockableObject.Block(this);
			EventHandler startedNeedingWater = this.StartedNeedingWater;
			if (startedNeedingWater == null)
			{
				return;
			}
			startedNeedingWater(this, EventArgs.Empty);
		}

		// Token: 0x04000092 RID: 146
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000093 RID: 147
		public BlockObject _blockObject;

		// Token: 0x04000094 RID: 148
		public BlockableObject _blockableObject;

		// Token: 0x04000095 RID: 149
		public TickableWaterBuildingSpec _tickableWaterBuildingSpec;

		// Token: 0x04000096 RID: 150
		public bool _hasWater = true;
	}
}
