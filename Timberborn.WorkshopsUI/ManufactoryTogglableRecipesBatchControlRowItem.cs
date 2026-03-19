using System;
using Timberborn.BatchControl;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x02000015 RID: 21
	public class ManufactoryTogglableRecipesBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000031CB File Offset: 0x000013CB
		public VisualElement Root { get; }

		// Token: 0x0600006B RID: 107 RVA: 0x000031D3 File Offset: 0x000013D3
		public ManufactoryTogglableRecipesBatchControlRowItem(VisualElement root, SliderToggle sliderToggle)
		{
			this.Root = root;
			this._sliderToggle = sliderToggle;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000031E9 File Offset: 0x000013E9
		public void UpdateRowItem()
		{
			this._sliderToggle.Update();
		}

		// Token: 0x0400005A RID: 90
		public readonly SliderToggle _sliderToggle;
	}
}
