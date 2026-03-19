using System;
using System.Collections.Generic;
using Timberborn.Wellbeing;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000023 RID: 35
	public class WellbeingSummary
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000048C3 File Offset: 0x00002AC3
		public VisualElement Root { get; }

		// Token: 0x060000C4 RID: 196 RVA: 0x000048CB File Offset: 0x00002ACB
		public WellbeingSummary(VisualElement root, WellbeingTracker wellbeingTracker, Label wellbeingValue, IEnumerable<WellbeingSummaryBonus> wellbeingSummaryBonuses)
		{
			this.Root = root;
			this._wellbeingTracker = wellbeingTracker;
			this._wellbeingValue = wellbeingValue;
			this._wellbeingSummaryBonuses = wellbeingSummaryBonuses;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000048F0 File Offset: 0x00002AF0
		public void UpdateContent()
		{
			this.UpdateWellbeing();
			this.UpdateBonuses();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004900 File Offset: 0x00002B00
		public void UpdateWellbeing()
		{
			int wellbeing = this._wellbeingTracker.Wellbeing;
			this._wellbeingValue.text = wellbeing.ToString();
			this._wellbeingValue.EnableInClassList(WellbeingSummary.NegativeWellbeingClass, wellbeing < 0);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004940 File Offset: 0x00002B40
		public void UpdateBonuses()
		{
			foreach (WellbeingSummaryBonus wellbeingSummaryBonus in this._wellbeingSummaryBonuses)
			{
				wellbeingSummaryBonus.UpdateBonus();
			}
		}

		// Token: 0x040000B0 RID: 176
		public static readonly string NegativeWellbeingClass = "wellbeing--negative";

		// Token: 0x040000B2 RID: 178
		public readonly WellbeingTracker _wellbeingTracker;

		// Token: 0x040000B3 RID: 179
		public readonly Label _wellbeingValue;

		// Token: 0x040000B4 RID: 180
		public readonly IEnumerable<WellbeingSummaryBonus> _wellbeingSummaryBonuses;
	}
}
