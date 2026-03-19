using System;

namespace Timberborn.TutorialSystem
{
	// Token: 0x0200000C RID: 12
	public interface ITutorialTriggers
	{
		// Token: 0x06000018 RID: 24
		void AddTrigger(string triggerId);

		// Token: 0x06000019 RID: 25
		bool TriggerPending(string triggerId);
	}
}
