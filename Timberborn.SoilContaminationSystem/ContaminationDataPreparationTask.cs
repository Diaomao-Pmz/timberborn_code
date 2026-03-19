using System;
using Timberborn.Multithreading;

namespace Timberborn.SoilContaminationSystem
{
	// Token: 0x02000009 RID: 9
	public readonly struct ContaminationDataPreparationTask : IParallelizerSingleTask
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002853 File Offset: 0x00000A53
		public ContaminationDataPreparationTask(float[] contaminationCandidates, float[] lastTickContaminationCandidates, bool[] contaminationsChangedLastTick)
		{
			this._contaminationCandidates = contaminationCandidates;
			this._lastTickContaminationCandidates = lastTickContaminationCandidates;
			this._contaminationsChangedLastTick = contaminationsChangedLastTick;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000286C File Offset: 0x00000A6C
		public void Run()
		{
			MemoryExtensions.AsSpan<float>(this._contaminationCandidates).CopyTo(this._lastTickContaminationCandidates);
			Array.Clear(this._contaminationsChangedLastTick, 0, this._contaminationsChangedLastTick.Length);
		}

		// Token: 0x04000022 RID: 34
		public readonly float[] _contaminationCandidates;

		// Token: 0x04000023 RID: 35
		public readonly float[] _lastTickContaminationCandidates;

		// Token: 0x04000024 RID: 36
		public readonly bool[] _contaminationsChangedLastTick;
	}
}
