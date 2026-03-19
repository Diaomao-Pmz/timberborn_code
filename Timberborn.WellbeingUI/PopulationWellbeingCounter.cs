using System;
using Timberborn.CoreUI;
using Timberborn.NeedSpecs;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000013 RID: 19
	public class PopulationWellbeingCounter
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000330A File Offset: 0x0000150A
		public string NeedId { get; }

		// Token: 0x0600005B RID: 91 RVA: 0x00003314 File Offset: 0x00001514
		public PopulationWellbeingCounter(NeedSpec needSpec, VisualElement root, VisualElement bar, VisualElement barWrapper, Label appliedCount, Label averageWellbeingShare)
		{
			this._needSpec = needSpec;
			this._root = root;
			this._bar = bar;
			this._barWrapper = barWrapper;
			this._appliedCount = appliedCount;
			this._averageWellbeingShare = averageWellbeingShare;
			this.NeedId = needSpec.Id;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003360 File Offset: 0x00001560
		public void UpdateValues(int appliedNeeds, int totalBeaverCount)
		{
			this.UpdateVisibility(appliedNeeds);
			this.UpdateColors();
			this.UpdateProgress(appliedNeeds, totalBeaverCount);
			this.UpdateAverageWellbeingShare(appliedNeeds, totalBeaverCount);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003380 File Offset: 0x00001580
		public void UpdateVisibility(int appliedNeeds)
		{
			bool isNeverPositive = this._needSpec.IsNeverPositive;
			this._root.ToggleDisplayStyle(!isNeverPositive || appliedNeeds > 0);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000033AE File Offset: 0x000015AE
		public void UpdateColors()
		{
			this._bar.EnableInClassList(PopulationWellbeingCounter.NegativeWellbeingClass, this._needSpec.IsNeverPositive);
			this._barWrapper.EnableInClassList(PopulationWellbeingCounter.NegativeWellbeingClass, !this._needSpec.IsNeverNegative);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000033EC File Offset: 0x000015EC
		public void UpdateProgress(int appliedNeeds, int totalPopulationCount)
		{
			float num = ((appliedNeeds == 0) ? 0f : ((float)appliedNeeds / (float)totalPopulationCount)) * 100f;
			this._bar.style.width = new StyleLength(Length.Percent(num));
			this._appliedCount.text = string.Format("{0} / {1}", appliedNeeds, totalPopulationCount);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000344C File Offset: 0x0000164C
		public void UpdateAverageWellbeingShare(int appliedNeeds, int totalPopulationCount)
		{
			int num = this.CalculateTotalWellbeing(appliedNeeds, totalPopulationCount);
			float num2 = (totalPopulationCount == 0) ? 0f : ((float)num / (float)totalPopulationCount);
			this._averageWellbeingShare.text = PopulationWellbeingCounter.FormatAverageWellbeingShare(num2);
			this._averageWellbeingShare.EnableInClassList(PopulationWellbeingCounter.PositiveWellbeingClass, num2 > 0f);
			this._averageWellbeingShare.EnableInClassList(PopulationWellbeingCounter.NegativeWellbeingClass, num2 < 0f);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000034B4 File Offset: 0x000016B4
		public int CalculateTotalWellbeing(int appliedNeeds, int totalPopulationCount)
		{
			int favorableWellbeing = this._needSpec.GetFavorableWellbeing();
			int unfavorableWellbeing = this._needSpec.GetUnfavorableWellbeing();
			int num = totalPopulationCount - appliedNeeds;
			int num2;
			int num3;
			if (!this._needSpec.IsNeverPositive && !this._needSpec.IsNeverNegative)
			{
				num2 = appliedNeeds * favorableWellbeing;
				num3 = num * unfavorableWellbeing;
			}
			else
			{
				num2 = (this._needSpec.IsNeverNegative ? (appliedNeeds * favorableWellbeing) : 0);
				num3 = (this._needSpec.IsNeverPositive ? (appliedNeeds * unfavorableWellbeing) : 0);
			}
			return num2 + num3;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003530 File Offset: 0x00001730
		public static string FormatAverageWellbeingShare(float averageWellbeingShare)
		{
			string arg = (averageWellbeingShare != 0f) ? ((averageWellbeingShare < 0f) ? "-" : "+") : string.Empty;
			return string.Format("{0}{1:0.0}", arg, Math.Abs(averageWellbeingShare));
		}

		// Token: 0x04000059 RID: 89
		public static readonly string PositiveWellbeingClass = "wellbeing--positive";

		// Token: 0x0400005A RID: 90
		public static readonly string NegativeWellbeingClass = "wellbeing--negative";

		// Token: 0x0400005C RID: 92
		public readonly NeedSpec _needSpec;

		// Token: 0x0400005D RID: 93
		public readonly VisualElement _root;

		// Token: 0x0400005E RID: 94
		public readonly VisualElement _bar;

		// Token: 0x0400005F RID: 95
		public readonly VisualElement _barWrapper;

		// Token: 0x04000060 RID: 96
		public readonly Label _appliedCount;

		// Token: 0x04000061 RID: 97
		public readonly Label _averageWellbeingShare;
	}
}
