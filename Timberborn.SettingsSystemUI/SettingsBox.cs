using System;
using Timberborn.CoreUI;
using Timberborn.IntroSettingsSystem;
using Timberborn.KeyBindingSystemUI;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000019 RID: 25
	public class SettingsBox : ISettingsController, IPanelController, ILoadableSingleton
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003A34 File Offset: 0x00001C34
		public SettingsBox(DevModeSettingsController devModeSettingsController, GraphicsSettingsController graphicsSettingsController, ScreenSettingsController screenSettingsController, UISettingsController uiSettingsController, TutorialSettingsController tutorialSettingsController, AccessibilitySettingsController accessibilitySettingsController, InputSettingsController inputSettingsController, SoundSettingsController soundSettingsController, GameSavingSettingsController gameSavingSettingsController, VisualElementLoader visualElementLoader, PanelStack panelStack, LanguageSettingsController languageSettingsController, KeyBindingsBox keyBindingsBox, AnalyticsSettingsController analyticsSettingsController, IntroSettingsController introSettingsController, CameraSettingsController cameraSettingsController)
		{
			this._devModeSettingsController = devModeSettingsController;
			this._graphicsSettingsController = graphicsSettingsController;
			this._screenSettingsController = screenSettingsController;
			this._uiSettingsController = uiSettingsController;
			this._tutorialSettingsController = tutorialSettingsController;
			this._accessibilitySettingsController = accessibilitySettingsController;
			this._inputSettingsController = inputSettingsController;
			this._soundSettingsController = soundSettingsController;
			this._gameSavingSettingsController = gameSavingSettingsController;
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._languageSettingsController = languageSettingsController;
			this._keyBindingsBox = keyBindingsBox;
			this._analyticsSettingsController = analyticsSettingsController;
			this._introSettingsController = introSettingsController;
			this._cameraSettingsController = cameraSettingsController;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Options/SettingsBox");
			this._content = UQueryExtensions.Q<ScrollView>(this._root, "Content", null);
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "BindingsButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OpenKeyBindingsBox), 0);
			this._devModeSettingsController.Initialize(this._root, new Action(this.OnUICancelled));
			this._graphicsSettingsController.Initialize(this._root);
			this._screenSettingsController.Initialize(this._root);
			this._uiSettingsController.Initialize(this._root);
			this._tutorialSettingsController.Initialize(this._root);
			this._accessibilitySettingsController.Initialize(this._root);
			this._inputSettingsController.Initialize(this._root);
			this._soundSettingsController.Initialize(this._root);
			this._gameSavingSettingsController.Initialize(this._root);
			this._languageSettingsController.Initialize(this._root);
			this._analyticsSettingsController.Initialize(this._root);
			this._introSettingsController.Initialize(this._root);
			this._cameraSettingsController.Initialize(this._root);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003C30 File Offset: 0x00001E30
		public VisualElement GetPanel()
		{
			this._devModeSettingsController.Update();
			this._screenSettingsController.Update();
			this._uiSettingsController.Update();
			this._tutorialSettingsController.Update();
			this._accessibilitySettingsController.Update();
			this._inputSettingsController.Update();
			this._soundSettingsController.Update();
			this._gameSavingSettingsController.Update();
			this._analyticsSettingsController.Update();
			this._introSettingsController.Update();
			this._cameraSettingsController.Update();
			return this._root;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003CBC File Offset: 0x00001EBC
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003CBF File Offset: 0x00001EBF
		public void OnUICancelled()
		{
			this._content.scrollOffset = Vector2.zero;
			this._screenSettingsController.Clear();
			this._panelStack.Pop(this);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public void OpenKeyBindingsBox(ClickEvent evt)
		{
			this._panelStack.HideAndPushOverlay(this._keyBindingsBox);
		}

		// Token: 0x0400007C RID: 124
		public readonly DevModeSettingsController _devModeSettingsController;

		// Token: 0x0400007D RID: 125
		public readonly GraphicsSettingsController _graphicsSettingsController;

		// Token: 0x0400007E RID: 126
		public readonly ScreenSettingsController _screenSettingsController;

		// Token: 0x0400007F RID: 127
		public readonly UISettingsController _uiSettingsController;

		// Token: 0x04000080 RID: 128
		public readonly TutorialSettingsController _tutorialSettingsController;

		// Token: 0x04000081 RID: 129
		public readonly AccessibilitySettingsController _accessibilitySettingsController;

		// Token: 0x04000082 RID: 130
		public readonly InputSettingsController _inputSettingsController;

		// Token: 0x04000083 RID: 131
		public readonly SoundSettingsController _soundSettingsController;

		// Token: 0x04000084 RID: 132
		public readonly GameSavingSettingsController _gameSavingSettingsController;

		// Token: 0x04000085 RID: 133
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000086 RID: 134
		public readonly PanelStack _panelStack;

		// Token: 0x04000087 RID: 135
		public readonly LanguageSettingsController _languageSettingsController;

		// Token: 0x04000088 RID: 136
		public readonly KeyBindingsBox _keyBindingsBox;

		// Token: 0x04000089 RID: 137
		public readonly AnalyticsSettingsController _analyticsSettingsController;

		// Token: 0x0400008A RID: 138
		public readonly IntroSettingsController _introSettingsController;

		// Token: 0x0400008B RID: 139
		public readonly CameraSettingsController _cameraSettingsController;

		// Token: 0x0400008C RID: 140
		public VisualElement _root;

		// Token: 0x0400008D RID: 141
		public ScrollView _content;
	}
}
