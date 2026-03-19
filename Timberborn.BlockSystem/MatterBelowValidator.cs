using System;
using Timberborn.Common;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200004F RID: 79
	public class MatterBelowValidator
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x000063DA File Offset: 0x000045DA
		public MatterBelowValidator(IBlockService blockService, ITerrainService terrainService, StackableBlockService stackableBlockService)
		{
			this._blockService = blockService;
			this._terrainService = terrainService;
			this._stackableBlockService = stackableBlockService;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000063F7 File Offset: 0x000045F7
		public bool Validate(in Block block)
		{
			return this.Validate(block, false);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006401 File Offset: 0x00004601
		public bool ValidateIgnoringUnfinishedStackable(in Block block)
		{
			return this.Validate(block, true);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000640C File Offset: 0x0000460C
		public bool Validate(in Block block, bool ignoreUnfinishedStackable)
		{
			MatterBelow matterBelow = block.MatterBelow;
			Vector3Int coordinates = block.Coordinates;
			bool result;
			switch (matterBelow)
			{
			case MatterBelow.Ground:
				result = (this.AtGroundLevel(coordinates) || (!ignoreUnfinishedStackable && this.UnfinishedGroundBelow(coordinates)) || (block.Underground && this._terrainService.Underground(coordinates.Below())));
				break;
			case MatterBelow.GroundOrStackable:
				result = (this.AtGroundLevel(coordinates) || (block.Underground && this._terrainService.Underground(coordinates.Below())) || this.StackableBelow(coordinates, ignoreUnfinishedStackable));
				break;
			case MatterBelow.Air:
				result = (this.AboveGround(coordinates) && !this.TopBlockBelow(coordinates));
				break;
			case MatterBelow.Any:
				result = true;
				break;
			case MatterBelow.Stackable:
				result = this.StackableBelow(coordinates, ignoreUnfinishedStackable);
				break;
			default:
				throw new NotSupportedException(matterBelow.ToString());
			}
			return result;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000064F1 File Offset: 0x000046F1
		public bool AtGroundLevel(Vector3Int coordinates)
		{
			return !this._terrainService.Underground(coordinates) && this._terrainService.Underground(coordinates.Below());
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006514 File Offset: 0x00004714
		public bool AboveGround(Vector3Int coordinates)
		{
			return !this._terrainService.Underground(coordinates) && !this._terrainService.Underground(coordinates.Below());
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000653A File Offset: 0x0000473A
		public bool TopBlockBelow(Vector3Int coordinates)
		{
			return this._blockService.AnyNonOverridableObjectsAt(coordinates.Below(), BlockOccupations.Top);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006550 File Offset: 0x00004750
		public bool StackableBelow(Vector3Int coordinates, bool ignoreUnfinishedStackable)
		{
			Vector3Int coords = coordinates - new Vector3Int(0, 0, 1);
			if (!ignoreUnfinishedStackable)
			{
				return this._stackableBlockService.IsStackableBlockAt(coords, false);
			}
			return this._stackableBlockService.IsFinishedStackableBlockAt(coords);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000658C File Offset: 0x0000478C
		public bool UnfinishedGroundBelow(Vector3Int coordinates)
		{
			Vector3Int coords = coordinates - new Vector3Int(0, 0, 1);
			return this._stackableBlockService.IsUnfinishedGroundBlockAt(coords);
		}

		// Token: 0x040000E9 RID: 233
		public readonly IBlockService _blockService;

		// Token: 0x040000EA RID: 234
		public readonly ITerrainService _terrainService;

		// Token: 0x040000EB RID: 235
		public readonly StackableBlockService _stackableBlockService;
	}
}
