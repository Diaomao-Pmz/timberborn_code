using System;
using Timberborn.TimeSystem;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x02000011 RID: 17
	public class WaterPoweredGeneratorSpeedCalculator
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002A8D File Offset: 0x00000C8D
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002A95 File Offset: 0x00000C95
		public float MaxSpeed { get; private set; } = 1f;

		// Token: 0x0600004D RID: 77 RVA: 0x00002A9E File Offset: 0x00000C9E
		public WaterPoweredGeneratorSpeedCalculator(NonlinearAnimationManager nonlinearAnimationManager)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public float CalculateSpeed(float generatedRotation)
		{
			int num = (generatedRotation < 0f) ? -1 : 1;
			return Math.Min(Math.Abs(generatedRotation), this.MaxSpeed) * this._nonlinearAnimationManager.SpeedMultiplier * (float)num;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002AF2 File Offset: 0x00000CF2
		public void IncreaseMaxSpeed(float change)
		{
			this.MaxSpeed += change;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B02 File Offset: 0x00000D02
		public void DecreaseMaxSpeed(float change)
		{
			this.MaxSpeed -= change;
		}

		// Token: 0x0400002A RID: 42
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;
	}
}
