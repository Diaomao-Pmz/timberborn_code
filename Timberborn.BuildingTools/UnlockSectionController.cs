using System;
using Timberborn.BlockObjectTools;
using Timberborn.CoreUI;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.BuildingTools
{
	// Token: 0x0200000C RID: 12
	public class UnlockSectionController
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000026B2 File Offset: 0x000008B2
		public UnlockSectionController(ToolUnlockingService toolUnlockingService)
		{
			this._toolUnlockingService = toolUnlockingService;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026C4 File Offset: 0x000008C4
		public void UpdateSection(VisualElement section, BlockObjectTool tool)
		{
			if (this._toolUnlockingService.IsLocked(tool))
			{
				section.ToggleDisplayStyle(true);
				section.EnableInClassList(UnlockSectionController.HighlightClass, this._toolToHighlight != null && this._toolToHighlight == tool);
				return;
			}
			section.ToggleDisplayStyle(false);
			section.RemoveFromClassList(UnlockSectionController.HighlightClass);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002718 File Offset: 0x00000918
		public void ToggleHighlight(bool state, ITool tool)
		{
			this._toolToHighlight = (state ? tool : null);
		}

		// Token: 0x04000023 RID: 35
		public static readonly string HighlightClass = "highlight";

		// Token: 0x04000024 RID: 36
		public readonly ToolUnlockingService _toolUnlockingService;

		// Token: 0x04000025 RID: 37
		public ITool _toolToHighlight;
	}
}
