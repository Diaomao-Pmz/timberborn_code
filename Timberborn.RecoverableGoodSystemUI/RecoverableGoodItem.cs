using System;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.RecoverableGoodSystemUI
{
	// Token: 0x02000009 RID: 9
	public class RecoverableGoodItem
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000257B File Offset: 0x0000077B
		public VisualElement Root { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002583 File Offset: 0x00000783
		public string GoodId { get; }

		// Token: 0x0600001E RID: 30 RVA: 0x0000258B File Offset: 0x0000078B
		public RecoverableGoodItem(VisualElement root, string goodId, Label amountLabel)
		{
			this.Root = root;
			this.GoodId = goodId;
			this._amountLabel = amountLabel;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025A8 File Offset: 0x000007A8
		public void Update(int amount)
		{
			this.Root.ToggleDisplayStyle(amount > 0);
			if (amount > 0)
			{
				this._amountLabel.text = amount.ToString();
			}
		}

		// Token: 0x04000019 RID: 25
		public readonly Label _amountLabel;
	}
}
