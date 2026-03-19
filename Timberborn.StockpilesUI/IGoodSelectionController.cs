using System;
using Timberborn.Stockpiles;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x0200000E RID: 14
	public interface IGoodSelectionController
	{
		// Token: 0x0600002C RID: 44
		void Initialize(VisualElement root);

		// Token: 0x0600002D RID: 45
		void Update();

		// Token: 0x0600002E RID: 46
		void SetStockpile(Stockpile stockpile);

		// Token: 0x0600002F RID: 47
		void ShowGoodSelectionBox();

		// Token: 0x06000030 RID: 48
		void Clear();
	}
}
