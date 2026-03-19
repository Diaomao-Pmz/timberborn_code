using System;
using Timberborn.CoreUI;
using Timberborn.Population;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.PopulationUI
{
	// Token: 0x02000008 RID: 8
	public class PopulationDataRow : IPopulationRow
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000022D0 File Offset: 0x000004D0
		public PopulationDataRow(PopulationService populationService, Label adultCount, Label childCount, Label botCount, Label contaminatedCount, VisualElement botIcon, VisualElement contaminatedIcon, Func<PopulationData> populationDataGetter)
		{
			this._populationService = populationService;
			this._adultCount = adultCount;
			this._childCount = childCount;
			this._botCount = botCount;
			this._contaminatedCount = contaminatedCount;
			this._botIcon = botIcon;
			this._contaminatedIcon = contaminatedIcon;
			this._populationDataGetter = populationDataGetter;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002320 File Offset: 0x00000520
		public void UpdateData()
		{
			PopulationData populationData = this._populationDataGetter();
			this.UpdateBeaverCount(populationData);
			this.UpdateBotCount(populationData);
			this.UpdateContaminatedCount(populationData);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000234E File Offset: 0x0000054E
		public void UpdateBeaverCount(PopulationData populationData)
		{
			this._adultCount.text = (NumberFormatter.Format(populationData.NumberOfHealthyAdults) ?? "");
			this._childCount.text = (NumberFormatter.Format(populationData.NumberOfHealthyChildren) ?? "");
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002390 File Offset: 0x00000590
		public void UpdateBotCount(PopulationData populationData)
		{
			bool botCreated = this._populationService.BotCreated;
			this._botIcon.ToggleDisplayStyle(botCreated);
			this._botCount.ToggleDisplayStyle(botCreated);
			this._botCount.text = (NumberFormatter.Format(populationData.NumberOfBots) ?? "");
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023E0 File Offset: 0x000005E0
		public void UpdateContaminatedCount(PopulationData populationData)
		{
			int num = populationData.ContaminationData.ContaminatedAdults + populationData.ContaminationData.ContaminatedChildren;
			bool visible = num > 0;
			this._contaminatedIcon.ToggleDisplayStyle(visible);
			this._contaminatedCount.ToggleDisplayStyle(visible);
			this._contaminatedCount.text = (NumberFormatter.Format(num) ?? "");
		}

		// Token: 0x04000012 RID: 18
		public readonly PopulationService _populationService;

		// Token: 0x04000013 RID: 19
		public readonly Label _adultCount;

		// Token: 0x04000014 RID: 20
		public readonly Label _childCount;

		// Token: 0x04000015 RID: 21
		public readonly Label _botCount;

		// Token: 0x04000016 RID: 22
		public readonly Label _contaminatedCount;

		// Token: 0x04000017 RID: 23
		public readonly VisualElement _botIcon;

		// Token: 0x04000018 RID: 24
		public readonly VisualElement _contaminatedIcon;

		// Token: 0x04000019 RID: 25
		public readonly Func<PopulationData> _populationDataGetter;
	}
}
