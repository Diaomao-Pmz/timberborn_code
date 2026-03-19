using System;
using System.Collections.Generic;
using Timberborn.TimeSystem;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000007 RID: 7
	public class BatteryCharger
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public BatteryCharger(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211A File Offset: 0x0000031A
		public void Charge(MechanicalGraph mechanicalGraph, int chargingPower)
		{
			this.GetChargableBatteries(mechanicalGraph);
			if (this._batteries.Count > 0)
			{
				this.ChargeBatteries(chargingPower);
				this._batteries.Clear();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002144 File Offset: 0x00000344
		public void GetChargableBatteries(MechanicalGraph mechanicalGraph)
		{
			foreach (MechanicalNode mechanicalNode in mechanicalGraph.Batteries)
			{
				if (mechanicalNode.Active && mechanicalNode.Actuals.BatteryCharge < mechanicalNode.Actuals.BatteryCapacity)
				{
					this._batteries.Add(mechanicalNode);
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C0 File Offset: 0x000003C0
		public void ChargeBatteries(int chargingPower)
		{
			float chargeDelta = this._dayNightCycle.FixedDeltaTimeInHours * (float)chargingPower / (float)this._batteries.Count;
			foreach (MechanicalNode mechanicalNode in this._batteries)
			{
				mechanicalNode.Battery.ModifyCharge(chargeDelta);
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000009 RID: 9
		public readonly List<MechanicalNode> _batteries = new List<MechanicalNode>();
	}
}
