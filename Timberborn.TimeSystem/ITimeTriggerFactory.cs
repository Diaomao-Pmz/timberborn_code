using System;

namespace Timberborn.TimeSystem
{
	// Token: 0x02000011 RID: 17
	public interface ITimeTriggerFactory
	{
		// Token: 0x0600007B RID: 123
		ITimeTrigger Create(Action action, float delayInDays);
	}
}
