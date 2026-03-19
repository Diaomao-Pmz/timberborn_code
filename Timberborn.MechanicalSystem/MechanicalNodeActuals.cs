using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000019 RID: 25
	public class MechanicalNodeActuals : BaseComponent
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000A6 RID: 166 RVA: 0x00003694 File Offset: 0x00001894
		// (remove) Token: 0x060000A7 RID: 167 RVA: 0x000036CC File Offset: 0x000018CC
		public event EventHandler<ValueChangedEventArgs<int>> PowerOutputChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000A8 RID: 168 RVA: 0x00003704 File Offset: 0x00001904
		// (remove) Token: 0x060000A9 RID: 169 RVA: 0x0000373C File Offset: 0x0000193C
		public event EventHandler<ValueChangedEventArgs<int>> PowerInputChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000AA RID: 170 RVA: 0x00003774 File Offset: 0x00001974
		// (remove) Token: 0x060000AB RID: 171 RVA: 0x000037AC File Offset: 0x000019AC
		public event EventHandler<ValueChangedEventArgs<int>> BatteryChargeChanged;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000AC RID: 172 RVA: 0x000037E4 File Offset: 0x000019E4
		// (remove) Token: 0x060000AD RID: 173 RVA: 0x0000381C File Offset: 0x00001A1C
		public event EventHandler<ValueChangedEventArgs<int>> BatteryCapacityChanged;

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003851 File Offset: 0x00001A51
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003859 File Offset: 0x00001A59
		public int PowerOutput { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003862 File Offset: 0x00001A62
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x0000386A File Offset: 0x00001A6A
		public int PowerInput { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003873 File Offset: 0x00001A73
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x0000387B File Offset: 0x00001A7B
		public int BatteryCharge { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003884 File Offset: 0x00001A84
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000388C File Offset: 0x00001A8C
		public int BatteryCapacity { get; private set; }

		// Token: 0x060000B6 RID: 182 RVA: 0x00003898 File Offset: 0x00001A98
		public void SetPowerOutput(int value)
		{
			if (this.PowerOutput != value)
			{
				int powerOutput = this.PowerOutput;
				this.PowerOutput = value;
				EventHandler<ValueChangedEventArgs<int>> powerOutputChanged = this.PowerOutputChanged;
				if (powerOutputChanged == null)
				{
					return;
				}
				powerOutputChanged(this, new ValueChangedEventArgs<int>(powerOutput, value));
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000038D4 File Offset: 0x00001AD4
		public void SetPowerInput(int value)
		{
			if (this.PowerInput != value)
			{
				int powerInput = this.PowerInput;
				this.PowerInput = value;
				EventHandler<ValueChangedEventArgs<int>> powerInputChanged = this.PowerInputChanged;
				if (powerInputChanged == null)
				{
					return;
				}
				powerInputChanged(this, new ValueChangedEventArgs<int>(powerInput, value));
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003910 File Offset: 0x00001B10
		public void SetBatteryCharge(int value)
		{
			if (this.BatteryCharge != value)
			{
				int batteryCharge = this.BatteryCharge;
				this.BatteryCharge = value;
				EventHandler<ValueChangedEventArgs<int>> batteryChargeChanged = this.BatteryChargeChanged;
				if (batteryChargeChanged == null)
				{
					return;
				}
				batteryChargeChanged(this, new ValueChangedEventArgs<int>(batteryCharge, value));
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000394C File Offset: 0x00001B4C
		public void SetBatteryCapacity(int value)
		{
			if (this.BatteryCapacity != value)
			{
				int batteryCapacity = this.BatteryCapacity;
				this.BatteryCapacity = value;
				EventHandler<ValueChangedEventArgs<int>> batteryCapacityChanged = this.BatteryCapacityChanged;
				if (batteryCapacityChanged == null)
				{
					return;
				}
				batteryCapacityChanged(this, new ValueChangedEventArgs<int>(batteryCapacity, value));
			}
		}
	}
}
