using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.InventorySystem;
using UnityEngine.UIElements;

namespace Timberborn.InventorySystemUI
{
	// Token: 0x02000008 RID: 8
	public class InformationalRowsFactory
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000220D File Offset: 0x0000040D
		public InformationalRowsFactory(VisualElementLoader visualElementLoader, GoodDescriber goodDescriber)
		{
			this._visualElementLoader = visualElementLoader;
			this._goodDescriber = goodDescriber;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002223 File Offset: 0x00000423
		public IEnumerable<InformationalRow> CreateRowsWithLimits(Inventory inventory, VisualElement parent)
		{
			return this.CreateRows(inventory, parent, true);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000222E File Offset: 0x0000042E
		public IEnumerable<InformationalRow> CreateRowsWithoutLimits(Inventory inventory, VisualElement parent)
		{
			return this.CreateRows(inventory, parent, false);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002239 File Offset: 0x00000439
		public InformationalRow CreateInputRowWithLimit(StorableGood good, Inventory inventory, VisualElement parent)
		{
			return this.CreateInputRow(good, inventory, parent, true);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002245 File Offset: 0x00000445
		public InformationalRow CreateOutputRowWithLimit(StorableGood good, Inventory inventory, VisualElement parent)
		{
			return this.CreateOutputRow(good, inventory, parent, true);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002251 File Offset: 0x00000451
		public InformationalRow CreateSimpleRowWithoutLimit(StorableGood good, Inventory inventory, VisualElement parent)
		{
			return this.CreateInformationalRow(good, inventory, parent, false);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000225D File Offset: 0x0000045D
		public IEnumerable<InformationalRow> CreateRows(Inventory inventory, VisualElement parent, bool withLimits)
		{
			ReadOnlyList<StorableGoodAmount> goods = inventory.AllowedGoods;
			IEnumerable<StorableGood> enumerable = from good in goods
			where good.StorableGood.IsOnlyGivable
			select good.StorableGood;
			foreach (StorableGood good3 in enumerable)
			{
				yield return this.CreateInputRow(good3, inventory, parent, withLimits);
			}
			IEnumerator<StorableGood> enumerator = null;
			IEnumerable<StorableGood> enumerable2 = from good in goods
			where good.StorableGood.IsOnlyTakeable
			select good.StorableGood;
			foreach (StorableGood good2 in enumerable2)
			{
				yield return this.CreateOutputRow(good2, inventory, parent, withLimits);
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002282 File Offset: 0x00000482
		public InformationalRow CreateInputRow(StorableGood good, Inventory inventory, VisualElement parent, bool withLimit)
		{
			InformationalRow informationalRow = this.CreateInformationalRow(good, inventory, parent, withLimit);
			UQueryExtensions.Q<Image>(informationalRow.Root, "Type", null).AddToClassList(InformationalRowsFactory.InputClassName);
			return informationalRow;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022AA File Offset: 0x000004AA
		public InformationalRow CreateOutputRow(StorableGood good, Inventory inventory, VisualElement parent, bool withLimit)
		{
			InformationalRow informationalRow = this.CreateInformationalRow(good, inventory, parent, withLimit);
			UQueryExtensions.Q<Image>(informationalRow.Root, "Type", null).AddToClassList(InformationalRowsFactory.OutputClassName);
			return informationalRow;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022D4 File Offset: 0x000004D4
		public InformationalRow CreateInformationalRow(StorableGood good, Inventory inventory, VisualElement parent, bool withLimit)
		{
			string goodId = good.GoodId;
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/InventoryInformationalRow");
			parent.Add(visualElement);
			DescribedGood describedGood = this._goodDescriber.GetDescribedGood(goodId);
			UQueryExtensions.Q<Image>(visualElement, "Image", null).sprite = describedGood.Icon;
			UQueryExtensions.Q<Label>(visualElement, "Name", null).text = describedGood.DisplayName;
			return new InformationalRow(goodId, visualElement, UQueryExtensions.Q<Label>(visualElement, "Amount", null), () => inventory.AmountInStock(goodId), withLimit, () => inventory.LimitedAmount(goodId), UQueryExtensions.Q<Label>(visualElement, "Limit", null), UQueryExtensions.Q<Label>(visualElement, "Separator", null));
		}

		// Token: 0x04000010 RID: 16
		public static readonly string InputClassName = "inventory-row-informational__type--input";

		// Token: 0x04000011 RID: 17
		public static readonly string OutputClassName = "inventory-row-informational__type--output";

		// Token: 0x04000012 RID: 18
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000013 RID: 19
		public readonly GoodDescriber _goodDescriber;
	}
}
