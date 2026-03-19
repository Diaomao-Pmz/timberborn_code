using System;
using System.Collections;
using System.Text;
using Timberborn.ApplicationLifetime;
using Timberborn.AssetSystem;
using Timberborn.CoreUI;
using Timberborn.ErrorReporting;
using Timberborn.Language;
using Timberborn.Localization;
using Timberborn.Modding;
using Timberborn.PlatformUtilities;
using Timberborn.WebNavigation;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.ErrorReportingUI
{
	// Token: 0x02000005 RID: 5
	public class CrashScreen : MonoBehaviour
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002167 File Offset: 0x00000367
		public IEnumerator Start()
		{
			yield return new WaitForSecondsRealtime(CrashScreen.Delay);
			ErrorReporter.CreateErrorReport();
			this.ShowUI();
			yield break;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002178 File Offset: 0x00000378
		public void ShowUI()
		{
			LocalizationLoader localizationLoader = new LocalizationLoader(new CrashScreen.LocalizationCsvValidator(), new AssetLoader(new ResourceAssetProvider[]
			{
				new ResourceAssetProvider()
			}));
			string @string = PlayerPrefs.GetString(LanguageSettings.LanguageKey, LocalizationCodes.Default);
			this._loc.Initialize(localizationLoader.GetLocalization(@string, false));
			this._uiDocument.enabled = true;
			VisualElement rootVisualElement = this._uiDocument.rootVisualElement;
			Button button = UQueryExtensions.Q<Button>(rootVisualElement, "ExitButton", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				GameQuitter.Quit();
			}, 0);
			button.text = this._loc.T(CrashScreen.ExitGameLocKey);
			this._commentTextField = UQueryExtensions.Q<TextField>(rootVisualElement, "Comment", null);
			this._commentTextField.textEdition.placeholder = this._loc.T(CrashScreen.CommentPlaceholderLocKey);
			this._commentTextField.textEdition.hidePlaceholderOnFocus = true;
			this._commentTextField.verticalScrollerVisibility = 0;
			this._emailTextField = UQueryExtensions.Q<TextField>(rootVisualElement, "Email", null);
			this._emailTextField.textEdition.placeholder = this._loc.T(CrashScreen.EmailPlaceholderLocKey);
			this._emailTextField.textEdition.hidePlaceholderOnFocus = true;
			this._sendReportButton = UQueryExtensions.Q<Button>(rootVisualElement, "SendReportButton", null);
			this._sendReportButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnSendReportButtonClick), 0);
			this._sendReportButton.text = this._loc.T(CrashScreen.SendReportLocKey);
			this._sendReportButton.SetEnabled(false);
			Button button2 = UQueryExtensions.Q<Button>(rootVisualElement, "PrivacyPolicyButton", null);
			button2.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._urlOpener.OpenPrivacyPolicy();
			}, 0);
			button2.text = this._loc.T(CrashScreen.PrivacyPolicyLinkLocKey);
			this._privacyPolicyToggle = UQueryExtensions.Q<Toggle>(rootVisualElement, "PrivacyPolicyToggle", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._privacyPolicyToggle, delegate(ChangeEvent<bool> value)
			{
				this._sendReportButton.SetEnabled(value.newValue);
			});
			this._privacyPolicyToggle.text = this._loc.T(CrashScreen.PrivacyPolicyAcceptLocKey);
			Button button3 = UQueryExtensions.Q<Button>(rootVisualElement, "ErrorReportFolder", null);
			button3.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._explorerOpener.OpenDirectory(ErrorReporter.ErrorReportsFolder);
			}, 0);
			button3.text = ErrorReporter.ErrorReportsFolder;
			Button button4 = UQueryExtensions.Q<Button>(rootVisualElement, "ErrorReportFolderModded", null);
			button4.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._explorerOpener.OpenDirectory(ErrorReporter.ErrorReportsFolder);
			}, 0);
			button4.text = ErrorReporter.ErrorReportsFolder;
			Button button5 = UQueryExtensions.Q<Button>(rootVisualElement, "BugWebsite", null);
			button5.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._urlOpener.OpenBugInfo();
			}, 0);
			button5.text = UrlOpener.BugInfoUrl;
			bool flag = !ModdedState.IsModded && !CrashSceneLoader.DevModeEnabled;
			UQueryExtensions.Q<VisualElement>(rootVisualElement, "VanillaInfo", null).ToggleDisplayStyle(flag);
			UQueryExtensions.Q<VisualElement>(rootVisualElement, "TamperedInfo", null).ToggleDisplayStyle(!flag);
			UQueryExtensions.Q<VisualElement>(rootVisualElement, "ModdedWarning", null).ToggleDisplayStyle(ModdedState.IsModded);
			Button button6 = UQueryExtensions.Q<Button>(rootVisualElement, "ModdedWebsite", null);
			button6.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._urlOpener.OpenHowToRemoveMods();
			}, 0);
			button6.text = UrlOpener.HowToRemoveModsUrl;
			if (!flag)
			{
				TextField textField = UQueryExtensions.Q<TextField>(rootVisualElement, "Exception", null);
				textField.value = CrashScreen.GetExceptionText();
				textField.verticalScrollerVisibility = 0;
			}
			UQueryExtensions.Q<Label>(rootVisualElement, "Title", null).text = this._loc.T(CrashScreen.TitleLocKey);
			UQueryExtensions.Q<Label>(rootVisualElement, "Introduction", null).text = this._loc.T(CrashScreen.IntroductionLocKey);
			UQueryExtensions.Q<Label>(rootVisualElement, "HowToFindReport", null).text = this._loc.T(CrashScreen.HowToFindReportLocKey);
			UQueryExtensions.Q<Label>(rootVisualElement, "HowToFindReportShort", null).text = this._loc.T(CrashScreen.HowToFindReportShortLocKey);
			UQueryExtensions.Q<Label>(rootVisualElement, "ManualInstructions", null).text = this._loc.T(CrashScreen.ManualInstructionsLocKey);
			UQueryExtensions.Q<Label>(rootVisualElement, "ModdedIntroduction", null).text = this._loc.T(CrashScreen.ModdedIntroductionLocKey);
			UQueryExtensions.Q<Label>(rootVisualElement, "ModdedInstructions", null).text = this._loc.T(CrashScreen.ModdedInstructionsLocKey);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000257C File Offset: 0x0000077C
		public void OnSendReportButtonClick(ClickEvent evt)
		{
			this._sendReportButton.SetEnabled(false);
			this._privacyPolicyToggle.SetEnabled(false);
			this._commentTextField.SetEnabled(false);
			this._emailTextField.SetEnabled(false);
			this._sendReportButton.text = this._loc.T(CrashScreen.SendingLocKey);
			base.StartCoroutine(this.SendReportCoroutine(this._commentTextField.value, this._emailTextField.value));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000025F7 File Offset: 0x000007F7
		public IEnumerator SendReportCoroutine(string comment, string email)
		{
			yield return null;
			if (ErrorReportSender.SendErrorReport(comment, email))
			{
				this._sendReportButton.text = this._loc.T(CrashScreen.SendSuccessLocKey);
			}
			else
			{
				this._sendReportButton.text = this._loc.T(CrashScreen.SendFailLocKey);
				this._privacyPolicyToggle.SetEnabled(true);
				this._sendReportButton.SetEnabled(true);
			}
			yield break;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002614 File Offset: 0x00000814
		public static string GetExceptionText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrWhiteSpace(ErrorReporter.LogString))
			{
				stringBuilder.AppendLine(ErrorReporter.LogString);
			}
			if (!string.IsNullOrWhiteSpace(ErrorReporter.StackTrace))
			{
				stringBuilder.AppendLine(ErrorReporter.StackTrace);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400000A RID: 10
		[HideInInspector]
		public static readonly float Delay = 3f;

		// Token: 0x0400000B RID: 11
		[HideInInspector]
		public static readonly string SendingLocKey = "CrashScreen.Sending";

		// Token: 0x0400000C RID: 12
		[HideInInspector]
		public static readonly string SendSuccessLocKey = "CrashScreen.SendSuccess";

		// Token: 0x0400000D RID: 13
		[HideInInspector]
		public static readonly string SendFailLocKey = "CrashScreen.SendFail";

		// Token: 0x0400000E RID: 14
		[HideInInspector]
		public static readonly string HowToFindReportLocKey = "CrashScreen.HowToFindReport";

		// Token: 0x0400000F RID: 15
		[HideInInspector]
		public static readonly string HowToFindReportShortLocKey = "CrashScreen.HowToFindReportShort";

		// Token: 0x04000010 RID: 16
		[HideInInspector]
		public static readonly string IntroductionLocKey = "CrashScreen.Introduction";

		// Token: 0x04000011 RID: 17
		[HideInInspector]
		public static readonly string ManualInstructionsLocKey = "CrashScreen.ManualInstructions";

		// Token: 0x04000012 RID: 18
		[HideInInspector]
		public static readonly string PrivacyPolicyAcceptLocKey = "CrashScreen.PrivacyPolicyAccept";

		// Token: 0x04000013 RID: 19
		[HideInInspector]
		public static readonly string PrivacyPolicyLinkLocKey = "CrashScreen.PrivacyPolicyLink";

		// Token: 0x04000014 RID: 20
		[HideInInspector]
		public static readonly string SendReportLocKey = "CrashScreen.SendReport";

		// Token: 0x04000015 RID: 21
		[HideInInspector]
		public static readonly string TitleLocKey = "CrashScreen.Title";

		// Token: 0x04000016 RID: 22
		[HideInInspector]
		public static readonly string CommentPlaceholderLocKey = "CrashScreen.CommentPlaceholder";

		// Token: 0x04000017 RID: 23
		[HideInInspector]
		public static readonly string EmailPlaceholderLocKey = "CrashScreen.EmailPlaceholder";

		// Token: 0x04000018 RID: 24
		[HideInInspector]
		public static readonly string ModdedIntroductionLocKey = "CrashScreen.ModdedIntroduction";

		// Token: 0x04000019 RID: 25
		[HideInInspector]
		public static readonly string ModdedInstructionsLocKey = "CrashScreen.ModdedInstructions";

		// Token: 0x0400001A RID: 26
		[HideInInspector]
		public static readonly string ExitGameLocKey = "Menu.ExitGame";

		// Token: 0x0400001B RID: 27
		[SerializeField]
		public UIDocument _uiDocument;

		// Token: 0x0400001C RID: 28
		[HideInInspector]
		public readonly UrlOpener _urlOpener = new UrlOpener();

		// Token: 0x0400001D RID: 29
		[HideInInspector]
		public readonly Loc _loc = new Loc();

		// Token: 0x0400001E RID: 30
		[HideInInspector]
		public readonly ExplorerOpener _explorerOpener = new ExplorerOpener();

		// Token: 0x0400001F RID: 31
		[HideInInspector]
		public Toggle _privacyPolicyToggle;

		// Token: 0x04000020 RID: 32
		[HideInInspector]
		public Button _sendReportButton;

		// Token: 0x04000021 RID: 33
		[HideInInspector]
		public TextField _commentTextField;

		// Token: 0x04000022 RID: 34
		[HideInInspector]
		public TextField _emailTextField;

		// Token: 0x02000006 RID: 6
		public class LocalizationCsvValidator : ILocalizationCsvValidator
		{
			// Token: 0x06000014 RID: 20 RVA: 0x0000278B File Offset: 0x0000098B
			public void Validate(TextAsset textAsset)
			{
			}
		}
	}
}
