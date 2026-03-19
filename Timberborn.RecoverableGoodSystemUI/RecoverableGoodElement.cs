using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.CoreUI;
using Timberborn.Goods;
using Timberborn.RecoverableGoodSystem;
using UnityEngine.UIElements;

namespace Timberborn.RecoverableGoodSystemUI
{
	// Token: 0x02000006 RID: 6
	public class RecoverableGoodElement
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000211B File Offset: 0x0000031B
		public VisualElement Root { get; }

		// Token: 0x06000007 RID: 7 RVA: 0x00002123 File Offset: 0x00000323
		public RecoverableGoodElement(VisualElement root, Label label, IEnumerable<RecoverableGoodItem> recoverableGoodItems)
		{
			this.Root = root;
			this._label = label;
			this._recoverableGoodItems = recoverableGoodItems.ToImmutableArray<RecoverableGoodItem>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002145 File Offset: 0x00000345
		public void Update(RecoverableGoodRegistry recoverableGoodRegistry)
		{
			this.UpdateItems(recoverableGoodRegistry.GoodAmounts);
			this._label.ToggleDisplayStyle(recoverableGoodRegistry.TotalAmount > 0);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000216C File Offset: 0x0000036C
		public void UpdateItems(IReadOnlyList<GoodAmount> goodAmounts)
		{
			for (int i = 0; i < this._recoverableGoodItems.Length; i++)
			{
				int amount = RecoverableGoodElement.GetAmount(goodAmounts, this._recoverableGoodItems[i].GoodId);
				this._recoverableGoodItems[i].Update(amount);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021BC File Offset: 0x000003BC
		public static int GetAmount(IReadOnlyList<GoodAmount> goodAmounts, string goodId)
		{
			for (int i = 0; i < goodAmounts.Count; i++)
			{
				if (goodAmounts[i].GoodId == goodId)
				{
					return goodAmounts[i].Amount;
				}
			}
			return 0;
		}

		// Token: 0x04000009 RID: 9
		public readonly Label _label;

		// Token: 0x0400000A RID: 10
		public readonly ImmutableArray<RecoverableGoodItem> _recoverableGoodItems;
	}
}
