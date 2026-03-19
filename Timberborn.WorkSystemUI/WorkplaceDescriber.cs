using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using Timberborn.WorkerTypesUI;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000014 RID: 20
	public class WorkplaceDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002F13 File Offset: 0x00001113
		public WorkplaceDescriber(DescribedAmountFactory describedAmountFactory, ILoc loc, WorkerTypeHelper workerTypeHelper)
		{
			this._describedAmountFactory = describedAmountFactory;
			this._loc = loc;
			this._workerTypeHelper = workerTypeHelper;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F30 File Offset: 0x00001130
		public void Awake()
		{
			this._workplace = base.GetComponent<Workplace>();
			this._workplaceSpec = base.GetComponent<WorkplaceSpec>();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F4A File Offset: 0x0000114A
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (!base.GameObject.activeInHierarchy)
			{
				int maxWorkers = this._workplace.MaxWorkers;
				string tooltip = this._loc.T<int>(WorkplaceDescriber.WorkersLocKey, maxWorkers);
				VisualElement content = this._describedAmountFactory.CreatePlain(WorkplaceDescriber.WorkersClass, string.Format("{0}", maxWorkers), tooltip);
				yield return EntityDescription.CreateMiddleSection(content, 2);
				if (this._workplaceSpec.DisallowOtherWorkerTypes)
				{
					string disallowedWorkerText = this._workerTypeHelper.GetDisallowedWorkerText(this._workplaceSpec.DefaultWorkerType);
					string content2 = SpecialStrings.RowStarter + disallowedWorkerText;
					yield return EntityDescription.CreateTextSection(content2, 2020);
				}
			}
			yield break;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F5C File Offset: 0x0000115C
		public string GetWorkersTooltip()
		{
			int numberOfAssignedWorkers = this._workplace.NumberOfAssignedWorkers;
			int desiredWorkers = this._workplace.DesiredWorkers;
			int maxWorkers = this._workplace.MaxWorkers;
			string str = this._loc.T<int, int>(WorkplaceDescriber.CurrentWorkersLocKey, numberOfAssignedWorkers, desiredWorkers);
			string str2 = this._loc.T<int>(WorkplaceDescriber.MaximumWorkersLocKey, maxWorkers);
			return str + "\n" + str2;
		}

		// Token: 0x04000053 RID: 83
		public static readonly string WorkersClass = "described-amount--workers";

		// Token: 0x04000054 RID: 84
		public static readonly string WorkersLocKey = "Work.Workers";

		// Token: 0x04000055 RID: 85
		public static readonly string CurrentWorkersLocKey = "Work.CurrentWorkers";

		// Token: 0x04000056 RID: 86
		public static readonly string MaximumWorkersLocKey = "Work.MaximumWorkers";

		// Token: 0x04000057 RID: 87
		public readonly DescribedAmountFactory _describedAmountFactory;

		// Token: 0x04000058 RID: 88
		public readonly ILoc _loc;

		// Token: 0x04000059 RID: 89
		public readonly WorkerTypeHelper _workerTypeHelper;

		// Token: 0x0400005A RID: 90
		public Workplace _workplace;

		// Token: 0x0400005B RID: 91
		public WorkplaceSpec _workplaceSpec;
	}
}
