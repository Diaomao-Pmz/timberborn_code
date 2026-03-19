using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.CoreUI;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using UnityEngine.UIElements;

namespace Timberborn.BuildingTools
{
	// Token: 0x02000004 RID: 4
	public class BuildingCostSectionProvider
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public BuildingCostSectionProvider(VisualElementLoader visualElementLoader, GoodItemFactory goodItemFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._goodItemFactory = goodItemFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public bool TryGetSection(Preview preview, out VisualElement section)
		{
			BuildingSpec component = preview.GetComponent<BuildingSpec>();
			string elementName = "Game/ToolPanel/DescriptionPanelCostSection";
			section = this._visualElementLoader.LoadVisualElement(elementName);
			ImmutableArray<GoodAmountSpec> buildingCost = component.BuildingCost;
			bool flag = buildingCost.Length > 0;
			if (flag)
			{
				this.AddCost(buildingCost, section);
			}
			return flag;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000211C File Offset: 0x0000031C
		public void AddCost(IEnumerable<GoodAmountSpec> cost, VisualElement root)
		{
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(root, "Materials", null);
			foreach (GoodAmountSpec goodAmount in cost)
			{
				visualElement.Add(this._goodItemFactory.Create(goodAmount, true));
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000007 RID: 7
		public readonly GoodItemFactory _goodItemFactory;
	}
}
