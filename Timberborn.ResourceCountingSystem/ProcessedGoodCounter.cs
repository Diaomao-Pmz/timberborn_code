using System;
using System.Collections.Generic;
using Timberborn.Goods;
using Timberborn.InventorySystem;

namespace Timberborn.ResourceCountingSystem
{
	// Token: 0x02000009 RID: 9
	public class ProcessedGoodCounter
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000254C File Offset: 0x0000074C
		public void UpdateStock()
		{
			this._processedStock.Clear();
			this._awaitingProcessStock.Clear();
			foreach (IGoodProcessor goodProcessor in this._goodProcessors)
			{
				this.CountGoodProcessor(goodProcessor);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025B8 File Offset: 0x000007B8
		public int GetProcessedStock(string goodId)
		{
			return CollectionExtensions.GetValueOrDefault<string, int>(this._processedStock, goodId);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025C6 File Offset: 0x000007C6
		public int GetInputStock(string goodId)
		{
			return CollectionExtensions.GetValueOrDefault<string, int>(this._awaitingProcessStock, goodId);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025D4 File Offset: 0x000007D4
		public void Add(IGoodProcessor goodProcessor)
		{
			this._goodProcessors.Add(goodProcessor);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025E2 File Offset: 0x000007E2
		public void Remove(IGoodProcessor goodProcessor)
		{
			this._goodProcessors.Remove(goodProcessor);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025F4 File Offset: 0x000007F4
		public void CountGoodProcessor(IGoodProcessor goodProcessor)
		{
			foreach (GoodAmount goodAmount in goodProcessor.GetProcessingGoods().Goods)
			{
				if (this._processedStock.ContainsKey(goodAmount.GoodId))
				{
					Dictionary<string, int> dictionary = this._processedStock;
					string key = goodAmount.GoodId;
					dictionary[key] += goodAmount.Amount;
				}
				else
				{
					this._processedStock[goodAmount.GoodId] = goodAmount.Amount;
				}
			}
			Inventory inventory = goodProcessor.Inventory;
			if (inventory)
			{
				foreach (string text in inventory.InputGoods)
				{
					if (inventory.Takes(text))
					{
						if (this._awaitingProcessStock.ContainsKey(text))
						{
							Dictionary<string, int> dictionary = this._awaitingProcessStock;
							string key = text;
							dictionary[key] += inventory.AmountInStock(text);
						}
						else
						{
							this._awaitingProcessStock.Add(text, inventory.AmountInStock(text));
						}
					}
				}
			}
		}

		// Token: 0x04000013 RID: 19
		public readonly Dictionary<string, int> _processedStock = new Dictionary<string, int>();

		// Token: 0x04000014 RID: 20
		public readonly Dictionary<string, int> _awaitingProcessStock = new Dictionary<string, int>();

		// Token: 0x04000015 RID: 21
		public readonly List<IGoodProcessor> _goodProcessors = new List<IGoodProcessor>();
	}
}
