using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.MechanicalSystem;
using Timberborn.TickSystem;
using Timberborn.WorkSystem;

namespace Timberborn.Workshops
{
	// Token: 0x0200002A RID: 42
	public class WorkplacePowerConsumptionSwitch : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600014F RID: 335 RVA: 0x000054DB File Offset: 0x000036DB
		public void Awake()
		{
			this._mechanicalBuilding = base.GetComponent<MechanicalBuilding>();
			this._workplace = base.GetComponent<Workplace>();
			base.DisableComponent();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000054FB File Offset: 0x000036FB
		public void OnEnterFinishedState()
		{
			if (this._mechanicalBuilding)
			{
				base.EnableComponent();
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00002711 File Offset: 0x00000911
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005510 File Offset: 0x00003710
		public override void StartTickable()
		{
			this.UpdatePowerConsumption();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005510 File Offset: 0x00003710
		public override void Tick()
		{
			this.UpdatePowerConsumption();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005518 File Offset: 0x00003718
		public void UpdatePowerConsumption()
		{
			this._mechanicalBuilding.SetConsumptionDisabled(!this._workplace.AnyWorkerHasJobRunning());
		}

		// Token: 0x04000095 RID: 149
		public MechanicalBuilding _mechanicalBuilding;

		// Token: 0x04000096 RID: 150
		public Workplace _workplace;
	}
}
