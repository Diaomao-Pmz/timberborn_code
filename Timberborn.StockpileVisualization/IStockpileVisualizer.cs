using System;
using Timberborn.Goods;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x02000014 RID: 20
	public interface IStockpileVisualizer
	{
		// Token: 0x06000079 RID: 121
		bool CanVisualize(string stockpileVisualization);

		// Token: 0x0600007A RID: 122
		void Initialize(GoodSpec goodSpec, int capacity);

		// Token: 0x0600007B RID: 123
		void UpdateAmount(int amountInStock);

		// Token: 0x0600007C RID: 124
		void Clear();
	}
}
