using System;
using Timberborn.Metrics;

namespace Timberborn.TickSystem
{
	// Token: 0x0200000E RID: 14
	public class MeteredTickableComponent
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002100 File Offset: 0x00000300
		public MeteredTickableComponent(TickableComponent tickableComponent, ITimerMetric timerMetric, bool metricsEnabled)
		{
			this._tickableComponent = tickableComponent;
			this._timerMetric = timerMetric;
			this._metricsEnabled = metricsEnabled;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000211D File Offset: 0x0000031D
		public bool Enabled
		{
			get
			{
				return this._tickableComponent.Enabled;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000212A File Offset: 0x0000032A
		public void StartAndTick()
		{
			if (this._metricsEnabled)
			{
				this._timerMetric.Resume();
			}
			this._tickableComponent.StartAndTick();
			if (this._metricsEnabled)
			{
				this._timerMetric.Pause();
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly TickableComponent _tickableComponent;

		// Token: 0x04000009 RID: 9
		public readonly ITimerMetric _timerMetric;

		// Token: 0x0400000A RID: 10
		public readonly bool _metricsEnabled;
	}
}
