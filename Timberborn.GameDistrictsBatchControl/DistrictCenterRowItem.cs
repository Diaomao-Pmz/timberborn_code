using System;
using Timberborn.BatchControl;
using Timberborn.GameDistricts;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsBatchControl
{
	// Token: 0x02000004 RID: 4
	public class DistrictCenterRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public DistrictCenterRowItem(VisualElement root, DistrictCenter districtCenter, Label districtNameLabel)
		{
			this.Root = root;
			this._districtCenter = districtCenter;
			this._districtNameLabel = districtNameLabel;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E3 File Offset: 0x000002E3
		public void UpdateRowItem()
		{
			this._districtNameLabel.text = this._districtCenter.DistrictName;
		}

		// Token: 0x04000007 RID: 7
		public readonly DistrictCenter _districtCenter;

		// Token: 0x04000008 RID: 8
		public readonly Label _districtNameLabel;
	}
}
