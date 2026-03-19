using System;
using System.Collections.Generic;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.InventorySystem;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.InventorySystemBatchControl
{
	// Token: 0x02000006 RID: 6
	public class InventoryCapacityBatchControlRowItemFactory
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021AC File Offset: 0x000003AC
		public InventoryCapacityBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, GoodDescriber goodDescriber)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._goodDescriber = goodDescriber;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021CC File Offset: 0x000003CC
		public IBatchControlRowItem Create(Inventory inventory)
		{
			VisualElement visualElement = this.CreateRoot();
			VisualElement inventoryWrapper = UQueryExtensions.Q<VisualElement>(visualElement, "InventoryWrapper", null);
			IEnumerable<InventoryCapacityBatchControlGood> goods = this.CreateGoods(inventory, inventoryWrapper);
			return new InventoryCapacityBatchControlRowItem(visualElement, inventory, goods);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FC File Offset: 0x000003FC
		public VisualElement CreateRoot()
		{
			string elementName = "Game/BatchControl/InventoryCapacityBatchControlRowItem";
			return this._visualElementLoader.LoadVisualElement(elementName);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000221B File Offset: 0x0000041B
		public IEnumerable<InventoryCapacityBatchControlGood> CreateGoods(Inventory inventory, VisualElement inventoryWrapper)
		{
			foreach (StorableGoodAmount storableGoodAmount in inventory.AllowedGoods)
			{
				string goodId = storableGoodAmount.StorableGood.GoodId;
				DescribedGood describedGood = this._goodDescriber.GetDescribedGood(goodId);
				VisualElement visualElement = this.CreateGoodElement();
				InventoryCapacityBatchControlRowItemFactory.InitializeIcon(visualElement, describedGood);
				InventoryCapacityBatchControlRowItemFactory.InitializeLabels(visualElement, inventory, goodId);
				this.InitializeTooltip(visualElement, describedGood);
				inventoryWrapper.Add(visualElement);
				yield return new InventoryCapacityBatchControlGood(UQueryExtensions.Q<Label>(visualElement, "CapacityAmount", null), inventory, goodId);
			}
			List<StorableGoodAmount>.Enumerator enumerator = default(List<StorableGoodAmount>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000223C File Offset: 0x0000043C
		public VisualElement CreateGoodElement()
		{
			string elementName = "Game/BatchControl/InventoryCapacityBatchControlGood";
			return this._visualElementLoader.LoadVisualElement(elementName);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000225B File Offset: 0x0000045B
		public static void InitializeIcon(VisualElement goodElement, DescribedGood describedGood)
		{
			UQueryExtensions.Q<Image>(goodElement, "GoodIcon", null).sprite = describedGood.Icon;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002278 File Offset: 0x00000478
		public static void InitializeLabels(VisualElement goodElement, Inventory inventory, string goodId)
		{
			UQueryExtensions.Q<Label>(goodElement, "CapacityAmount", null).text = inventory.AmountInStock(goodId).ToString();
			UQueryExtensions.Q<Label>(goodElement, "CapacityLimit", null).text = inventory.LimitedAmount(goodId).ToString();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022C8 File Offset: 0x000004C8
		public void InitializeTooltip(VisualElement goodElement, DescribedGood describedGood)
		{
			this._tooltipRegistrar.Register(goodElement, () => describedGood.DisplayName);
		}

		// Token: 0x0400000C RID: 12
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000D RID: 13
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000E RID: 14
		public readonly GoodDescriber _goodDescriber;
	}
}
