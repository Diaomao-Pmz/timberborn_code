using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.EntitySystem;
using Timberborn.NeedSystem;
using Timberborn.TimeSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200002D RID: 45
	public class InjuredJustBornBeaverTracker : BaseComponent, IAwakableComponent, IPostInitializableEntity
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00003BAD File Offset: 0x00001DAD
		public InjuredJustBornBeaverTracker(InjuredJustBornBeaverAchievement injuredJustBornBeaverAchievement, IDayNightCycle dayNightCycle)
		{
			this._injuredJustBornBeaverAchievement = injuredJustBornBeaverAchievement;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003BC3 File Offset: 0x00001DC3
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._character = base.GetComponent<Character>();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003BDD File Offset: 0x00001DDD
		public void PostInitializeEntity()
		{
			if (this._injuredJustBornBeaverAchievement.CanTrackInjury && this.IsBornToday())
			{
				this._needManager.NeedChangedActiveState += this.OnNeedChangedActiveState;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003C0C File Offset: 0x00001E0C
		public void OnNeedChangedActiveState(object sender, NeedChangedActiveStateEventArgs e)
		{
			if (this._injuredJustBornBeaverAchievement.IsEnabled && this.IsBornToday())
			{
				if (e.IsActive && e.NeedSpec.Id == InjuredJustBornBeaverTracker.InjuryNeedId)
				{
					this._injuredJustBornBeaverAchievement.Unlock();
					this.DisableTracking();
					return;
				}
			}
			else
			{
				this.DisableTracking();
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003C67 File Offset: 0x00001E67
		public bool IsBornToday()
		{
			return this._character.DayOfBirth == this._dayNightCycle.DayNumber;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003C81 File Offset: 0x00001E81
		public void DisableTracking()
		{
			this._needManager.NeedChangedActiveState -= this.OnNeedChangedActiveState;
		}

		// Token: 0x04000062 RID: 98
		public static readonly string InjuryNeedId = "Injury";

		// Token: 0x04000063 RID: 99
		public readonly InjuredJustBornBeaverAchievement _injuredJustBornBeaverAchievement;

		// Token: 0x04000064 RID: 100
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000065 RID: 101
		public NeedManager _needManager;

		// Token: 0x04000066 RID: 102
		public Character _character;
	}
}
