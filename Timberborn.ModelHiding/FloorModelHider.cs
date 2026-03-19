using System;
using System.Collections.Generic;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.ConstructionMode;
using Timberborn.LevelVisibilitySystem;
using UnityEngine;

namespace Timberborn.ModelHiding
{
	// Token: 0x02000008 RID: 8
	public class FloorModelHider
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021A3 File Offset: 0x000003A3
		public FloorModelHider(ILevelVisibilityService levelVisibilityService, IBlockService blockService, ConstructionModeService constructionModeService, HidableModels hidableModels)
		{
			this._levelVisibilityService = levelVisibilityService;
			this._blockService = blockService;
			this._constructionModeService = constructionModeService;
			this._hidableModels = hidableModels;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021C8 File Offset: 0x000003C8
		public void UpdateVisibility(int minLevel, int maxLevel, ICollection<BlockObjectModelController> modelsToUnblock)
		{
			for (int i = minLevel; i <= maxLevel; i++)
			{
				foreach (BlockObjectModelController blockObjectModelController in this._hidableModels.ModelsAt(i))
				{
					if (this.IsValidFloor(blockObjectModelController))
					{
						if (this.CanShowUncoveredFloor(blockObjectModelController, modelsToUnblock))
						{
							blockObjectModelController.ShowUncoveredModel();
							modelsToUnblock.Add(blockObjectModelController);
						}
						else
						{
							blockObjectModelController.HideUncoveredModel();
						}
					}
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002254 File Offset: 0x00000454
		public void ShowModelIfPossible(BlockObjectModelController model)
		{
			if (this.IsValidFloor(model) && this.CanShowUncoveredFloor(model, FloorModelHider.EmptyModelsToUnblock))
			{
				model.UnblockModel();
				model.ShowUncoveredModel();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002279 File Offset: 0x00000479
		public bool IsValidFloor(BlockObjectModelController model)
		{
			return model.HasUncoveredModel && this._levelVisibilityService.BlockIsVisible(model.BlockObject.CoordinatesAtBaseZ.Below()) && model.BlockObject.IsFloor();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022B0 File Offset: 0x000004B0
		public bool CanShowUncoveredFloor(BlockObjectModelController model, ICollection<BlockObjectModelController> modelsToUnblock)
		{
			return (model.BlockObject.IsUnfinished && !this._constructionModeService.InConstructionMode) || (model.BlockObject.GetTopLevel() > this._levelVisibilityService.MaxVisibleLevel && this.HasFullyShownBlockObjectBelow(model, modelsToUnblock));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002300 File Offset: 0x00000500
		public bool HasFullyShownBlockObjectBelow(BlockObjectModelController model, ICollection<BlockObjectModelController> modelsToUnblock)
		{
			Vector3Int coordinates = model.BlockObject.CoordinatesAtBaseZ.Below();
			foreach (BlockObject blockObject in this._blockService.GetObjectsAt(coordinates))
			{
				if (!blockObject.IsFloor() && !FloorModelHider.IsFullyShown(blockObject, modelsToUnblock))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002380 File Offset: 0x00000580
		public static bool IsFullyShown(BlockObject blockObject, ICollection<BlockObjectModelController> modelsToUnblock)
		{
			BlockObjectModelController component = blockObject.GetComponent<BlockObjectModelController>();
			return !component || (!component.IsUncoveredModelShown && component.IsAnyModelShown) || (!component.ShouldShowUncoveredModel && modelsToUnblock.Contains(component));
		}

		// Token: 0x04000008 RID: 8
		public static readonly List<BlockObjectModelController> EmptyModelsToUnblock = new List<BlockObjectModelController>();

		// Token: 0x04000009 RID: 9
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x0400000A RID: 10
		public readonly IBlockService _blockService;

		// Token: 0x0400000B RID: 11
		public readonly ConstructionModeService _constructionModeService;

		// Token: 0x0400000C RID: 12
		public readonly HidableModels _hidableModels;
	}
}
