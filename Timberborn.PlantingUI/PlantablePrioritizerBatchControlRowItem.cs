using System;
using Timberborn.BatchControl;
using Timberborn.DropdownSystem;
using Timberborn.Planting;
using UnityEngine.UIElements;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000010 RID: 16
	public class PlantablePrioritizerBatchControlRowItem : IBatchControlRowItem, IClearableBatchControlRowItem
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002C7C File Offset: 0x00000E7C
		public VisualElement Root { get; }

		// Token: 0x0600004D RID: 77 RVA: 0x00002C84 File Offset: 0x00000E84
		public PlantablePrioritizerBatchControlRowItem(VisualElement root, Dropdown dropdown, PlantablePrioritizer plantablePrioritizer)
		{
			this.Root = root;
			this._dropdown = dropdown;
			this._plantablePrioritizer = plantablePrioritizer;
			this._plantablePrioritizer.PrioritizedPlantableChanged += this.OnPrioritizedPlantableChanged;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public void ClearRowItem()
		{
			this._plantablePrioritizer.PrioritizedPlantableChanged -= this.OnPrioritizedPlantableChanged;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CD1 File Offset: 0x00000ED1
		public void OnPrioritizedPlantableChanged(object sender, EventArgs e)
		{
			this._dropdown.UpdateSelectedValue();
		}

		// Token: 0x04000031 RID: 49
		public readonly Dropdown _dropdown;

		// Token: 0x04000032 RID: 50
		public readonly PlantablePrioritizer _plantablePrioritizer;
	}
}
