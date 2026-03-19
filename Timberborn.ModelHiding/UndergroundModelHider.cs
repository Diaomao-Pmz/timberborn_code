using System;
using System.Collections.Generic;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.ModelHiding
{
	// Token: 0x02000010 RID: 16
	public class UndergroundModelHider
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002BC1 File Offset: 0x00000DC1
		public UndergroundModelHider(ITerrainService terrainService, ILevelVisibilityService levelVisibilityService, HidableModels hidableModels)
		{
			this._terrainService = terrainService;
			this._levelVisibilityService = levelVisibilityService;
			this._hidableModels = hidableModels;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public void UpdateVisibility(int minLevel, int maxLevel, ICollection<BlockObjectModelController> modelsToUnblock)
		{
			for (int i = minLevel; i <= maxLevel; i++)
			{
				foreach (BlockObjectModelController blockObjectModelController in this._hidableModels.ModelsAt(i))
				{
					if (blockObjectModelController.HasUndergroundModel)
					{
						if (this.CanShowUndergroundModel(blockObjectModelController))
						{
							this.ShowUndergroundModel(blockObjectModelController);
							modelsToUnblock.Add(blockObjectModelController);
						}
						else
						{
							blockObjectModelController.HideUndergroundModel();
						}
					}
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C68 File Offset: 0x00000E68
		public void ShowModelIfPossible(BlockObjectModelController model)
		{
			if (model.HasUndergroundModel && this.CanShowUndergroundModel(model))
			{
				model.UnblockModel();
				this.ShowUndergroundModel(model);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C88 File Offset: 0x00000E88
		public bool CanShowUndergroundModel(BlockObjectModelController model)
		{
			BlockObject blockObject = model.BlockObject;
			int maxVisibleLevel = this._levelVisibilityService.MaxVisibleLevel;
			if (model.UndergroundBaseZ <= maxVisibleLevel && blockObject.CoordinatesAtBaseZ.z > maxVisibleLevel)
			{
				foreach (Vector3Int vector3Int in blockObject.PositionedBlocks.GetFoundationCoordinates())
				{
					if (!this._terrainService.Underground(new Vector3Int(vector3Int.x, vector3Int.y, maxVisibleLevel)))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D30 File Offset: 0x00000F30
		public void ShowUndergroundModel(BlockObjectModelController model)
		{
			model.ShowUndergroundModel();
			model.SetUndergroundModelZOffset(this._levelVisibilityService.MaxVisibleLevel - model.BlockObject.CoordinatesAtBaseZ.z + 1);
		}

		// Token: 0x0400001B RID: 27
		public readonly ITerrainService _terrainService;

		// Token: 0x0400001C RID: 28
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x0400001D RID: 29
		public readonly HidableModels _hidableModels;
	}
}
