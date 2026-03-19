using System;

namespace Timberborn.TimeSystem
{
	// Token: 0x02000010 RID: 16
	public interface ITimeTrigger
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000073 RID: 115
		float DaysLeft { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000074 RID: 116
		float Progress { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000075 RID: 117
		bool Finished { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000076 RID: 118
		bool InProgress { get; }

		// Token: 0x06000077 RID: 119
		void Reset();

		// Token: 0x06000078 RID: 120
		void Resume();

		// Token: 0x06000079 RID: 121
		void Pause();

		// Token: 0x0600007A RID: 122
		void FastForwardProgress(float progress);
	}
}
