using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.WorkSystem;

namespace Timberborn.WorkplacesBatchControl
{
	// Token: 0x02000007 RID: 7
	public class WorkplacesBatchControlTab : BatchControlTab
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000022BD File Offset: 0x000004BD
		public WorkplacesBatchControlTab(VisualElementLoader visualElementLoader, BatchControlDistrict batchControlDistrict, WorkplacesBatchControlRowFactory workplacesBatchControlRowFactory, BatchControlRowGroupFactory batchControlRowGroupFactory, EventBus eventBus) : base(visualElementLoader, batchControlDistrict, eventBus)
		{
			this._workplacesBatchControlRowFactory = workplacesBatchControlRowFactory;
			this._batchControlRowGroupFactory = batchControlRowGroupFactory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000022D8 File Offset: 0x000004D8
		public override string TabNameLocKey
		{
			get
			{
				return "BatchControl.Workplaces";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022DF File Offset: 0x000004DF
		public override string TabImage
		{
			get
			{
				return "Workplaces";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022E6 File Offset: 0x000004E6
		public override string BindingKey
		{
			get
			{
				return "WorkplacesTab";
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022ED File Offset: 0x000004ED
		public override IEnumerable<BatchControlRowGroup> GetRowGroups(IEnumerable<EntityComponent> entities)
		{
			IEnumerable<IGrouping<string, EntityComponent>> enumerable = from entity in entities
			where entity.GetComponent<Workplace>()
			select entity into workplace
			where workplace
			group workplace by workplace.GetComponent<LabeledEntitySpec>().DisplayNameLocKey;
			foreach (IGrouping<string, EntityComponent> grouping in enumerable)
			{
				BatchControlRowGroup batchControlRowGroup = this._batchControlRowGroupFactory.CreateSortedWithTextHeader(grouping.Key);
				foreach (EntityComponent entity2 in grouping)
				{
					batchControlRowGroup.AddRow(this._workplacesBatchControlRowFactory.Create(entity2));
				}
				yield return batchControlRowGroup;
			}
			IEnumerator<IGrouping<string, EntityComponent>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000017 RID: 23
		public readonly WorkplacesBatchControlRowFactory _workplacesBatchControlRowFactory;

		// Token: 0x04000018 RID: 24
		public readonly BatchControlRowGroupFactory _batchControlRowGroupFactory;
	}
}
