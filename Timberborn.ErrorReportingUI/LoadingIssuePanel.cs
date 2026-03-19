using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.CoreUI;
using Timberborn.ErrorReporting;
using Timberborn.Localization;
using Timberborn.MainMenuSceneLoading;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.ErrorReportingUI
{
	// Token: 0x0200000B RID: 11
	public class LoadingIssuePanel : ILoadableSingleton, IPanelController, IPanelBlocker
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000028F5 File Offset: 0x00000AF5
		public LoadingIssuePanel(ILoadingIssueService loadingIssueService, PanelStack panelStack, VisualElementLoader visualElementLoader, MainMenuSceneLoader mainMenuSceneLoader, EventBus eventBus, ILoc loc)
		{
			this._loadingIssueService = loadingIssueService;
			this._panelStack = panelStack;
			this._visualElementLoader = visualElementLoader;
			this._mainMenuSceneLoader = mainMenuSceneLoader;
			this._eventBus = eventBus;
			this._loc = loc;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000292A File Offset: 0x00000B2A
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002938 File Offset: 0x00000B38
		[OnEvent]
		public void ShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			if (this._loadingIssueService.HasAnyIssues)
			{
				this._panelStack.PushOverlay(this);
			}
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002960 File Offset: 0x00000B60
		public VisualElement GetPanel()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/LoadingIssuePanel");
			UQueryExtensions.Q<Button>(visualElement, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			UQueryExtensions.Q<Button>(visualElement, "ContinuePlaying", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			UQueryExtensions.Q<Button>(visualElement, "ExitToMenu", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._mainMenuSceneLoader.OpenMainMenu();
			}, 0);
			UQueryExtensions.Q<TextField>(visualElement, "Issues", null).value = this.GetText();
			return visualElement;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000029EE File Offset: 0x00000BEE
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000029F1 File Offset: 0x00000BF1
		public void OnUICancelled()
		{
			this._panelStack.Pop(this);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A00 File Offset: 0x00000C00
		public string GetText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<string> list = new List<string>();
			foreach (ValueTuple<LoadingIssueMessage, int> valueTuple in this._loadingIssueService.GetIssues())
			{
				LoadingIssueMessage item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				list.Add(this.BuildIssueText(stringBuilder, item, item2));
				stringBuilder.Clear();
			}
			list.Sort();
			foreach (string value in list)
			{
				stringBuilder.AppendLine(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public string BuildIssueText(StringBuilder stringBuilder, LoadingIssueMessage issue, int count)
		{
			stringBuilder.Append(SpecialStrings.RowStarter);
			stringBuilder.Append((issue.MessageParam != null) ? this._loc.T<string>(issue.MessageLocKey, this.GetParamText(issue)) : this._loc.T(issue.MessageLocKey));
			if (count > 1)
			{
				stringBuilder.Append(string.Format(" ({0})", count));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B3F File Offset: 0x00000D3F
		public string GetParamText(LoadingIssueMessage issue)
		{
			if (!issue.ParamIsLocKey)
			{
				return issue.MessageParam;
			}
			return this._loc.T(issue.MessageParam);
		}

		// Token: 0x0400002D RID: 45
		public readonly ILoadingIssueService _loadingIssueService;

		// Token: 0x0400002E RID: 46
		public readonly PanelStack _panelStack;

		// Token: 0x0400002F RID: 47
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000030 RID: 48
		public readonly MainMenuSceneLoader _mainMenuSceneLoader;

		// Token: 0x04000031 RID: 49
		public readonly EventBus _eventBus;

		// Token: 0x04000032 RID: 50
		public readonly ILoc _loc;
	}
}
