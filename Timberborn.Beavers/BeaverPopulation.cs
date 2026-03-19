using System;
using Timberborn.Characters;
using Timberborn.SingletonSystem;

namespace Timberborn.Beavers
{
	// Token: 0x02000010 RID: 16
	public class BeaverPopulation : ILoadableSingleton
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002773 File Offset: 0x00000973
		public BeaverPopulation(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000278D File Offset: 0x0000098D
		public int NumberOfAdults
		{
			get
			{
				return this._beaverCollection.NumberOfAdults;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000279A File Offset: 0x0000099A
		public int NumberOfChildren
		{
			get
			{
				return this._beaverCollection.NumberOfChildren;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000027A7 File Offset: 0x000009A7
		public int NumberOfBeavers
		{
			get
			{
				return this._beaverCollection.NumberOfBeavers;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000027B4 File Offset: 0x000009B4
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027C4 File Offset: 0x000009C4
		[OnEvent]
		public void OnCharacterCreated(CharacterCreatedEvent characterCreatedEvent)
		{
			Beaver component = characterCreatedEvent.Character.GetComponent<Beaver>();
			if (component != null)
			{
				this._beaverCollection.AddBeaver(component);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027EC File Offset: 0x000009EC
		[OnEvent]
		public void OnCharacterKilled(CharacterKilledEvent characterKilledEvent)
		{
			Beaver component = characterKilledEvent.Character.GetComponent<Beaver>();
			if (component != null)
			{
				this._beaverCollection.RemoveBeaver(component);
			}
		}

		// Token: 0x04000023 RID: 35
		public readonly EventBus _eventBus;

		// Token: 0x04000024 RID: 36
		public readonly BeaverCollection _beaverCollection = new BeaverCollection();
	}
}
