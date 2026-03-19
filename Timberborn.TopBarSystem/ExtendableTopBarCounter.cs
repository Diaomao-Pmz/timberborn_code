using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.CoreUI;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.TopBarSystem
{
	// Token: 0x02000004 RID: 4
	public class ExtendableTopBarCounter : ITopBarCounter
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public ExtendableTopBarCounter(IEnumerable<TopBarCounterRow> counterRows, Label emptyCounterPlaceholder, Label value)
		{
			this._emptyCounterPlaceholder = emptyCounterPlaceholder;
			this._value = value;
			this._counterRows = counterRows.ToImmutableArray<TopBarCounterRow>();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		public void UpdateValues()
		{
			int stockSum = this.GetStockSum();
			if (this._previousSum != stockSum)
			{
				this._value.text = NumberFormatter.Format(stockSum);
				this._emptyCounterPlaceholder.ToggleDisplayStyle(stockSum == 0);
				this._previousSum = stockSum;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000212C File Offset: 0x0000032C
		public int GetStockSum()
		{
			int num = 0;
			foreach (TopBarCounterRow topBarCounterRow in this._counterRows)
			{
				num += topBarCounterRow.UpdateAndGetStock();
			}
			return num;
		}

		// Token: 0x04000006 RID: 6
		public readonly ImmutableArray<TopBarCounterRow> _counterRows;

		// Token: 0x04000007 RID: 7
		public readonly Label _emptyCounterPlaceholder;

		// Token: 0x04000008 RID: 8
		public readonly Label _value;

		// Token: 0x04000009 RID: 9
		public int _previousSum = -1;
	}
}
