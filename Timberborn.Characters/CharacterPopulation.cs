using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.Characters
{
	// Token: 0x0200000B RID: 11
	public class CharacterPopulation : ILoadableSingleton
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000023C2 File Offset: 0x000005C2
		public CharacterPopulation(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023DC File Offset: 0x000005DC
		public ReadOnlyList<Character> Characters
		{
			get
			{
				return this._characters.AsReadOnlyList<Character>();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023E9 File Offset: 0x000005E9
		public int NumberOfCharacters
		{
			get
			{
				return this._characters.Count;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023F6 File Offset: 0x000005F6
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002404 File Offset: 0x00000604
		[OnEvent]
		public void OnCharacterCreated(CharacterCreatedEvent characterCreatedEvent)
		{
			this._characters.Add(characterCreatedEvent.Character);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002417 File Offset: 0x00000617
		[OnEvent]
		public void OnCharacterKilled(CharacterKilledEvent characterKilledEvent)
		{
			this._characters.Remove(characterKilledEvent.Character);
		}

		// Token: 0x04000016 RID: 22
		public readonly EventBus _eventBus;

		// Token: 0x04000017 RID: 23
		public readonly List<Character> _characters = new List<Character>();
	}
}
