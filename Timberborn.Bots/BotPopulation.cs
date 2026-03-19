using System;
using System.Collections.Generic;
using Timberborn.Characters;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Bots
{
	// Token: 0x0200000D RID: 13
	public class BotPopulation : ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002398 File Offset: 0x00000598
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000023A0 File Offset: 0x000005A0
		public bool BotCreated { get; private set; }

		// Token: 0x06000025 RID: 37 RVA: 0x000023A9 File Offset: 0x000005A9
		public BotPopulation(EventBus eventBus, ISingletonLoader singletonLoader)
		{
			this._eventBus = eventBus;
			this._singletonLoader = singletonLoader;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023CA File Offset: 0x000005CA
		public int NumberOfBots
		{
			get
			{
				return this._bots.Count;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023D7 File Offset: 0x000005D7
		public void Save(ISingletonSaver singletonSaver)
		{
			singletonSaver.GetSingleton(BotPopulation.BotPopulationKey).Set(BotPopulation.BotCreatedKey, this.BotCreated);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000023F4 File Offset: 0x000005F4
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(BotPopulation.BotPopulationKey, out objectLoader))
			{
				this.BotCreated = objectLoader.Get(BotPopulation.BotCreatedKey);
			}
			this._eventBus.Register(this);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002434 File Offset: 0x00000634
		[OnEvent]
		public void OnCharacterCreated(CharacterCreatedEvent characterCreatedEvent)
		{
			BotSpec component = characterCreatedEvent.Character.GetComponent<BotSpec>();
			if (component != null)
			{
				this._bots.Add(component);
				this.BotCreated = true;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002464 File Offset: 0x00000664
		[OnEvent]
		public void OnCharacterKilled(CharacterKilledEvent characterKilledEvent)
		{
			BotSpec component = characterKilledEvent.Character.GetComponent<BotSpec>();
			if (component != null)
			{
				this._bots.Remove(component);
			}
		}

		// Token: 0x04000013 RID: 19
		public static readonly SingletonKey BotPopulationKey = new SingletonKey("BotPopulation");

		// Token: 0x04000014 RID: 20
		public static readonly PropertyKey<bool> BotCreatedKey = new PropertyKey<bool>("BotCreated");

		// Token: 0x04000016 RID: 22
		public readonly EventBus _eventBus;

		// Token: 0x04000017 RID: 23
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000018 RID: 24
		public readonly List<BotSpec> _bots = new List<BotSpec>();
	}
}
