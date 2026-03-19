using System;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.FileSystem;
using Timberborn.Localization;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using Timberborn.WebNavigation;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuModdingUI
{
	// Token: 0x02000004 RID: 4
	public class CreateModBox : IPanelController, ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public CreateModBox(DialogBoxShower dialogBoxShower, IExplorerOpener explorerOpener, UrlOpener urlOpener, ILoc loc, DropdownItemsSetter dropdownItemsSetter, ModTemplateDropdownProvider modTemplateDropdownProvider, PanelStack panelStack, VisualElementLoader visualElementLoader, HyperlinkInitializer hyperlinkInitializer, ModCreator modCreator)
		{
			this._dialogBoxShower = dialogBoxShower;
			this._explorerOpener = explorerOpener;
			this._urlOpener = urlOpener;
			this._loc = loc;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._modTemplateDropdownProvider = modTemplateDropdownProvider;
			this._panelStack = panelStack;
			this._visualElementLoader = visualElementLoader;
			this._hyperlinkInitializer = hyperlinkInitializer;
			this._modCreator = modCreator;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002120 File Offset: 0x00000320
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Modding/CreateModBox");
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			Dropdown dropdown = UQueryExtensions.Q<Dropdown>(this._root, "TemplateDropdown", null);
			this._dropdownItemsSetter.SetItems(dropdown, this._modTemplateDropdownProvider);
			dropdown.ValueChanged += delegate(object _, EventArgs _)
			{
				this.UpdateControlsState();
			};
			this._modNameField = UQueryExtensions.Q<TextField>(this._root, "ModNameField", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<string>(this._modNameField, delegate(ChangeEvent<string> _)
			{
				this.UpdateControlsState();
			});
			this._languageCodeWrapper = UQueryExtensions.Q<VisualElement>(this._root, "LanguageCodeWrapper", null);
			this._languageCodeField = UQueryExtensions.Q<TextField>(this._root, "LanguageCodeField", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<string>(this._languageCodeField, delegate(ChangeEvent<string> _)
			{
				this.UpdateControlsState();
			});
			this._createModButton = UQueryExtensions.Q<Button>(this._root, "CreateModButton", null);
			this._createModButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.CreateTemplate), 0);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002240 File Offset: 0x00000440
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002248 File Offset: 0x00000448
		public bool OnUIConfirmed()
		{
			if (this._createModButton.enabledSelf)
			{
				this.CreateTemplate();
				return true;
			}
			return false;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002260 File Offset: 0x00000460
		public void OnUICancelled()
		{
			this._panelStack.Pop(this);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000226E File Offset: 0x0000046E
		public void Open()
		{
			this._panelStack.HideAndPushOverlay(this);
			this.UpdateControlsState();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002284 File Offset: 0x00000484
		public void UpdateControlsState()
		{
			bool localizationTemplateChosen = this._modTemplateDropdownProvider.LocalizationTemplateChosen;
			this._languageCodeWrapper.ToggleDisplayStyle(localizationTemplateChosen);
			bool flag = string.IsNullOrEmpty(this._modNameField.value) || (localizationTemplateChosen && string.IsNullOrEmpty(this._languageCodeField.value));
			this._createModButton.SetEnabled(!flag);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022E4 File Offset: 0x000004E4
		public void CreateTemplate(ClickEvent evt)
		{
			this.CreateTemplate();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022EC File Offset: 0x000004EC
		public void CreateTemplate()
		{
			string modName = this._modNameField.text.Trim();
			string localizationCode = this._modTemplateDropdownProvider.LocalizationTemplateChosen ? this._languageCodeField.text.Trim() : null;
			string destinationPath;
			switch (this._modCreator.CreateMod(modName, localizationCode, out destinationPath))
			{
			case DirectoryCreationResult.OK:
				this.ShowModCreatedMessage(destinationPath);
				return;
			case DirectoryCreationResult.NameTaken:
				this.ShowDialogBox(CreateModBox.ModNameTakenLocKey);
				return;
			case DirectoryCreationResult.NameInvalid:
				this.ShowDialogBox(CreateModBox.InvalidNameLocKey);
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002374 File Offset: 0x00000574
		public void ShowModCreatedMessage(string destinationPath)
		{
			this._panelStack.Pop(this);
			string text = this._loc.T<string>(CreateModBox.ModCreatedMessageLocKey, destinationPath);
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Modding/ModCreatedMessage");
			Label label = UQueryExtensions.Q<Label>(visualElement, "Message", null);
			label.text = text;
			this._hyperlinkInitializer.Initialize(label, delegate
			{
				this._explorerOpener.OpenDirectory(destinationPath);
			});
			this._dialogBoxShower.Create().AddContent(visualElement).SetInfoButton(delegate
			{
				this._urlOpener.OpenModdingDocumentation();
			}, this._loc.T(CreateModBox.DocumentationButtonLocKey)).HideTopAndShow();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000242C File Offset: 0x0000062C
		public void ShowDialogBox(string textLocKey)
		{
			this._dialogBoxShower.Create().SetLocalizedMessage(textLocKey).Show();
		}

		// Token: 0x04000006 RID: 6
		public static readonly string InvalidNameLocKey = "Saving.InvalidName";

		// Token: 0x04000007 RID: 7
		public static readonly string ModCreatedMessageLocKey = "Modding.ModCreatedMessage";

		// Token: 0x04000008 RID: 8
		public static readonly string ModNameTakenLocKey = "Modding.ModNameTaken";

		// Token: 0x04000009 RID: 9
		public static readonly string DocumentationButtonLocKey = "Modding.DocumentationButton";

		// Token: 0x0400000A RID: 10
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400000B RID: 11
		public readonly IExplorerOpener _explorerOpener;

		// Token: 0x0400000C RID: 12
		public readonly UrlOpener _urlOpener;

		// Token: 0x0400000D RID: 13
		public readonly ILoc _loc;

		// Token: 0x0400000E RID: 14
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400000F RID: 15
		public readonly ModTemplateDropdownProvider _modTemplateDropdownProvider;

		// Token: 0x04000010 RID: 16
		public readonly PanelStack _panelStack;

		// Token: 0x04000011 RID: 17
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000012 RID: 18
		public readonly HyperlinkInitializer _hyperlinkInitializer;

		// Token: 0x04000013 RID: 19
		public readonly ModCreator _modCreator;

		// Token: 0x04000014 RID: 20
		public VisualElement _root;

		// Token: 0x04000015 RID: 21
		public TextField _modNameField;

		// Token: 0x04000016 RID: 22
		public VisualElement _languageCodeWrapper;

		// Token: 0x04000017 RID: 23
		public TextField _languageCodeField;

		// Token: 0x04000018 RID: 24
		public Button _createModButton;
	}
}
