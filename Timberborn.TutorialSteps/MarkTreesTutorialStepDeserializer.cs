using System;
using Timberborn.BlueprintSystem;
using Timberborn.Forestry;
using Timberborn.ForestryUI;
using Timberborn.Localization;
using Timberborn.ToolButtonSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000036 RID: 54
	public class MarkTreesTutorialStepDeserializer : IStepDeserializer
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00005310 File Offset: 0x00003510
		public MarkTreesTutorialStepDeserializer(TreeCuttingArea treeCuttingArea, ILoc loc, ToolButtonService toolButtonService)
		{
			this._treeCuttingArea = treeCuttingArea;
			this._loc = loc;
			this._toolButtonService = toolButtonService;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005330 File Offset: 0x00003530
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			if (step.Specs[0] is MarkTreesTutorialStepSpec)
			{
				tutorialStep = this.Create();
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005364 File Offset: 0x00003564
		public TutorialStep Create()
		{
			ToolButton toolButton = this._toolButtonService.GetToolButton<TreeCuttingAreaSelectionTool>();
			ToolGroupButton toolGroupButton = this._toolButtonService.GetToolGroupButton(toolButton);
			return TutorialStep.Create(new MarkTreesTutorialStep(this._treeCuttingArea, this._loc.T(MarkTreesTutorialStepDeserializer.MarkTreesLocKey)), toolGroupButton, toolButton, null);
		}

		// Token: 0x040000AE RID: 174
		public static readonly string MarkTreesLocKey = "Tutorial.MarkTrees";

		// Token: 0x040000AF RID: 175
		public readonly TreeCuttingArea _treeCuttingArea;

		// Token: 0x040000B0 RID: 176
		public readonly ILoc _loc;

		// Token: 0x040000B1 RID: 177
		public readonly ToolButtonService _toolButtonService;
	}
}
