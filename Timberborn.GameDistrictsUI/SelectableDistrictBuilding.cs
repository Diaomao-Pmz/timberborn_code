using System;
using Timberborn.BaseComponentSystem;
using Timberborn.GameDistricts;
using Timberborn.SelectionSystem;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x0200001B RID: 27
	public class SelectableDistrictBuilding : BaseComponent, IAwakableComponent, ISelectionListener
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00003E12 File Offset: 0x00002012
		public SelectableDistrictBuilding(DistrictContextService districtContextService)
		{
			this._districtContextService = districtContextService;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003E21 File Offset: 0x00002021
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003E2F File Offset: 0x0000202F
		public void OnSelect()
		{
			this.UpdateDistrictSelection();
			this._districtBuilding.ReassignedInstantDistrict += this.OnReassignedInstantDistrict;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003E4E File Offset: 0x0000204E
		public void OnUnselect()
		{
			this._districtContextService.UnselectDistrict();
			this._districtBuilding.ReassignedInstantDistrict -= this.OnReassignedInstantDistrict;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003E72 File Offset: 0x00002072
		public void OnReassignedInstantDistrict(object sender, EventArgs e)
		{
			this.UpdateDistrictSelection();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003E7C File Offset: 0x0000207C
		public void UpdateDistrictSelection()
		{
			DistrictCenter instantOrConstructionDistrict = this._districtBuilding.GetInstantOrConstructionDistrict();
			if (instantOrConstructionDistrict != null)
			{
				this._districtContextService.SelectDistrict(instantOrConstructionDistrict);
				return;
			}
			this._districtContextService.UnselectDistrict();
		}

		// Token: 0x0400006C RID: 108
		public readonly DistrictContextService _districtContextService;

		// Token: 0x0400006D RID: 109
		public DistrictBuilding _districtBuilding;
	}
}
