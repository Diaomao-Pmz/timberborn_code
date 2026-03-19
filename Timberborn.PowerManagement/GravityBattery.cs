using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObstacles;
using Timberborn.BlockSystem;
using Timberborn.MechanicalSystem;
using UnityEngine;

namespace Timberborn.PowerManagement
{
	// Token: 0x0200000C RID: 12
	public class GravityBattery : BaseComponent, IAwakableComponent, IFinishedStateListener, IBattery
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000027BB File Offset: 0x000009BB
		public int CapacityPerTile
		{
			get
			{
				return this._gravityBatterySpec.CapacityPerTile;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027C8 File Offset: 0x000009C8
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._layeredBlockObstacle = base.GetComponent<LayeredBlockObstacle>();
			this._gravityBatterySpec = base.GetComponent<GravityBatterySpec>();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027EE File Offset: 0x000009EE
		public void OnEnterFinishedState()
		{
			this.UpdateNode();
			this._layeredBlockObstacle.MaxOccupancyRangeChanged += this.OnDependenciesChanged;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000280D File Offset: 0x00000A0D
		public void OnExitFinishedState()
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002810 File Offset: 0x00000A10
		public void ModifyCharge(float chargeDelta)
		{
			float occupancyRangeDelta = -chargeDelta / (float)this.CapacityPerTile;
			this._layeredBlockObstacle.ModifyOccupancyRange(occupancyRangeDelta);
			this.UpdateNode();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000283A File Offset: 0x00000A3A
		public void OnDependenciesChanged(object sender, EventArgs e)
		{
			this.UpdateNode();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002844 File Offset: 0x00000A44
		public void UpdateNode()
		{
			this._mechanicalNode.SetNominalBatteryCharge(Mathf.CeilToInt((float)this.CapacityPerTile * (this._layeredBlockObstacle.MaxOccupancyRange - this._layeredBlockObstacle.OccupancyRange)));
			this._mechanicalNode.SetNominalBatteryCapacity(Mathf.CeilToInt(this._layeredBlockObstacle.MaxOccupancyRange * (float)this.CapacityPerTile));
		}

		// Token: 0x0400001D RID: 29
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400001E RID: 30
		public LayeredBlockObstacle _layeredBlockObstacle;

		// Token: 0x0400001F RID: 31
		public GravityBatterySpec _gravityBatterySpec;
	}
}
