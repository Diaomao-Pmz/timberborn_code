using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.Population;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.PopulationUI
{
	// Token: 0x02000009 RID: 9
	public class PopulationDataRowFactory
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002442 File Offset: 0x00000642
		public PopulationDataRowFactory(ILoc loc, PopulationService populationService, ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._loc = loc;
			this._populationService = populationService;
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002468 File Offset: 0x00000668
		public PopulationDataRow Create(VisualElement root, Func<PopulationData> populationDataGetter)
		{
			string elementName = "Game/Population/PopulationDataRow";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			root.Add(visualElement);
			this._tooltipRegistrar.Register(visualElement, () => this.GetPopulationTooltip(populationDataGetter));
			Label adultCount = UQueryExtensions.Q<Label>(visualElement, "AdultCount", null);
			Label childCount = UQueryExtensions.Q<Label>(visualElement, "ChildCount", null);
			Label botCount = UQueryExtensions.Q<Label>(visualElement, "BotCount", null);
			Label contaminatedCount = UQueryExtensions.Q<Label>(visualElement, "ContaminatedCount", null);
			VisualElement botIcon = UQueryExtensions.Q<VisualElement>(visualElement, "BotIcon", null);
			VisualElement contaminatedIcon = UQueryExtensions.Q<VisualElement>(visualElement, "ContaminatedIcon", null);
			return new PopulationDataRow(this._populationService, adultCount, childCount, botCount, contaminatedCount, botIcon, contaminatedIcon, populationDataGetter);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000252C File Offset: 0x0000072C
		public string GetPopulationTooltip(Func<PopulationData> populationDataGetter)
		{
			PopulationData populationData = populationDataGetter();
			ContaminationData contaminationData = populationData.ContaminationData;
			string text = this._populationService.BotCreated ? string.Format("\n{0}: {1}", this._loc.T(PopulationDataRowFactory.BotsLocKey), populationData.NumberOfBots) : "";
			string text2 = (contaminationData.ContaminatedAdults > 0) ? string.Format("\n{0}: {1}", this._loc.T(PopulationDataRowFactory.ContaminatedAdultsLocKey), contaminationData.ContaminatedAdults) : "";
			string text3 = (contaminationData.ContaminatedChildren > 0) ? string.Format("\n{0}: {1}", this._loc.T(PopulationDataRowFactory.ContaminatedChildrenLocKey), contaminationData.ContaminatedChildren) : "";
			return string.Concat(new string[]
			{
				string.Format("{0}: {1}", this._loc.T(PopulationDataRowFactory.AdultsLocKey), populationData.NumberOfHealthyAdults),
				string.Format("\n{0}: {1}", this._loc.T(PopulationDataRowFactory.ChildrenLocKey), populationData.NumberOfHealthyChildren),
				text,
				text2,
				text3
			});
		}

		// Token: 0x0400001A RID: 26
		public static readonly string AdultsLocKey = "Beaver.Population.Adults";

		// Token: 0x0400001B RID: 27
		public static readonly string ChildrenLocKey = "Beaver.Population.Children";

		// Token: 0x0400001C RID: 28
		public static readonly string BotsLocKey = "Bot.PluralDisplayName";

		// Token: 0x0400001D RID: 29
		public static readonly string ContaminatedAdultsLocKey = "Beaver.Population.ContaminatedAdults";

		// Token: 0x0400001E RID: 30
		public static readonly string ContaminatedChildrenLocKey = "Beaver.Population.ContaminatedChildren";

		// Token: 0x0400001F RID: 31
		public readonly ILoc _loc;

		// Token: 0x04000020 RID: 32
		public readonly PopulationService _populationService;

		// Token: 0x04000021 RID: 33
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000022 RID: 34
		public readonly VisualElementLoader _visualElementLoader;
	}
}
