using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntityNaming;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Characters
{
	// Token: 0x02000007 RID: 7
	public class Character : BaseComponent, IAwakableComponent, IPersistentEntity, IChildhoodInfluenced
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler Died;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000216D File Offset: 0x0000036D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002175 File Offset: 0x00000375
		public bool Alive { get; private set; } = true;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000217E File Offset: 0x0000037E
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002186 File Offset: 0x00000386
		public int DayOfBirth { get; set; }

		// Token: 0x0600000D RID: 13 RVA: 0x0000218F File Offset: 0x0000038F
		public Character(EventBus eventBus, EntityService entityService, IDayNightCycle dayNightCycle)
		{
			this._eventBus = eventBus;
			this._entityService = entityService;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021B3 File Offset: 0x000003B3
		public int Age
		{
			get
			{
				return this._dayNightCycle.DayNumber - this.DayOfBirth;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021C7 File Offset: 0x000003C7
		public string FirstName
		{
			get
			{
				return this._namedEntity.EntityName;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021D4 File Offset: 0x000003D4
		public void Awake()
		{
			this._namedEntity = base.GetComponent<NamedEntity>();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021E4 File Offset: 0x000003E4
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Character.CharacterKey);
			component.Set(Character.PositionKey, base.Transform.position);
			component.Set(Character.AliveKey, this.Alive);
			component.Set(Character.DayOfBirthKey, this.DayOfBirth);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002234 File Offset: 0x00000434
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Character.CharacterKey);
			base.Transform.position = component.Get(Character.PositionKey);
			this.Alive = component.Get(Character.AliveKey);
			this.DayOfBirth = component.Get(Character.DayOfBirthKey);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002285 File Offset: 0x00000485
		public void InfluenceByChildhood(Character child)
		{
			this.DayOfBirth = child.DayOfBirth;
			this._namedEntity.SetEntityName(child.FirstName);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022A4 File Offset: 0x000004A4
		public void DestroyCharacter()
		{
			this.KillCharacter();
			this._entityService.Delete(this);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022B8 File Offset: 0x000004B8
		public void KillCharacter()
		{
			if (this.Alive)
			{
				this.Alive = false;
				EventHandler died = this.Died;
				if (died != null)
				{
					died(this, EventArgs.Empty);
				}
				this._eventBus.Post(new CharacterKilledEvent(this));
			}
		}

		// Token: 0x04000008 RID: 8
		public static readonly ComponentKey CharacterKey = new ComponentKey("Character");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<Vector3> PositionKey = new PropertyKey<Vector3>("Position");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<bool> AliveKey = new PropertyKey<bool>("Alive");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<int> DayOfBirthKey = new PropertyKey<int>("DayOfBirth");

		// Token: 0x0400000F RID: 15
		public readonly EventBus _eventBus;

		// Token: 0x04000010 RID: 16
		public readonly EntityService _entityService;

		// Token: 0x04000011 RID: 17
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000012 RID: 18
		public NamedEntity _namedEntity;
	}
}
