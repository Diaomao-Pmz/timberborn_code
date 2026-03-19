using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.GameDistrictsMigration;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x0200001F RID: 31
	public class PopulationDistributorBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003692 File Offset: 0x00001892
		public VisualElement Root { get; }

		// Token: 0x0600008A RID: 138 RVA: 0x0000369A File Offset: 0x0000189A
		public PopulationDistributorBatchControlRowItem(VisualElement root, IntegerField minimum, AlternateClickable decreaseMinimum, AlternateClickable increaseMinimum, VisualElement needingIcon, PopulationDistributor populationDistributor)
		{
			this.Root = root;
			this._minimum = minimum;
			this._decreaseMinimum = decreaseMinimum;
			this._increaseMinimum = increaseMinimum;
			this._needingIcon = needingIcon;
			this._populationDistributor = populationDistributor;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000036D0 File Offset: 0x000018D0
		public void UpdateRowItem()
		{
			if (!this._minimum.IsFocused())
			{
				this._minimum.SetValueWithoutNotify(this._populationDistributor.Minimum);
			}
			this._decreaseMinimum.Root.SetEnabled(this._populationDistributor.Minimum > 0);
			this._decreaseMinimum.Update();
			this._increaseMinimum.Update();
			this._needingIcon.ToggleDisplayStyle(this._populationDistributor.CanImmigrate);
		}

		// Token: 0x04000076 RID: 118
		public readonly IntegerField _minimum;

		// Token: 0x04000077 RID: 119
		public readonly AlternateClickable _decreaseMinimum;

		// Token: 0x04000078 RID: 120
		public readonly AlternateClickable _increaseMinimum;

		// Token: 0x04000079 RID: 121
		public readonly VisualElement _needingIcon;

		// Token: 0x0400007A RID: 122
		public readonly PopulationDistributor _populationDistributor;
	}
}
