using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.Stockpiles;

namespace Timberborn.StorageBatchControl
{
	// Token: 0x02000007 RID: 7
	public class StorageBatchControlTab : BatchControlTab
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021FE File Offset: 0x000003FE
		public StorageBatchControlTab(VisualElementLoader visualElementLoader, BatchControlDistrict batchControlDistrict, StorageBatchControlRowFactory storageBatchControlRowFactory, BatchControlRowGroupFactory batchControlRowGroupFactory, EventBus eventBus) : base(visualElementLoader, batchControlDistrict, eventBus)
		{
			this._storageBatchControlRowFactory = storageBatchControlRowFactory;
			this._batchControlRowGroupFactory = batchControlRowGroupFactory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002219 File Offset: 0x00000419
		public override string TabNameLocKey
		{
			get
			{
				return "BatchControl.Storage";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002220 File Offset: 0x00000420
		public override string TabImage
		{
			get
			{
				return "Storage";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002227 File Offset: 0x00000427
		public override string BindingKey
		{
			get
			{
				return "StorageTab";
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000222E File Offset: 0x0000042E
		public override IEnumerable<BatchControlRowGroup> GetRowGroups(IEnumerable<EntityComponent> entities)
		{
			IEnumerable<IGrouping<string, EntityComponent>> enumerable = from entity in entities
			where entity.GetComponent<Stockpile>()
			group entity by entity.GetComponent<LabeledEntitySpec>().DisplayNameLocKey;
			foreach (IGrouping<string, EntityComponent> grouping in enumerable)
			{
				string groupSortingKey = StorageBatchControlTab.GetGroupSortingKey(grouping);
				BatchControlRowGroup batchControlRowGroup = this._batchControlRowGroupFactory.CreateSortedWithTextHeader(grouping.Key, groupSortingKey);
				foreach (EntityComponent entity2 in grouping)
				{
					batchControlRowGroup.AddRow(this._storageBatchControlRowFactory.Create(entity2));
				}
				yield return batchControlRowGroup;
			}
			IEnumerator<IGrouping<string, EntityComponent>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002248 File Offset: 0x00000448
		public static string GetGroupSortingKey(IGrouping<string, EntityComponent> group)
		{
			EntityComponent entityComponent = group.First<EntityComponent>();
			Stockpile component = entityComponent.GetComponent<Stockpile>();
			if (component != null)
			{
				return string.Format("{0}_{1:00000}", component.WhitelistedGoodType, component.MaxCapacity);
			}
			return "_" + entityComponent.GetComponent<LabeledEntitySpec>().DisplayNameLocKey;
		}

		// Token: 0x0400000F RID: 15
		public readonly StorageBatchControlRowFactory _storageBatchControlRowFactory;

		// Token: 0x04000010 RID: 16
		public readonly BatchControlRowGroupFactory _batchControlRowGroupFactory;
	}
}
