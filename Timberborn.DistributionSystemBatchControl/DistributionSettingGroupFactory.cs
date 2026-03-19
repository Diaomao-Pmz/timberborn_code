using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using Timberborn.Goods;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemBatchControl
{
	// Token: 0x02000008 RID: 8
	public class DistributionSettingGroupFactory
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000023C4 File Offset: 0x000005C4
		public DistributionSettingGroupFactory(GoodDistributionSettingItemFactory goodDistributionSettingItemFactory, VisualElementLoader visualElementLoader)
		{
			this._goodDistributionSettingItemFactory = goodDistributionSettingItemFactory;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023DC File Offset: 0x000005DC
		public DistributionSettingGroup Create(GoodGroupSpec groupSpec, DistrictDistributionSetting districtDistributionSetting)
		{
			string elementName = "Game/BatchControl/DistributionSettingsGroup";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = groupSpec.Icon.Asset;
			List<GoodDistributionSettingItem> goodDistributionSettingItems = this.CreateItems(districtDistributionSetting, groupSpec.Id, UQueryExtensions.Q<VisualElement>(visualElement, "Items", null));
			return new DistributionSettingGroup(visualElement, goodDistributionSettingItems);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000243C File Offset: 0x0000063C
		public List<GoodDistributionSettingItem> CreateItems(DistrictDistributionSetting districtDistributionSetting, string groupId, VisualElement parent)
		{
			List<GoodDistributionSettingItem> list = new List<GoodDistributionSettingItem>();
			DistrictDistributableGoodProvider component = districtDistributionSetting.GetComponent<DistrictDistributableGoodProvider>();
			foreach (GoodDistributionSetting goodDistributionSetting in districtDistributionSetting.GetGoodDistributionSettingsForGroup(groupId))
			{
				GoodDistributionSettingItem goodDistributionSettingItem = this._goodDistributionSettingItemFactory.Create(component, goodDistributionSetting);
				list.Add(goodDistributionSettingItem);
				parent.Add(goodDistributionSettingItem.Root);
			}
			return list;
		}

		// Token: 0x04000014 RID: 20
		public readonly GoodDistributionSettingItemFactory _goodDistributionSettingItemFactory;

		// Token: 0x04000015 RID: 21
		public readonly VisualElementLoader _visualElementLoader;
	}
}
