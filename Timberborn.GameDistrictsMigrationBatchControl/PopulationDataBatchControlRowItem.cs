using System;
using Timberborn.BatchControl;
using Timberborn.GameDistricts;
using Timberborn.Population;
using Timberborn.PopulationUI;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x0200001A RID: 26
	public class PopulationDataBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000347F File Offset: 0x0000167F
		public VisualElement Root { get; }

		// Token: 0x0600007B RID: 123 RVA: 0x00003487 File Offset: 0x00001687
		public PopulationDataBatchControlRowItem(PopulationDataCollector populationDataCollector, VisualElement root, DistrictCenter districtCenter, PopulationData populationData, IPopulationRow populationRow)
		{
			this._populationDataCollector = populationDataCollector;
			this.Root = root;
			this._districtCenter = districtCenter;
			this._populationData = populationData;
			this._populationRow = populationRow;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000034B4 File Offset: 0x000016B4
		public void UpdateRowItem()
		{
			if (this._populationDataCollector.CollectData(this._districtCenter, this._populationData))
			{
				this._populationRow.UpdateData();
			}
		}

		// Token: 0x0400006A RID: 106
		public readonly PopulationDataCollector _populationDataCollector;

		// Token: 0x0400006B RID: 107
		public readonly DistrictCenter _districtCenter;

		// Token: 0x0400006C RID: 108
		public readonly PopulationData _populationData;

		// Token: 0x0400006D RID: 109
		public readonly IPopulationRow _populationRow;
	}
}
