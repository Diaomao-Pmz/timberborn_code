using System;
using Timberborn.Characters;
using Timberborn.PopulationStatisticsSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.BeaverContaminationSystem
{
	// Token: 0x0200000E RID: 14
	public class GlobalBeaverContaminationStatisticsProvider : IContaminationStatisticsProvider, ILoadableSingleton
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000029D5 File Offset: 0x00000BD5
		public GlobalBeaverContaminationStatisticsProvider(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029EF File Offset: 0x00000BEF
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029FD File Offset: 0x00000BFD
		public BeaverContaminationStatistics GetContaminationStatistics()
		{
			return new BeaverContaminationStatistics(this._beaverContaminationRegistry.NumberOfContaminatedAdults, this._beaverContaminationRegistry.NumberOfContaminatedChildren);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A1C File Offset: 0x00000C1C
		[OnEvent]
		public void OnCharacterCreated(CharacterCreatedEvent characterCreatedEvent)
		{
			Contaminable component = characterCreatedEvent.Character.GetComponent<Contaminable>();
			if (component != null)
			{
				this._beaverContaminationRegistry.AddContaminable(component);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A44 File Offset: 0x00000C44
		[OnEvent]
		public void OnCharacterKilled(CharacterKilledEvent characterKilledEvent)
		{
			Contaminable component = characterKilledEvent.Character.GetComponent<Contaminable>();
			if (component != null)
			{
				this._beaverContaminationRegistry.RemoveContaminable(component);
			}
		}

		// Token: 0x04000025 RID: 37
		public readonly EventBus _eventBus;

		// Token: 0x04000026 RID: 38
		public readonly BeaverContaminationRegistry _beaverContaminationRegistry = new BeaverContaminationRegistry();
	}
}
