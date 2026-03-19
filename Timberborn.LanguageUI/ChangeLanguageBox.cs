using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Language;
using Timberborn.Localization;
using Timberborn.MainMenuSceneLoading;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.LanguageUI
{
	// Token: 0x02000004 RID: 4
	public class ChangeLanguageBox : IPanelController, ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public ChangeLanguageBox(VisualElementLoader visualElementLoader, PanelStack panelStack, ILoc loc, LanguageSettings languageSettings, DialogBoxShower dialogBoxShower, ILocalizationService localizationService, MainMenuSceneLoader mainMenuSceneLoader)
		{
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._loc = loc;
			this._languageSettings = languageSettings;
			this._dialogBoxShower = dialogBoxShower;
			this._localizationService = localizationService;
			this._mainMenuSceneLoader = mainMenuSceneLoader;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002113 File Offset: 0x00000313
		public string LocalizedCurrentLanguageName
		{
			get
			{
				return this._loc.T(ChangeLanguageBox.LanguageNameKey);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002125 File Offset: 0x00000325
		public void ShowWithoutReloadConfirmation(Action closedCallback = null)
		{
			this.Show(true, closedCallback);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000212F File Offset: 0x0000032F
		public void ShowWithReloadConfirmation()
		{
			this.Show(false, null);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000213C File Offset: 0x0000033C
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Options/ChangeLanguageBox");
			UQueryExtensions.Q<Button>(this._root, "CancelButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Close();
			}, 0);
			this.AddRows(this._root);
			UQueryExtensions.Q<Button>(this._root, "ConfirmButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnConfirmClicked();
			}, 0);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B1 File Offset: 0x000003B1
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B9 File Offset: 0x000003B9
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021BC File Offset: 0x000003BC
		public void OnUICancelled()
		{
			this.Close();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021C4 File Offset: 0x000003C4
		public void Show(bool skipReloadConfirmation, Action closedCallback)
		{
			if (!this._isShown)
			{
				this._skipReloadConfirmation = skipReloadConfirmation;
				this._closedCallback = closedCallback;
				this._panelStack.HideAndPushOverlay(this);
				this._isShown = true;
				if (!this.TrySetInitialToggle(this._localizationService.CurrentLanguage))
				{
					this.TrySetInitialToggle(LocalizationCodes.Default);
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000221C File Offset: 0x0000041C
		public void AddRows(VisualElement root)
		{
			VisualElement items = UQueryExtensions.Q<VisualElement>(root, "Items", null);
			List<LanguageInfo> list = this._localizationService.AvailableLanguages.ToList<LanguageInfo>();
			bool flag = list.All((LanguageInfo language) => language.IsNew);
			foreach (LanguageInfo languageInfo in list)
			{
				string localizationCode = languageInfo.LocalizationCode;
				bool showAsNew = (!flag && languageInfo.IsNew) || ChangeLanguageBox.LanguagesWithForcedNewMarker.Contains(localizationCode);
				this.AddRow(items, localizationCode, languageInfo.DisplayName, showAsNew);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022E0 File Offset: 0x000004E0
		public void AddRow(VisualElement items, string localizationCode, string displayName, bool showAsNew)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Options/ChangeLanguageItem");
			Toggle toggle = UQueryExtensions.Q<Toggle>(visualElement, "Item", null);
			toggle.text = displayName;
			UQueryExtensions.Q<Label>(visualElement, "NewLanguageMarker", null).ToggleDisplayStyle(showAsNew);
			items.Add(visualElement);
			this._items.Add(new ChangeLanguageBox.ChangeLanguageItem(toggle, localizationCode));
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(toggle, delegate(ChangeEvent<bool> _)
			{
				this.SetValue(toggle);
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002374 File Offset: 0x00000574
		public void SetValue(Toggle toggle)
		{
			foreach (ChangeLanguageBox.ChangeLanguageItem changeLanguageItem in this._items)
			{
				changeLanguageItem.Toggle.SetValueWithoutNotify(false);
			}
			toggle.SetValueWithoutNotify(true);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023D4 File Offset: 0x000005D4
		public void OnConfirmClicked()
		{
			string newLanguage = this._items.Single((ChangeLanguageBox.ChangeLanguageItem item) => item.Toggle.value).LocalizationCode;
			if (newLanguage == this._localizationService.CurrentLanguage)
			{
				this.Close();
				return;
			}
			this._languageSettings.Language = newLanguage;
			if (this._skipReloadConfirmation)
			{
				this.ChangeAndReload(newLanguage);
				return;
			}
			this._dialogBoxShower.Create().SetLocalizedMessage(ChangeLanguageBox.WarningLocKey).SetConfirmButton(new Action(this.Close), this._loc.T(CommonLocKeys.OKKey)).SetCancelButton(delegate()
			{
				this.ChangeAndReload(newLanguage);
			}, this._loc.T(ChangeLanguageBox.RestartLocKey)).Show();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024C5 File Offset: 0x000006C5
		public void ChangeAndReload(string newLanguage)
		{
			this._localizationService.Load(newLanguage);
			this._mainMenuSceneLoader.SaveAndOpenMainMenu();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024DE File Offset: 0x000006DE
		public void Close()
		{
			this._panelStack.Pop(this);
			Action closedCallback = this._closedCallback;
			if (closedCallback != null)
			{
				closedCallback();
			}
			this._isShown = false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002504 File Offset: 0x00000704
		public bool TrySetInitialToggle(string localizationCode)
		{
			bool flag = false;
			foreach (ChangeLanguageBox.ChangeLanguageItem changeLanguageItem in this._items)
			{
				bool flag2 = changeLanguageItem.LocalizationCode == localizationCode;
				flag = (flag || flag2);
				changeLanguageItem.Toggle.SetValueWithoutNotify(flag2);
			}
			return flag;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string LanguageNameKey = "Settings.Language.Name";

		// Token: 0x04000007 RID: 7
		public static readonly string WarningLocKey = "Settings.Language.Warning";

		// Token: 0x04000008 RID: 8
		public static readonly string RestartLocKey = "Settings.Language.Restart";

		// Token: 0x04000009 RID: 9
		public static readonly string[] LanguagesWithForcedNewMarker = new string[0];

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000B RID: 11
		public readonly PanelStack _panelStack;

		// Token: 0x0400000C RID: 12
		public readonly ILoc _loc;

		// Token: 0x0400000D RID: 13
		public readonly LanguageSettings _languageSettings;

		// Token: 0x0400000E RID: 14
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400000F RID: 15
		public readonly ILocalizationService _localizationService;

		// Token: 0x04000010 RID: 16
		public readonly MainMenuSceneLoader _mainMenuSceneLoader;

		// Token: 0x04000011 RID: 17
		public readonly List<ChangeLanguageBox.ChangeLanguageItem> _items = new List<ChangeLanguageBox.ChangeLanguageItem>();

		// Token: 0x04000012 RID: 18
		public bool _skipReloadConfirmation;

		// Token: 0x04000013 RID: 19
		public Action _closedCallback;

		// Token: 0x04000014 RID: 20
		public VisualElement _root;

		// Token: 0x04000015 RID: 21
		public bool _isShown;

		// Token: 0x02000005 RID: 5
		public class ChangeLanguageItem
		{
			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000016 RID: 22 RVA: 0x000025A3 File Offset: 0x000007A3
			public Toggle Toggle { get; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000017 RID: 23 RVA: 0x000025AB File Offset: 0x000007AB
			public string LocalizationCode { get; }

			// Token: 0x06000018 RID: 24 RVA: 0x000025B3 File Offset: 0x000007B3
			public ChangeLanguageItem(Toggle toggle, string localizationCode)
			{
				this.Toggle = toggle;
				this.LocalizationCode = localizationCode;
			}
		}
	}
}
