using System;
using Timberborn.BatchControl;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using Timberborn.EntitySystem;
using Timberborn.Goods;

namespace Timberborn.DistributionSystemBatchControl
{
	// Token: 0x02000009 RID: 9
	public class DistributionSettingsRowItemFactory
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000024B8 File Offset: 0x000006B8
		public DistributionSettingsRowItemFactory(DistributionSettingGroupFactory distributionSettingGroupFactory, GoodsGroupSpecService goodsGroupSpecService, VisualElementLoader visualElementLoader)
		{
			this._distributionSettingGroupFactory = distributionSettingGroupFactory;
			this._goodsGroupSpecService = goodsGroupSpecService;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024D8 File Offset: 0x000006D8
		public BatchControlRow Create(DistrictDistributionSetting districtDistributionSetting)
		{
			string elementName = "Game/BatchControl/DistributionSettingsRowItem";
			return new BatchControlRow(this._visualElementLoader.LoadVisualElement(elementName), districtDistributionSetting.GetComponent<EntityComponent>(), this.CreateSettingGroups(districtDistributionSetting));
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002509 File Offset: 0x00000709
		public ReadOnlyList<GoodGroupSpec> GoodGroupSpecs
		{
			get
			{
				return this._goodsGroupSpecService.GoodGroupSpecs;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002518 File Offset: 0x00000718
		public IBatchControlRowItem[] CreateSettingGroups(DistrictDistributionSetting districtDistributionSetting)
		{
			IBatchControlRowItem[] array = new IBatchControlRowItem[this.GoodGroupSpecs.Count];
			for (int i = 0; i < this.GoodGroupSpecs.Count; i++)
			{
				GoodGroupSpec groupSpec = this.GoodGroupSpecs[i];
				array[i] = this._distributionSettingGroupFactory.Create(groupSpec, districtDistributionSetting);
			}
			return array;
		}

		// Token: 0x04000016 RID: 22
		public readonly DistributionSettingGroupFactory _distributionSettingGroupFactory;

		// Token: 0x04000017 RID: 23
		public readonly GoodsGroupSpecService _goodsGroupSpecService;

		// Token: 0x04000018 RID: 24
		public readonly VisualElementLoader _visualElementLoader;
	}
}
