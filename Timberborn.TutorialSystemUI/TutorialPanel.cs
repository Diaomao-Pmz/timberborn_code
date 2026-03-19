using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.SingletonSystem;
using Timberborn.TutorialSystem;
using UnityEngine.UIElements;

namespace Timberborn.TutorialSystemUI
{
	// Token: 0x0200000A RID: 10
	public class TutorialPanel
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022FC File Offset: 0x000004FC
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002304 File Offset: 0x00000504
		public bool IsVisible { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000230D File Offset: 0x0000050D
		public VisualElement Root { get; }

		// Token: 0x0600001A RID: 26 RVA: 0x00002318 File Offset: 0x00000518
		public TutorialPanel(ITutorialService tutorialService, AchievedStepsController achievedStepsController, EventBus eventBus, DevModeManager devModeManager, TutorialPanelBlinker tutorialPanelBlinker, VisualElement root, TutorialConfiguration tutorialConfiguration, TutorialStepViewFactory tutorialStepViewFactory)
		{
			this._tutorialService = tutorialService;
			this._achievedStepsController = achievedStepsController;
			this._eventBus = eventBus;
			this._devModeManager = devModeManager;
			this._tutorialPanelBlinker = tutorialPanelBlinker;
			this.Root = root;
			this._tutorialConfiguration = tutorialConfiguration;
			this._tutorialStepViewFactory = tutorialStepViewFactory;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002373 File Offset: 0x00000573
		public int SortOrder
		{
			get
			{
				return this._tutorialConfiguration.SortOrder;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002380 File Offset: 0x00000580
		public void Initialize()
		{
			this._stageState = UQueryExtensions.Q<Label>(this.Root, "TutorialStageState", null);
			this._intro = UQueryExtensions.Q<Label>(this.Root, "Intro", null);
			this._tutorialId = this._tutorialConfiguration.TutorialId;
			this._achievedStepsController.Initialize(this._tutorialId);
			this._tutorialSteps = UQueryExtensions.Q<VisualElement>(this.Root, "TutorialSteps", null);
			this._devCompleteButton = UQueryExtensions.Q<Button>(this.Root, "DevComplete", null);
			this._devCompleteButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.ForceComplete();
			}, 0);
			this._devCompleteAllButton = UQueryExtensions.Q<Button>(this.Root, "DevCompleteAll", null);
			this._devCompleteAllButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.ForceCompleteAll();
			}, 0);
			this._eventBus.Register(this);
			this._enabled = true;
			this._tutorialPanelBlinker.StartBlinking(this.Root, this._tutorialConfiguration.KeepBlinking);
			this.Root.ToggleDisplayStyle(true);
			this.Hide();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002493 File Offset: 0x00000693
		public void Disable()
		{
			this._tutorialPanelBlinker.StopBlinking(this.Root);
			this._enabled = false;
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024BC File Offset: 0x000006BC
		[OnEvent]
		public void OnTutorialStageStarted(TutorialStageStartedEvent tutorialStageStartedEvent)
		{
			if (tutorialStageStartedEvent.TutorialId == this._tutorialId)
			{
				this._intro.text = tutorialStageStartedEvent.TutorialStage.Intro;
				this.TransitionIntoNextStage(tutorialStageStartedEvent.TutorialStage);
				this._achievedStepsController.ChangeActiveTutorialStage(tutorialStageStartedEvent.TutorialStage);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000250F File Offset: 0x0000070F
		[OnEvent]
		public void OnTutorialHeaderClicked(TutorialHeaderClickedEvent tutorialHeaderClickedEvent)
		{
			if (this._tutorialId == tutorialHeaderClickedEvent.TutorialId && !this.IsVisible)
			{
				this.Show();
				return;
			}
			this.Hide();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000253C File Offset: 0x0000073C
		public void Update()
		{
			this._achievedStepsController.UpdateVisibility();
			int num = 0;
			foreach (TutorialStepView tutorialStepView in this._tutorialStepViews)
			{
				tutorialStepView.Update();
				num += (tutorialStepView.IsAchieved ? 1 : 0);
			}
			this._stageState.text = string.Format("{0}/{1}", num, this._tutorialStepViews.Count);
			this.UpdateDevButtons();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025DC File Offset: 0x000007DC
		public void UnhighlightAssociatedTools()
		{
			foreach (TutorialStepView tutorialStepView in this._tutorialStepViews)
			{
				tutorialStepView.UnhighlightAssociatedTools();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000262C File Offset: 0x0000082C
		public void TransitionIntoNextStage(TutorialStage tutorialStage)
		{
			this.DeactivateActiveStage();
			this.ActivateStage(tutorialStage);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000263B File Offset: 0x0000083B
		public void DeactivateActiveStage()
		{
			this.UnhighlightAssociatedTools();
			this._tutorialSteps.Clear();
			this._tutorialStepViews.Clear();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002659 File Offset: 0x00000859
		public void ActivateStage(TutorialStage tutorialStage)
		{
			this.AddTutorialStepViews(tutorialStage.TutorialSteps);
			this._stageState.ToggleDisplayStyle(this._tutorialStepViews.Count > 0);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002688 File Offset: 0x00000888
		public void AddTutorialStepViews(IEnumerable<TutorialStep> tutorialSteps)
		{
			foreach (TutorialStep tutorialStep in tutorialSteps)
			{
				this.AddTutorialStepView(tutorialStep);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026D0 File Offset: 0x000008D0
		public void AddTutorialStepView(TutorialStep tutorialStep)
		{
			TutorialStepView tutorialStepView = this._tutorialStepViewFactory.Create(tutorialStep, this);
			this._tutorialStepViews.Add(tutorialStepView);
			this._tutorialSteps.Add(tutorialStepView.Root);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002708 File Offset: 0x00000908
		public void Show()
		{
			this._tutorialPanelBlinker.StopBlinking(this.Root);
			this.IsVisible = true;
			this.Root.RemoveFromClassList(TutorialPanel.HiddenClass);
			this.Update();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002738 File Offset: 0x00000938
		public void Hide()
		{
			this.UnhighlightAssociatedTools();
			this.Root.AddToClassList(TutorialPanel.HiddenClass);
			this.IsVisible = false;
			this.UpdateDevButtons();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002760 File Offset: 0x00000960
		public void UpdateDevButtons()
		{
			bool visible = this._devModeManager.Enabled && this.IsVisible;
			this._devCompleteButton.ToggleDisplayStyle(visible);
			this._devCompleteAllButton.ToggleDisplayStyle(visible);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000279C File Offset: 0x0000099C
		public void ForceComplete()
		{
			this.UnhighlightAssociatedTools();
			this._tutorialService.StartNextStage(this._tutorialId);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027B5 File Offset: 0x000009B5
		public void ForceCompleteAll()
		{
			this.UnhighlightAssociatedTools();
			while (this._enabled)
			{
				this._tutorialService.StartNextStage(this._tutorialId);
			}
		}

		// Token: 0x04000015 RID: 21
		public static readonly string HiddenClass = "hidden";

		// Token: 0x04000018 RID: 24
		public readonly ITutorialService _tutorialService;

		// Token: 0x04000019 RID: 25
		public readonly AchievedStepsController _achievedStepsController;

		// Token: 0x0400001A RID: 26
		public readonly EventBus _eventBus;

		// Token: 0x0400001B RID: 27
		public readonly DevModeManager _devModeManager;

		// Token: 0x0400001C RID: 28
		public readonly TutorialPanelBlinker _tutorialPanelBlinker;

		// Token: 0x0400001D RID: 29
		public readonly TutorialConfiguration _tutorialConfiguration;

		// Token: 0x0400001E RID: 30
		public readonly TutorialStepViewFactory _tutorialStepViewFactory;

		// Token: 0x0400001F RID: 31
		public string _tutorialId;

		// Token: 0x04000020 RID: 32
		public Label _stageState;

		// Token: 0x04000021 RID: 33
		public Label _intro;

		// Token: 0x04000022 RID: 34
		public VisualElement _tutorialSteps;

		// Token: 0x04000023 RID: 35
		public readonly List<TutorialStepView> _tutorialStepViews = new List<TutorialStepView>();

		// Token: 0x04000024 RID: 36
		public Button _devCompleteButton;

		// Token: 0x04000025 RID: 37
		public Button _devCompleteAllButton;

		// Token: 0x04000026 RID: 38
		public bool _enabled;
	}
}
