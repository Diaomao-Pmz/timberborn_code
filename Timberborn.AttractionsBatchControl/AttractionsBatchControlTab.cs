using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Attractions;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;

namespace Timberborn.AttractionsBatchControl
{
	// Token: 0x02000007 RID: 7
	public class AttractionsBatchControlTab : BatchControlTab
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002221 File Offset: 0x00000421
		public AttractionsBatchControlTab(VisualElementLoader visualElementLoader, BatchControlDistrict batchControlDistrict, BatchControlRowGroupFactory batchControlRowGroupFactory, AttractionsBatchControlRowFactory attractionsBatchControlRowFactory, EventBus eventBus) : base(visualElementLoader, batchControlDistrict, eventBus)
		{
			this._batchControlRowGroupFactory = batchControlRowGroupFactory;
			this._attractionsBatchControlRowFactory = attractionsBatchControlRowFactory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000223C File Offset: 0x0000043C
		public override string TabNameLocKey
		{
			get
			{
				return "Wellbeing.DisplayName";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002243 File Offset: 0x00000443
		public override string TabImage
		{
			get
			{
				return "Attractions";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000224A File Offset: 0x0000044A
		public override string BindingKey
		{
			get
			{
				return "AttractionsTab";
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002251 File Offset: 0x00000451
		public override IEnumerable<BatchControlRowGroup> GetRowGroups(IEnumerable<EntityComponent> entities)
		{
			IEnumerable<IGrouping<string, EntityComponent>> enumerable = from entity in entities
			where entity.GetComponent<Attraction>()
			group entity by entity.GetComponent<LabeledEntitySpec>().DisplayNameLocKey;
			foreach (IGrouping<string, EntityComponent> grouping in enumerable)
			{
				BatchControlRowGroup batchControlRowGroup = this._batchControlRowGroupFactory.CreateSortedWithTextHeader(grouping.Key);
				foreach (EntityComponent entity2 in grouping)
				{
					batchControlRowGroup.AddRow(this._attractionsBatchControlRowFactory.Create(entity2));
				}
				yield return batchControlRowGroup;
			}
			IEnumerator<IGrouping<string, EntityComponent>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000010 RID: 16
		public readonly BatchControlRowGroupFactory _batchControlRowGroupFactory;

		// Token: 0x04000011 RID: 17
		public readonly AttractionsBatchControlRowFactory _attractionsBatchControlRowFactory;
	}
}
