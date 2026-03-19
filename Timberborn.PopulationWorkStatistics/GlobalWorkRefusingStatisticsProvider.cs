using System;
using Timberborn.Characters;
using Timberborn.PopulationStatisticsSystem;
using Timberborn.SingletonSystem;
using Timberborn.WorkSystem;

namespace Timberborn.PopulationWorkStatistics
{
	// Token: 0x02000007 RID: 7
	public class GlobalWorkRefusingStatisticsProvider : ILoadableSingleton, IWorkRefusingStatisticsProvider
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002419 File Offset: 0x00000619
		public GlobalWorkRefusingStatisticsProvider(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002433 File Offset: 0x00000633
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002441 File Offset: 0x00000641
		public WorkRefusingStatistics GetWorkRefusingStatistics(string workerType)
		{
			return this._workRefuserRegistry.GetWorkRefusingStatistics(workerType);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002450 File Offset: 0x00000650
		[OnEvent]
		public void OnCharacterCreated(CharacterCreatedEvent characterCreatedEvent)
		{
			WorkRefuser component = characterCreatedEvent.Character.GetComponent<WorkRefuser>();
			if (component != null)
			{
				this._workRefuserRegistry.AddWorkRefuser(component);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002478 File Offset: 0x00000678
		[OnEvent]
		public void OnCharacterKilled(CharacterKilledEvent characterKilledEvent)
		{
			WorkRefuser component = characterKilledEvent.Character.GetComponent<WorkRefuser>();
			if (component != null)
			{
				this._workRefuserRegistry.RemoveWorkRefuser(component);
			}
		}

		// Token: 0x0400000A RID: 10
		public readonly EventBus _eventBus;

		// Token: 0x0400000B RID: 11
		public readonly WorkRefuserRegistry _workRefuserRegistry = new WorkRefuserRegistry();
	}
}
