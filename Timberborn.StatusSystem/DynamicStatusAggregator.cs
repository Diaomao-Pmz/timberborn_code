using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.SingletonSystem;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000006 RID: 6
	public class DynamicStatusAggregator : IUpdatableSingleton, IStatusAggregator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000219C File Offset: 0x0000039C
		public DynamicStatusAggregator(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C4 File Offset: 0x000003C4
		public void AddStatus(StatusInstance statusInstance)
		{
			if (statusInstance.ShowAlert)
			{
				string alertDescription = statusInstance.AlertDescription;
				if (!this._visibleStatuses.ContainsKey(alertDescription))
				{
					this._visibleStatuses[alertDescription] = new List<StatusInstance>();
					this._allStatuses[alertDescription] = new List<StatusInstance>();
					this._eventBus.Post(new DynamicStatusAlertAddedEvent(statusInstance));
				}
				this._allStatuses[alertDescription].Add(statusInstance);
				this.UpdateVisibleStatuses(alertDescription);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000223C File Offset: 0x0000043C
		public void RemoveStatuses(StatusSubject statusSubject)
		{
			Predicate<StatusInstance> <>9__0;
			foreach (string key in this._allStatuses.Keys)
			{
				List<StatusInstance> list = this._allStatuses[key];
				Predicate<StatusInstance> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((StatusInstance instance) => instance.StatusSubject == statusSubject));
				}
				list.RemoveAll(match);
				this.UpdateVisibleStatuses(key);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022D4 File Offset: 0x000004D4
		public void UpdateSingleton()
		{
			foreach (string key in this._allStatuses.Keys)
			{
				this.UpdateVisibleStatuses(key);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000232C File Offset: 0x0000052C
		public ImmutableArray<StatusInstance> GetVisibleStatuses(string alertDescription)
		{
			return this._visibleStatuses[alertDescription].ToImmutableArray<StatusInstance>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002340 File Offset: 0x00000540
		public bool TryGetStatusData(string alertDescription, out StatusData statusData)
		{
			List<StatusInstance> list;
			if (this._visibleStatuses.TryGetValue(alertDescription, out list) && list.Count > 0)
			{
				StatusInstance statusInstance = list[0];
				statusData = new StatusData(list.Count, statusInstance.StatusValueGetter(), statusInstance.StatusWarningTypeGetter());
				return true;
			}
			statusData = default(StatusData);
			return false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023A0 File Offset: 0x000005A0
		public void UpdateVisibleStatuses(string key)
		{
			List<StatusInstance> list = this._visibleStatuses[key];
			list.Clear();
			List<StatusInstance> list2 = this._allStatuses[key];
			float num = float.MaxValue;
			foreach (StatusInstance statusInstance in list2)
			{
				if (DynamicStatusAggregator.IsVisible(statusInstance))
				{
					float num2 = statusInstance.StatusValueGetter();
					if (num2 <= num)
					{
						if (num2 < num)
						{
							list.Clear();
							num = num2;
						}
						list.Add(statusInstance);
					}
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000243C File Offset: 0x0000063C
		public static bool IsVisible(StatusInstance statusInstance)
		{
			return statusInstance.IsActive && statusInstance.IsVisible();
		}

		// Token: 0x0400000B RID: 11
		public readonly EventBus _eventBus;

		// Token: 0x0400000C RID: 12
		public readonly Dictionary<string, List<StatusInstance>> _allStatuses = new Dictionary<string, List<StatusInstance>>();

		// Token: 0x0400000D RID: 13
		public readonly Dictionary<string, List<StatusInstance>> _visibleStatuses = new Dictionary<string, List<StatusInstance>>();
	}
}
