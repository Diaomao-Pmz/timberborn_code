using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Wonders
{
	// Token: 0x02000011 RID: 17
	public class WonderDeactivationTimer : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000027C9 File Offset: 0x000009C9
		public WonderDeactivationTimer(ITimeTriggerFactory timeTriggerFactory)
		{
			this._timeTriggerFactory = timeTriggerFactory;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027D8 File Offset: 0x000009D8
		public void Awake()
		{
			this._wonder = base.GetComponent<Wonder>();
			this._wonder.WonderActivated += this.OnWonderActivated;
			float timerDelayInHours = base.GetComponent<WonderDeactivationTimerSpec>().TimerDelayInHours;
			this._timeTrigger = this._timeTriggerFactory.Create(new Action(this.DeactivateWonder), timerDelayInHours / 24f);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002838 File Offset: 0x00000A38
		public void Save(IEntitySaver entitySaver)
		{
			if (this._timeTrigger.InProgress)
			{
				entitySaver.GetComponent(WonderDeactivationTimer.WonderDeactivationTimerKey).Set(WonderDeactivationTimer.DelayProgressKey, this._timeTrigger.Progress);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002868 File Offset: 0x00000A68
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(WonderDeactivationTimer.WonderDeactivationTimerKey, out objectLoader))
			{
				float progress = objectLoader.Get(WonderDeactivationTimer.DelayProgressKey);
				this._timeTrigger.FastForwardProgress(progress);
				this._timeTrigger.Resume();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028A7 File Offset: 0x00000AA7
		public void DeleteEntity()
		{
			this._timeTrigger.Reset();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028B4 File Offset: 0x00000AB4
		public void OnWonderActivated(object sender, EventArgs e)
		{
			this._timeTrigger.Reset();
			this._timeTrigger.Resume();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028CC File Offset: 0x00000ACC
		public void DeactivateWonder()
		{
			this._wonder.Deactivate();
		}

		// Token: 0x04000023 RID: 35
		public static readonly ComponentKey WonderDeactivationTimerKey = new ComponentKey("WonderDeactivationTimer");

		// Token: 0x04000024 RID: 36
		public static readonly PropertyKey<float> DelayProgressKey = new PropertyKey<float>("DelayProgress");

		// Token: 0x04000025 RID: 37
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x04000026 RID: 38
		public Wonder _wonder;

		// Token: 0x04000027 RID: 39
		public ITimeTrigger _timeTrigger;
	}
}
