using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using Timberborn.TutorialSettingsSystem;
using Timberborn.TutorialSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.TutorialSystemUI
{
	// Token: 0x02000010 RID: 16
	public class TutorialPanels : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002A75 File Offset: 0x00000C75
		public TutorialPanels(UILayout uiLayout, VisualElementLoader visualElementLoader, EventBus eventBus, TutorialSettings tutorialSettings, TutorialPanelFactory tutorialPanelFactory)
		{
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._eventBus = eventBus;
			this._tutorialSettings = tutorialSettings;
			this._tutorialPanelFactory = tutorialPanelFactory;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/Tutorial/TutorialPanels");
			this._uiLayout.AddBottomRight(this._root, 4);
			this._eventBus.Register(this);
			this._tutorialSettings.DisableTutorialChanged += delegate(object _, SettingChangedEventArgs<bool> _)
			{
				this.ShowIfConditionsMet();
			};
			this.Hide();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B10 File Offset: 0x00000D10
		public void UpdateSingleton()
		{
			if (this.TutorialIsOn)
			{
				foreach (TutorialPanel tutorialPanel in this._tutorialPanels.Values)
				{
					tutorialPanel.Update();
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B70 File Offset: 0x00000D70
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._enabled = true;
			this.ShowIfConditionsMet();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B80 File Offset: 0x00000D80
		[OnEvent]
		public void OnTutorialCreated(TutorialCreatedEvent tutorialCreatedEvent)
		{
			TutorialConfiguration configuration = tutorialCreatedEvent.Configuration;
			TutorialPanel tutorialPanel = this._tutorialPanelFactory.Create(configuration);
			this._tutorialPanels.Add(configuration.TutorialId, tutorialPanel);
			this._root.Add(tutorialPanel.Root);
			this.ShowIfConditionsMet();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BCC File Offset: 0x00000DCC
		[OnEvent]
		public void OnTutorialFinished(TutorialFinishedEvent tutorialFinishedEvent)
		{
			string tutorialId = tutorialFinishedEvent.TutorialId;
			TutorialPanel tutorialPanel = this._tutorialPanels[tutorialId];
			tutorialPanel.Disable();
			this._root.Remove(tutorialPanel.Root);
			this._tutorialPanels.Remove(tutorialId);
			if (this._tutorialPanels.Count == 0)
			{
				this.Hide();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002C24 File Offset: 0x00000E24
		public bool TutorialIsOn
		{
			get
			{
				return this._enabled && !this._tutorialSettings.DisableTutorial && this._tutorialPanels.Count > 0;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C4B File Offset: 0x00000E4B
		public void ShowIfConditionsMet()
		{
			if (this.TutorialIsOn)
			{
				this.Show();
				return;
			}
			this.Hide();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C62 File Offset: 0x00000E62
		public void Show()
		{
			this._root.ToggleDisplayStyle(true);
			this.SortTutorialPanels();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C78 File Offset: 0x00000E78
		public void Hide()
		{
			this._root.ToggleDisplayStyle(false);
			foreach (TutorialPanel tutorialPanel in this._tutorialPanels.Values)
			{
				if (tutorialPanel.IsVisible)
				{
					tutorialPanel.UnhighlightAssociatedTools();
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public void SortTutorialPanels()
		{
			foreach (TutorialPanel tutorialPanel in from panel in this._tutorialPanels.Values
			orderby panel.SortOrder
			select panel)
			{
				tutorialPanel.Root.BringToFront();
			}
		}

		// Token: 0x04000036 RID: 54
		public readonly UILayout _uiLayout;

		// Token: 0x04000037 RID: 55
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000038 RID: 56
		public readonly EventBus _eventBus;

		// Token: 0x04000039 RID: 57
		public readonly TutorialSettings _tutorialSettings;

		// Token: 0x0400003A RID: 58
		public readonly TutorialPanelFactory _tutorialPanelFactory;

		// Token: 0x0400003B RID: 59
		public VisualElement _root;

		// Token: 0x0400003C RID: 60
		public readonly Dictionary<string, TutorialPanel> _tutorialPanels = new Dictionary<string, TutorialPanel>();

		// Token: 0x0400003D RID: 61
		public bool _enabled;
	}
}
