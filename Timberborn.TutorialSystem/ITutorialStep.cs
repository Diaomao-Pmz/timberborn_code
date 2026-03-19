using System;

namespace Timberborn.TutorialSystem
{
	// Token: 0x0200000A RID: 10
	public interface ITutorialStep
	{
		// Token: 0x06000015 RID: 21
		string Description();

		// Token: 0x06000016 RID: 22
		bool Achieved();
	}
}
