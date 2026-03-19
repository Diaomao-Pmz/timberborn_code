using System;
using Timberborn.BonusSystem;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000024 RID: 36
	public class WellbeingSummaryBonus
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004998 File Offset: 0x00002B98
		public VisualElement Root { get; }

		// Token: 0x060000CA RID: 202 RVA: 0x000049A0 File Offset: 0x00002BA0
		public WellbeingSummaryBonus(VisualElement root, BonusManager bonusManager, Label bonusValue, string bonusId)
		{
			this.Root = root;
			this._bonusManager = bonusManager;
			this._bonusValue = bonusValue;
			this._bonusId = bonusId;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000049C8 File Offset: 0x00002BC8
		public void UpdateBonus()
		{
			float num = this._bonusManager.Multiplier(this._bonusId) - 1f;
			this._bonusValue.text = num.ToString(WellbeingSummaryBonus.BonusFormat);
		}

		// Token: 0x040000B5 RID: 181
		public static readonly string BonusFormat = "+#%;-#%;0%";

		// Token: 0x040000B7 RID: 183
		public readonly BonusManager _bonusManager;

		// Token: 0x040000B8 RID: 184
		public readonly Label _bonusValue;

		// Token: 0x040000B9 RID: 185
		public readonly string _bonusId;
	}
}
