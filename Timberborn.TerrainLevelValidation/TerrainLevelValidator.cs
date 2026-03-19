using System;
using Timberborn.BlockSystem;
using Timberborn.Localization;
using Timberborn.MapStateSystem;
using UnityEngine;

namespace Timberborn.TerrainLevelValidation
{
	// Token: 0x0200000E RID: 14
	public class TerrainLevelValidator : IBlockObjectValidator
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000244A File Offset: 0x0000064A
		public TerrainLevelValidator(ILoc loc, MapSize mapSize)
		{
			this._loc = loc;
			this._mapSize = mapSize;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002460 File Offset: 0x00000660
		public bool IsValid(BlockObject blockObject, out string errorMessage)
		{
			if (TerrainLevelValidator.IsBottomConstraintDissatisfied(blockObject))
			{
				errorMessage = this._loc.T(TerrainLevelValidator.BottomConstraintLocKey);
				return false;
			}
			if (this.IsTopTerrainConstraintDissatisfied(blockObject) || this.IsTopBlockObjectConstraintDissatisfied(blockObject))
			{
				errorMessage = this._loc.T(TerrainLevelValidator.TopConstraintLocKey);
				return false;
			}
			errorMessage = null;
			return true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024B4 File Offset: 0x000006B4
		public static bool IsBottomConstraintDissatisfied(BlockObject blockObject)
		{
			BottomTerrainLevelValidationConstraint component = blockObject.GetComponent<BottomTerrainLevelValidationConstraint>();
			return component != null && component.BottomLevel <= 0;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024DC File Offset: 0x000006DC
		public bool IsTopTerrainConstraintDissatisfied(BlockObject blockObject)
		{
			return blockObject.GetComponent<TopTerrainLevelValidationConstraint>() != null && blockObject.Coordinates.z >= this._mapSize.MaxGameTerrainHeight;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002514 File Offset: 0x00000714
		public bool IsTopBlockObjectConstraintDissatisfied(BlockObject blockObject)
		{
			int z = this._mapSize.TotalSize.z;
			foreach (Vector3Int vector3Int in blockObject.PositionedBlocks.GetOccupiedCoordinates())
			{
				if (vector3Int.z >= z)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000011 RID: 17
		public static readonly string BottomConstraintLocKey = "Buildings.BottomConstraint";

		// Token: 0x04000012 RID: 18
		public static readonly string TopConstraintLocKey = "Buildings.TopConstraint";

		// Token: 0x04000013 RID: 19
		public readonly ILoc _loc;

		// Token: 0x04000014 RID: 20
		public readonly MapSize _mapSize;
	}
}
