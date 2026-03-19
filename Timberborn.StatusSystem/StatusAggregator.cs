using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.SingletonSystem;

namespace Timberborn.StatusSystem
{
	// Token: 0x0200000E RID: 14
	public class StatusAggregator : IUpdatableSingleton, IStatusAggregator
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002599 File Offset: 0x00000799
		public StatusAggregator(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025BE File Offset: 0x000007BE
		public void UpdateSingleton()
		{
			this.UpdateVisibleStatuses();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025C8 File Offset: 0x000007C8
		public void AddStatus(StatusInstance statusInstance)
		{
			if (statusInstance.ShowAlert)
			{
				this._statuses.Add(statusInstance);
				string alertDescription = statusInstance.AlertDescription;
				if (!this._visibleStatuses.ContainsKey(alertDescription))
				{
					this._visibleStatuses[alertDescription] = new List<StatusInstance>();
					this._eventBus.Post(new StatusAlertAddedEvent(alertDescription, statusInstance.IconSmall));
				}
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002628 File Offset: 0x00000828
		public void RemoveStatuses(StatusSubject statusSubject)
		{
			this._statuses.RemoveAll((StatusInstance instance) => instance.StatusSubject == statusSubject);
			this.UpdateVisibleStatuses();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002660 File Offset: 0x00000860
		public ImmutableArray<StatusInstance> GetVisibleStatuses(string alertDescription)
		{
			return this._visibleStatuses[alertDescription].ToImmutableArray<StatusInstance>();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002673 File Offset: 0x00000873
		public int GetVisibleStatusesCount(string alertDescription)
		{
			return this._visibleStatuses[alertDescription].Count;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002688 File Offset: 0x00000888
		public void UpdateVisibleStatuses()
		{
			foreach (string key in this._visibleStatuses.Keys)
			{
				this._visibleStatuses[key].Clear();
			}
			foreach (StatusInstance statusInstance in this._statuses)
			{
				if (StatusAggregator.IsVisible(statusInstance))
				{
					this._visibleStatuses[statusInstance.AlertDescription].Add(statusInstance);
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000243C File Offset: 0x0000063C
		public static bool IsVisible(StatusInstance statusInstance)
		{
			return statusInstance.IsActive && statusInstance.IsVisible();
		}

		// Token: 0x04000018 RID: 24
		public readonly EventBus _eventBus;

		// Token: 0x04000019 RID: 25
		public readonly List<StatusInstance> _statuses = new List<StatusInstance>();

		// Token: 0x0400001A RID: 26
		public readonly Dictionary<string, List<StatusInstance>> _visibleStatuses = new Dictionary<string, List<StatusInstance>>();
	}
}
