using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;
using Timberborn.CoreUI;
using Timberborn.TooltipSystem;
using Timberborn.Wellbeing;
using UnityEngine.UIElements;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000027 RID: 39
	public class WellbeingSummaryFactory
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00004AF9 File Offset: 0x00002CF9
		public WellbeingSummaryFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, WellbeingSummaryBonusFactory wellbeingSummaryBonusFactory, WellbeingNameHelper wellbeingNameHelper)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._wellbeingSummaryBonusFactory = wellbeingSummaryBonusFactory;
			this._wellbeingNameHelper = wellbeingNameHelper;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004B20 File Offset: 0x00002D20
		public WellbeingSummary Create(BaseComponent entity)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/WellbeingSummaryFragment");
			WellbeingBonusesSubjectSpec component = entity.GetComponent<WellbeingBonusesSubjectSpec>();
			WellbeingTracker component2 = entity.GetComponent<WellbeingTracker>();
			IEnumerable<WellbeingSummaryBonus> wellbeingSummaryBonuses = this.CreateBonuses(visualElement, component, entity);
			Label wellbeingValue = UQueryExtensions.Q<Label>(visualElement, "WellbeingText", null);
			this.CreateWellbeingTooltip(visualElement, component2);
			WellbeingSummary wellbeingSummary = new WellbeingSummary(visualElement, component2, wellbeingValue, wellbeingSummaryBonuses);
			wellbeingSummary.UpdateContent();
			return wellbeingSummary;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004B7C File Offset: 0x00002D7C
		public IEnumerable<WellbeingSummaryBonus> CreateBonuses(VisualElement root, WellbeingBonusesSubjectSpec wellbeingBonusesSubjectSpec, BaseComponent entity)
		{
			List<WellbeingSummaryBonus> list = new List<WellbeingSummaryBonus>();
			BonusManager component = entity.GetComponent<BonusManager>();
			foreach (string bonusId in wellbeingBonusesSubjectSpec.Bonuses)
			{
				WellbeingSummaryBonus wellbeingSummaryBonus = this._wellbeingSummaryBonusFactory.Create(component, bonusId);
				root.Add(wellbeingSummaryBonus.Root);
				list.Add(wellbeingSummaryBonus);
			}
			return list;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004BE0 File Offset: 0x00002DE0
		public void CreateWellbeingTooltip(VisualElement root, WellbeingTracker wellbeingTracker)
		{
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(root, "Wellbeing", null);
			this._tooltipRegistrar.RegisterUpdatable(visualElement, () => this.GetWellbeingTooltipText(wellbeingTracker));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004C28 File Offset: 0x00002E28
		public string GetWellbeingTooltipText(WellbeingTracker wellbeingTracker)
		{
			string wellbeingName = this._wellbeingNameHelper.GetWellbeingName(wellbeingTracker);
			return string.Format("{0}: {1}", wellbeingName, wellbeingTracker.Wellbeing);
		}

		// Token: 0x040000C1 RID: 193
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x040000C2 RID: 194
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x040000C3 RID: 195
		public readonly WellbeingSummaryBonusFactory _wellbeingSummaryBonusFactory;

		// Token: 0x040000C4 RID: 196
		public readonly WellbeingNameHelper _wellbeingNameHelper;
	}
}
