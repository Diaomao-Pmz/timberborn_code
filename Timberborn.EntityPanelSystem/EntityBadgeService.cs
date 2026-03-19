using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x0200000A RID: 10
	public class EntityBadgeService : ILoadableSingleton
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000025F0 File Offset: 0x000007F0
		public EntityBadgeService(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000260A File Offset: 0x0000080A
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002618 File Offset: 0x00000818
		public string GetEntitySubtitle(BaseComponent subject)
		{
			IEntityBadge highestPriorityEntityBadge = this.GetHighestPriorityEntityBadge(subject);
			return ((highestPriorityEntityBadge != null) ? highestPriorityEntityBadge.GetEntitySubtitle() : null) ?? "";
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002636 File Offset: 0x00000836
		public ClickableSubtitle GetEntityClickableSubtitle(BaseComponent subject)
		{
			IEntityBadge highestPriorityEntityBadge = this.GetHighestPriorityEntityBadge(subject);
			if (highestPriorityEntityBadge == null)
			{
				return ClickableSubtitle.CreateEmpty();
			}
			return highestPriorityEntityBadge.GetEntityClickableSubtitle();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000264E File Offset: 0x0000084E
		public Sprite GetEntityAvatar(BaseComponent subject)
		{
			IEntityBadge highestPriorityEntityBadge = this.GetHighestPriorityEntityBadge(subject);
			if (highestPriorityEntityBadge == null)
			{
				return null;
			}
			return highestPriorityEntityBadge.GetEntityAvatar();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002662 File Offset: 0x00000862
		public IEntityBadge GetHighestPriorityEntityBadge(BaseComponent subject)
		{
			subject.GetComponents<IEntityBadge>(this._entityBadgesCache);
			return this.GetHighestPriorityEntityBadge();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002678 File Offset: 0x00000878
		public IEntityBadge GetHighestPriorityEntityBadge()
		{
			if (this._entityBadgesCache.Count > 0)
			{
				int num = int.MinValue;
				IEntityBadge result = null;
				foreach (IEntityBadge entityBadge in this._entityBadgesCache)
				{
					if (entityBadge.EntityBadgeEnabled && entityBadge.EntityBadgePriority > num)
					{
						num = entityBadge.EntityBadgePriority;
						result = entityBadge;
					}
				}
				this._entityBadgesCache.Clear();
				return result;
			}
			return null;
		}

		// Token: 0x04000015 RID: 21
		public readonly EventBus _eventBus;

		// Token: 0x04000016 RID: 22
		public readonly List<IEntityBadge> _entityBadgesCache = new List<IEntityBadge>();
	}
}
