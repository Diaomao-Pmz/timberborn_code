using System;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.SingletonSystem;
using Timberborn.TutorialSystem;
using UnityEngine.UIElements;

namespace Timberborn.TutorialSystemUI
{
	// Token: 0x0200000E RID: 14
	public class TutorialPanelFactory
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002948 File Offset: 0x00000B48
		public TutorialPanelFactory(DisableTutorialButtonInitializer disableTutorialButtonInitializer, VisualElementLoader visualElementLoader, EventBus eventBus, DevModeManager devModeManager, ITutorialService tutorialService, TutorialPanelBlinker tutorialPanelBlinker, TutorialStepViewFactory tutorialStepViewFactory)
		{
			this._disableTutorialButtonInitializer = disableTutorialButtonInitializer;
			this._visualElementLoader = visualElementLoader;
			this._eventBus = eventBus;
			this._devModeManager = devModeManager;
			this._tutorialService = tutorialService;
			this._tutorialPanelBlinker = tutorialPanelBlinker;
			this._tutorialStepViewFactory = tutorialStepViewFactory;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002988 File Offset: 0x00000B88
		public TutorialPanel Create(TutorialConfiguration tutorialConfiguration)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/Tutorial/TutorialPanel");
			this._disableTutorialButtonInitializer.Initialize(visualElement);
			UQueryExtensions.Q<Label>(visualElement, "TutorialTitle", null).text = tutorialConfiguration.DisplayName;
			UQueryExtensions.Q<Button>(visualElement, "HeaderButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnHeaderClicked(tutorialConfiguration.TutorialId);
			}, 0);
			AchievedStepsController achievedStepsController = new AchievedStepsController(this._tutorialService, visualElement, UQueryExtensions.Q<Button>(visualElement, "Continue", null));
			TutorialPanel tutorialPanel = new TutorialPanel(this._tutorialService, achievedStepsController, this._eventBus, this._devModeManager, this._tutorialPanelBlinker, visualElement, tutorialConfiguration, this._tutorialStepViewFactory);
			tutorialPanel.Initialize();
			return tutorialPanel;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A4A File Offset: 0x00000C4A
		public void OnHeaderClicked(string tutorialId)
		{
			this._eventBus.Post(new TutorialHeaderClickedEvent(tutorialId));
		}

		// Token: 0x0400002D RID: 45
		public readonly DisableTutorialButtonInitializer _disableTutorialButtonInitializer;

		// Token: 0x0400002E RID: 46
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002F RID: 47
		public readonly EventBus _eventBus;

		// Token: 0x04000030 RID: 48
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000031 RID: 49
		public readonly ITutorialService _tutorialService;

		// Token: 0x04000032 RID: 50
		public readonly TutorialPanelBlinker _tutorialPanelBlinker;

		// Token: 0x04000033 RID: 51
		public readonly TutorialStepViewFactory _tutorialStepViewFactory;
	}
}
