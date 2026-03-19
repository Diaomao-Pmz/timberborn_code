using System;
using Timberborn.Multithreading;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x0200000E RID: 14
	public readonly struct MoistureDataPreparationTask : IParallelizerSingleTask
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002EE2 File Offset: 0x000010E2
		public MoistureDataPreparationTask(float[] moistureLevels, float[] lastTickMoistureLevels, bool[] moistureLevelsChangedLastTick)
		{
			this._moistureLevels = moistureLevels;
			this._lastTickMoistureLevels = lastTickMoistureLevels;
			this._moistureLevelsChangedLastTick = moistureLevelsChangedLastTick;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002EFC File Offset: 0x000010FC
		public void Run()
		{
			MemoryExtensions.AsSpan<float>(this._moistureLevels).CopyTo(this._lastTickMoistureLevels);
			Array.Clear(this._moistureLevelsChangedLastTick, 0, this._moistureLevelsChangedLastTick.Length);
		}

		// Token: 0x0400002F RID: 47
		public readonly float[] _moistureLevels;

		// Token: 0x04000030 RID: 48
		public readonly float[] _lastTickMoistureLevels;

		// Token: 0x04000031 RID: 49
		public readonly bool[] _moistureLevelsChangedLastTick;
	}
}
