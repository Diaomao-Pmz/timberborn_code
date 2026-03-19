using System;
using Timberborn.BlockSystem;
using Timberborn.GameDistricts;
using Timberborn.Localization;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x02000017 RID: 23
	public class DistrictPreviewsValidator : IBlockObjectValidator
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00003BE2 File Offset: 0x00001DE2
		public DistrictPreviewsValidator(IDistrictService districtService, ILoc loc)
		{
			this._districtService = districtService;
			this._loc = loc;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public bool IsValid(BlockObject blockObject, out string errorMessage)
		{
			if (this.IsPreviewDistrictInConflict(blockObject))
			{
				errorMessage = this._loc.T(DistrictPreviewsValidator.ErrorMessageLocKey);
				return false;
			}
			errorMessage = null;
			return true;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003C1C File Offset: 0x00001E1C
		public bool IsPreviewDistrictInConflict(BlockObject blockObject)
		{
			if (blockObject.IsPreview)
			{
				DistrictCenter component = blockObject.GetComponent<DistrictCenter>();
				Vector3Int? previewDistrictCenter = (component != null) ? new Vector3Int?(component.CenterCoordinates) : null;
				return this._districtService.IsPreviewDistrictInConflict(previewDistrictCenter);
			}
			return false;
		}

		// Token: 0x04000066 RID: 102
		public static readonly string ErrorMessageLocKey = "BuildingTools.DistrictsInConflict";

		// Token: 0x04000067 RID: 103
		public readonly IDistrictService _districtService;

		// Token: 0x04000068 RID: 104
		public readonly ILoc _loc;
	}
}
