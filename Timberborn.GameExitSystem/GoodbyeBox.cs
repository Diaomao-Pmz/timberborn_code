using System;
using Timberborn.CoreUI;
using Timberborn.WebNavigation;
using UnityEngine.UIElements;

namespace Timberborn.GameExitSystem
{
	// Token: 0x02000005 RID: 5
	public class GoodbyeBox : IPanelController
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D4 File Offset: 0x000002D4
		public GoodbyeBox(VisualElementLoader visualElementLoader, PanelStack panelStack, UrlOpener urlOpener, Action action)
		{
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._urlOpener = urlOpener;
			this._exitAction = action;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FC File Offset: 0x000002FC
		public VisualElement GetPanel()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/GoodbyeBox");
			UQueryExtensions.Q<Button>(visualElement, "Feedback", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._urlOpener.OpenFeatureUpvote();
			}, 0);
			UQueryExtensions.Q<Button>(visualElement, "Exit", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._exitAction();
			}, 0);
			UQueryExtensions.Q<Button>(visualElement, "CancelButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			return visualElement;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002173 File Offset: 0x00000373
		public bool OnUIConfirmed()
		{
			this._exitAction();
			return true;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002181 File Offset: 0x00000381
		public void OnUICancelled()
		{
			this._panelStack.Pop(this);
		}

		// Token: 0x04000006 RID: 6
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000007 RID: 7
		public readonly PanelStack _panelStack;

		// Token: 0x04000008 RID: 8
		public readonly UrlOpener _urlOpener;

		// Token: 0x04000009 RID: 9
		public readonly Action _exitAction;
	}
}
