using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.Population;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.PopulationUI
{
	// Token: 0x02000005 RID: 5
	public class HousingDataRowFactory
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002166 File Offset: 0x00000366
		public HousingDataRowFactory(ILoc loc, ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._loc = loc;
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002184 File Offset: 0x00000384
		public HousingDataRow Create(VisualElement root, Func<PopulationData> populationDataGetter)
		{
			string elementName = "Game/Population/HousingDataRow";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			root.Add(visualElement);
			this._tooltipRegistrar.Register(visualElement, () => this.GetHousingTooltip(populationDataGetter));
			Label occupiedBedCount = UQueryExtensions.Q<Label>(visualElement, "OccupiedBedCount", null);
			Label freeBedCount = UQueryExtensions.Q<Label>(visualElement, "FreeBedCount", null);
			Label homelessCount = UQueryExtensions.Q<Label>(visualElement, "HomelessCount", null);
			return new HousingDataRow(occupiedBedCount, freeBedCount, homelessCount, populationDataGetter);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000220C File Offset: 0x0000040C
		public string GetHousingTooltip(Func<PopulationData> populationDataGetter)
		{
			BedData bedData = populationDataGetter().BedData;
			return string.Format("{0}: {1}", this._loc.T(HousingDataRowFactory.OccupiedBedsLocKey), bedData.OccupiedBeds) + string.Format("\n{0}: {1}", this._loc.T(HousingDataRowFactory.FreeBedsLocKey), bedData.FreeBeds) + string.Format("\n{0}: {1}", this._loc.T(HousingDataRowFactory.HomelessLocKey), bedData.Homeless);
		}

		// Token: 0x0400000A RID: 10
		public static readonly string OccupiedBedsLocKey = "Dwellings.OccupiedBeds";

		// Token: 0x0400000B RID: 11
		public static readonly string FreeBedsLocKey = "Dwellings.FreeBeds";

		// Token: 0x0400000C RID: 12
		public static readonly string HomelessLocKey = "Beaver.HomelessPlural";

		// Token: 0x0400000D RID: 13
		public readonly ILoc _loc;

		// Token: 0x0400000E RID: 14
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000F RID: 15
		public readonly VisualElementLoader _visualElementLoader;
	}
}
