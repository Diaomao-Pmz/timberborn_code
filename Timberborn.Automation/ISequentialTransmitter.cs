using System;

namespace Timberborn.Automation
{
	// Token: 0x02000020 RID: 32
	public interface ISequentialTransmitter : ITransmitter
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D5 RID: 213
		bool IsProcessingNewInput { get; }

		// Token: 0x060000D6 RID: 214
		void EvaluateNext();

		// Token: 0x060000D7 RID: 215
		void CommitTick();

		// Token: 0x060000D8 RID: 216
		void Reset();
	}
}
