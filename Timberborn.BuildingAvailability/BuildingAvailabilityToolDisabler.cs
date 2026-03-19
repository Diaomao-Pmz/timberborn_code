using System;
using Timberborn.BlockObjectTools;
using Timberborn.ToolSystem;

namespace Timberborn.BuildingAvailability
{
	// Token: 0x02000006 RID: 6
	public class BuildingAvailabilityToolDisabler : IToolDisabler
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021A3 File Offset: 0x000003A3
		public BuildingAvailabilityToolDisabler(BuildingAvailabilityValidator buildingAvailabilityValidator)
		{
			this._buildingAvailabilityValidator = buildingAvailabilityValidator;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B4 File Offset: 0x000003B4
		public bool IsEnabled(ITool tool)
		{
			BlockObjectTool blockObjectTool = tool as BlockObjectTool;
			return blockObjectTool == null || this._buildingAvailabilityValidator.IsAvailableForPlacement(blockObjectTool.Template);
		}

		// Token: 0x04000008 RID: 8
		public readonly BuildingAvailabilityValidator _buildingAvailabilityValidator;
	}
}
