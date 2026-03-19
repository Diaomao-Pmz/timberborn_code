using System;
using Timberborn.Analytics;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.WebNavigation;
using UnityEngine.UIElements;

namespace Timberborn.AnalyticsUI
{
	// Token: 0x02000004 RID: 4
	public class AnalyticsConsentBox : IPanelController, ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public AnalyticsConsentBox(PanelStack panelStack, VisualElementLoader visualElementLoader, AnalyticsConsent analyticsConsent, HyperlinkInitializer hyperlinkInitializer, UrlOpener urlOpener)
		{
			this._panelStack = panelStack;
			this._visualElementLoader = visualElementLoader;
			this._analyticsConsent = analyticsConsent;
			this._hyperlinkInitializer = hyperlinkInitializer;
			this._urlOpener = urlOpener;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MainMenu/AnalyticsConsentBox");
			this._hyperlinkInitializer.Initialize(UQueryExtensions.Q<Label>(this._root, "Info", null), new Action(this._urlOpener.OpenAnalyticsPrivacyPolicy));
			UQueryExtensions.Q<Button>(this._root, "Agree", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Agree();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "Disagree", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Disagree();
			}, 0);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000217E File Offset: 0x0000037E
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002186 File Offset: 0x00000386
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002189 File Offset: 0x00000389
		public void OnUICancelled()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000218B File Offset: 0x0000038B
		public void Show(Action closedCallback)
		{
			if (!this._analyticsConsent.WasConsentAsked)
			{
				this._closedCallback = closedCallback;
				this._panelStack.Push(this);
				return;
			}
			closedCallback();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B4 File Offset: 0x000003B4
		public void Agree()
		{
			this._analyticsConsent.GiveConsent();
			this.Close();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C7 File Offset: 0x000003C7
		public void Disagree()
		{
			this._analyticsConsent.RemoveConsent();
			this.Close();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021DA File Offset: 0x000003DA
		public void Close()
		{
			this._panelStack.Pop(this);
			this._closedCallback();
		}

		// Token: 0x04000006 RID: 6
		public readonly PanelStack _panelStack;

		// Token: 0x04000007 RID: 7
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000008 RID: 8
		public readonly AnalyticsConsent _analyticsConsent;

		// Token: 0x04000009 RID: 9
		public readonly HyperlinkInitializer _hyperlinkInitializer;

		// Token: 0x0400000A RID: 10
		public readonly UrlOpener _urlOpener;

		// Token: 0x0400000B RID: 11
		public VisualElement _root;

		// Token: 0x0400000C RID: 12
		public Action _closedCallback;
	}
}
