using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.Reproduction;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.ReproductionUI
{
	// Token: 0x02000004 RID: 4
	public class BreedingPodBatchControlRowItem : IUpdatableBatchControlRowItem, IBatchControlRowItem, IFinishableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public BreedingPodBatchControlRowItem(VisualElement root, BreedingPod breedingPod, Label progressLabel)
		{
			this.Root = root;
			this._breedingPod = breedingPod;
			this._progressLabel = progressLabel;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E3 File Offset: 0x000002E3
		public void UpdateRowItem()
		{
			this._progressLabel.text = NumberFormatter.FormatAsPercentFloored((double)this._breedingPod.CalculateProgress());
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002101 File Offset: 0x00000301
		public void SetFinishedState(bool isFinished)
		{
			this.Root.ToggleDisplayStyle(isFinished);
		}

		// Token: 0x04000007 RID: 7
		public readonly BreedingPod _breedingPod;

		// Token: 0x04000008 RID: 8
		public readonly Label _progressLabel;
	}
}
