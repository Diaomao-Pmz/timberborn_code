using System;
using System.Collections.Generic;
using Timberborn.BlockObjectTools;
using Timberborn.BottomBarSystem;
using Timberborn.DeconstructionSystemUI;
using Timberborn.DemolishingUI;
using Timberborn.RecoveredGoodSystemUI;
using Timberborn.ToolButtonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.DemolishingToolGroupSystem
{
	// Token: 0x02000004 RID: 4
	public class DemolishingButton : IBottomBarElementsProvider
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public DemolishingButton(DemolishableSelectionTool demolishableSelectionTool, DemolishableUnselectionTool demolishableUnselectionTool, ToolButtonFactory toolButtonFactory, ToolGroupButtonFactory toolGroupButtonFactory, BuildingDeconstructionTool buildingDeconstructionTool, EntityBlockObjectDeletionTool entityBlockObjectDeletionTool, RecoveredGoodStackDeletionTool recoveredGoodStackDeletionTool, ToolGroupService toolGroupService)
		{
			this._demolishableSelectionTool = demolishableSelectionTool;
			this._demolishableUnselectionTool = demolishableUnselectionTool;
			this._toolButtonFactory = toolButtonFactory;
			this._toolGroupButtonFactory = toolGroupButtonFactory;
			this._buildingDeconstructionTool = buildingDeconstructionTool;
			this._entityBlockObjectDeletionTool = entityBlockObjectDeletionTool;
			this._recoveredGoodStackDeletionTool = recoveredGoodStackDeletionTool;
			this._toolGroupService = toolGroupService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002110 File Offset: 0x00000310
		public IEnumerable<BottomBarElement> GetElements()
		{
			ToolGroupSpec group = this._toolGroupService.GetGroup(DemolishingButton.ToolGroupId);
			ToolGroupButton toolGroupButton = this._toolGroupButtonFactory.CreateBlue(group);
			this.AddTool(this._buildingDeconstructionTool, DemolishingButton.DeleteBuildingImageKey, group, toolGroupButton);
			this.AddTool(this._recoveredGoodStackDeletionTool, DemolishingButton.DeleteRecoveredGoodStackToolImageKey, group, toolGroupButton);
			this.AddTool(this._demolishableSelectionTool, DemolishingButton.DemolishToolImageKey, group, toolGroupButton);
			this.AddTool(this._entityBlockObjectDeletionTool, DemolishingButton.DeleteBuildingImageKey, group, toolGroupButton);
			this.AddTool(this._demolishableUnselectionTool, DemolishingButton.CancelToolImageKey, group, toolGroupButton);
			yield return BottomBarElement.CreateMultiLevel(toolGroupButton.Root, toolGroupButton.ToolButtonsElement);
			yield break;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002120 File Offset: 0x00000320
		public void AddTool(ITool tool, string imageName, ToolGroupSpec toolGroup, ToolGroupButton toolGroupButton)
		{
			ToolButton button = this._toolButtonFactory.Create(tool, imageName, toolGroupButton.ToolButtonsElement);
			toolGroupButton.AddTool(button);
			this._toolGroupService.AssignToGroup(toolGroup, tool);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string ToolGroupId = "Demolishing";

		// Token: 0x04000007 RID: 7
		public static readonly string DeleteRecoveredGoodStackToolImageKey = "DeleteRecoveredGoodStackToolIcon";

		// Token: 0x04000008 RID: 8
		public static readonly string DeleteBuildingImageKey = "DeleteObjectIcon";

		// Token: 0x04000009 RID: 9
		public static readonly string DemolishToolImageKey = "DemolishResourcesTool";

		// Token: 0x0400000A RID: 10
		public static readonly string CancelToolImageKey = "CancelToolIcon";

		// Token: 0x0400000B RID: 11
		public readonly RecoveredGoodStackDeletionTool _recoveredGoodStackDeletionTool;

		// Token: 0x0400000C RID: 12
		public readonly DemolishableSelectionTool _demolishableSelectionTool;

		// Token: 0x0400000D RID: 13
		public readonly DemolishableUnselectionTool _demolishableUnselectionTool;

		// Token: 0x0400000E RID: 14
		public readonly BuildingDeconstructionTool _buildingDeconstructionTool;

		// Token: 0x0400000F RID: 15
		public readonly EntityBlockObjectDeletionTool _entityBlockObjectDeletionTool;

		// Token: 0x04000010 RID: 16
		public readonly ToolButtonFactory _toolButtonFactory;

		// Token: 0x04000011 RID: 17
		public readonly ToolGroupButtonFactory _toolGroupButtonFactory;

		// Token: 0x04000012 RID: 18
		public readonly ToolGroupService _toolGroupService;
	}
}
