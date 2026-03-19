using System;
using Timberborn.BatchControl;
using Timberborn.GameDistrictsMigration;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x02000004 RID: 4
	public class CurrentPopulationBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public CurrentPopulationBatchControlRowItem(VisualElement root, Label currentPopulationLabel, PopulationDistributor populationDistributor)
		{
			this.Root = root;
			this._currentPopulationLabel = currentPopulationLabel;
			this._populationDistributor = populationDistributor;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E8 File Offset: 0x000002E8
		public void UpdateRowItem()
		{
			TextElement currentPopulationLabel = this._currentPopulationLabel;
			int num = this._populationDistributor.Current;
			currentPopulationLabel.text = num.ToString();
		}

		// Token: 0x04000007 RID: 7
		public readonly Label _currentPopulationLabel;

		// Token: 0x04000008 RID: 8
		public readonly PopulationDistributor _populationDistributor;
	}
}
