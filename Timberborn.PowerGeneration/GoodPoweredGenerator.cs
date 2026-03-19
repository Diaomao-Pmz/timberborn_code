using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.GoodConsumingBuildingSystem;
using Timberborn.MechanicalSystem;
using Timberborn.TickSystem;

namespace Timberborn.PowerGeneration
{
	// Token: 0x02000009 RID: 9
	public class GoodPoweredGenerator : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002264 File Offset: 0x00000464
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._goodConsumingBuilding = base.GetComponent<GoodConsumingBuilding>();
			this._goodConsumingToggle = this._goodConsumingBuilding.GetGoodConsumingToggle();
			this._mechanicalNode.AddedToGraph += this.OnAddedToGraph;
			base.DisableComponent();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022B7 File Offset: 0x000004B7
		public override void StartTickable()
		{
			this.UpdateState();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022B7 File Offset: 0x000004B7
		public override void Tick()
		{
			this.UpdateState();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022BF File Offset: 0x000004BF
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022C7 File Offset: 0x000004C7
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022B7 File Offset: 0x000004B7
		public void OnAddedToGraph(object sender, EventArgs e)
		{
			this.UpdateState();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022D0 File Offset: 0x000004D0
		public void UpdateState()
		{
			if (this._mechanicalNode.Graph == null || (!this._mechanicalNode.Graph.RequiresPower && !this._goodConsumingBuilding.ConsumptionPaused))
			{
				this._goodConsumingToggle.PauseConsumption();
			}
			else if (this._mechanicalNode.Graph.RequiresPower && this._goodConsumingBuilding.ConsumptionPaused)
			{
				this._goodConsumingToggle.ResumeConsumption();
			}
			this._mechanicalNode.SetOutputMultiplier(this._goodConsumingBuilding.IsConsuming ? 1f : 0f);
		}

		// Token: 0x0400000B RID: 11
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400000C RID: 12
		public GoodConsumingBuilding _goodConsumingBuilding;

		// Token: 0x0400000D RID: 13
		public GoodConsumingToggle _goodConsumingToggle;
	}
}
