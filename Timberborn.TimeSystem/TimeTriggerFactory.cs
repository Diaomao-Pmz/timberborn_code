using System;

namespace Timberborn.TimeSystem
{
	// Token: 0x0200001B RID: 27
	public class TimeTriggerFactory : ITimeTriggerFactory
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000030DB File Offset: 0x000012DB
		public TimeTriggerFactory(IDayNightCycle dayNightCycle, TimeTriggerService timeTriggerService)
		{
			this._dayNightCycle = dayNightCycle;
			this._timeTriggerService = timeTriggerService;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000030F1 File Offset: 0x000012F1
		public ITimeTrigger Create(Action action, float delayInDays)
		{
			return new TimeTrigger(this._dayNightCycle, this._timeTriggerService, action, delayInDays);
		}

		// Token: 0x04000041 RID: 65
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000042 RID: 66
		public readonly TimeTriggerService _timeTriggerService;
	}
}
