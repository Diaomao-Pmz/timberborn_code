using System;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.GameWonderCompletion
{
	// Token: 0x0200000E RID: 14
	public class WonderCompletionCountdownStarter : ITickableSingleton, ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002709 File Offset: 0x00000909
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002711 File Offset: 0x00000911
		public bool CountdownFinished { get; private set; }

		// Token: 0x0600003A RID: 58 RVA: 0x0000271A File Offset: 0x0000091A
		public WonderCompletionCountdownStarter(GameWonderCompletionService gameWonderCompletionService, EventBus eventBus, IDayNightCycle dayNightCycle, ISingletonLoader singletonLoader)
		{
			this._gameWonderCompletionService = gameWonderCompletionService;
			this._eventBus = eventBus;
			this._dayNightCycle = dayNightCycle;
			this._singletonLoader = singletonLoader;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000274A File Offset: 0x0000094A
		public void Tick()
		{
			if (!this.CountdownFinished && this._unlockDay < this._dayNightCycle.PartialDayNumber)
			{
				this._gameWonderCompletionService.CompleteWonder();
				this._eventBus.Post(new WonderCompletedEvent());
				this.CountdownFinished = true;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000278C File Offset: 0x0000098C
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(WonderCompletionCountdownStarter.WonderCompletionCountdownStarterKey, out objectLoader))
			{
				this.CountdownFinished = objectLoader.Get(WonderCompletionCountdownStarter.CountdownFinishedKey);
				this._unlockDay = objectLoader.Get(WonderCompletionCountdownStarter.UnlockDayKey);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027CF File Offset: 0x000009CF
		public void Save(ISingletonSaver singletonSaver)
		{
			IObjectSaver singleton = singletonSaver.GetSingleton(WonderCompletionCountdownStarter.WonderCompletionCountdownStarterKey);
			singleton.Set(WonderCompletionCountdownStarter.CountdownFinishedKey, this.CountdownFinished);
			singleton.Set(WonderCompletionCountdownStarter.UnlockDayKey, this._unlockDay);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000027FD File Offset: 0x000009FD
		public void BeginUnlockCountdown()
		{
			if (this._unlockDay == 3.4028235E+38f)
			{
				this._eventBus.Post(new WonderCompletionCountdownStartedEvent());
				this._unlockDay = this._dayNightCycle.DayNumberHoursFromNow(WonderCompletionCountdownStarter.UnlockOffsetInHours);
			}
		}

		// Token: 0x0400001C RID: 28
		public static readonly float UnlockOffsetInHours = 0.5f;

		// Token: 0x0400001D RID: 29
		public static readonly SingletonKey WonderCompletionCountdownStarterKey = new SingletonKey("WonderCompletionCountdownStarter");

		// Token: 0x0400001E RID: 30
		public static readonly PropertyKey<bool> CountdownFinishedKey = new PropertyKey<bool>("CountdownFinished");

		// Token: 0x0400001F RID: 31
		public static readonly PropertyKey<float> UnlockDayKey = new PropertyKey<float>("UnlockDay");

		// Token: 0x04000021 RID: 33
		public readonly GameWonderCompletionService _gameWonderCompletionService;

		// Token: 0x04000022 RID: 34
		public readonly EventBus _eventBus;

		// Token: 0x04000023 RID: 35
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000024 RID: 36
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000025 RID: 37
		public float _unlockDay = float.MaxValue;
	}
}
