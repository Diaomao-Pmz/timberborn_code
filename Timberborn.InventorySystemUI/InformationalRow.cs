using System;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.InventorySystemUI
{
	// Token: 0x02000007 RID: 7
	public class InformationalRow
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public string GoodId { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public VisualElement Root { get; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public InformationalRow(string goodId, VisualElement root, Label goodAmount, Func<int> goodAmountGetter, bool showLimit, Func<int> limitAmountGetter, Label limit, Label separator)
		{
			this.GoodId = goodId;
			this.Root = root;
			this._goodAmountGetter = goodAmountGetter;
			this._goodAmount = goodAmount;
			this._showLimit = showLimit;
			this._limitAmountGetter = limitAmountGetter;
			this._limit = limit;
			this._separator = separator;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002160 File Offset: 0x00000360
		public int CurrentAmount
		{
			get
			{
				return this._goodAmountGetter();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002170 File Offset: 0x00000370
		public void ShowUpdated()
		{
			this._goodAmount.text = this.CurrentAmount.ToString();
			this.UpdateLimits();
			this.Root.ToggleDisplayStyle(true);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A8 File Offset: 0x000003A8
		public void Hide()
		{
			this.Root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B8 File Offset: 0x000003B8
		public void UpdateLimits()
		{
			if (this._showLimit)
			{
				this._limit.text = this._limitAmountGetter().ToString();
			}
			this._separator.ToggleDisplayStyle(this._showLimit);
			this._limit.ToggleDisplayStyle(this._showLimit);
		}

		// Token: 0x0400000A RID: 10
		public readonly Label _goodAmount;

		// Token: 0x0400000B RID: 11
		public readonly Func<int> _goodAmountGetter;

		// Token: 0x0400000C RID: 12
		public readonly bool _showLimit;

		// Token: 0x0400000D RID: 13
		public readonly Func<int> _limitAmountGetter;

		// Token: 0x0400000E RID: 14
		public readonly Label _limit;

		// Token: 0x0400000F RID: 15
		public readonly Label _separator;
	}
}
