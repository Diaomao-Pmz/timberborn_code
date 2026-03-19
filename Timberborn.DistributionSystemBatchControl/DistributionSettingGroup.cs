using System;
using System.Collections.Generic;
using Timberborn.BatchControl;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemBatchControl
{
	// Token: 0x02000007 RID: 7
	public class DistributionSettingGroup : IBatchControlRowItem, IUpdatableBatchControlRowItem, IClearableBatchControlRowItem
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000233B File Offset: 0x0000053B
		public VisualElement Root { get; }

		// Token: 0x06000015 RID: 21 RVA: 0x00002343 File Offset: 0x00000543
		public DistributionSettingGroup(VisualElement root, List<GoodDistributionSettingItem> goodDistributionSettingItems)
		{
			this.Root = root;
			this._goodDistributionSettingItems = goodDistributionSettingItems;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000235C File Offset: 0x0000055C
		public void UpdateRowItem()
		{
			for (int i = 0; i < this._goodDistributionSettingItems.Count; i++)
			{
				this._goodDistributionSettingItems[i].Update();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002390 File Offset: 0x00000590
		public void ClearRowItem()
		{
			for (int i = 0; i < this._goodDistributionSettingItems.Count; i++)
			{
				this._goodDistributionSettingItems[i].Clear();
			}
		}

		// Token: 0x04000013 RID: 19
		public readonly List<GoodDistributionSettingItem> _goodDistributionSettingItems;
	}
}
