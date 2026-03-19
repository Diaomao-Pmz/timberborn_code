using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.GameDistrictsBatchControl;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemBatchControl
{
	// Token: 0x02000004 RID: 4
	public class DistributionBatchControlRowGroupFactory
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public DistributionBatchControlRowGroupFactory(BatchControlRowGroupFactory batchControlRowGroupFactory, DistrictCenterRowItemFactory districtCenterRowItemFactory, DistributionSettingsRowItemFactory distributionSettingsRowItemFactory, DistrictDistributionControlRowItemFactory districtDistributionControlRowItemFactory, VisualElementLoader visualElementLoader)
		{
			this._batchControlRowGroupFactory = batchControlRowGroupFactory;
			this._districtCenterRowItemFactory = districtCenterRowItemFactory;
			this._distributionSettingsRowItemFactory = distributionSettingsRowItemFactory;
			this._districtDistributionControlRowItemFactory = districtDistributionControlRowItemFactory;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F0 File Offset: 0x000002F0
		public BatchControlRowGroup Create(DistrictCenter districtCenter)
		{
			string elementName = "Game/BatchControl/BatchControlRow";
			VisualElement root = this._visualElementLoader.LoadVisualElement(elementName);
			DistrictDistributionSetting component = districtCenter.GetComponent<DistrictDistributionSetting>();
			IBatchControlRowItem batchControlRowItem = this._districtCenterRowItemFactory.Create(districtCenter);
			IBatchControlRowItem batchControlRowItem2 = this._districtDistributionControlRowItemFactory.Create(component);
			BatchControlRow header = new BatchControlRow(root, districtCenter.GetComponent<EntityComponent>(), new IBatchControlRowItem[]
			{
				batchControlRowItem,
				batchControlRowItem2
			});
			BatchControlRowGroup batchControlRowGroup = this._batchControlRowGroupFactory.CreateUnsorted(header);
			batchControlRowGroup.AddRow(this._distributionSettingsRowItemFactory.Create(component));
			return batchControlRowGroup;
		}

		// Token: 0x04000006 RID: 6
		public readonly BatchControlRowGroupFactory _batchControlRowGroupFactory;

		// Token: 0x04000007 RID: 7
		public readonly DistrictCenterRowItemFactory _districtCenterRowItemFactory;

		// Token: 0x04000008 RID: 8
		public readonly DistributionSettingsRowItemFactory _distributionSettingsRowItemFactory;

		// Token: 0x04000009 RID: 9
		public readonly DistrictDistributionControlRowItemFactory _districtDistributionControlRowItemFactory;

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;
	}
}
