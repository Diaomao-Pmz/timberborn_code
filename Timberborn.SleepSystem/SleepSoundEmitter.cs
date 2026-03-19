using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.EntitySystem;
using Timberborn.NeedBehaviorSystem;
using Timberborn.TickSystem;

namespace Timberborn.SleepSystem
{
	// Token: 0x0200000E RID: 14
	public class SleepSoundEmitter : TickableComponent, IAwakableComponent, IDeletableEntity
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000028E2 File Offset: 0x00000AE2
		public SleepSoundEmitter(SleepSoundController sleepSoundController)
		{
			this._sleepSoundController = sleepSoundController;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000028F1 File Offset: 0x00000AF1
		public void Awake()
		{
			this._behaviorManager = base.GetComponent<BehaviorManager>();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028FF File Offset: 0x00000AFF
		public void DeleteEntity()
		{
			if (this._wasSleeping)
			{
				this._sleepSoundController.RemoveSleepingBeaver(this);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002918 File Offset: 0x00000B18
		public override void Tick()
		{
			bool flag = this._behaviorManager.IsRunningBehavior<SleepNeedBehavior>() && this._behaviorManager.IsRunningExecutor<ApplyEffectExecutor>();
			if (this._wasSleeping && !flag)
			{
				this._sleepSoundController.RemoveSleepingBeaver(this);
			}
			if (!this._wasSleeping && flag)
			{
				this._sleepSoundController.AddSleepingBeaver(this);
			}
			this._wasSleeping = flag;
		}

		// Token: 0x04000026 RID: 38
		public readonly SleepSoundController _sleepSoundController;

		// Token: 0x04000027 RID: 39
		public BehaviorManager _behaviorManager;

		// Token: 0x04000028 RID: 40
		public bool _wasSleeping;
	}
}
