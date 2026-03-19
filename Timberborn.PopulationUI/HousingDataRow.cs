using System;
using Timberborn.Population;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.PopulationUI
{
	// Token: 0x02000004 RID: 4
	public class HousingDataRow : IPopulationRow
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public HousingDataRow(Label occupiedBedCount, Label freeBedCount, Label homelessCount, Func<PopulationData> populationDataGetter)
		{
			this._occupiedBedCount = occupiedBedCount;
			this._freeBedCount = freeBedCount;
			this._homelessCount = homelessCount;
			this._populationDataGetter = populationDataGetter;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		public void UpdateData()
		{
			BedData bedData = this._populationDataGetter().BedData;
			this._occupiedBedCount.text = (NumberFormatter.Format(bedData.OccupiedBeds) ?? "");
			this._freeBedCount.text = (NumberFormatter.Format(bedData.FreeBeds) ?? "");
			this._homelessCount.text = (NumberFormatter.Format(bedData.Homeless) ?? "");
		}

		// Token: 0x04000006 RID: 6
		public readonly Label _occupiedBedCount;

		// Token: 0x04000007 RID: 7
		public readonly Label _freeBedCount;

		// Token: 0x04000008 RID: 8
		public readonly Label _homelessCount;

		// Token: 0x04000009 RID: 9
		public readonly Func<PopulationData> _populationDataGetter;
	}
}
