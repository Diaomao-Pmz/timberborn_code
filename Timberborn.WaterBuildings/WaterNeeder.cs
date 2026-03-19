using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200003B RID: 59
	public class WaterNeeder : TickableComponent, IAwakableComponent, IFinishedStateListener, IWaterNeedingBuilding
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060002B2 RID: 690 RVA: 0x00008074 File Offset: 0x00006274
		// (remove) Token: 0x060002B3 RID: 691 RVA: 0x000080AC File Offset: 0x000062AC
		public event EventHandler StartedNeedingWater;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060002B4 RID: 692 RVA: 0x000080E4 File Offset: 0x000062E4
		// (remove) Token: 0x060002B5 RID: 693 RVA: 0x0000811C File Offset: 0x0000631C
		public event EventHandler StoppedNeedingWater;

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00008151 File Offset: 0x00006351
		public Vector3Int WaterCoordinatesTransformed
		{
			get
			{
				return this._waterInputCoordinates.Coordinates;
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000815E File Offset: 0x0000635E
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._waterInput = base.GetComponent<WaterInput>();
			this._waterInputCoordinates = base.GetComponent<WaterInputCoordinates>();
			base.DisableComponent();
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00005270 File Offset: 0x00003470
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00004BFE File Offset: 0x00002DFE
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000818A File Offset: 0x0000638A
		public override void Tick()
		{
			if (this._isBlocked && this._waterInput.IsUnderwater)
			{
				this.UnblockBuilding();
				return;
			}
			if (!this._isBlocked && !this._waterInput.IsUnderwater)
			{
				this.BlockBuilding();
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000081C3 File Offset: 0x000063C3
		public void BlockBuilding()
		{
			this._isBlocked = true;
			this._blockableObject.Block(this);
			EventHandler startedNeedingWater = this.StartedNeedingWater;
			if (startedNeedingWater == null)
			{
				return;
			}
			startedNeedingWater(this, EventArgs.Empty);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000081EE File Offset: 0x000063EE
		public void UnblockBuilding()
		{
			this._isBlocked = false;
			this._blockableObject.Unblock(this);
			EventHandler stoppedNeedingWater = this.StoppedNeedingWater;
			if (stoppedNeedingWater == null)
			{
				return;
			}
			stoppedNeedingWater(this, EventArgs.Empty);
		}

		// Token: 0x04000110 RID: 272
		public BlockableObject _blockableObject;

		// Token: 0x04000111 RID: 273
		public WaterInput _waterInput;

		// Token: 0x04000112 RID: 274
		public WaterInputCoordinates _waterInputCoordinates;

		// Token: 0x04000113 RID: 275
		public bool _isBlocked;
	}
}
