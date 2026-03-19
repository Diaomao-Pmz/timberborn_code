using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x0200000B RID: 11
	public class MechanicalBuilding : BaseComponent, IAwakableComponent, IFinishedStateListener, IFinishedPausable, IBuildingEfficiencyProvider
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000244C File Offset: 0x0000064C
		public bool ActiveAndPowered
		{
			get
			{
				return this._mechanicalNode.ActiveAndPowered;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002459 File Offset: 0x00000659
		public bool CanUse
		{
			get
			{
				return this.ActiveAndPowered;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002461 File Offset: 0x00000661
		public float Efficiency
		{
			get
			{
				return this._mechanicalNode.PowerEfficiency;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000246E File Offset: 0x0000066E
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			base.DisableComponent();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002482 File Offset: 0x00000682
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000248A File Offset: 0x0000068A
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002492 File Offset: 0x00000692
		public void SetConsumptionDisabled(bool value)
		{
			if (this._mechanicalNode.IsConsumer)
			{
				this._mechanicalNode.SetInputMultiplier((float)(value ? 0 : 1));
			}
		}

		// Token: 0x04000010 RID: 16
		public MechanicalNode _mechanicalNode;
	}
}
