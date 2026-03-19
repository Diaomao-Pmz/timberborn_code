using System;
using Timberborn.Multithreading;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000007 RID: 7
	public readonly struct ClearBuffersTask : IParallelizerSingleTask
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public ClearBuffersTask(float[] contaminationsBuffer, byte[] targetedDiffusionCount, WaterFlow[] baseLevelFlows, Diffusions[] baseLevelDiffusions)
		{
			this._contaminationsBuffer = contaminationsBuffer;
			this._targetedDiffusionCount = targetedDiffusionCount;
			this._baseLevelFlows = baseLevelFlows;
			this._baseLevelDiffusions = baseLevelDiffusions;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		public void Run()
		{
			Array.Clear(this._contaminationsBuffer, 0, this._contaminationsBuffer.Length);
			Array.Clear(this._targetedDiffusionCount, 0, this._targetedDiffusionCount.Length);
			Array.Clear(this._baseLevelFlows, 0, this._baseLevelFlows.Length);
			Array.Clear(this._baseLevelDiffusions, 0, this._baseLevelDiffusions.Length);
		}

		// Token: 0x04000008 RID: 8
		public readonly float[] _contaminationsBuffer;

		// Token: 0x04000009 RID: 9
		public readonly byte[] _targetedDiffusionCount;

		// Token: 0x0400000A RID: 10
		public readonly WaterFlow[] _baseLevelFlows;

		// Token: 0x0400000B RID: 11
		public readonly Diffusions[] _baseLevelDiffusions;
	}
}
