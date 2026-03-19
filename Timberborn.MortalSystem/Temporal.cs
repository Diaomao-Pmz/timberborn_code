using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.EntitySystem;
using Timberborn.LifeSystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;

namespace Timberborn.MortalSystem
{
	// Token: 0x02000013 RID: 19
	public class Temporal : BaseComponent, IAwakableComponent, IDeletableEntity
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00002F04 File Offset: 0x00001104
		public Temporal(EventBus eventBus, ILoc loc)
		{
			this._eventBus = eventBus;
			this._loc = loc;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F1A File Offset: 0x0000111A
		public void Awake()
		{
			this._mortal = base.GetComponent<Mortal>();
			this._character = base.GetComponent<Character>();
			this._lifeProgressor = base.GetComponent<LifeProgressor>();
			this._eventBus.Register(this);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F4C File Offset: 0x0000114C
		public void DeleteEntity()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002F5A File Offset: 0x0000115A
		[OnEvent]
		public void OnDaytimeStart(DaytimeStartEvent daytimeStartEvent)
		{
			this.DieIfTooOld();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002F62 File Offset: 0x00001162
		public void DieIfTooOld()
		{
			if (this._lifeProgressor.ShouldDie)
			{
				this._mortal.DieSilentlyAsSoonAsPossible(this._loc.T<string>(Temporal.DiedOldAgeLocKey, this._character.FirstName));
			}
		}

		// Token: 0x04000036 RID: 54
		public static readonly string DiedOldAgeLocKey = "Beaver.DiedOldAge";

		// Token: 0x04000037 RID: 55
		public readonly EventBus _eventBus;

		// Token: 0x04000038 RID: 56
		public readonly ILoc _loc;

		// Token: 0x04000039 RID: 57
		public Mortal _mortal;

		// Token: 0x0400003A RID: 58
		public Character _character;

		// Token: 0x0400003B RID: 59
		public LifeProgressor _lifeProgressor;
	}
}
