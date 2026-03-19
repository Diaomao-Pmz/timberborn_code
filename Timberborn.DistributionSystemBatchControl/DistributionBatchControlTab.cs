using System;
using System.Collections.Generic;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.SingletonSystem;

namespace Timberborn.DistributionSystemBatchControl
{
	// Token: 0x02000005 RID: 5
	public class DistributionBatchControlTab : BatchControlTab
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000216A File Offset: 0x0000036A
		public DistributionBatchControlTab(VisualElementLoader visualElementLoader, BatchControlDistrict batchControlDistrict, DistrictCenterRegistry districtCenterRegistry, DistributionBatchControlRowGroupFactory distributionBatchControlRowGroupFactory, EventBus eventBus) : base(visualElementLoader, batchControlDistrict, eventBus)
		{
			this._districtCenterRegistry = districtCenterRegistry;
			this._distributionBatchControlRowGroupFactory = distributionBatchControlRowGroupFactory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002185 File Offset: 0x00000385
		public override string TabNameLocKey
		{
			get
			{
				return "BatchControl.Distribution";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000218C File Offset: 0x0000038C
		public override string TabImage
		{
			get
			{
				return "Distribution";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002193 File Offset: 0x00000393
		public override string BindingKey
		{
			get
			{
				return "DistributionTab";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000219A File Offset: 0x0000039A
		public override bool RemoveEmptyRowGroups
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000219D File Offset: 0x0000039D
		public override IEnumerable<BatchControlRowGroup> GetRowGroups(IEnumerable<EntityComponent> entities)
		{
			foreach (DistrictCenter districtCenter in this._districtCenterRegistry.FinishedDistrictCenters)
			{
				yield return this._distributionBatchControlRowGroupFactory.Create(districtCenter);
			}
			List<DistrictCenter>.Enumerator enumerator = default(List<DistrictCenter>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0400000B RID: 11
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x0400000C RID: 12
		public readonly DistributionBatchControlRowGroupFactory _distributionBatchControlRowGroupFactory;
	}
}
