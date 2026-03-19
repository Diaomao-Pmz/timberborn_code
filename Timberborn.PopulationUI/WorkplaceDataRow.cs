using System;
using Timberborn.Population;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.PopulationUI
{
	// Token: 0x0200000E RID: 14
	public class WorkplaceDataRow : IPopulationRow
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002A05 File Offset: 0x00000C05
		public WorkplaceDataRow(Label occupiedWorkslotCount, Label freeWorkslotCount, Label unemployedCount, Func<WorkplaceData> workplaceDataGetter)
		{
			this._occupiedWorkslotCount = occupiedWorkslotCount;
			this._freeWorkslotCount = freeWorkslotCount;
			this._unemployedCount = unemployedCount;
			this._workplaceDataGetter = workplaceDataGetter;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A2C File Offset: 0x00000C2C
		public void UpdateData()
		{
			WorkplaceData workplaceData = this._workplaceDataGetter();
			this._occupiedWorkslotCount.text = (NumberFormatter.Format(workplaceData.OccupiedWorkslots) ?? "");
			this._freeWorkslotCount.text = (NumberFormatter.Format(workplaceData.FreeWorkslots) ?? "");
			this._unemployedCount.text = (NumberFormatter.Format(workplaceData.Unemployed) ?? "");
		}

		// Token: 0x04000038 RID: 56
		public readonly Label _occupiedWorkslotCount;

		// Token: 0x04000039 RID: 57
		public readonly Label _freeWorkslotCount;

		// Token: 0x0400003A RID: 58
		public readonly Label _unemployedCount;

		// Token: 0x0400003B RID: 59
		public readonly Func<WorkplaceData> _workplaceDataGetter;
	}
}
