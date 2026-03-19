using System;
using Timberborn.BatchControl;
using UnityEngine.UIElements;

namespace Timberborn.FieldsUI
{
	// Token: 0x02000004 RID: 4
	public class FarmHouseBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BF File Offset: 0x000002BF
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C7 File Offset: 0x000002C7
		public FarmHouseBatchControlRowItem(VisualElement root, FarmHouseToggle farmHouseToggle)
		{
			this.Root = root;
			this._farmHouseToggle = farmHouseToggle;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DD File Offset: 0x000002DD
		public void UpdateRowItem()
		{
			this._farmHouseToggle.Update();
		}

		// Token: 0x04000007 RID: 7
		public readonly FarmHouseToggle _farmHouseToggle;
	}
}
