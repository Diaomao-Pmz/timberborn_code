using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;
using Timberborn.SelectionSystem;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000004 RID: 4
	public class AlertStatusSubjectSelector
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public AlertStatusSubjectSelector(IStatusAggregator statusAggregator, EntitySelectionService entitySelectionService)
		{
			this._statusAggregator = statusAggregator;
			this._entitySelectionService = entitySelectionService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E4 File Offset: 0x000002E4
		public void SelectNextSubject(string alertDescription)
		{
			ImmutableArray<StatusInstance> visibleStatuses = this._statusAggregator.GetVisibleStatuses(alertDescription);
			if (visibleStatuses.Length > 0)
			{
				StatusInstance orAdd = this._previousSelectedStatuses.GetOrAdd(alertDescription, () => null);
				int num = visibleStatuses.IndexOf(orAdd);
				StatusInstance statusInstance = AlertStatusSubjectSelector.ShouldShowFirst(num, visibleStatuses) ? visibleStatuses[0] : visibleStatuses[num + 1];
				this._previousSelectedStatuses[alertDescription] = statusInstance;
				this._entitySelectionService.SelectAndFocusOn(statusInstance.StatusSubject);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002179 File Offset: 0x00000379
		public static bool ShouldShowFirst(int previousIndex, ImmutableArray<StatusInstance> statuses)
		{
			return previousIndex == -1 || previousIndex == statuses.Length - 1;
		}

		// Token: 0x04000006 RID: 6
		public readonly IStatusAggregator _statusAggregator;

		// Token: 0x04000007 RID: 7
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000008 RID: 8
		public readonly Dictionary<string, StatusInstance> _previousSelectedStatuses = new Dictionary<string, StatusInstance>();
	}
}
