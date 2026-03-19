using System;
using Timberborn.BlockObjectPickingSystem;

namespace Timberborn.AreaSelectionSystem
{
	// Token: 0x02000010 RID: 16
	public class AreaBlockObjectPickerFactory
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002677 File Offset: 0x00000877
		public AreaBlockObjectPickerFactory(AreaSelectionController areaSelectionController, AreaSelector areaSelector, BlockObjectPicker blockObjectPicker)
		{
			this._areaSelectionController = areaSelectionController;
			this._areaSelector = areaSelector;
			this._blockObjectPicker = blockObjectPicker;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002694 File Offset: 0x00000894
		public AreaBlockObjectPicker CreatePickingUpwards()
		{
			return this.Create(BlockObjectPickingMode.UpwardStack);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000269D File Offset: 0x0000089D
		public AreaBlockObjectPicker CreatePickingDownwards()
		{
			return this.Create(BlockObjectPickingMode.DownwardStack);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026A6 File Offset: 0x000008A6
		public AreaBlockObjectPicker Create(BlockObjectPickingMode pickingMode)
		{
			return new AreaBlockObjectPicker(this._areaSelectionController, this._areaSelector, this._blockObjectPicker, pickingMode);
		}

		// Token: 0x04000025 RID: 37
		public readonly AreaSelectionController _areaSelectionController;

		// Token: 0x04000026 RID: 38
		public readonly AreaSelector _areaSelector;

		// Token: 0x04000027 RID: 39
		public readonly BlockObjectPicker _blockObjectPicker;
	}
}
