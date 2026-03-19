using System;
using Timberborn.BlockSystem;
using Timberborn.Localization;

namespace Timberborn.TerrainLevelValidation
{
	// Token: 0x0200000B RID: 11
	public class ContinuousTerrainConstraintValidator : IBlockObjectValidator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002354 File Offset: 0x00000554
		public ContinuousTerrainConstraintValidator(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002364 File Offset: 0x00000564
		public bool IsValid(BlockObject blockObject, out string errorMessage)
		{
			ContinuousTerrainConstraint component = blockObject.GetComponent<ContinuousTerrainConstraint>();
			if (component != null && component.IsNotOnFirstColumnOfTerrain())
			{
				errorMessage = this._loc.T(ContinuousTerrainConstraintValidator.ErrorMessageLocKey);
				return false;
			}
			errorMessage = null;
			return true;
		}

		// Token: 0x0400000F RID: 15
		public static readonly string ErrorMessageLocKey = "Buildings.ContinuousTerrainConstraint";

		// Token: 0x04000010 RID: 16
		public readonly ILoc _loc;
	}
}
