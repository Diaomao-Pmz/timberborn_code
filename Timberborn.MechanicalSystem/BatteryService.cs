using System;
using Timberborn.TickSystem;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000009 RID: 9
	public class BatteryService : ITickableSingleton
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002390 File Offset: 0x00000590
		public BatteryService(MechanicalGraphRegistry mechanicalGraphRegistry, BatteryCharger batteryCharger, BatteryDischarger batteryDischarger)
		{
			this._mechanicalGraphRegistry = mechanicalGraphRegistry;
			this._batteryCharger = batteryCharger;
			this._batteryDischarger = batteryDischarger;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023AD File Offset: 0x000005AD
		public void Tick()
		{
			this.ChargeAndDischarge();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023B8 File Offset: 0x000005B8
		public void ChargeAndDischarge()
		{
			foreach (MechanicalGraph mechanicalGraph in this._mechanicalGraphRegistry.MechanicalGraphs)
			{
				this.ChargeAndDischarge(mechanicalGraph);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002414 File Offset: 0x00000614
		public void ChargeAndDischarge(MechanicalGraph mechanicalGraph)
		{
			int powerSurplus = mechanicalGraph.PowerSurplus;
			if (powerSurplus > 0)
			{
				this._batteryCharger.Charge(mechanicalGraph, powerSurplus);
				return;
			}
			if (powerSurplus < 0)
			{
				this._batteryDischarger.Discharge(mechanicalGraph, -powerSurplus);
			}
		}

		// Token: 0x0400000D RID: 13
		public readonly MechanicalGraphRegistry _mechanicalGraphRegistry;

		// Token: 0x0400000E RID: 14
		public readonly BatteryCharger _batteryCharger;

		// Token: 0x0400000F RID: 15
		public readonly BatteryDischarger _batteryDischarger;
	}
}
