using System;
using Timberborn.ToolButtonSystem;
using Timberborn.TutorialSystem;
using UnityEngine.UIElements;

namespace Timberborn.TutorialSystemUI
{
	// Token: 0x02000012 RID: 18
	public class TutorialStepView
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002D78 File Offset: 0x00000F78
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002D80 File Offset: 0x00000F80
		public bool IsAchieved { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002D89 File Offset: 0x00000F89
		public VisualElement Root { get; }

		// Token: 0x06000050 RID: 80 RVA: 0x00002D91 File Offset: 0x00000F91
		public TutorialStepView(TutorialStep tutorialStep, TutorialPanel parent, VisualElement root, Label description)
		{
			this._tutorialStep = tutorialStep;
			this._parent = parent;
			this.Root = root;
			this._description = description;
			this.UpdateDescription();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002DBC File Offset: 0x00000FBC
		public void Update()
		{
			this.IsAchieved = this._tutorialStep.Step.Achieved();
			this.UpdateStyle();
			this.UpdateDescription();
			this.HighlightAssociatedTools(false);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DE7 File Offset: 0x00000FE7
		public void UnhighlightAssociatedTools()
		{
			this.HighlightAssociatedTools(true);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public void UpdateStyle()
		{
			this.Root.EnableInClassList(TutorialStepView.FinishedClassKey, this.IsAchieved);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E08 File Offset: 0x00001008
		public void UpdateDescription()
		{
			this._description.text = this._tutorialStep.Step.Description();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E28 File Offset: 0x00001028
		public void HighlightAssociatedTools(bool forceHide = false)
		{
			bool flag;
			if (this._parent.IsVisible && !forceHide && !this.IsAchieved)
			{
				ITutorialStepWithTool tutorialStepWithTool = this._tutorialStep.Step as ITutorialStepWithTool;
				if (tutorialStepWithTool == null || tutorialStepWithTool.KeepBlinking)
				{
					flag = HighlightTimer.IsTimeForPulsingHighlight();
					goto IL_3E;
				}
			}
			flag = false;
			IL_3E:
			bool flag2 = flag;
			foreach (ToolButton toolButton in this._tutorialStep.ToolButtons)
			{
				TutorialStepView.ToggleHighlight(toolButton.Root, flag2);
			}
			ToolGroupButton toolGroupButton = this._tutorialStep.ToolGroupButton;
			if (toolGroupButton != null)
			{
				TutorialStepView.ToggleHighlight(toolGroupButton.Root, flag2);
			}
			Action<bool> highlight = this._tutorialStep.Highlight;
			if (highlight == null)
			{
				return;
			}
			highlight(flag2);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002ED6 File Offset: 0x000010D6
		public static void ToggleHighlight(VisualElement root, bool state)
		{
			root.EnableInClassList(TutorialStepView.TutorialToolClassKey, state);
		}

		// Token: 0x04000040 RID: 64
		public static readonly string FinishedClassKey = "tutorial-step-view--finished";

		// Token: 0x04000041 RID: 65
		public static readonly string TutorialToolClassKey = "tutorial-tool--highlighted";

		// Token: 0x04000044 RID: 68
		public readonly TutorialStep _tutorialStep;

		// Token: 0x04000045 RID: 69
		public readonly TutorialPanel _parent;

		// Token: 0x04000046 RID: 70
		public readonly Label _description;
	}
}
