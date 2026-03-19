using System;
using System.Linq;
using Timberborn.AchievementSystem;
using Timberborn.Beavers;
using Timberborn.BlockSystem;
using Timberborn.Characters;
using Timberborn.DwellingSystem;
using Timberborn.EntitySystem;
using Timberborn.GameFactionSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Achievements
{
	// Token: 0x0200004D RID: 77
	public class ReachPopulationWithoutDwellingsAchievement : Achievement, ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x0600012A RID: 298 RVA: 0x000049C4 File Offset: 0x00002BC4
		public ReachPopulationWithoutDwellingsAchievement(ISingletonLoader singletonLoader, EntityComponentRegistry entityComponentRegistry, FactionService factionService, BeaverPopulation beaverPopulation, EventBus eventBus)
		{
			this._singletonLoader = singletonLoader;
			this._entityComponentRegistry = entityComponentRegistry;
			this._factionService = factionService;
			this._beaverPopulation = beaverPopulation;
			this._eventBus = eventBus;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000049F1 File Offset: 0x00002BF1
		public override string Id
		{
			get
			{
				return "REACH_POPULATION_WITHOUT_DWELLINGS";
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000049F8 File Offset: 0x00002BF8
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			if (enteredFinishedStateEvent.BlockObject.HasComponent<Dwelling>())
			{
				this._dwellingBuilt = true;
				this.DisableInternal();
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004A14 File Offset: 0x00002C14
		public void Save(ISingletonSaver singletonSaver)
		{
			if (this._dwellingBuilt)
			{
				singletonSaver.GetSingleton(ReachPopulationWithoutDwellingsAchievement.ReachPopulationWithoutDwellingsKey).Set(ReachPopulationWithoutDwellingsAchievement.DwellingBuiltKey, this._dwellingBuilt);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004A3C File Offset: 0x00002C3C
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(ReachPopulationWithoutDwellingsAchievement.ReachPopulationWithoutDwellingsKey, out objectLoader))
			{
				this._dwellingBuilt = objectLoader.Get(ReachPopulationWithoutDwellingsAchievement.DwellingBuiltKey);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004A6E File Offset: 0x00002C6E
		[OnEvent]
		public void OnCharacterCreated(CharacterCreatedEvent characterCreatedEvent)
		{
			this.ValidatePopulation();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004A78 File Offset: 0x00002C78
		public override void EnableInternal()
		{
			if (this._factionService.Current.Id == AchievementHelper.IronTeeth && !this._entityComponentRegistry.GetEnabled<Dwelling>().Any<Dwelling>() && !this._dwellingBuilt)
			{
				this._eventBus.Register(this);
				this.ValidatePopulation();
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004ACD File Offset: 0x00002CCD
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004ADB File Offset: 0x00002CDB
		public void ValidatePopulation()
		{
			if (this._beaverPopulation.NumberOfBeavers >= ReachPopulationWithoutDwellingsAchievement.RequiredPopulation)
			{
				base.Unlock();
			}
		}

		// Token: 0x040000A2 RID: 162
		public static readonly SingletonKey ReachPopulationWithoutDwellingsKey = new SingletonKey("ReachPopulationWithoutDwellings");

		// Token: 0x040000A3 RID: 163
		public static readonly PropertyKey<bool> DwellingBuiltKey = new PropertyKey<bool>("DwellingBuilt");

		// Token: 0x040000A4 RID: 164
		public static readonly int RequiredPopulation = 200;

		// Token: 0x040000A5 RID: 165
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x040000A6 RID: 166
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x040000A7 RID: 167
		public readonly FactionService _factionService;

		// Token: 0x040000A8 RID: 168
		public readonly BeaverPopulation _beaverPopulation;

		// Token: 0x040000A9 RID: 169
		public readonly EventBus _eventBus;

		// Token: 0x040000AA RID: 170
		public bool _dwellingBuilt;
	}
}
