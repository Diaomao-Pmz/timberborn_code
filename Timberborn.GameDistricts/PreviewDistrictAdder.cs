using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200002D RID: 45
	public class PreviewDistrictAdder : BaseComponent, IAwakableComponent, IPreviewServiceMember, IPreviewSelectionListener
	{
		// Token: 0x0600011F RID: 287 RVA: 0x000047F2 File Offset: 0x000029F2
		public PreviewDistrictAdder(IDistrictService districtService)
		{
			this._districtService = districtService;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004801 File Offset: 0x00002A01
		public void Awake()
		{
			this._districtCenter = base.GetComponent<DistrictCenter>();
			this._preview = base.GetComponent<Preview>();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000481C File Offset: 0x00002A1C
		public void OnPreviewSelect()
		{
			if (this._preview.PreviewState.IsBuildable)
			{
				this.AddToPreviewDistrict();
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004844 File Offset: 0x00002A44
		public void OnPreviewUnselect()
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004844 File Offset: 0x00002A44
		public void AddToPreviewService()
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004846 File Offset: 0x00002A46
		public void RemoveFromPreviewService()
		{
			this.RemoveFromDistrict();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004850 File Offset: 0x00002A50
		public void AddToPreviewDistrict()
		{
			Vector3Int centerCoordinates = this._districtCenter.CenterCoordinates;
			this._district = this._districtService.AddPreviewDistrict(centerCoordinates);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000487B File Offset: 0x00002A7B
		public void RemoveFromDistrict()
		{
			if (this._district != null)
			{
				this._districtService.RemovePreviewDistrict(this._district);
				this._district = null;
			}
		}

		// Token: 0x04000068 RID: 104
		public readonly IDistrictService _districtService;

		// Token: 0x04000069 RID: 105
		public DistrictCenter _districtCenter;

		// Token: 0x0400006A RID: 106
		public Preview _preview;

		// Token: 0x0400006B RID: 107
		public District _district;
	}
}
