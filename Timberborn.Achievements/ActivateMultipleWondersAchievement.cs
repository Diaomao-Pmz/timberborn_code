using System;
using System.Collections.Generic;
using Timberborn.AchievementSystem;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.Wonders;

namespace Timberborn.Achievements
{
	// Token: 0x02000006 RID: 6
	public class ActivateMultipleWondersAchievement : Achievement
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002537 File Offset: 0x00000737
		public ActivateMultipleWondersAchievement(EventBus eventBus, EntityComponentRegistry entityComponentRegistry)
		{
			this._eventBus = eventBus;
			this._entityComponentRegistry = entityComponentRegistry;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000254D File Offset: 0x0000074D
		public override string Id
		{
			get
			{
				return "ACTIVATE_MULTIPLE_WONDERS";
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002554 File Offset: 0x00000754
		[OnEvent]
		public void OnWonderActivated(WonderActivatedEvent wonderActivatedEvent)
		{
			if (this.GetActiveWonderCount() >= ActivateMultipleWondersAchievement.RequiredActiveWonders)
			{
				base.Unlock();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002569 File Offset: 0x00000769
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002577 File Offset: 0x00000777
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002588 File Offset: 0x00000788
		public int GetActiveWonderCount()
		{
			int num = 0;
			using (IEnumerator<Wonder> enumerator = this._entityComponentRegistry.GetEnabled<Wonder>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsActive)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x04000006 RID: 6
		public static readonly int RequiredActiveWonders = 3;

		// Token: 0x04000007 RID: 7
		public readonly EventBus _eventBus;

		// Token: 0x04000008 RID: 8
		public readonly EntityComponentRegistry _entityComponentRegistry;
	}
}
