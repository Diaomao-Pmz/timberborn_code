using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.Localization;
using Timberborn.MortalSystem;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.DeteriorationSystem
{
	// Token: 0x02000007 RID: 7
	public class Deteriorable : TickableComponent, IAwakableComponent, IPersistentEntity
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FB File Offset: 0x000002FB
		public Deteriorable(IDayNightCycle dayNightCycle, ILoc loc)
		{
			this._dayNightCycle = dayNightCycle;
			this._loc = loc;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002111 File Offset: 0x00000311
		public float DeteriorationProgress
		{
			get
			{
				return this._currentDeterioration / (float)this._deteriorableSpec.DeteriorationInDays;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002126 File Offset: 0x00000326
		public void Awake()
		{
			this._mortal = base.GetComponent<Mortal>();
			this._deteriorableSpec = base.GetComponent<DeteriorableSpec>();
			this.SetDeteriorationToMaximum();
			this._fixedDeltaTimeInDays = this._dayNightCycle.FixedDeltaTimeInHours / 24f;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002160 File Offset: 0x00000360
		public override void Tick()
		{
			if (this._currentDeterioration > 0f)
			{
				this._currentDeterioration -= this._fixedDeltaTimeInDays;
				return;
			}
			string deathMessage = this._loc.T<string>(Deteriorable.BotDeathLocKey, this._mortal.GetComponent<Character>().FirstName);
			this._mortal.DiePubliclyAsSoonAsPossible(deathMessage);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021BB File Offset: 0x000003BB
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(Deteriorable.DeteriorableKey).Set(Deteriorable.CurrentDeteriorationKey, this._currentDeterioration);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D8 File Offset: 0x000003D8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Deteriorable.DeteriorableKey);
			this._currentDeterioration = component.Get(Deteriorable.CurrentDeteriorationKey);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002202 File Offset: 0x00000402
		public void SetDeteriorationToZero()
		{
			this._currentDeterioration = 0f;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000220F File Offset: 0x0000040F
		public void SetDeteriorationToMaximum()
		{
			this._currentDeterioration = (float)this._deteriorableSpec.DeteriorationInDays;
		}

		// Token: 0x04000008 RID: 8
		public static readonly string BotDeathLocKey = "Bot.DeathMessage";

		// Token: 0x04000009 RID: 9
		public static readonly ComponentKey DeteriorableKey = new ComponentKey("Deteriorable");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<float> CurrentDeteriorationKey = new PropertyKey<float>("CurrentDeterioration");

		// Token: 0x0400000B RID: 11
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400000C RID: 12
		public readonly ILoc _loc;

		// Token: 0x0400000D RID: 13
		public Mortal _mortal;

		// Token: 0x0400000E RID: 14
		public DeteriorableSpec _deteriorableSpec;

		// Token: 0x0400000F RID: 15
		public float _currentDeterioration;

		// Token: 0x04000010 RID: 16
		public float _fixedDeltaTimeInDays;
	}
}
