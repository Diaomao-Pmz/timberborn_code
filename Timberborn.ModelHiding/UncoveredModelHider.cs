using System;
using Timberborn.BlockObjectModelSystem;
using Timberborn.LevelVisibilitySystem;

namespace Timberborn.ModelHiding
{
	// Token: 0x0200000F RID: 15
	public class UncoveredModelHider
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002ADC File Offset: 0x00000CDC
		public UncoveredModelHider(ILevelVisibilityService levelVisibilityService, HidableModels hidableModels)
		{
			this._levelVisibilityService = levelVisibilityService;
			this._hidableModels = hidableModels;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public void UpdateVisibility(int minLevel, int maxLevel)
		{
			int maxVisibleLevel = this._levelVisibilityService.MaxVisibleLevel;
			for (int i = minLevel; i <= maxLevel; i++)
			{
				foreach (BlockObjectModelController blockObjectModelController in this._hidableModels.ModelsAt(i))
				{
					if (blockObjectModelController.HasUncoveredModel)
					{
						if (blockObjectModelController.BlockObject.GetTopLevel() > maxVisibleLevel)
						{
							blockObjectModelController.ShowUncoveredModel();
						}
						else
						{
							blockObjectModelController.HideUncoveredModel();
						}
					}
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B8C File Offset: 0x00000D8C
		public void ShowModelIfPossible(BlockObjectModelController model)
		{
			if (model.HasUncoveredModel)
			{
				int maxVisibleLevel = this._levelVisibilityService.MaxVisibleLevel;
				if (model.BlockObject.GetTopLevel() > maxVisibleLevel)
				{
					model.ShowUncoveredModel();
				}
			}
		}

		// Token: 0x04000019 RID: 25
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x0400001A RID: 26
		public readonly HidableModels _hidableModels;
	}
}
