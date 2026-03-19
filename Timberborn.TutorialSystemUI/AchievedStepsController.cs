using System;
using Timberborn.TutorialSystem;
using UnityEngine.UIElements;

namespace Timberborn.TutorialSystemUI
{
	// Token: 0x02000004 RID: 4
	public class AchievedStepsController
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public AchievedStepsController(ITutorialService tutorialService, VisualElement root, Button button)
		{
			this._tutorialService = tutorialService;
			this._root = root;
			this._button = button;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E0 File Offset: 0x000002E0
		public void Initialize(string tutorialId)
		{
			this._button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._tutorialService.StartNextStage(tutorialId);
			}, 0);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002119 File Offset: 0x00000319
		public void ChangeActiveTutorialStage(TutorialStage tutorialStage)
		{
			this._activeTutorialStage = tutorialStage;
			this.UpdateVisibility();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002128 File Offset: 0x00000328
		public void UpdateVisibility()
		{
			TutorialStage activeTutorialStage = this._activeTutorialStage;
			bool flag = activeTutorialStage != null && activeTutorialStage.AllStepsAchieved;
			this._root.EnableInClassList(AchievedStepsController.AllStepsAchievedClass, flag);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string AllStepsAchievedClass = "all-steps-achieved";

		// Token: 0x04000007 RID: 7
		public readonly ITutorialService _tutorialService;

		// Token: 0x04000008 RID: 8
		public readonly VisualElement _root;

		// Token: 0x04000009 RID: 9
		public readonly Button _button;

		// Token: 0x0400000A RID: 10
		public TutorialStage _activeTutorialStage;
	}
}
