using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.MortalComponents;
using Timberborn.NotificationSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.MortalSystem
{
	// Token: 0x0200000F RID: 15
	public class Mortal : TickableComponent, IAwakableComponent, IPersistentEntity, IInitializableEntity, IPostInitializableEntity, IDeadNeededComponent
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002987 File Offset: 0x00000B87
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000298F File Offset: 0x00000B8F
		public bool ShouldDiePublicly { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002998 File Offset: 0x00000B98
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000029A0 File Offset: 0x00000BA0
		public bool ShouldDie { get; private set; }

		// Token: 0x0600004D RID: 77 RVA: 0x000029A9 File Offset: 0x00000BA9
		public Mortal(NotificationBus notificationBus, IDayNightCycle dayNightCycle, EventBus eventBus, IRandomNumberGenerator randomNumberGenerator, LongLastingCorpsesService longLastingCorpsesService, DeadComponentDisabler deadComponentDisabler)
		{
			this._notificationBus = notificationBus;
			this._dayNightCycle = dayNightCycle;
			this._eventBus = eventBus;
			this._randomNumberGenerator = randomNumberGenerator;
			this._longLastingCorpsesService = longLastingCorpsesService;
			this._deadComponentDisabler = deadComponentDisabler;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000029DE File Offset: 0x00000BDE
		public bool Dead
		{
			get
			{
				return !this._character.Alive;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000029EE File Offset: 0x00000BEE
		public bool IsTimeToDie
		{
			get
			{
				return !this.Dead && this.ShouldDie;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A00 File Offset: 0x00000C00
		public void Awake()
		{
			this._character = base.GetComponent<Character>();
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			this._deadStatus = base.GetComponent<DeadStatus>();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A26 File Offset: 0x00000C26
		public override void Tick()
		{
			this.DestroyBodyIfItIsTime();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A2E File Offset: 0x00000C2E
		public void InitializeEntity()
		{
			if (!this.Dead)
			{
				this._eventBus.Post(new CharacterCreatedEvent(this._character));
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A4E File Offset: 0x00000C4E
		public void PostInitializeEntity()
		{
			if (this.Dead)
			{
				this.BecomeCorpse(true);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A5F File Offset: 0x00000C5F
		public void DiePubliclyAsSoonAsPossible(string deathMessage)
		{
			this.DieEventually(true, deathMessage);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A69 File Offset: 0x00000C69
		public void DieSilentlyAsSoonAsPossible(string deathMessage)
		{
			this.DieEventually(false, deathMessage);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002A73 File Offset: 0x00000C73
		public void DieIfItIsTime()
		{
			if (this.IsTimeToDie)
			{
				this.BecomeCorpse(false);
				this._character.KillCharacter();
				this._notificationBus.Post(this._deathMessage, this._character);
				return;
			}
			throw new InvalidOperationException("DieIfItIsTime was called even though IsTimeToDie is false.");
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002AB1 File Offset: 0x00000CB1
		public void DieInstantly(string deathMessage)
		{
			this.DiePubliclyAsSoonAsPossible(deathMessage);
			this.DieIfItIsTime();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public void Save(IEntitySaver entitySaver)
		{
			if (this.ShouldDie)
			{
				IObjectSaver component = entitySaver.GetComponent(Mortal.MortalKey);
				component.Set(Mortal.ShouldDieKey, this.ShouldDie);
				component.Set(Mortal.DiePubliclyKey, this.ShouldDiePublicly);
				component.Set(Mortal.DeathMessageKey, this._deathMessage);
				component.Set(Mortal.BodyDisappearanceDayKey, this._bodyDisappearanceDay);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B24 File Offset: 0x00000D24
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Mortal.MortalKey, out objectLoader))
			{
				this.ShouldDie = objectLoader.Get(Mortal.ShouldDieKey);
				this.ShouldDiePublicly = objectLoader.Get(Mortal.DiePubliclyKey);
				this._deathMessage = objectLoader.Get(Mortal.DeathMessageKey);
				this._bodyDisappearanceDay = objectLoader.Get(Mortal.BodyDisappearanceDayKey);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002B84 File Offset: 0x00000D84
		public void BecomeCorpse(bool postLoad)
		{
			this._deadComponentDisabler.DisableComponentsDeadDoNotNeed(this);
			if (!postLoad)
			{
				this.SetBodyDisappearanceTimestamp();
			}
			this._characterAnimator.SetBool(postLoad ? "DeadNoAnimation" : "Dead", true);
			this._deadStatus.Activate(this.ShouldDiePublicly);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002BD2 File Offset: 0x00000DD2
		public void DieEventually(bool diePublicly, string deathMessage)
		{
			if (!this.ShouldDie)
			{
				this.ShouldDie = true;
				this.ShouldDiePublicly = diePublicly;
				this._deathMessage = deathMessage;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public void SetBodyDisappearanceTimestamp()
		{
			float hours;
			if (this._longLastingCorpsesService.Enabled)
			{
				hours = 48f;
			}
			else
			{
				float inclusiveMax = Mortal.MaxHoursForBodyToDisappear - Mortal.MinHoursForBodyToDisappear;
				hours = this._randomNumberGenerator.Range(0f, inclusiveMax) + Mortal.MinHoursForBodyToDisappear;
			}
			this._bodyDisappearanceDay = this._dayNightCycle.DayNumberHoursFromNow(hours);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C4C File Offset: 0x00000E4C
		public void DestroyBodyIfItIsTime()
		{
			if (this.Dead && this._dayNightCycle.PartialDayNumber > this._bodyDisappearanceDay)
			{
				base.GameObject.SetActive(false);
				this._character.DestroyCharacter();
			}
		}

		// Token: 0x04000020 RID: 32
		public static readonly ComponentKey MortalKey = new ComponentKey("Mortal");

		// Token: 0x04000021 RID: 33
		public static readonly PropertyKey<bool> ShouldDieKey = new PropertyKey<bool>("ShouldDie");

		// Token: 0x04000022 RID: 34
		public static readonly PropertyKey<bool> DiePubliclyKey = new PropertyKey<bool>("DiePublicly");

		// Token: 0x04000023 RID: 35
		public static readonly PropertyKey<string> DeathMessageKey = new PropertyKey<string>("DeathMessage");

		// Token: 0x04000024 RID: 36
		public static readonly PropertyKey<float> BodyDisappearanceDayKey = new PropertyKey<float>("BodyDisappearanceDay");

		// Token: 0x04000025 RID: 37
		public static readonly float MinHoursForBodyToDisappear = 0.75f;

		// Token: 0x04000026 RID: 38
		public static readonly float MaxHoursForBodyToDisappear = 1.75f;

		// Token: 0x04000029 RID: 41
		public readonly NotificationBus _notificationBus;

		// Token: 0x0400002A RID: 42
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400002B RID: 43
		public readonly EventBus _eventBus;

		// Token: 0x0400002C RID: 44
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400002D RID: 45
		public readonly LongLastingCorpsesService _longLastingCorpsesService;

		// Token: 0x0400002E RID: 46
		public readonly DeadComponentDisabler _deadComponentDisabler;

		// Token: 0x0400002F RID: 47
		public Character _character;

		// Token: 0x04000030 RID: 48
		public CharacterAnimator _characterAnimator;

		// Token: 0x04000031 RID: 49
		public DeadStatus _deadStatus;

		// Token: 0x04000032 RID: 50
		public float _bodyDisappearanceDay;

		// Token: 0x04000033 RID: 51
		public string _deathMessage;
	}
}
