using System;
using Timberborn.Debugging;
using Timberborn.QuickNotificationSystem;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x02000012 RID: 18
	public class WaterPoweredGeneratorSpeedChanger : IDevModule
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002B12 File Offset: 0x00000D12
		public WaterPoweredGeneratorSpeedChanger(WaterPoweredGeneratorSpeedCalculator waterPoweredGeneratorSpeedCalculator, QuickNotificationService quickNotificationService)
		{
			this._waterPoweredGeneratorSpeedCalculator = waterPoweredGeneratorSpeedCalculator;
			this._quickNotificationService = quickNotificationService;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B28 File Offset: 0x00000D28
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Water wheels: increase max speed", new Action(this.IncreaseMaxSpeed))).AddMethod(DevMethod.Create("Water wheels: decrease max speed", new Action(this.DecreaseMaxSpeed))).Build();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B75 File Offset: 0x00000D75
		public void IncreaseMaxSpeed()
		{
			this._waterPoweredGeneratorSpeedCalculator.IncreaseMaxSpeed(WaterPoweredGeneratorSpeedChanger.SpeedChange);
			this.SendNotification();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B8D File Offset: 0x00000D8D
		public void DecreaseMaxSpeed()
		{
			this._waterPoweredGeneratorSpeedCalculator.DecreaseMaxSpeed(WaterPoweredGeneratorSpeedChanger.SpeedChange);
			this.SendNotification();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BA5 File Offset: 0x00000DA5
		public void SendNotification()
		{
			this._quickNotificationService.SendNotification(string.Format("Water wheels max speed: {0:F2}", this._waterPoweredGeneratorSpeedCalculator.MaxSpeed));
		}

		// Token: 0x0400002B RID: 43
		public static readonly float SpeedChange = 0.1f;

		// Token: 0x0400002C RID: 44
		public readonly WaterPoweredGeneratorSpeedCalculator _waterPoweredGeneratorSpeedCalculator;

		// Token: 0x0400002D RID: 45
		public readonly QuickNotificationService _quickNotificationService;
	}
}
