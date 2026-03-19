using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Illumination;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;

namespace Timberborn.IlluminationUI
{
	// Token: 0x0200000C RID: 12
	public class NightTimeLightController : BaseComponent, IFinishedStateListener
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002614 File Offset: 0x00000814
		public NightTimeLightController(EventBus eventBus, IDayNightCycle dayNightCycle)
		{
			this._eventBus = eventBus;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000262A File Offset: 0x0000082A
		public void OnEnterFinishedState()
		{
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
			this._eventBus.Register(this);
			if (this._dayNightCycle.IsNighttime)
			{
				this._illuminatorToggle.TurnOn();
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002661 File Offset: 0x00000861
		public void OnExitFinishedState()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000266F File Offset: 0x0000086F
		[OnEvent]
		public void OnNighttimeStartEvent(NighttimeStartEvent nighttimeStartEvent)
		{
			this._illuminatorToggle.TurnOn();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000267C File Offset: 0x0000087C
		[OnEvent]
		public void OnDayTimeStartEvent(DaytimeStartEvent daytimeStartEvent)
		{
			this._illuminatorToggle.TurnOff();
		}

		// Token: 0x04000019 RID: 25
		public readonly EventBus _eventBus;

		// Token: 0x0400001A RID: 26
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400001B RID: 27
		public IlluminatorToggle _illuminatorToggle;
	}
}
