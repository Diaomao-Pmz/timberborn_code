using System;
using System.Collections.Generic;
using Timberborn.BottomBarSystem;
using Timberborn.ToolButtonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.ForestryUI
{
	// Token: 0x02000010 RID: 16
	public class TreeCuttingAreaButton : IBottomBarElementsProvider
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000026A2 File Offset: 0x000008A2
		public TreeCuttingAreaButton(TreeCuttingAreaSelectionTool treeCuttingAreaSelectionTool, TreeCuttingAreaUnselectionTool treeCuttingAreaUnselectionTool, ToolButtonFactory toolButtonFactory, ToolGroupButtonFactory toolGroupButtonFactory, ToolGroupService toolGroupService)
		{
			this._treeCuttingAreaSelectionTool = treeCuttingAreaSelectionTool;
			this._treeCuttingAreaUnselectionTool = treeCuttingAreaUnselectionTool;
			this._toolButtonFactory = toolButtonFactory;
			this._toolGroupButtonFactory = toolGroupButtonFactory;
			this._toolGroupService = toolGroupService;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026CF File Offset: 0x000008CF
		public IEnumerable<BottomBarElement> GetElements()
		{
			ToolGroupSpec group = this._toolGroupService.GetGroup(TreeCuttingAreaButton.ToolGroupId);
			ToolGroupButton toolGroupButton = this._toolGroupButtonFactory.CreateBlue(group);
			this.AddTool(this._treeCuttingAreaSelectionTool, TreeCuttingAreaButton.SelectionToolImageKey, group, toolGroupButton);
			this.AddTool(this._treeCuttingAreaUnselectionTool, TreeCuttingAreaButton.UnselectionToolImageKey, group, toolGroupButton);
			yield return BottomBarElement.CreateMultiLevel(toolGroupButton.Root, toolGroupButton.ToolButtonsElement);
			yield break;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026E0 File Offset: 0x000008E0
		public void AddTool(ITool tool, string imageName, ToolGroupSpec toolGroup, ToolGroupButton toolGroupButton)
		{
			ToolButton button = this._toolButtonFactory.Create(tool, imageName, toolGroupButton.ToolButtonsElement);
			toolGroupButton.AddTool(button);
			this._toolGroupService.AssignToGroup(toolGroup, tool);
		}

		// Token: 0x04000021 RID: 33
		public static readonly string ToolGroupId = "TreeCutting";

		// Token: 0x04000022 RID: 34
		public static readonly string SelectionToolImageKey = "TreeCuttingAreaSelectionTool";

		// Token: 0x04000023 RID: 35
		public static readonly string UnselectionToolImageKey = "CancelToolIcon";

		// Token: 0x04000024 RID: 36
		public readonly TreeCuttingAreaSelectionTool _treeCuttingAreaSelectionTool;

		// Token: 0x04000025 RID: 37
		public readonly TreeCuttingAreaUnselectionTool _treeCuttingAreaUnselectionTool;

		// Token: 0x04000026 RID: 38
		public readonly ToolButtonFactory _toolButtonFactory;

		// Token: 0x04000027 RID: 39
		public readonly ToolGroupButtonFactory _toolGroupButtonFactory;

		// Token: 0x04000028 RID: 40
		public readonly ToolGroupService _toolGroupService;
	}
}
