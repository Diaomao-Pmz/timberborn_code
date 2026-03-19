using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.Population;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.PopulationUI
{
	// Token: 0x0200000F RID: 15
	public class WorkplaceDataRowFactory
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002AA5 File Offset: 0x00000CA5
		public WorkplaceDataRowFactory(ILoc loc, ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._loc = loc;
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public WorkplaceDataRow CreateBeaverWorkplaceDataRow(VisualElement root, Func<PopulationData> populationDataGetter)
		{
			return this.Create(root, "Game/Population/BeaverWorkplaceDataRow", () => populationDataGetter().BeaverWorkplaceData, () => populationDataGetter().BeaverWorkforceData, WorkplaceDataRowFactory.BeaversLocKey);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B08 File Offset: 0x00000D08
		public WorkplaceDataRow CreateBotWorkplaceDataRow(VisualElement root, Func<PopulationData> populationDataGetter)
		{
			return this.Create(root, "Game/Population/BotWorkplaceDataRow", () => populationDataGetter().BotWorkplaceData, () => populationDataGetter().BotWorkforceData, WorkplaceDataRowFactory.BotsLocKey);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B4C File Offset: 0x00000D4C
		public WorkplaceDataRow Create(VisualElement root, string elementName, Func<WorkplaceData> workplaceDataGetter, Func<WorkforceData> workforceDataGetter, string headerLocKey)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			root.Add(visualElement);
			this._tooltipRegistrar.Register(visualElement, () => this.GetWorkplaceData(workplaceDataGetter(), workforceDataGetter(), headerLocKey));
			Label occupiedWorkslotCount = UQueryExtensions.Q<Label>(root, "OccupiedWorkslotCount", null);
			Label freeWorkslotCount = UQueryExtensions.Q<Label>(root, "FreeWorkslotCount", null);
			Label unemployedCount = UQueryExtensions.Q<Label>(root, "UnemployedCount", null);
			return new WorkplaceDataRow(occupiedWorkslotCount, freeWorkslotCount, unemployedCount, workplaceDataGetter);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002BDC File Offset: 0x00000DDC
		public string GetWorkplaceData(WorkplaceData workplaceData, WorkforceData workforceData, string headerLocKey)
		{
			int unemployable = workforceData.Unemployable;
			string text = (unemployable > 0) ? string.Format("\n{0}: {1}", this._loc.T(WorkplaceDataRowFactory.UnavailableLocKey), unemployable) : "";
			return string.Concat(new string[]
			{
				"<b>",
				this._loc.T(headerLocKey),
				"</b>",
				string.Format("\n{0}: {1}", this._loc.T(WorkplaceDataRowFactory.OccupiedWorkslotsLocKey), workplaceData.OccupiedWorkslots),
				string.Format("\n{0}: {1}", this._loc.T(WorkplaceDataRowFactory.FreeWorkslotsLocKey), workplaceData.FreeWorkslots),
				string.Format("\n{0}: {1}", this._loc.T(WorkplaceDataRowFactory.UnemployedLocKey), workplaceData.Unemployed),
				text
			});
		}

		// Token: 0x0400003C RID: 60
		public static readonly string OccupiedWorkslotsLocKey = "Work.WorkersLabel";

		// Token: 0x0400003D RID: 61
		public static readonly string FreeWorkslotsLocKey = "Work.VacantPlural";

		// Token: 0x0400003E RID: 62
		public static readonly string UnemployedLocKey = "Beaver.UnemployedPlural";

		// Token: 0x0400003F RID: 63
		public static readonly string UnavailableLocKey = "Work.Incapacitated";

		// Token: 0x04000040 RID: 64
		public static readonly string BeaversLocKey = "Beaver.PluralDisplayName";

		// Token: 0x04000041 RID: 65
		public static readonly string BotsLocKey = "Bot.PluralDisplayName";

		// Token: 0x04000042 RID: 66
		public readonly ILoc _loc;

		// Token: 0x04000043 RID: 67
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000044 RID: 68
		public readonly VisualElementLoader _visualElementLoader;
	}
}
