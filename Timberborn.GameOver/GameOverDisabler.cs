using System;
using Timberborn.Benchmarking;
using Timberborn.GameWonderCompletion;
using Timberborn.SingletonSystem;

namespace Timberborn.GameOver
{
	// Token: 0x02000007 RID: 7
	public class GameOverDisabler : ILoadableSingleton
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002275 File Offset: 0x00000475
		public GameOverDisabler(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002284 File Offset: 0x00000484
		public bool Disabled
		{
			get
			{
				return this._blockers > 0;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000228F File Offset: 0x0000048F
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000229D File Offset: 0x0000049D
		[OnEvent]
		public void OnBenchmarkStarted(BenchmarkStartedEvent benchmarkStartedEvent)
		{
			this._blockers++;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000229D File Offset: 0x0000049D
		[OnEvent]
		public void OnWonderCompletionCountdownStarted(WonderCompletionCountdownStartedEvent wonderCompletionCountdownStartedEvent)
		{
			this._blockers++;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022AD File Offset: 0x000004AD
		[OnEvent]
		public void OnWonderCompleted(WonderCompletedEvent wonderCompletedEvent)
		{
			this._blockers--;
		}

		// Token: 0x04000010 RID: 16
		public readonly EventBus _eventBus;

		// Token: 0x04000011 RID: 17
		public int _blockers;
	}
}
