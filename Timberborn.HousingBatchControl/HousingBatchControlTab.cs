using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.DwellingSystem;
using Timberborn.EntitySystem;
using Timberborn.Reproduction;
using Timberborn.SingletonSystem;

namespace Timberborn.HousingBatchControl
{
	// Token: 0x02000007 RID: 7
	public class HousingBatchControlTab : BatchControlTab
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002215 File Offset: 0x00000415
		public HousingBatchControlTab(VisualElementLoader visualElementLoader, BatchControlDistrict batchControlDistrict, HousingBatchControlRowFactory housingBatchControlRowFactory, BatchControlRowGroupFactory batchControlRowGroupFactory, EventBus eventBus) : base(visualElementLoader, batchControlDistrict, eventBus)
		{
			this._housingBatchControlRowFactory = housingBatchControlRowFactory;
			this._batchControlRowGroupFactory = batchControlRowGroupFactory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002230 File Offset: 0x00000430
		public override string TabNameLocKey
		{
			get
			{
				return "BatchControl.Housing";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002237 File Offset: 0x00000437
		public override string TabImage
		{
			get
			{
				return "Housing";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000223E File Offset: 0x0000043E
		public override string BindingKey
		{
			get
			{
				return "HousingTab";
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002245 File Offset: 0x00000445
		public override IEnumerable<BatchControlRowGroup> GetRowGroups(IEnumerable<EntityComponent> entities)
		{
			IEnumerable<IGrouping<string, EntityComponent>> enumerable = from gameObject in entities
			where gameObject.GetComponent<Dwelling>() || gameObject.GetComponent<BreedingPod>()
			group gameObject by gameObject.GetComponent<LabeledEntitySpec>().DisplayNameLocKey;
			foreach (IGrouping<string, EntityComponent> grouping in enumerable)
			{
				BatchControlRowGroup batchControlRowGroup = this._batchControlRowGroupFactory.CreateSortedWithTextHeader(grouping.Key);
				foreach (EntityComponent entity in grouping)
				{
					batchControlRowGroup.AddRow(this._housingBatchControlRowFactory.Create(entity));
				}
				yield return batchControlRowGroup;
			}
			IEnumerator<IGrouping<string, EntityComponent>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000010 RID: 16
		public readonly HousingBatchControlRowFactory _housingBatchControlRowFactory;

		// Token: 0x04000011 RID: 17
		public readonly BatchControlRowGroupFactory _batchControlRowGroupFactory;
	}
}
