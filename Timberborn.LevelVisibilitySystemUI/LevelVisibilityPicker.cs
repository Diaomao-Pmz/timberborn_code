using System;
using Timberborn.ConstructionMode;
using Timberborn.CursorToolSystem;
using Timberborn.InputSystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.SingletonSystem;

namespace Timberborn.LevelVisibilitySystemUI
{
	// Token: 0x02000008 RID: 8
	public class LevelVisibilityPicker : IInputProcessor, ILoadableSingleton
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000026C4 File Offset: 0x000008C4
		public LevelVisibilityPicker(InputService inputService, CursorCoordinatesPicker cursorCoordinatesPicker, ILevelVisibilityService levelVisibilityService, ConstructionModeService constructionModeService)
		{
			this._inputService = inputService;
			this._cursorCoordinatesPicker = cursorCoordinatesPicker;
			this._levelVisibilityService = levelVisibilityService;
			this._constructionModeService = constructionModeService;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026E9 File Offset: 0x000008E9
		public void Load()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026F7 File Offset: 0x000008F7
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(LevelVisibilityPicker.PickVisibleLayerKey))
			{
				this.PickOrResetLayer();
				return true;
			}
			return false;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002714 File Offset: 0x00000914
		public void PickOrResetLayer()
		{
			CursorCoordinates? cursorCoordinates = this._constructionModeService.InConstructionMode ? this._cursorCoordinatesPicker.Pick() : this._cursorCoordinatesPicker.PickOnFinished();
			if (cursorCoordinates != null)
			{
				int z = cursorCoordinates.GetValueOrDefault().TileCoordinates.z;
				if (z < this._levelVisibilityService.MaxVisibleLevel)
				{
					this._levelVisibilityService.SetMaxVisibleLevel(z);
					return;
				}
				this._levelVisibilityService.ResetMaxVisibleLevel();
			}
		}

		// Token: 0x0400002B RID: 43
		public static readonly string PickVisibleLayerKey = "PickVisibleLayer";

		// Token: 0x0400002C RID: 44
		public readonly InputService _inputService;

		// Token: 0x0400002D RID: 45
		public readonly CursorCoordinatesPicker _cursorCoordinatesPicker;

		// Token: 0x0400002E RID: 46
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x0400002F RID: 47
		public readonly ConstructionModeService _constructionModeService;
	}
}
