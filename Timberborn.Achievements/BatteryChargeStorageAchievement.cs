using System;
using Timberborn.AchievementSystem;
using Timberborn.MechanicalSystem;
using Timberborn.TickSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200000B RID: 11
	public class BatteryChargeStorageAchievement : Achievement, ITickableSingleton
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002778 File Offset: 0x00000978
		public BatteryChargeStorageAchievement(MechanicalGraphRegistry mechanicalGraphRegistry)
		{
			this._mechanicalGraphRegistry = mechanicalGraphRegistry;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002787 File Offset: 0x00000987
		public override string Id
		{
			get
			{
				return "BATTERY_CHARGE_STORAGE";
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000278E File Offset: 0x0000098E
		public void Tick()
		{
			if (base.IsEnabled)
			{
				this.ValidateTotalCharge();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027A0 File Offset: 0x000009A0
		public void ValidateTotalCharge()
		{
			float num = 0f;
			foreach (MechanicalGraph mechanicalGraph in this._mechanicalGraphRegistry.MechanicalGraphs)
			{
				num += (float)mechanicalGraph.BatteryCharge;
			}
			if (num >= BatteryChargeStorageAchievement.RequiredCharge)
			{
				base.Unlock();
			}
		}

		// Token: 0x04000012 RID: 18
		public static readonly float RequiredCharge = 655321f;

		// Token: 0x04000013 RID: 19
		public readonly MechanicalGraphRegistry _mechanicalGraphRegistry;
	}
}
