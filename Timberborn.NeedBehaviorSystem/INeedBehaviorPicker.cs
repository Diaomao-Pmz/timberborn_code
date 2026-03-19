using System;
using Timberborn.BehaviorSystem;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x02000019 RID: 25
	public interface INeedBehaviorPicker
	{
		// Token: 0x0600006D RID: 109
		Behavior GetBestNeedBehaviorAffectingNeedsInCriticalState();

		// Token: 0x0600006E RID: 110
		Behavior GetBestNeedBehavior();

		// Token: 0x0600006F RID: 111
		bool NeedIsBeingCriticallySatisfied(string needId);
	}
}
