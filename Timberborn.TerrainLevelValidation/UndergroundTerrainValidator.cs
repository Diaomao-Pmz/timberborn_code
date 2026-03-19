using System;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Localization;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainLevelValidation
{
	// Token: 0x02000010 RID: 16
	public class UndergroundTerrainValidator : IBlockObjectValidator
	{
		// Token: 0x0600002A RID: 42 RVA: 0x0000259E File Offset: 0x0000079E
		public UndergroundTerrainValidator(IBlockService blockService, ILoc loc, ITerrainService terrainService)
		{
			this._blockService = blockService;
			this._loc = loc;
			this._terrainService = terrainService;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025BC File Offset: 0x000007BC
		public bool IsValid(BlockObject blockObject, out string errorMessage)
		{
			foreach (Block block in blockObject.PositionedBlocks.GetAllBlocks())
			{
				if (block.MatterBelow == MatterBelow.Ground)
				{
					Vector3Int blockBelow = block.Coordinates.Below();
					if (!this._terrainService.Underground(blockBelow) && !this._blockService.GetObjectsAt(blockBelow).FastAny((BlockObject foundObject) => UndergroundTerrainValidator.IsUnfinishedGroundAtPosition(foundObject, blockBelow)))
					{
						errorMessage = this._loc.T(UndergroundTerrainValidator.MissingGroundBelowLocKey);
						return false;
					}
				}
			}
			errorMessage = null;
			return true;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002668 File Offset: 0x00000868
		public static bool IsUnfinishedGroundAtPosition(BlockObject blockObject, Vector3Int position)
		{
			Block block;
			return blockObject.PositionedBlocks.TryGetBlock(position, out block) && block.Stackable == BlockStackable.UnfinishedGround;
		}

		// Token: 0x04000015 RID: 21
		public static readonly string MissingGroundBelowLocKey = "Buildings.MissingGroundBelow";

		// Token: 0x04000016 RID: 22
		public readonly IBlockService _blockService;

		// Token: 0x04000017 RID: 23
		public readonly ILoc _loc;

		// Token: 0x04000018 RID: 24
		public readonly ITerrainService _terrainService;
	}
}
