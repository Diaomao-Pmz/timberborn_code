using System;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Wellbeing
{
	// Token: 0x0200000D RID: 13
	public class WellbeingHighscore : ITickableSingleton, ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000221F File Offset: 0x0000041F
		public WellbeingHighscore(ISingletonLoader singletonLoader, WellbeingService wellbeingService, EventBus eventBus)
		{
			this._singletonLoader = singletonLoader;
			this._wellbeingService = wellbeingService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000223C File Offset: 0x0000043C
		public void Tick()
		{
			if (this._wellbeingService.AverageGlobalWellbeing > this._averageWellbeingHighscore)
			{
				this._averageWellbeingHighscore = this._wellbeingService.AverageGlobalWellbeing;
				int tickCounter = this._tickCounter;
				this._tickCounter = tickCounter + 1;
				if (tickCounter > 1)
				{
					this._eventBus.Post(new NewWellbeingHighscoreEvent(this._averageWellbeingHighscore));
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002298 File Offset: 0x00000498
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(WellbeingHighscore.WellbeingHighscoreKey, out objectLoader))
			{
				this._averageWellbeingHighscore = objectLoader.Get(WellbeingHighscore.AverageWellbeingHighscoreKey);
			}
			this._eventBus.Register(this);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022D6 File Offset: 0x000004D6
		public void Save(ISingletonSaver singletonSaver)
		{
			singletonSaver.GetSingleton(WellbeingHighscore.WellbeingHighscoreKey).Set(WellbeingHighscore.AverageWellbeingHighscoreKey, this._averageWellbeingHighscore);
		}

		// Token: 0x0400000D RID: 13
		public static readonly SingletonKey WellbeingHighscoreKey = new SingletonKey("WellbeingHighscore");

		// Token: 0x0400000E RID: 14
		public static readonly PropertyKey<int> AverageWellbeingHighscoreKey = new PropertyKey<int>("AverageWellbeingHighscore");

		// Token: 0x0400000F RID: 15
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000010 RID: 16
		public readonly WellbeingService _wellbeingService;

		// Token: 0x04000011 RID: 17
		public readonly EventBus _eventBus;

		// Token: 0x04000012 RID: 18
		public int _averageWellbeingHighscore;

		// Token: 0x04000013 RID: 19
		public int _tickCounter;
	}
}
