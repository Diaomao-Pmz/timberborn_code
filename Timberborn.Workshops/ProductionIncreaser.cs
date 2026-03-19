using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;

namespace Timberborn.Workshops
{
	// Token: 0x0200001E RID: 30
	public class ProductionIncreaser : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00003F56 File Offset: 0x00002156
		public ProductionIncreaser(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003F65 File Offset: 0x00002165
		public void Awake()
		{
			this._manufactory = base.GetComponent<Manufactory>();
			base.DisableComponent();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003F79 File Offset: 0x00002179
		public override void Tick()
		{
			this._manufactory.IncreaseProductionProgress(this._dayNightCycle.FixedDeltaTimeInHours);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003F91 File Offset: 0x00002191
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002711 File Offset: 0x00000911
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x04000065 RID: 101
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000066 RID: 102
		public Manufactory _manufactory;
	}
}
