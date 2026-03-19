using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.TickSystem;
using Timberborn.WaterBuildings;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterContaminationBuildings
{
	// Token: 0x02000004 RID: 4
	public class ContaminationBlockableBuilding : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public event EventHandler BlockedByContamination;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002168 File Offset: 0x00000368
		public event EventHandler UnblockedByContamination;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000219D File Offset: 0x0000039D
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000021A5 File Offset: 0x000003A5
		public bool IsBlocked { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x000021AE File Offset: 0x000003AE
		public ContaminationBlockableBuilding(IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021BD File Offset: 0x000003BD
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._waterNeedingBuilding = base.GetComponent<IWaterNeedingBuilding>();
			base.DisableComponent();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021DD File Offset: 0x000003DD
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E5 File Offset: 0x000003E5
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021ED File Offset: 0x000003ED
		public override void StartTickable()
		{
			this.CheckContamination();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021ED File Offset: 0x000003ED
		public override void Tick()
		{
			this.CheckContamination();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021F8 File Offset: 0x000003F8
		public void CheckContamination()
		{
			Vector3Int waterCoordinatesTransformed = this._waterNeedingBuilding.WaterCoordinatesTransformed;
			float num = this._threadSafeWaterMap.ColumnContamination(waterCoordinatesTransformed);
			if (num <= ContaminationBlockableBuilding.ContaminationToUnblock && this.IsBlocked)
			{
				this.UnblockBuilding();
				return;
			}
			if (num > ContaminationBlockableBuilding.ContaminationToBlock && !this.IsBlocked)
			{
				this.BlockBuilding();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000224B File Offset: 0x0000044B
		public void BlockBuilding()
		{
			this.IsBlocked = true;
			this._blockableObject.Block(this);
			EventHandler blockedByContamination = this.BlockedByContamination;
			if (blockedByContamination == null)
			{
				return;
			}
			blockedByContamination(this, EventArgs.Empty);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002276 File Offset: 0x00000476
		public void UnblockBuilding()
		{
			this.IsBlocked = false;
			this._blockableObject.Unblock(this);
			EventHandler unblockedByContamination = this.UnblockedByContamination;
			if (unblockedByContamination == null)
			{
				return;
			}
			unblockedByContamination(this, EventArgs.Empty);
		}

		// Token: 0x04000006 RID: 6
		public static readonly float MaximumWaterContamination = 0.05f;

		// Token: 0x04000007 RID: 7
		public static readonly float Offset = 0.005f;

		// Token: 0x04000008 RID: 8
		public static readonly float ContaminationToUnblock = ContaminationBlockableBuilding.MaximumWaterContamination - ContaminationBlockableBuilding.Offset;

		// Token: 0x04000009 RID: 9
		public static readonly float ContaminationToBlock = ContaminationBlockableBuilding.MaximumWaterContamination + ContaminationBlockableBuilding.Offset;

		// Token: 0x0400000D RID: 13
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x0400000E RID: 14
		public BlockableObject _blockableObject;

		// Token: 0x0400000F RID: 15
		public IWaterNeedingBuilding _waterNeedingBuilding;
	}
}
