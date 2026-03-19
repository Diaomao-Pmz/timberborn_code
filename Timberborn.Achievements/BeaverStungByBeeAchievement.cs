using System;
using System.Collections.Generic;
using Timberborn.AchievementSystem;
using Timberborn.BlockSystem;
using Timberborn.GameFactionSystem;
using Timberborn.NeedApplication;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200000D RID: 13
	public class BeaverStungByBeeAchievement : Achievement
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000028E0 File Offset: 0x00000AE0
		public BeaverStungByBeeAchievement(EventBus eventBus, FactionService factionService)
		{
			this._eventBus = eventBus;
			this._factionService = factionService;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002901 File Offset: 0x00000B01
		public override string Id
		{
			get
			{
				return "BEAVER_STUNG_BY_BEE";
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002908 File Offset: 0x00000B08
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			AreaNeedApplier component = enteredFinishedStateEvent.BlockObject.GetComponent<AreaNeedApplier>();
			if (component != null && this._needAppliers.Add(component))
			{
				component.NeedApplied += this.OnNeedApplied;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002944 File Offset: 0x00000B44
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			AreaNeedApplier component = exitedFinishedStateEvent.BlockObject.GetComponent<AreaNeedApplier>();
			if (component != null)
			{
				this._needAppliers.Remove(component);
				component.NeedApplied -= this.OnNeedApplied;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000297F File Offset: 0x00000B7F
		public override void EnableInternal()
		{
			if (this._factionService.Current.Id == AchievementHelper.Folktails)
			{
				this._eventBus.Register(this);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029AC File Offset: 0x00000BAC
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
			foreach (AreaNeedApplier areaNeedApplier in this._needAppliers)
			{
				areaNeedApplier.NeedApplied -= this.OnNeedApplied;
			}
			this._needAppliers.Clear();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A20 File Offset: 0x00000C20
		public void OnNeedApplied(object sender, NeedAppliedEventArgs needAppliedEventArgs)
		{
			if (needAppliedEventArgs.NeedEffect.NeedId == BeaverStungByBeeAchievement.BeeStingNeedId)
			{
				base.Unlock();
			}
		}

		// Token: 0x04000019 RID: 25
		public static readonly string BeeStingNeedId = "BeeSting";

		// Token: 0x0400001A RID: 26
		public readonly EventBus _eventBus;

		// Token: 0x0400001B RID: 27
		public readonly FactionService _factionService;

		// Token: 0x0400001C RID: 28
		public readonly HashSet<AreaNeedApplier> _needAppliers = new HashSet<AreaNeedApplier>();
	}
}
