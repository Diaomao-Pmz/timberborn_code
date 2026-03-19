using System;
using System.Collections.Generic;
using Timberborn.BottomBarSystem;
using Timberborn.PrioritySystem;
using Timberborn.ToolButtonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.BuilderPrioritySystemUI
{
	// Token: 0x02000007 RID: 7
	public class BuilderPrioritiesButton : IBottomBarElementsProvider
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FF File Offset: 0x000002FF
		public BuilderPrioritiesButton(BuilderPrioritiesButtonFactory builderPrioritiesButtonFactory, ToolGroupButtonFactory toolGroupButtonFactory, ToolGroupService toolGroupService)
		{
			this._builderPrioritiesButtonFactory = builderPrioritiesButtonFactory;
			this._toolGroupButtonFactory = toolGroupButtonFactory;
			this._toolGroupService = toolGroupService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211C File Offset: 0x0000031C
		public IEnumerable<BottomBarElement> GetElements()
		{
			ToolGroupSpec group = this._toolGroupService.GetGroup(BuilderPrioritiesButton.ToolGroupId);
			ToolGroupButton toolGroupButton = this._toolGroupButtonFactory.CreateBlue(group);
			foreach (Priority priority in Priorities.Ascending)
			{
				ToolButton toolButton = this._builderPrioritiesButtonFactory.CreateButton(priority, toolGroupButton.ToolButtonsElement);
				this._toolGroupService.AssignToGroup(group, toolButton.Tool);
				toolGroupButton.AddTool(toolButton);
			}
			yield return BottomBarElement.CreateMultiLevel(toolGroupButton.Root, toolGroupButton.ToolButtonsElement);
			yield break;
		}

		// Token: 0x04000008 RID: 8
		public static readonly string ToolGroupId = "BuilderPriority";

		// Token: 0x04000009 RID: 9
		public readonly BuilderPrioritiesButtonFactory _builderPrioritiesButtonFactory;

		// Token: 0x0400000A RID: 10
		public readonly ToolGroupButtonFactory _toolGroupButtonFactory;

		// Token: 0x0400000B RID: 11
		public readonly ToolGroupService _toolGroupService;
	}
}
