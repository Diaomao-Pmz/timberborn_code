using System;
using Timberborn.AreaSelectionSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.InputSystem;
using Timberborn.ToolSystem;
using Timberborn.UISound;
using Timberborn.UndoSystem;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x02000010 RID: 16
	public class BlockObjectToolFactory
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public BlockObjectToolFactory(ToolService toolService, InputService inputService, AreaPicker areaPicker, PreviewPlacerFactory previewPlacerFactory, UISoundController uiSoundController, ToolUnlockingService toolUnlockingService, IUndoRegistry undoRegistry, Duplicator duplicator, PreviewPlacement previewPlacement)
		{
			this._toolService = toolService;
			this._inputService = inputService;
			this._areaPicker = areaPicker;
			this._previewPlacerFactory = previewPlacerFactory;
			this._uiSoundController = uiSoundController;
			this._toolUnlockingService = toolUnlockingService;
			this._undoRegistry = undoRegistry;
			this._duplicator = duplicator;
			this._previewPlacement = previewPlacement;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E08 File Offset: 0x00001008
		public BlockObjectTool Create(PlaceableBlockObjectSpec template, IBlockObjectPlacer blockObjectPlacer, IBlockObjectToolDescriber blockObjectToolDescriber)
		{
			return new BlockObjectTool(template, this._toolService, this._inputService, this._areaPicker, this._uiSoundController, this._toolUnlockingService, blockObjectPlacer, blockObjectToolDescriber, this._previewPlacerFactory.Create(template), this._undoRegistry, this._duplicator, this._previewPlacement);
		}

		// Token: 0x0400003C RID: 60
		public readonly ToolService _toolService;

		// Token: 0x0400003D RID: 61
		public readonly InputService _inputService;

		// Token: 0x0400003E RID: 62
		public readonly AreaPicker _areaPicker;

		// Token: 0x0400003F RID: 63
		public readonly PreviewPlacerFactory _previewPlacerFactory;

		// Token: 0x04000040 RID: 64
		public readonly UISoundController _uiSoundController;

		// Token: 0x04000041 RID: 65
		public readonly ToolUnlockingService _toolUnlockingService;

		// Token: 0x04000042 RID: 66
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000043 RID: 67
		public readonly Duplicator _duplicator;

		// Token: 0x04000044 RID: 68
		public readonly PreviewPlacement _previewPlacement;
	}
}
