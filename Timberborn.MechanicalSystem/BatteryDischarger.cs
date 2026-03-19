using System;
using System.Collections.Generic;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000008 RID: 8
	public class BatteryDischarger
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002234 File Offset: 0x00000434
		public BatteryDischarger(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002259 File Offset: 0x00000459
		public void Discharge(MechanicalGraph mechanicalGraph, int dischargingPower)
		{
			if (dischargingPower > 0)
			{
				this.GetDischargableBatteries(mechanicalGraph);
				if (this._batteries.Count > 0)
				{
					this.DischargeBatteries(dischargingPower);
					this._batteries.Clear();
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002288 File Offset: 0x00000488
		public void GetDischargableBatteries(MechanicalGraph graph)
		{
			foreach (MechanicalNode mechanicalNode in graph.Batteries)
			{
				if (mechanicalNode.Active && mechanicalNode.Actuals.BatteryCharge > 0)
				{
					this._batteries.Add(mechanicalNode);
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022FC File Offset: 0x000004FC
		public void DischargeBatteries(int dischargingPower)
		{
			float chargeToRemovePerBattery = Mathf.Max(this._dayNightCycle.FixedDeltaTimeInHours * (float)dischargingPower / (float)this._batteries.Count, this._minChargeToRemovePerBattery);
			this.RemoveChargeFromBatteries(chargeToRemovePerBattery);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002338 File Offset: 0x00000538
		public void RemoveChargeFromBatteries(float chargeToRemovePerBattery)
		{
			foreach (MechanicalNode mechanicalNode in this._batteries)
			{
				mechanicalNode.Battery.ModifyCharge(-chargeToRemovePerBattery);
			}
		}

		// Token: 0x0400000A RID: 10
		public readonly float _minChargeToRemovePerBattery = 0.01f;

		// Token: 0x0400000B RID: 11
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400000C RID: 12
		public readonly List<MechanicalNode> _batteries = new List<MechanicalNode>();
	}
}
