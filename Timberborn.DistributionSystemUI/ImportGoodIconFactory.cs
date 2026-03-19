using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemUI
{
	// Token: 0x02000009 RID: 9
	public class ImportGoodIconFactory
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000245B File Offset: 0x0000065B
		public ImportGoodIconFactory(GoodDescriber goodDescriber, IGoodService goodService, GoodsGroupSpecService goodsGroupSpecService, ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._goodDescriber = goodDescriber;
			this._goodService = goodService;
			this._goodsGroupSpecService = goodsGroupSpecService;
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002488 File Offset: 0x00000688
		public IEnumerable<ImportGoodIcon> CreateImportGoods(VisualElement parent)
		{
			List<ImportGoodIcon> list = new List<ImportGoodIcon>();
			foreach (GoodGroupSpec groupSpec in this._goodsGroupSpecService.GoodGroupSpecs)
			{
				list.AddRange(this.CreateImportGoodsGroup(parent, groupSpec));
			}
			return list;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024F4 File Offset: 0x000006F4
		public ImportGoodIcon CreateImportGoodIcon(VisualElement parent, string goodId)
		{
			string elementName = "Game/ImportGoodIcon";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			parent.Add(visualElement);
			Image image = UQueryExtensions.Q<Image>(visualElement, "Icon", null);
			DescribedGood describedGood = this._goodDescriber.GetDescribedGood(goodId);
			image.sprite = describedGood.Icon;
			VisualElement importableIcon = UQueryExtensions.Q<VisualElement>(visualElement, "ImportableIcon", null);
			VisualElement nonImportableIcon = UQueryExtensions.Q<VisualElement>(visualElement, "NonImportableIcon", null);
			ImportGoodIcon importGoodIcon = new ImportGoodIcon(goodId, importableIcon, nonImportableIcon);
			this._tooltipRegistrar.Register(image, () => this.GetTooltip(importGoodIcon, goodId, describedGood.DisplayName));
			return importGoodIcon;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025B3 File Offset: 0x000007B3
		public IEnumerable<ImportGoodIcon> CreateImportGoodsGroup(VisualElement parent, GoodGroupSpec groupSpec)
		{
			string elementName = "Game/EntityPanel/ImportGoodsGroup";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = groupSpec.Icon.Asset;
			parent.Add(visualElement);
			VisualElement iconsParent = UQueryExtensions.Q<VisualElement>(visualElement, "Items", null);
			foreach (string goodId in this._goodService.GetGoodsForGroup(groupSpec.Id))
			{
				yield return this.CreateImportGoodIcon(iconsParent, goodId);
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025D4 File Offset: 0x000007D4
		public VisualElement GetTooltip(ImportGoodIcon importGoodIcon, string goodId, string goodDisplayName)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/ImportGoodIconTooltip");
			UQueryExtensions.Q<Label>(visualElement, "GoodLabel", null).text = goodDisplayName;
			DistrictDistributableGoodProvider districtDistributableGoodProvider = importGoodIcon.DistrictDistributableGoodProvider;
			bool flag = districtDistributableGoodProvider.IsImportEnabled(goodId);
			ImportOption goodImportOption = districtDistributableGoodProvider.GetGoodImportOption(goodId);
			UQueryExtensions.Q<VisualElement>(visualElement, "DisabledInfo", null).ToggleDisplayStyle(goodImportOption == ImportOption.Disabled);
			UQueryExtensions.Q<VisualElement>(visualElement, "ForcedInfo", null).ToggleDisplayStyle(goodImportOption == ImportOption.Forced);
			UQueryExtensions.Q<VisualElement>(visualElement, "ImportableInfo", null).ToggleDisplayStyle(goodImportOption == ImportOption.Auto && flag);
			UQueryExtensions.Q<VisualElement>(visualElement, "NonImportableInfo", null).ToggleDisplayStyle(goodImportOption == ImportOption.Auto && !flag);
			return visualElement;
		}

		// Token: 0x04000018 RID: 24
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000019 RID: 25
		public readonly IGoodService _goodService;

		// Token: 0x0400001A RID: 26
		public readonly GoodsGroupSpecService _goodsGroupSpecService;

		// Token: 0x0400001B RID: 27
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400001C RID: 28
		public readonly VisualElementLoader _visualElementLoader;
	}
}
