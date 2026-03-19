using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.GameDistricts;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x0200001A RID: 26
	public class PreviewDistrictObstacle : BaseComponent, IAwakableComponent, IPreviewServiceMember
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003DCC File Offset: 0x00001FCC
		public void Awake()
		{
			this._districtObstacle = base.GetComponent<DistrictObstacle>();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003DDA File Offset: 0x00001FDA
		public void AddToPreviewService()
		{
			if (!this._isAdded)
			{
				this._districtObstacle.AddToPreviewDistricts();
				this._isAdded = true;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public void RemoveFromPreviewService()
		{
			if (this._isAdded)
			{
				this._districtObstacle.RemoveFromPreviewDistricts();
				this._isAdded = false;
			}
		}

		// Token: 0x0400006A RID: 106
		public DistrictObstacle _districtObstacle;

		// Token: 0x0400006B RID: 107
		public bool _isAdded;
	}
}
